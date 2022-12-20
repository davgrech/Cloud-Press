#include "lzss.h"
#include <time.h>
int main(int argc, char* argv[])
{
    int opt;
    FILE* inFile, * outFile;  /* input & output files */
    MODES mode;

    /* initialize data */
    char* inFilePath = "tests\\bible.txt";
    char* outFilePath = "encoded_files\\aresults0.txt"; 
    inFile = NULL;
    outFile = NULL;
    mode = ENCODE;

    if (!(inFile = fopen(inFilePath, "rb")))
    {
        printf("opening test file failed");
        fclose(inFile);
    }
    if (!(outFile = fopen(outFilePath, "wb")))
    {
        printf("opening result file failed");
        fclose(outFile);
    }
    //runEncoding(inFilePath, outFilePath); -- default is optimized for encoding
    //runDecoding(inFilePath, outFilePath);
    encodeBenchamrk();

    
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
//run a quick encdoing test
int encodeBenchamrk()
{
    clock_t start, end;
    char inputFilePath[] = "encoded_files\\aresultsX.txt", outputFilePath[] = "encoded_files\\aresultsX.txt";
    double cpu_time_used;

    for (int i = 1; i < 10; i++)
    {
        inputFilePath[22] = (i - 1) + '0';
        if (i == 1) { strcpy(inputFilePath, "tests\\bible.txt"); } // for first run
        outputFilePath[22] = i + '0';
        start = clock();

        runEncoding(inputFilePath, outputFilePath);

        end = clock();

        cpu_time_used = ((double)(end - start)) / CLOCKS_PER_SEC;
        printf("time used: %f\n", cpu_time_used);
    }
}
//run a adecoding test
int decodeTest()
{
    //lol
}