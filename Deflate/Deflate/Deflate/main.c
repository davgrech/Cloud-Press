#include "lzss.h"
int main(int argc, char* argv[])
{
    int opt;
    FILE* inFile, * outFile;  /* input & output files */
    MODES mode;

    /* initialize data */
    inFile = NULL;
    outFile = NULL;
    mode = ENCODE;

    if (!(inFile = fopen("tests\\a.txt", "rb")))
    {
        printf("opening test file failed");
        fclose(inFile);
    }
    if (!(outFile = fopen("results\\aresults.txt", "wb")))
    {
        printf("opening result file failed");
        fclose(outFile);
    }


    
    encodeTest();
}
//run a quick encdoing test
int encodeTest()
{
    FILE* inFile, * outFile;
    inFile = fopen("tests\\a.txt", "rb");
    outFile = fopen("results\\aresults.txt", "wb");
    EncodeLZSS(inFile, outFile);
    fclose(inFile);
    fclose(outFile);
}
//run a adecoding test
int decodeTest()
{
    //
}