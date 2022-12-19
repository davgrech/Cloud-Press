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

    /* parse command line */
    while ((opt = getopt(argc, argv, "cdtni:o:h?")) != -1)
    {
        switch (opt)
        {
        case 'c':       /* compression mode */
            mode = ENCODE;
            break;

        case 'd':       /* decompression mode */
            mode = DECODE;
            break;

        case 'i':       /* input file name */
            if (inFile != NULL)
            {
                fprintf(stderr, "Multiple input files not allowed.\n");
                fclose(inFile);

                if (outFile != NULL)
                {
                    fclose(outFile);
                }

                exit(EXIT_FAILURE);
            }
            else if ((inFile = fopen(optarg, "rb")) == NULL)
            {
                perror("Opening inFile");

                if (outFile != NULL)
                {
                    fclose(outFile);
                }

                exit(EXIT_FAILURE);
            }
            break;

        case 'o':       /* output file name */
            if (outFile != NULL)
            {
                fprintf(stderr, "Multiple output files not allowed.\n");
                fclose(outFile);

                if (inFile != NULL)
                {
                    fclose(inFile);
                }

                exit(EXIT_FAILURE);
            }
            else if ((outFile = fopen(optarg, "wb")) == NULL)
            {
                perror("Opening outFile");

                if (outFile != NULL)
                {
                    fclose(inFile);
                }

                exit(EXIT_FAILURE);
            }
            break;

        case 'h':
        case '?':
            printf("Usage: lzss <options>\n\n");
            printf("options:\n");
            printf("  -c : Encode input file to output file.\n");
            printf("  -d : Decode input file to output file.\n");
            printf("  -i <filename> : Name of input file.\n");
            printf("  -o <filename> : Name of output file.\n");
            printf("  -h | ?  : Print out command line options.\n\n");
            printf("Default: lzss -c\n");
            return(EXIT_SUCCESS);
        }
    }
    /* validate command line */
    if (inFile == NULL)
    {
        fprintf(stderr, "Input file must be provided\n");
        fprintf(stderr, "Enter \"lzss -?\" for help.\n");

        if (outFile != NULL)
        {
            fclose(outFile);
        }

        exit(EXIT_FAILURE);
    }
    else if (outFile == NULL)
    {
        fprintf(stderr, "Output file must be provided\n");
        fprintf(stderr, "Enter \"lzss -?\" for help.\n");

        if (inFile != NULL)
        {
            fclose(inFile);
        }

        exit(EXIT_FAILURE);
    }

    /* we have valid parameters encode or decode */
    if (mode == ENCODE)
    {
        EncodeLZSS(inFile, outFile);
    }
    else
    {
        DecodeLZSS(inFile, outFile);
    }

    fclose(inFile);
    fclose(outFile);
    return EXIT_SUCCESS;

}