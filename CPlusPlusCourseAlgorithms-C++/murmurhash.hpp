#ifndef MURMURHASH_HPP
#define MURMURHASH_HPP

#include <cstdint>

// implementation of mumurhash 3 from wikipedia
// https://en.wikipedia.org/wiki/MurmurHash
// usage:
// int i = 50;
// uint32_t seed = 42;
// murmur3_32(reinterpret_cast<uint8_t*>(&i), sizeof(i), seed);
uint32_t murmur3_32(const uint8_t* key, std::size_t len, uint32_t seed) {
  uint32_t h = seed;
  if (len > 3) {
    const uint32_t* key_x4 = (const uint32_t*)key;
    std::size_t i = len >> 2;
    do {
      uint32_t k = *key_x4++;
      k *= 0xcc9e2d51;
      k = (k << 15) | (k >> 17);
      k *= 0x1b873593;
      h ^= k;
      h = (h << 13) | (h >> 19);
      h += (h << 2) + 0xe6546b64;
    } while (--i);
    key = (const uint8_t*)key_x4;
  }
  if (len & 3) {
    std::size_t i = len & 3;
    uint32_t k = 0;
    key = &key[i - 1];
    do {
      k <<= 8;
      k |= *key--;
    } while (--i);
    k *= 0xcc9e2d51;
    k = (k << 15) | (k >> 17);
    k *= 0x1b873593;
    h ^= k;
  }
  h ^= len;
  h ^= h >> 16;
  h *= 0x85ebca6b;
  h ^= h >> 13;
  h *= 0xc2b2ae35;
  h ^= h >> 16;
  return h;
}

#endif  // MURMURHASH_HPP
