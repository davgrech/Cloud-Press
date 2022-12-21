#include "lzss.c"
#include <time.h>
#include <fstream>
#include "Huffman.h"
#include <locale>


void clearFile(char* path_to_file)
{
    FILE* file = fopen(path_to_file, "wb"); // oveerwrite file
    fclose(file);
}
/*
* write content to a file ty
*/
void writeContentToFile(std::string str, const char * path)
{
    std::ofstream out(path);
    out << str;
    out.close();



}
std::string readFileToString(const std::string& path)
{
    std::string content;
    std::ifstream myfile(path);
    myfile >> content;
    return content;

}



void EncodeDelfate(char* in_path, char* out_path)
{
    FILE* input, * output;
    std::string encoded_data = "";
    input = fopen(in_path, "rb");
    output = fopen(out_path, "wb");

    if (!output)
    {
        output = fopen("output.txt", "wb");
        strcpy(out_path, "output.txt");
    }
    EncodeLZSS(input, output); // encode with lzss

    huffEncode(readFileToString(out_path), out_path); // huffman encoding (pass out_path since it is the file lzss outputted to)

    fclose(output);
    fclose(input);

    std::cout << "\ndone";
    return;
}

/*
* func that decodes our algorithm
*/
void decodeDelfate(char* in_path, char* out_path)
{
    FILE* input, * output;
    std::string decoded_data, file_data;
    input = fopen(in_path, "rb");
    output = fopen(out_path, "rb");
    if (!output)
    {
        output = fopen("output.txt", "wb");
        strcpy(out_path, "output.txt");
    }

    decoded_data = decodeHuffman(readFileToString(in_path));

    writeContentToFile(decoded_data, out_path);
    DecodeLZSS(input, output);
    fclose(input);
    fclose(output);

}

/*
* encode a file once
*/
void runEncoding(char* inFilePath, char* outFilePath)
{
    FILE* inFile, * outFile;
    inFile = fopen(inFilePath, "rb");
    outFile = fopen(outFilePath, "wb");
    EncodeLZSS(inFile, outFile);
    fclose(inFile);
    fclose(outFile);
}

/*
* decode a file once
*/
void runDecoding(char* inFilePath, char* outFilePath)
{
    FILE* inFile, * outFile;
    inFile = fopen(inFilePath, "rb");
    outFile = fopen(outFilePath, "wb");
    DecodeLZSS(inFile, outFile);
    fclose(inFile);
    fclose(outFile);
}


int main(int argc, char* argv[])
{
    int opt;
    FILE* inFile, * outFile;  /* input & output files */
    MODES mode;

    /* initialize data */
    char inFilePath[] = "tests\\bible.txt";
    char outFilePath[] = "encoded_files\\aresults0.txt";
    inFile = NULL;
    outFile = NULL;
    mode = ENCODE;




    if (!(inFile = fopen(inFilePath, "rb")))
    {
        printf("opening test file failed");
        fclose(inFile);
    }
    if (!(outFile = fopen(outFilePath, "rb")))
    {
        printf("opening result file failed");
        fclose(outFile);
    }
    decodeDelfate(outFilePath, inFilePath);
    //EncodeDelfate(inFilePath, outFilePath);
    //runEncoding(inFilePath, outFilePath);
    //runDecoding(outFilePath, inFilePath);
    //encodeBenchamrk();
    


}