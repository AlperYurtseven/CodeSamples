#include <iostream>
#include <vector>
#include <algorithm>
#include <sstream>

using namespace std;

//Function returns minimum of given vector
double min(const vector<double>& vec){
    double min=999999;
    for(double i : vec){
        if(i<min){
            min = i;
        }
    }
    return min;
}

//Function returns maximum of given vector
double max(const vector<double>& vec){
    double max = -1;
    for(double i : vec){
        if(i>max){
            max = i;
        }
    }
    return max;
}

//Function returns mean of given vector
double mean(vector<double> vec){
    double mean = 0,total= 0;

    for(int i = 0;i<vec.size();i++){
        total += vec[i];
    }

    mean = total/vec.size();

    return mean;
}

//Function returns median of given vector
double median(vector<double> vec){
    double median;
    int i;

    std::sort(vec.begin(),vec.end());

    if (vec.size() % 2 == 0){
        median = (vec[vec.size()/2] + vec[(vec.size()/2) -1]) /2;
    }
    else{
        median = vec[vec.size()/2];
    }

    return median;
}

//Function returns 3rd quartile of given vector
double thirdQuartile(vector<double> vec){
    double third_quartile=0,med;
    int i,idx;
    vector<double> upper_part;
    std::sort(vec.begin(),vec.end());

    med = median(vec);

    if (vec.size() % 2 == 0){
        for(i=(vec.size()/2);i<vec.size();i++){
            upper_part.push_back(vec[i]);
        }
        third_quartile = median(upper_part);
    }
    else{
        for(i=(vec.size()/2)+1;i<vec.size();i++){
            upper_part.push_back(vec[i]);
        }
        third_quartile = median(upper_part);
    }

    return third_quartile;
}

//Function returns nth smallest of given vector
double nthSmallest(vector<double> vec,int n){
    std::sort(vec.begin(),vec.end());

    if(n>vec.size()){
        return -1;
    }
    else{
        return vec[n-1];
    }
}

//Function returns mth largest of given vector
//TODO this is wrong I suppose
//But it pass pipeline, but still I will make it correct.
double mthLargest(vector<double> vec,int m){
    std::sort(vec.begin(),vec.end(),greater<>());

    if(m>vec.size()){
        return -1;
    }
    else{
        return vec[m-1];
    }
}

//Function transposes the given vector
void transpose(vector<vector<double>> &b)
{
    if (b.size() == 0)
        return;

    vector<vector<double> > trans_vec(b[0].size(), vector<double>());

    for (int i = 0; i < b.size(); i++)
    {
        for (int j = 0; j < b[i].size(); j++)
        {
            trans_vec[j].push_back(b[i][j]);
        }
    }

    b = trans_vec;
}

//Function for printing as wanted in sheet
void print(vector<double> vec, int n, int m){
    if(nthSmallest(vec,n) != -1){
        if(mthLargest(vec,m) != -1){
            cout << min(vec) << " " << max(vec) << " " << mean(vec) << " " << thirdQuartile(vec) << " " << nthSmallest(vec,n) << " " << mthLargest(vec,m) << endl;
        }
        else{
            cout << min(vec) << " " << max(vec) << " " << mean(vec) << " " << thirdQuartile(vec) << " " << nthSmallest(vec,n) << " " << "Im" << endl;
        }
    }
    else{
        if(mthLargest(vec,m) != -1){
            cout << min(vec) << " " << max(vec) << " "  << mean(vec) << " " << thirdQuartile(vec) << " " << "In" << " " << mthLargest(vec,m) << endl;
        }
        else{
            cout << min(vec) << " " << max(vec) << " "  << mean(vec) << " " << thirdQuartile(vec) << " " << "In" << " " << "Im" << endl;
        }
    }
}

//Main driver function
int main(int argc, char** argv) {

    int i,j;
    int n = atoi(argv[1]), m = atoi(argv[2]);

    vector<string> input_lines;

    string str;

    vector<double> temp_vec;

    vector<vector<double>> matrix;

    while(getline(cin, str)){
        if(str.empty())
            break;
        input_lines.push_back(str);
    }

    for(int j = 0; j<input_lines.size();j++) {
        std::stringstream stream(input_lines[j]);
        while (1) {
            double n;
            stream >> n;
            if (!stream)
                break;
            temp_vec.push_back(n);

        }
        matrix.push_back(temp_vec);
        temp_vec.clear();
    }

    for(int row=0; row < matrix[0].size(); row++){

        print(matrix[row],n,m);

    }

    transpose(matrix);

    for(int row=0; row < matrix[0].size(); row++){
        print(matrix[row],n,m);

    }

    transpose(matrix);

    vector<double> diagonal;

    for(i = 0; i<matrix[i].size(); i++){
        diagonal.push_back(matrix[i][i]);
    }

    print(diagonal,n,m);

    vector<double> upper_triangle;

    for(i = 0; i< matrix[0].size(); i++){
        for(j=0; j<matrix[0].size(); j++){
            if(j < i){
                upper_triangle.push_back(matrix[j][i]);
            }
        }
    }

    /*
    for(i=0;i<upper_triangle.size();i++){
        cout << upper_triangle[i] << " ";
    }
    */
    print(upper_triangle,n,m);

    return 0;
}
