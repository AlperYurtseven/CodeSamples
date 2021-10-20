#include "bloom_filter.hpp"
#include "murmurhash.hpp"
#include <iostream>
#include <math.h>

using namespace std;

int main(int argc, const char* argv[]) {
  // you might test your bloom filter here

  initializer_list<int> list{3,2,4,1,2};

  BloomFilter<int, 1000, BloomHash<int>> bloom(list,2);

  cout << bloom.contains(3) << endl;
  cout << bloom.contains(2) << endl;

  cout << bloom.contains(5) << endl;
  cout << bloom.approx_size() << endl;

  //aaaa

  return 0;
}
