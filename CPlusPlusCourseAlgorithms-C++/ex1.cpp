#include <iostream>
#include <vector>


void print_triforce (unsigned int dimension);
void change_line (std::string line,unsigned int left_spacing, unsigned int right_spacing);


void change_line (std::string line,unsigned int left_spacing, unsigned int right_spacing){
  for (auto i = left_spacing; i<=(line.size()-right_spacing);i+=2 ){
      line[i]='#';
  }
  for (auto i = right_spacing; i<=(line.size()-left_spacing);i+=2 ){
      line[i]='#';
  }
  std::cout<<line<<'\n';
  // Since it is a void function, no need for return
  //return;
}

//#TODO "add comments"

void print_triforce (unsigned int dimension){
  //first triangle
  for (unsigned int i = 0; i<dimension/2;++i ){
    if (i%2==0){
      auto line=std::string(dimension, '_');
      change_line(line,(dimension/2)-(i/2),(dimension/2)-(i/2));
      //std::cout<<line<<'\n';
    }else
        std::cout<<'\n';
    
  }
  //second and third triangle
  for (unsigned int i = 0; i<dimension/2;++i ){
    if (i%2==0){
      auto line=std::string(dimension, '_');
      change_line(line,(dimension/4)-(i/2),((3*dimension)/4)-(i/2));
      //std::cout<<line<<'\n';
    }else
      std::cout<<std::endl;
    
  }
    // Since it is a void function, no need for return
    //return;
}

int main() {
  std::cout << "Power, Wisdom and Courage"<< std::endl;
  std::vector<std::string> v;
  unsigned int dimension=21;
  print_triforce(dimension);
  return 0;
}
