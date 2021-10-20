#include <iostream>
#include <vector>
#include <algorithm>
#include <fstream>

using namespace std;

bool isDNAorRNA(string seq);
bool isHairpin(string seq);
string extractHairpins(string seq);
string transferToRNA(string seq);
string makeAllCapitalsForHairpin(string seq);
string buildComplementary(string seq);
string firstHalf(string seq);
bool isDNA(string seq);
bool isRNA(string seq);
bool isDNAandRNA(string seq);

//Function to read given fasta file and return information asked in assignment sheet in vector
vector<string> fastaReader(const string& fasta_file) {

    ifstream input(fasta_file);
    string line, rna_seq, line_count_str, seq_count_str, not_dna_or_rna, seq;
    int seq_count = 0, line_count = 0;
    vector<string> outputs;

    if (!input.good()) {
        cerr << "Fasta_file could not be opened";
        exit(1);
    }

    while (getline(input, line)) {

        if (line.empty()) {
            cerr << "line is empty";
            exit(-1);
        }

        line_count++;

        if(line[0] == '>'){
            seq_count++;
            not_dna_or_rna = line;
        }
        else{
            if(line[0] == '>'){
                cerr << "No sequences given";
                exit(-1);
            }
            if(isDNAorRNA(line)){

                if(isDNAandRNA(line)){
                    cerr << not_dna_or_rna << endl;
                    continue;
                }

                else if(isDNA(line)){
                    line = transferToRNA(line);
                }

                line = makeAllCapitalsForHairpin(line);

                rna_seq += line;
            }

            else{
                cerr << not_dna_or_rna << endl;
            }
        }

    }


    line_count_str = to_string(line_count);
    seq_count_str = to_string(seq_count);

    outputs.push_back(line_count_str);
    outputs.push_back(seq_count_str);
    outputs.push_back(rna_seq);


    return outputs;

}

void hairpinExtractorator(const string& fasta_file, const string& out_file){
    string seq,line;
    ofstream output_file;
    output_file.open(out_file);
    vector<string> hairpins;
    vector<string> seqs;
    ifstream input(fasta_file);

    while(getline(input,line)) {
        if (line.empty()) {
            break;
        }
        if(isDNAorRNA(line)) {
            if (isDNAandRNA(line)) {
                continue;
            }
        }
        if(line[0] == '>'){
            if(!seq.empty()){
                seqs.push_back(seq);
                seq = "";
            }

        }
        else{
            if(isDNA(line)){
                line = transferToRNA(line);
            }
            line = makeAllCapitalsForHairpin(line);
            seq += line;
        }
    }
    seqs.push_back(seq);

    for(int i = 0; i<seqs.size();i++){
        if(isHairpin(seqs[i])){
            hairpins.push_back(extractHairpins(seqs[i]));
        }
    }

    for(int i = 0; i < hairpins.size(); i++){
        if(i<hairpins.size()-1){
            output_file << hairpins[i] << endl;
        }
        else
            output_file << hairpins[i];

    }

    output_file.close();
}


//Turn the characters into the sequence in capitals
string makeAllCapitalsForHairpin(string seq){

    for(int i = 0; i<seq.size();i++){
        if(seq[i] == 'a')
            seq[i] = 'A';
        if(seq[i] == 'c')
            seq[i] = 'C';
        if(seq[i] == 'g')
            seq[i] = 'G';
        if(seq[i] == 'u')
            seq[i] = 'U';
    }

    return seq;
}

//Function checks given sequence if it forms hairpin
bool isHairpin(string seq){

    int i;

    if (seq.size() <= 1)
        return false;

    for(i = 0; i<seq.size()/2;i++){
        if (seq[i] == 'A' && seq[seq.size()-i-1] != 'U'){
            return false;
        }
        else if (seq[i] == 'G' && seq[seq.size()-i-1] != 'C'){
            return false;
        }
        else if (seq[i] == 'U' && seq[seq.size()-i-1] != 'A'){
            return false;
        }
        else if (seq[i] == 'C' && seq[seq.size()-i-1] != 'G'){
            return false;
        }
    }

    return true;
}

string buildComplementary(string seq){
    int i = 0;
    string complementary;

    if(seq.size()%2 == 0){
        for(i=seq.size()/2; i<seq.size(); i++){
            if(seq[i] == 'A')
                complementary += 'U';
            if(seq[i] == 'G')
                complementary += 'C';
            if(seq[i] == 'C')
                complementary += 'G';
            if(seq[i] == 'U')
                complementary += 'A';
        }
    } else{
        for(i=(seq.size()+1)/2; i<seq.size(); i++){
            if(seq[i] == 'A')
                complementary += 'U';
            if(seq[i] == 'G')
                complementary += 'C';
            if(seq[i] == 'C')
                complementary += 'G';
            if(seq[i] == 'U')
                complementary += 'A';
        }
    }


    return complementary;
}

