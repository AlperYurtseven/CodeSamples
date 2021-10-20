#ifndef MATRIX_HPP
#define MATRIX_HPP

#include <cassert>

#include <initializer_list>
#include <istream>
#include <memory>
#include <ostream>
#include <sstream>
#include <vector>

template <typename T>
class Matrix {
 public:
  template <typename ref_t>
  class col_reverse_iterator_base;

  template <typename ref_t>
  class diag_iterator_base;

  // alternative implementation allowing conversion, comparison between const
  // and non-const and providing the typical standard typedefs
  template <class Type>
  class diag_iterator_base2;
  template <class Type>
  class col_reverse_iterator_base2;

  using col_reverse_iterator = col_reverse_iterator_base<T>;
  using const_col_reverse_iterator = col_reverse_iterator_base<const T>;
  using diag_iterator = diag_iterator_base<T>;
  using const_diag_iterator = diag_iterator_base<const T>;

  class Invalid {};

  Matrix(std::size_t nrows, std::size_t ncols) : Matrix(nrows, ncols, T()) {}

  Matrix(std::size_t nrows, std::size_t ncols, T init)
      : nrows_(nrows),
        ncols_(ncols),
        elements_(std::make_unique<T[]>(nrows * ncols)) {
    if (nrows == 0 || ncols == 0) {
      throw Invalid{};
    }
    for (std::size_t i = 0, end = nrows * ncols; i < end; ++i) {
      elements_[i] = init;
    }
  }

  Matrix(std::initializer_list<std::initializer_list<T>> elements)
      : nrows_(elements.size()), ncols_(0), elements_() {
    if (elements.size() == 0 || elements.begin()->size() == 0) {
      throw Invalid{};
    }
    ncols_ = elements.begin()->size();
    elements_ = std::make_unique<T[]>(nrows_ * ncols_);

    std::size_t i = 0;
    for (auto row : elements) {
      // the number of columns must be the same for each row
      if (row.size() != ncols_) {
        throw Invalid{};
      }
      for (auto e : row) {
        elements_[i] = e;
        ++i;
      }
    }
  }

  Matrix(const Matrix& m) : nrows_(m.nrows()), ncols_(m.ncols()), elements_() {
    elements_ = std::make_unique<T[]>(nrows_ * ncols_);
    std::copy(m.elements_.get(), m.elements_.get() + (nrows_ * ncols_),
              elements_.get());
  }

  Matrix& operator=(const Matrix& m) {
    // self-assignment, don't do anything
    if (this == &m) {
      return *this;
    }
    // there is not enough space in our current matrix, get enough
    if (nrows_ * ncols_ < m.nrows() * m.ncols()) {
      elements_ = std::make_unique<T[]>(m.nrows() * m.ncols());
    }
    nrows_ = m.nrows();
    ncols_ = m.ncols();
    std::copy(m.elements_.get(), m.elements_.get() + (nrows_ * ncols_),
              elements_.get());
    return *this;
  }

  T& operator()(std::size_t i, std::size_t j) {
    assert(i * ncols_ + j < nrows_ * ncols_);
    return elements_[i * ncols_ + j];
  }

  T operator()(std::size_t i, std::size_t j) const {
    assert(i * ncols_ + j < nrows_ * ncols_);
    return elements_[i * ncols_ + j];
  }

  std::size_t nrows() const { return nrows_; }

  std::size_t ncols() const { return ncols_; }

  col_reverse_iterator col_rbegin(std::size_t col_num) {
    assert(col_num < ncols_);
    return col_reverse_iterator(
        elements_.get() + col_num + (nrows_ - 1) * ncols_, ncols_);
  }

  col_reverse_iterator col_rend(std::size_t col_num) {
    assert(col_num < ncols_);
    return col_reverse_iterator(elements_.get() + col_num - ncols_, ncols_);
  }

  diag_iterator diag_begin() {
    if (ncols_ != nrows_) {
      throw Invalid{};
    }
    return diag_iterator(elements_.get(), ncols_);
  }

