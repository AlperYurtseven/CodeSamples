#include "munkres_algorithm.hpp"
#include <vector>
#include <iostream>
#include <fstream>

using namespace std;

void step1(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered);
void step2(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered);
void step3(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered);
void step4(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered, int emin);
void step5(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered);
vector<pair<int,int>> done(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered);

Matrix<int> row_reduction(Matrix<int> mtx){

    for(int i = 0; i < mtx.nrows(); i++){
        int row_min = 0;

        for (int j = 0; j < mtx.ncols(); j++) {
            if (j == 0){
                row_min = mtx(i,j);
                continue;
            }
            if (mtx(i,j) < row_min){
                row_min = mtx(i,j);
            }
        }
        for (int j = 0; j < mtx.ncols(); j++) {
            mtx(i,j) = mtx(i,j) - row_min;
        }
    }

    return mtx;
}

Matrix<int> col_reduction(Matrix<int> mtx){

    for(int i = 0; i < mtx.ncols(); i++){
        int col_min = 0;

        for (int j = 0; j < mtx.ncols(); j++) {
            if (j == 0){
                col_min = mtx(j,i);
                continue;
            }
            if (mtx(j,i) < col_min){
                col_min = mtx(j,i);
            }
        }
        for (int j = 0; j < mtx.ncols(); j++) {
            mtx(j,i) = mtx(j,i) - col_min;
        }
    }

    return mtx;
}

bool is_all_covered(vector<bool> vec){

    for (int i = 0; i < vec.size(); ++i) {
        if (!vec[i]){
            return false;
        }
    }
    return true;
}

Matrix<bool> creation_of_star_matrix(Matrix<int> mat){

    Matrix<bool> star_matrix(mat.nrows(), mat.nrows(), false);
    vector<bool> r_starred(mat.nrows(),false);
    vector<bool> c_starred(mat.ncols(),false);


    for (int r = 0; r < mat.nrows(); ++r) {
        for (int c = 0; c < mat.ncols(); ++c) {
            if (mat(r, c) == 0) {
                if (!r_starred[r] && !c_starred[c]) {
                    r_starred[r] = true;
                    c_starred[c] = true;
                    star_matrix(r, c) = true;
                }
            }
        }
    }

    return star_matrix;

}

void step1(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered){

    // for Each column containing a starred zero do
    // cover this column
    // end for

    for (int r = 0; r < mat.nrows(); ++r) {
        for (int c = 0; c < mat.ncols(); ++c) {
            if (star_matrix(r,c)){
                c_covered[c] = true;
            }
        }
    }

    //if n columns are covered then GOTO DONE else GOTO STEP 2
    for (int i = 0; i < c_covered.size(); ++i) {
        if (!c_covered[i]){
            step2(mat,star_matrix,prime_matrix,c_covered,r_covered);
        }
    }

    done(mat,star_matrix,prime_matrix,c_covered,r_covered);

}

int find_the_smallest_uncovered_element(Matrix<int> mat, vector<bool> c_covered,  vector<bool> r_covered){

    int emin = 1215752191;

    for (int r = 0; r < mat.nrows(); ++r) {
        for (int c = 0; c < mat.ncols(); ++c) {
            if (!r_covered[r] && !c_covered[c]){
                if (mat(r,c) < emin){
                    emin = mat(r,c);
                }
            }
        }
    }
    return emin;
}

void step2(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered){

    //C contains an uncovered zero then
    //11: Find an arbitrary uncovered zero Z0 and prime it
    //12: if There is no starred zero in the row of Z0 then
    //13: GOTO STEP 3
    //14: else
    //15: Cover this row, and uncover the column containing the
    //starred zero GOTO STEP 2

    for (int r = 0; r < mat.nrows(); ++r) {
        for (int c = 0; c < mat.ncols(); ++c) {
            if (mat(r,c) == 0 && !c_covered[c]){
                prime_matrix(r,c) = true;
                for (int i = 0; i < mat.ncols(); ++i) {
                    if(star_matrix(r,i)){
                        r_covered[r] = true;
                        c_covered[i] = false;
                        step2(mat,star_matrix,prime_matrix,c_covered,r_covered);
                    }
                    step3(mat,star_matrix,prime_matrix,c_covered,r_covered);
                }
            }

        }
    }

    //Save the smallest uncovered element emin GOTO STEP 4

    int emin = find_the_smallest_uncovered_element(mat,c_covered,r_covered);
    step4(mat,star_matrix,prime_matrix,c_covered,r_covered,emin);
}