bool isDNAandRNA(string seq){
    bool u = false, t = false;
    for(int i = 0; i< seq.size();i++){
        if(seq[i] == 'U'){
            u = true;
        }
        if(seq[i] == 'T'){
            t = true;
        }
    }
    if (u && t)
        return true;
    else
        return false;
}


//Function to extract hairpin by neglecting middle base in odds
string extractHairpins(string seq){

    int i=0;
    string hairpin;

    for(i = 0; i<(seq.size()/2); i++){
        hairpin += seq[i];
    }

    return hairpin;
}

//Function calculate GC content of given sequence
float GCContent(const string& seq){

    int gc=0;
    float percentage=0;

    for(char i : seq){
        if (i == 'G' || i == 'C'){
            gc++;
        }
    }

    percentage = float (gc) / seq.size();

    return percentage;
}

//Function that checks if given sequence is RNA
bool isRNA(string seq){
    for(char i : seq){
        if (i == 'T'){
            return false;
        }
    }
    return true;
}

//Function that checks if given sequence is DNA
bool isDNA(string seq){
    for(char i : seq){
        if (i == 'U'){
            return false;
        }
    }
    return true;
}

//Function that checks if given sequence is either DNA or RNA or something else like dummy
bool isDNAorRNA(string seq){

    int count = 0;
    seq = makeAllCapitalsForHairpin(seq);
    for(char i: seq){
        if (i == 'A' || i == 'C' || i == 'G' || i == 'T' || i == 'U'){
            count++;
        }
    }

    if(count == seq.size())
        return true;
    else
        return false;
}

//Functions that makes DNA string to RNA string
string transferToRNA(string seq){

    for(char & i : seq){
        if (i == 'T'){
            i = 'U';
        }
    }

    return seq;
}

//Function returns Adenine percentage of given sequence
float aPercentage(const string& seq){
    int count = 0;
    float percentage;

    for(char i : seq){
        if (i == 'A'){
            count++;
        }
    }

    percentage = float(count) / seq.size();

    return percentage;
}

//Function returns Guanine percentage of given sequence
float gPercentage(const string& seq){
    int count = 0;
    float percentage;

    for(char i : seq){
        if (i == 'G'){
            count++;
        }
    }

    percentage = float(count) / seq.size();

    return percentage;
}

//Function returns Citosine (God knows how to write this name down) percentage of given sequence
float cPercentage(const string& seq){
    int count = 0;
    float percentage;

    for(char i : seq){
        if (i == 'C'){
            count++;
        }
    }

    percentage = float(count) / seq.size();

    return percentage;
}

//Function returns Uracil percentage of given sequence
float uPercentage(const string& seq){
    int count = 0;
    float percentage;

    for(char i : seq){
        if (i == 'U'){
            count++;
        }
    }

    percentage = float(count) / seq.size();

    return percentage;
}

//Function returns Thymine percentage of given sequence (NO NEED since LONG LIVE THE RNA)
float tPercentage(const string& seq){
    int count = 0;
    float percentage;

    for(char i : seq){
        if (i == 'T'){
            count++;
        }
    }

    percentage = float(count) / seq.size();

    return percentage;
}

//Driver function that won his 7th world title couple of days ago
int main(int argc, char** argv) {

    string seq,line_count_str,seq_count_str;
    int line_count,seq_count;
    vector<string> results;

    if(argc == 1){
        cerr << "Please give arguments";
        exit(-1);
    }
    if(argc == 2){
        cerr << "Please give output file too";
        exit(-1);
    }
    if(argc > 3){
        cerr << "Too much argument";
        exit(-1);
    }

    results = fastaReader(argv[1]);
    hairpinExtractorator(argv[1],argv[2]);


    //line_count = stoi(line_count_str);
    //seq_count = stoi(seq_count_str);

    seq_count_str = results[1];
    if (stoi(seq_count_str) == 0){
        cerr << "No Sequences given";
        exit(-1);
    }
    line_count_str = results[0];
    if (stoi(line_count_str) == 0){
        cerr << "No Lines given";
        exit(-1);
    }
    seq = results[2];

    if(!isRNA(seq)){
        seq = transferToRNA(seq);
    }

    cout << "Lines: " <<line_count_str << endl;
    cout << "Sequences: " << seq_count_str << endl;
    cout << "GC content: " << GCContent(seq) << endl;
    cout << "A: " << aPercentage(seq) << endl;
    cout << "C: " << cPercentage(seq) << endl;
    cout << "G: " << gPercentage(seq) << endl;
    cout << "U: " << uPercentage(seq) << endl;

    return 0;
}
