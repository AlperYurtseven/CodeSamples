#ifndef BLOOM_FILTER_HPP
#define BLOOM_FILTER_HPP

#include <cstdint>  // uint64_t

#include <bitset>
#include <math.h>

#include "murmurhash.hpp"

using namespace std;

template <typename Key>
struct BloomHash {
  std::size_t operator()(Key key, unsigned int seed) const {
    // implement using murmur3_32

    return murmur3_32(reinterpret_cast<uint8_t*>(&key), sizeof(key) ,seed);

  }
};

template <typename Key, unsigned int m, typename Hash = BloomHash<Key>>
class BloomFilter {
 public:
  explicit BloomFilter(unsigned int num_hashes);

  BloomFilter(std::initializer_list<Key> init, unsigned int num_hashes);

  template <typename It>
  BloomFilter(It first, It last, unsigned int num_hashes);

  bool insert(const Key& key);

  bool contains(const Key& key) const;

  uint64_t approx_size() const;

  template <typename It>
  double false_positive_rate(It positive_begin, It positive_end,
                             It negative_begin, It negative_end) const;

  double space_ratio(uint64_t num_elements);

 private:
  std::bitset<m> data_;
  unsigned int k_;
};

template<typename Key, unsigned int m, typename Hash>
BloomFilter<Key, m, Hash>::BloomFilter(unsigned int num_hashes) {

    k_ = num_hashes;

}

template<typename Key, unsigned int m, typename Hash>
BloomFilter<Key, m, Hash>::BloomFilter(std::initializer_list<Key> init, unsigned int num_hashes) {

    k_ = num_hashes;
    for (Key key: init) {
        insert(key);
    }

}

template<typename Key, unsigned int m, typename Hash>
template<typename It>
BloomFilter<Key, m, Hash>::BloomFilter(It first, It last, unsigned int num_hashes) {

    k_ = num_hashes;

    for (auto iter = first; iter != last ; iter++) {

        insert(*iter);

    }
}

template<typename Key, unsigned int m, typename Hash>
bool BloomFilter<Key, m, Hash>::insert(const Key &key) {


    int size = data_.size();
    bool insert_flag = false;
    for (int s = 0; s < k_; s++) {
        Hash hash;
        int idx = hash(key,s) % size;
        if (data_[idx] == 0){
            data_[idx] = 1;
            insert_flag = true;
        }
        else{
            continue;
        }
    }

    return insert_flag;

}

template<typename Key, unsigned int m, typename Hash>
bool BloomFilter<Key, m, Hash>::contains(const Key &key) const {

    int size = data_.size();
    for (int s = 0; s < k_; s++) {
        Hash hash;
        int idx = hash(key,s) % size;
        if (data_[idx] == 0){
            return false;
        }
    }
    return true;
}

template<typename Key, unsigned int m, typename Hash>
uint64_t BloomFilter<Key, m, Hash>::approx_size() const {

    double x = 0;

    for(int i = 0; i < data_.size(); i++){
        if (data_[i]){
            x++;
        }
    }

    double k = k_;

    double m_d = m;

    uint64_t n_star = (-m_d/k) * log((1-(x/m_d)));

    return n_star;
}

template<typename Key, unsigned int m, typename Hash>
template<typename It>
double BloomFilter<Key, m, Hash>::false_positive_rate(It positive_begin, It positive_end, It negative_begin,
                                                      It negative_end) const {


    double True_Pos = 0, False_Pos = 0, True_Neg = 0, False_Neg = 0;

    for (auto i = positive_begin; i != positive_end; ++i) {

        if(contains(*i)){
            True_Pos++;
        }

        else{
            False_Pos++;
        }

    }

    for (auto i = negative_begin; i != negative_end; ++i) {

        if(!contains(*i)){
            True_Neg++;
        }
        else{
            False_Neg++;
        }

    }

    return False_Pos/(False_Pos+True_Neg);
}

template<typename Key, unsigned int m, typename Hash>
double BloomFilter<Key, m, Hash>::space_ratio(uint64_t num_elements) {

    double num_elem = num_elements;

    double sp_rat = (double(sizeof(data_)))/(sizeof(Key)*num_elem);

    return sp_rat;
}


#endif  // BLOOM_FILTER_HPP