int if_starred_zero_in_column(Matrix<bool> star_matrix, int column){

    for (int r = 0; r < star_matrix.nrows(); ++r) {
        if (star_matrix(r,column)){
            return r;
        }
    }

    return -1;
}

int find_primed_zero_in_row(Matrix<bool> prime_matrix, int row){

    for (int c = 0; c < prime_matrix.nrows(); ++c) {

        if(prime_matrix(row,c)){
            return c;
        }

    }

    return -1;

}



void step3(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered){

    vector<pair<int,int>> S;

    //Construct a series S of alternating primed and starred
    //zeros as follows:
    //21: Insert Z0 into S

    for (int r = 0; r < prime_matrix.nrows(); ++r) {
        for (int c = 0; c < prime_matrix.ncols(); ++c) {
            if (prime_matrix(r, c)) {
                S.push_back(make_pair(r, c));
            }
        }
    }

    while (if_starred_zero_in_column(star_matrix,S[0].second) != -1){

        //insert z1 into s
        S.push_back(make_pair(if_starred_zero_in_column(star_matrix,S[0].second),S[0].second));
        int temp = mat(S[S.size()-1].first,S[S.size()-1].second);
        mat(S[0].first,S[0].second) = mat(S[0].first,find_primed_zero_in_row(prime_matrix,S[0].first));
        // TODO

    }


    for (int i = 0; i < S.size(); ++i) {
        //unstar each starred zero in S
        star_matrix(S[i].first,S[i].second) = false;
    }

    for (int r = 0; r < star_matrix.nrows(); ++r) {

        for (int c = 0; c < star_matrix.ncols(); ++c) {

            // erase all other primes
            prime_matrix(r,c) = false;

            if(star_matrix(r,c)){
                // replace all primes with stars
                prime_matrix(r,c) = star_matrix(r,c);
            }
        }

        // uncover every line in C
        r_covered[r] = false;
        c_covered[r] = false;

    }

    // GOTO STEP 1
    step1(mat,star_matrix,prime_matrix,c_covered,r_covered);

}

void step4(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered, int emin){

    for (int r = 0; r < mat.nrows(); ++r) {

        for (int c = 0; c < mat.ncols(); ++c) {

            if(r_covered[r]){

                //Add emin to every element in covered rows

                mat(r,c) += emin;

            }

            if(!c_covered[c]){

                //subtract it from every element in uncovered columns.

                mat(r,c) -= emin;

            }


        }

    }

    // GOTO STEP 2

    step2(mat,star_matrix,prime_matrix,c_covered,r_covered);

}

vector<pair<int,int>> done(Matrix<int> mat,Matrix<bool> star_matrix,Matrix<bool> prime_matrix, vector<bool> c_covered, vector<bool> r_covered){


    vector<pair<int,int>> assigned_ones;

    for (int r = 0; r < star_matrix.nrows(); ++r) {

        for (int c = 0; c < star_matrix.ncols(); ++c) {

            if (star_matrix(r,c)){

                assigned_ones.push_back(make_pair(r,c));

            }
        }

    }

    ofstream temp_file;
    temp_file.open ("pairs.txt");
    for (int i = 0; i < assigned_ones.size(); ++i) {
        temp_file << assigned_ones[i].first << " " << assigned_ones[i].second << endl;
    }
    temp_file.close();


    return assigned_ones;

}


Matrix<int> run_munkres_algorithm(Matrix<int> c) {

    c = row_reduction(c);
    c = col_reduction(c);

    Matrix<bool> star_matrix = creation_of_star_matrix(c);
    Matrix<bool> prime_matrix(c.nrows(),c.ncols(), false);
    vector<bool> c_covered(c.ncols(),false);
    vector<bool> r_covered(c.nrows(),false);

    Matrix<int> return_matrix(c.nrows(),c.ncols(),0);

    step1(c,star_matrix,prime_matrix,c_covered,r_covered);

    ifstream temp_file("pairs.txt");
    string line;
    vector<string> lines;
    while (getline(temp_file,line)){
        lines.push_back(line);
    }

    temp_file.close();

    vector<pair<int,int>> coordinates;

    for (int i = 0; i < lines.size(); ++i) {
        stringstream iss(lines[i]);
        int r1,c1;
        iss >> r1 >> c1;
        return_matrix(r1,c1) = 1;
    }


    return return_matrix;
}



