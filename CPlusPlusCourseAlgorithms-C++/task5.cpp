#include <iostream>
#include <vector>
#include <math.h>
#include <string>
#include <sstream>
#include <ctype.h>

using namespace std;

void print_matrix(vector<vector<int>> mtx){

    for (int i = 0; i < mtx.size(); ++i) {
        for (int j = 0; j < mtx[i].size(); ++j) {
            cout << mtx[i][j] << " ";
        }
        cout << endl;
    }
    cout << endl;
}

bool is_0_1_2(int k){

    if (k == 0 || k == 1 || k == 2){
        return true;
    } else
        return false;
}

bool is_two_consequtive_ones(vector<int> vec1){

    for (int i = 1; i < vec1.size(); ++i) {
        if (vec1[i-1] == 1 && vec1[i] == 1){
            return true;
        }
    }
    return false;

}

int one_counter(vector<int> vec1){

    int counter = 0;

    for (int i = 0; i < vec1.size(); ++i) {
        if (vec1[i] == 1){
            if (i != vec1.size()-1){
                if(vec1[i+1] == 1){
                    if (i != vec1.size()-2){
                        if(vec1[i+2] != 1){
                            counter++;
                            i++;
                        }
                        else{
                            counter += 3;
                            i += 2;
                        }
                    }
                    else{
                        counter++;
                        i++;
                    }
                }
                else{
                    counter++;
                }
            }
            else{
                counter++;
            }
        }
    }

    return counter;

}

bool is_three_consequtive_ones(vector<int> vec1){

    for (int i = 2; i < vec1.size(); ++i) {
        if (vec1[i-2] == 1 && vec1[i-1] == 1 && vec1[i] == 1){
            return true;
        }
    }
    return false;

}

bool invalid_open_area(vector<vector<int>> mtx, int row, int col){

    bool check_right = false;
    bool check_left = false;
    bool check_up = false;
    bool check_down = false;

    int count = 0;

    if (col+1 < mtx[row].size()){
        if (mtx[row][col+1] == 1){
            count++;
            check_right = true;
        }
    }

    if (row+1 < mtx.size()){
        if (mtx[row+1][col] == 1){
            count++;
            check_down = true;
        }
    }

    if (row > 0){
        if (mtx[row-1][col] == 1){
            count++;
            check_up = true;
        }
    }

    if (col > 0){
        if (mtx[row][col-1] == 1){
            count++;
            check_left = true;
        }
    }

    if (count != 2) {
        return true;
    }
    else return false;

}


// returns 0 for outside,1 for inside, 2 for unsure
int is_inside(vector<vector<int>> mtx, int row, int col){

    bool already_checked_four_directions = false;

    vector<int> to_right;
    vector<int> to_left;
    vector<int> to_down;
    vector<int> to_up;

    bool right_checked = false;
    bool left_checked = false;
    bool up_checked = false;
    bool down_checked = false;

    int right_ones_count = 0;
    int left_ones_count = 0;
    int down_ones_count = 0;
    int up_ones_count = 0;

    for (int x = col; x < mtx[row].size(); x++) {

        if(x > col){
            to_right.push_back(mtx[row][x]);
        }

        right_ones_count = one_counter(to_right);

        /*if (mtx[row][x] == 1){
            right_ones_count++;
        }*/

        right_checked = true;

    }

    if (is_three_consequtive_ones(to_right)){


        // checks left

        for (int x = col; x >= 0 ; x--) {

            if(x < col){
                to_left.push_back(mtx[row][x]);
            }

           /* if (mtx[row][x] == 1){
                left_ones_count++;
            }*/

            left_ones_count = one_counter(to_left);

            left_checked = true;
        }

        if (is_three_consequtive_ones(to_left)){

            // checks down

            for (int y = row; y < mtx.size(); y++) {

                if(y > row){
                    to_down.push_back(mtx[y][col]);
                }

                /*if (mtx[y][col] == 1){
                    down_ones_count++;
                }*/

                down_ones_count = one_counter(to_down);

                down_checked = true;

            }

            if (is_three_consequtive_ones(to_down)){

                // checks up

                already_checked_four_directions = true;

                for (int y = row; y >= 0 ; y--) {

                    if(y < row){
                        to_up.push_back(mtx[y][col]);
                    }

                    /*if (mtx[y][col] == 1){
                        up_ones_count++;
                    }*/

                    up_ones_count = one_counter(to_up);

                }

                up_checked = true;

            }

        }
    }

    if (right_checked){

        if (left_checked){

            if (down_checked){

                if (up_checked){

                    if (is_three_consequtive_ones(to_right) && is_three_consequtive_ones(to_down) && is_three_consequtive_ones(to_left) && is_three_consequtive_ones(to_up)){
                        return 2;
                    }
                    if (up_ones_count % 2 == 1){
                        return 1;
                    } else
                        return 0;
                }

                if (down_ones_count % 2 == 1){
                    return 1;
                } else
                    return 0;

            }

            if (left_ones_count % 2 == 1){
                return 1;
            } else
                return 0;
        }

        if (right_ones_count % 2 == 1){
            return 1;
        } else
            return 0;

    }

}

int main(int argc, const char* argv[]) {

    vector<vector<int>> input_matrix;

    vector<int> temp_vec;

    vector<string> lines;

    int in = 0;
    int out = 0;
    int unsure = 0;

    while (true) {
        string line;
        getline(cin, line);

        if (!line.empty()) {
            /*for (int i = 0; i < line.size(); ++i) {

            }
            if(!is_number(line)){
                cout << "Invalid input";
                exit(-1);
            }*/
            lines.push_back(line);
        } else
            break;
    }

    for (int i = 0; i < lines.size(); ++i) {
        stringstream iss(lines[i]);
        int number;

        while (iss >> number) {
            if (is_0_1_2(number)) {
                temp_vec.push_back(number);
            } else {
                cout << "Invalid Input";
                return 0;
            }
        }
        if(!input_matrix.empty()){
            if (temp_vec.size() != input_matrix[0].size()){
                cout << "Invalid Input";
                return 0;
            }
        }

        input_matrix.push_back(temp_vec);
        temp_vec.clear();
    }

    if (input_matrix.size() < 3 || input_matrix[1].size() < 3) {

        cout << "Invalid Input";
        return 0;

    }

    //To Matthias; Java is better language, sincerely, me :(

    for (int i = 0; i < input_matrix.size() - 1; i++) {
        for (int j = 0; j < input_matrix[i].size() - 1; ++j) {
            if (input_matrix[i][j] == 1 && input_matrix[i + 1][j] == 1 && input_matrix[i][j + 1] == 1 &&
                input_matrix[i + 1][j + 1] == 1) {
                cout << "Invalid Input";
                return 0;
            }
        }
    }

    for (int i = 0; i < input_matrix.size(); ++i) {
        for (int j = 0; j < input_matrix[i].size(); ++j) {

            if (input_matrix[i][j] == 1) {
                if (invalid_open_area(input_matrix, i, j)) {
                    cout << "Invalid Input";
                    return 0;
                }
            }
        }
    }

    for (int r = 0; r < input_matrix.size(); r++) {

        for (int c = 0; c < input_matrix[r].size(); c++) {

            if (input_matrix[r][c] == 2) {
                int temp = is_inside(input_matrix, r, c);
                if (temp == 0) {
                    out++;
                }
                if (temp == 1) {
                    in++;
                }
                if (temp == 2) {
                    unsure++;
                }

            }
        }
    }

    cout << "in:" << in << endl;
    cout << "out:" << out << endl;
    cout << "unsure:" << unsure << endl;

    return 0;
}

