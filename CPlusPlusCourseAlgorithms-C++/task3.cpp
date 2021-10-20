#include <iostream>

#include "munkres_algorithm.hpp"
#include "matrix.hpp"


int main(int /*argc*/, const char* /*argv*/[]) {
    const Matrix<int> m = {{50, 0, 50}, {50, 50, 0}, {0, 50, 50}};
    const Matrix<int> s = {{0, 1, 0}, {0, 0, 1}, {1, 0, 0}};

    const auto res = run_munkres_algorithm(m);

    if(s.nrows() != res.nrows() || s.ncols() != res.ncols()) {
        std::cerr << "Cannot compare matrices" << std::endl;
        return 1;
    }

    for(size_t i = 0; i < s.nrows(); i++) {
        for(size_t j = 0; j < s.ncols(); j++) {
            if(s(i,j) != res(i,j)){
                std::cerr << "Found invalid result" << std::endl;
                return 1;
            }
        }
    }

    return 0;
}