  diag_iterator diag_end() {
    if (ncols_ != nrows_) {
      throw Invalid{};
    }
    return diag_iterator(elements_.get() + ncols_ * (nrows_ + 1), ncols_);
  }

  const_col_reverse_iterator col_rbegin(std::size_t col_num) const {
    assert(col_num < ncols_);
    return const_col_reverse_iterator(
        elements_.get() + col_num + (nrows_ - 1) * ncols_, ncols_);
  }

  const_col_reverse_iterator col_rend(std::size_t col_num) const {
    assert(col_num < ncols_);
    return const_col_reverse_iterator(elements_.get() + col_num - ncols_,
                                      ncols_);
  }

  const_diag_iterator diag_begin() const {
    if (ncols_ != nrows_) {
      throw Invalid{};
    }
    return const_diag_iterator(elements_.get(), ncols_);
  }

  const_diag_iterator diag_end() const {
    if (ncols_ != nrows_) {
      throw Invalid{};
    }
    return const_diag_iterator(elements_.get() + ncols_ * (nrows_ + 1), ncols_);
  }

 private:
  std::size_t nrows_;
  std::size_t ncols_;
  std::unique_ptr<T[]> elements_;
};

template <typename T>
template <typename reference_t>
class Matrix<T>::diag_iterator_base {
 public:
  diag_iterator_base(T* p, std::size_t ncol) : curr(p), cols(ncol) {}

  // prefix increment
  diag_iterator_base& operator++() {
    assert(curr != nullptr);
    curr += cols + 1;
    return *this;
  }

  // postfix increment
  diag_iterator_base operator++(int) {
    assert(curr != nullptr);
    auto tmp = curr;
    curr += cols + 1;
    return diag_iterator_base(tmp);
  }

  reference_t operator*() const {
    assert(curr != nullptr);
    return *curr;
  }

  // == operator
  bool operator==(const diag_iterator_base& lhs) const {
    return curr == lhs.curr;
  }
  bool operator!=(const diag_iterator_base& lhs) const {
    return !(*this == lhs);
  }

 private:
  T* curr;
  std::size_t cols;
};

// column iterator class
template <typename T>
template <typename reference_t>
class Matrix<T>::col_reverse_iterator_base {
 public:
  // constructor
  col_reverse_iterator_base(T* p, std::size_t ncol) : curr(p), cols(ncol) {}

  // ++ operator
  col_reverse_iterator_base& operator++() {
    assert(curr != nullptr);
    curr -= cols;
    return *this;
  }

  // postfix increment
  col_reverse_iterator_base operator++(int) {
    assert(curr != nullptr);
    col_reverse_iterator_base result(*this);
    curr -= cols;
    return result;
  }
  reference_t operator*() const {
    assert(curr != nullptr);
    return *curr;
  }

  bool operator==(const col_reverse_iterator_base& lhs) const {
    return curr == lhs.curr && cols == lhs.cols;
  }

  bool operator!=(const col_reverse_iterator_base& lhs) const {
    return !(*this == lhs);
  }

 private:
  T* curr;
  std::size_t cols;
};

// alternative implementation

template <typename T>
template <class Type>
class Matrix<T>::diag_iterator_base2 {
 public:
  // typedefs provided by standard iterators (can be provided by inheriting from
  // std::iterator if we are not in a template class?)
  using iterator_category = std::forward_iterator_tag;
  using value_type = T;
  using difference_type = std::ptrdiff_t;
  using pointer = Type*;
  using reference = Type&;

  diag_iterator_base2(T* ptr, std::size_t ncol) : curr(ptr), cols(ncol) {}

  // prefix increment
  diag_iterator_base2& operator++() {
    assert(curr != nullptr);
    curr += cols + 1;
    return *this;
  }

  // postfix increment
  diag_iterator_base2 operator++(int) {
    assert(curr != nullptr);
    auto result = diag_iterator_base2(curr);
    curr += cols + 1;
    return result;
  }

  // allow access to private members for classes with other template types (i.e.
  // const T and T)
  template <typename OtherType>
  friend class diag_iterator_base2;

