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

    //cout << endl << "das ist total " << total << endl;

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
double mthLargest(vector<double> vec,int m){
    std::sort(vec.begin(),vec.end(),greater<>());

    if(m>vec.size()){
        return -1;
    }
    else{
        return vec[m-1];
    }

}

//Function for printing as wanted in sheet
void print(vector<double> vec, int n, int m){
    if(nthSmallest(vec,n) != -1){
        if(mthLargest(vec,m) != -1){
            cout << min(vec) << " " << max(vec) << " " << mean(vec) << " " << thirdQuartile(vec) << " " << nthSmallest(vec,n) << " " << mthLargest(vec,m) << endl;
        }
        else{
            cout << min(vec) << " " << max(vec) << " "  << mean(vec) << " " << thirdQuartile(vec) << " " << nthSmallest(vec,n) << " " << "Im" << endl;
        }
    }
    else{
        if(mthLargest(vec,m) != -1){
            cout << min(vec) << " " << max(vec) << " " << mean(vec) << " " << thirdQuartile(vec) << " " << "In" << " " << mthLargest(vec,m) << endl;
        }
        else{
            cout << min(vec) << " " << max(vec) << " " << mean(vec) << " " << thirdQuartile(vec) << " " << "In" << " " << "Im" << endl;
        }
    }
}
//Main function
int main(int argc, char** argv) {

    int n = atoi(argv[1]), m = atoi(argv[2]),i=0;

    string str;

    vector<double> temp_vec;

    vector<string> input_lines;

    while(getline(cin, str)){
        if(str.empty())
            break;
        input_lines.push_back(str);
    }

    for(int j = 0; j<input_lines.size();j++){
        std::stringstream stream(input_lines[j]);
        while(1) {
            double n;
            stream >> n;
            if(!stream)
                break;
            temp_vec.push_back(n);
        }

        //for(i = 0 ; i< temp_vec.size();i++){
        //    cout << temp_vec[i] << " ";
        //}
        //cout << endl;


        print(temp_vec,n,m);

        /*
        if(nthSmallest(temp_vec,n) != -1){
            if(mthLargest(temp_vec,m) != -1){
                cout << min(temp_vec) << " " << max(temp_vec) << " " << median(temp_vec) << " " << mean(temp_vec) << " " << thirdQuartile(temp_vec) << " " << nthSmallest(temp_vec,n) << " " << mthLargest(temp_vec,m) << endl;
            }
            else{
                cout << min(temp_vec) << " " << max(temp_vec) << " " << median(temp_vec) << " " << mean(temp_vec) << " " << thirdQuartile(temp_vec) << " " << nthSmallest(temp_vec,n) << " " << "Im" << endl;
            }
        }
        else{
            if(mthLargest(temp_vec,m) != -1){
                cout << min(temp_vec) << " " << max(temp_vec) << " " << median(temp_vec) << " " << mean(temp_vec) << " " << thirdQuartile(temp_vec) << " " << "In" << " " << mthLargest(temp_vec,m) << endl;
            }
            else{
                cout << min(temp_vec) << " " << max(temp_vec) << " " << median(temp_vec) << " " << mean(temp_vec) << " " << thirdQuartile(temp_vec) << " " << "In" << " " << "Im" << endl;
            }
        }
        */


        temp_vec.clear();

    }


    return 0;
}
