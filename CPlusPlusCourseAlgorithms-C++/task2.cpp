#include <iostream>
#include <string>
#include <vector>

using namespace std;

int how_many_ones(const vector<int>& vec){

    int count = 0;

    for(int i = 0; i < vec.size(); i++){
        if(vec[i] == 1){
            count++;
        }
    }

    return count;
}

int how_many_twos(const vector<int>& vec){

    int count = 0;

    for(int i = 0; i < vec.size(); i++){
        if(vec[i] == 2){
            count++;
        }
    }

    return count;
}

bool is_all_covered(const vector<bool>& vec){

    int count = 0;

    for(int i = 0; i < vec.size(); i++){
        if(vec[i]){
            count++;
        }
    }

    return count == vec.size();

}

// calculates & returns how many catapult is needed, if not enough is exist returns -1
int catapult(const vector<int>& landscape, int distance) {

    vector<bool> is_covered(landscape.size(), false);

    int two_count = how_many_twos(landscape);

    if (two_count < (landscape.size()/distance)-1){
        return -1;
    }

    if (two_count == landscape.size()){
        int ret = landscape.size()/distance;
        return ret-1;
    }

    if (two_count == (landscape.size()/distance)-1){
        for (int i = 0; i < landscape.size(); i++) {
            if(landscape[i] == 1){
                for (int j = 0; j <= distance; j++) {
                    /*for (int i = 0; i < is_covered.size(); ++i) {
                        cout << is_covered[i] << " ";
                    }
                    cout << endl;*/
                    if(i-j > -1){
                        is_covered[i-j] = true;
                    }
                    if(i+j < landscape.size()){
                        is_covered[i+j] = true;
                    }
                }
                is_covered[i] = true;
            }
        }
        if (is_all_covered(is_covered)){
            return two_count;
        }
        else{
            /*for (int i = 0; i < is_covered.size(); ++i) {
                cout << is_covered[i] << " ";
            }*/
            return -1;
        }
    }

    for (int i = 0; i < landscape.size(); i++) {

        int number_of_catapult_needed = 0;
        bool change_flag = false;

        for (int i = 0; i < landscape.size(); i++) {
            if(landscape[i] == 1){
                for (int j = 1; j < distance; j++) {
                    if(i-j > -1 && !is_covered[i-j]){
                        is_covered[i-j] = true;
                        change_flag = true;
                    }
                    if(i+j < landscape.size() && !is_covered[i+j]){
                        is_covered[i+j] = true;
                        change_flag = true;
                    }
                }
                is_covered[i] = true;
                if (change_flag){
                    number_of_catapult_needed++;
                    change_flag = false;
                }
                i = i+distance-1;

            }
            if (is_all_covered(is_covered)){
                return number_of_catapult_needed;
            }
        }
        if (is_all_covered(is_covered)){
            return number_of_catapult_needed;
        }
    }
}

// calculates & returns how many beam station is needed, if not enough is exist returns -1
int beaming_station(const vector<int>& landscape, int distance){

    vector<bool> is_covered(landscape.size(), false);

    int one_count = how_many_ones(landscape);

    if (one_count < (landscape.size()/distance)-1){
        return -1;
    }

    if (one_count == landscape.size()){
        int ret = landscape.size()/distance;
        return ret-1;
    }

    if (one_count == (landscape.size()/distance)-1){
        for (int i = 0; i < landscape.size(); i++) {
            if(landscape[i] == 1){
                for (int j = 0; j <= distance; j++) {
                    /*for (int i = 0; i < is_covered.size(); ++i) {
                        cout << is_covered[i] << " ";
                    }
                    cout << endl;*/
                    if(i-j > -1){
                        is_covered[i-j] = true;
                    }
                    if(i+j < landscape.size()){
                        is_covered[i+j] = true;
                    }
                }
                is_covered[i] = true;
            }
        }
        if (is_all_covered(is_covered)){
            return one_count;
        }
        else{
            /*for (int i = 0; i < is_covered.size(); ++i) {
                cout << is_covered[i] << " ";
            }*/
            return -1;
        }
    }

    for (int i = 0; i < landscape.size(); i++) {

        int number_of_beam_stations_needed = 0;
        bool change_flag = false;

        for (int i = 0; i < landscape.size(); i++) {
            if(landscape[i] == 1){
                for (int j = 1; j < distance; j++) {
                    if(i-j > -1 && !is_covered[i-j]){
                        is_covered[i-j] = true;
                        change_flag = true;
                    }
                    if(i+j < landscape.size() && !is_covered[i+j]){
                        is_covered[i+j] = true;
                        change_flag = true;
                    }
                }
                is_covered[i] = true;
                if (change_flag){
                    number_of_beam_stations_needed++;
                    change_flag = false;
                }
                i = i+distance-1;

            }
            if (is_all_covered(is_covered)){
                return number_of_beam_stations_needed;
            }
        }
        if (is_all_covered(is_covered)){
            return number_of_beam_stations_needed;
        }
    }
}


int main(int argc, const char* argv[]) {

    int n,k,l,temp;

    cin >> n >> k >> l;

    vector<int> landscape;

    for (int i = 0; i < n; i++) {
        cin >> temp;
        landscape.push_back(temp);
    }

    cout << "beamer:" << beaming_station(landscape,k) << endl;
    cout << "cata:" << catapult(landscape,l);

    return 0;
}