  // two-way comparison: v.begin() == v.cbegin() and vice versa
  template <class OtherType>
  bool operator==(const diag_iterator_base2<OtherType>& rhs) const {
    return curr == rhs.curr;
  }

  template <class OtherType>
  bool operator!=(const diag_iterator_base2<OtherType>& rhs) const {
    return curr != rhs.curr;
  }

  reference operator*() const {
    assert(curr != nullptr);
    return *curr;
  }

  pointer operator->() const {
    assert(curr != nullptr);
    return *curr;
  }

  // One way conversion: iterator -> const_iterator
  operator diag_iterator_base2<const Type>() const {
    return diag_iterator_base2<const Type>(curr);
  }

 private:
  T* curr;
  std::size_t cols;
};

template <typename T>
template <class Type>
class Matrix<T>::col_reverse_iterator_base2 {
 public:
  // typedefs provided by standard iterators (can be provided by inheriting from
  // std::iterator if we are not in a template class?)
  using iterator_category = std::forward_iterator_tag;
  using value_type = T;
  using difference_type = std::ptrdiff_t;
  using pointer = Type*;
  using reference = Type&;

  // constructor
  col_reverse_iterator_base2(T* p, std::size_t ncol) : curr(p), cols(ncol) {}

  // ++ operator
  col_reverse_iterator_base2& operator++() {
    assert(curr != nullptr);
    curr -= cols;
    return *this;
  }

  // postfix increment
  col_reverse_iterator_base2 operator++(int) {
    assert(curr != nullptr);
    col_reverse_iterator_base2 result(*this);
    curr -= cols;
    return result;
  }

  reference operator*() const {
    assert(curr != nullptr);
    return *curr;
  }

  pointer operator->() const {
    assert(curr != nullptr);
    return *curr;
  }

  // allow access to private members for classes with other template types (i.e.
  // const T and T)
  template <typename OtherType>
  friend class col_reverse_iterator_base2;

  // two-way comparison: v.begin() == v.cbegin() and vice versa
  template <class OtherType>
  bool operator==(const col_reverse_iterator_base2<OtherType>& rhs) const {
    return curr == rhs.curr && cols == rhs.cols;
  }

  template <class OtherType>
  bool operator!=(const col_reverse_iterator_base2<OtherType>& rhs) const {
    return curr != rhs.curr || cols != rhs.cols;
  }

  // One way conversion: iterator -> const_iterator
  operator col_reverse_iterator_base2<const Type>() const {
    return col_reverse_iterator_base2<const Type>(curr, cols);
  }

 private:
  T* curr;
  std::size_t cols;
};

/*
 * Row-wise space separated output of the matrix
 *
 */
template <typename T>
std::ostream& operator<<(std::ostream& os, const Matrix<T>& m) {
  for (std::size_t i = 0; i < m.nrows(); ++i) {
    for (std::size_t j = 0; j < m.ncols() - 1; ++j) {
      os << m(i, j) << ' ';
    }
    os << m(i, m.ncols() - 1) << '\n';
  }
  return os;
}

template <typename T>
std::istream& operator>>(std::istream& is, Matrix<T>& m) {
  std::vector<std::vector<double>> result;
  std::string line;
  while (std::getline(is, line)) {
    if (!line.empty()) {
      result.emplace_back();  // add empty vector
      std::istringstream ss(line);
      T el;
      while (ss >> el) {
        result.back().push_back(el);
      }
      if (result.size() > 1 &&
          result.back().size() != result[result.size() - 2].size()) {
        is.setstate(std::ios_base::failbit);  // parsing failed
        return is;
      }
    }
  }
  if (result.empty() || result[0].empty()) {
    return is;
  }

  m = Matrix<T>(result.size(), result[0].size());
  for (std::size_t i = 0; i < result.size(); ++i) {
    for (std::size_t j = 0; j < result[i].size(); ++j) {
      m(i, j) = result[i][j];
    }
  }
  return is;
}

#endif  // MATRIX_HPP
