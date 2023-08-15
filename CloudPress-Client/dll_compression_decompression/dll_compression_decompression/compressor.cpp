// MathLibrary.cpp : Defines the exported functions for the DLL.

#include <utility>
#include <limits.h>
#include "compressor.h"
#include "EncryptionHandler.h"

#define _CRT_SECURE_NO_WARNINGS


// DLL internal state variables:
typedef struct encoded_string_t
{
    int offset;     // offset to start of longest match 
    int length;     // length of longest match 
} encoded_string_t;

typedef enum
{
    ENCODE,
    DECODE
} MODES; // encoded/decoded

#define FALSE   0
#define TRUE    1

#define WINDOW_SIZE     4096   /* size of sliding window (12 bits) */

/* maximum match length not encoded and encoded (4 bits) */
#define MAX_UNCODED     2
#define MAX_CODED       (15 + MAX_UNCODED + 1)

/* cyclic buffer sliding window of already read characters */
unsigned char slidingWindow[WINDOW_SIZE];
unsigned char uncodedLookahead[MAX_CODED];


encoded_string_t findMatch(int windowHead, int uncodedHead);

encoded_string_t FindMatch(int windowHead, int uncodedHead)
{
    encoded_string_t matchData;
    int i, j;

    matchData.length = 0;
    i = windowHead;  /* start at the beginning of the sliding window */
    j = 0;

    while (TRUE)
    {
        if (slidingWindow[i] == uncodedLookahead[uncodedHead])
        {
            /* we matched one how many more match? */
            j = 1;

            while (slidingWindow[(i + j) % WINDOW_SIZE] ==
                uncodedLookahead[(uncodedHead + j) % MAX_CODED])
            {
                if (j >= MAX_CODED)
                {
                    break;
                }
                j++;
            };

            if (j > matchData.length)
            {
                matchData.length = j;
                matchData.offset = i;
            }
        }

        if (j >= MAX_CODED)
        {
            matchData.length = MAX_CODED;
            break;
        }

        i = (i + 1) % WINDOW_SIZE;
        if (i == windowHead)
        {
            /* we wrapped around */
            break;
        }
    }

    return matchData;
}


void EncodeLZSS(char* inFilePath, char* outFilePath)
{
    //open files
    unsigned char flags, flagPos, encodedData[16];
    int nextEncoded;                /* index into encodedData */
    encoded_string_t matchData;
    int i, c;
    int len;                        /* length of string */
    int windowHead, uncodedHead;    /* head of sliding window and lookahead */

    flags = 0;
    flagPos = 0x01;
    nextEncoded = 0;
    windowHead = 0;
    uncodedHead = 0;


    FILE* inFile;
    FILE* outFile;
                         /* next char in sliding window */

    encoded_string_t code;
    errno_t error_codeA, error_codeB;
    error_codeA = fopen_s(&inFile, inFilePath, "rb");

    error_codeB = fopen_s(&outFile, outFilePath, "wb");
    
    for (i = 0; i < WINDOW_SIZE; i++)
    {
        slidingWindow[i] = ' ';
    }

    /************************************************************************
    * Copy MAX_CODED bytes from the input file into the uncoded lookahead
    * buffer.
    ************************************************************************/
    for (len = 0; len < MAX_CODED && (c = getc(inFile)) != EOF; len++)
    {
        uncodedLookahead[len] = c;
    }

    if (len == 0)
    {


        fclose(inFile);
        fclose(outFile);
        return;  /* inFile was empty */
    }

    /* Look for matching string in sliding window */
    matchData = FindMatch(windowHead, uncodedHead);

    /* now encoded the rest of the file until an EOF is read */
    while (len > 0)
    {
        if (matchData.length > len)
        {
            /* garbage beyond last data happened to extend match length */
            matchData.length = len;
        }

        if (matchData.length <= MAX_UNCODED)
        {
            /* not long enough match.  write uncoded byte */
            matchData.length = 1;   /* set to 1 for 1 byte uncoded */
            flags |= flagPos;       /* mark with uncoded byte flag */
            encodedData[nextEncoded++] = uncodedLookahead[uncodedHead];
        }
        else
        {
            /* match length > MAX_UNCODED.  Encode as offset and length. */
            encodedData[nextEncoded++] =
                (unsigned char)((matchData.offset & 0x0FFF) >> 4);

            encodedData[nextEncoded++] =
                (unsigned char)(((matchData.offset & 0x000F) << 4) |
                    (matchData.length - (MAX_UNCODED + 1)));
        }

        if (flagPos == 0x80)
        {
            /* we have 8 code flags, write out flags and code buffer */
            putc(flags, outFile);

            for (i = 0; i < nextEncoded; i++)
            {
                /* send at most 8 units of code together */
                putc(encodedData[i], outFile);
            }

            /* reset encoded data buffer */
            flags = 0;
            flagPos = 0x01;
            nextEncoded = 0;
        }
        else
        {
            /* we don't have 8 code flags yet, use next bit for next flag */
            flagPos <<= 1;
        }

        /********************************************************************
        * Replace the matchData.length worth of bytes we've matched in the
        * sliding window with new bytes from the input file.
        ********************************************************************/
        i = 0;
        while ((i < matchData.length) && ((c = getc(inFile)) != EOF))
        {
            /* add old byte into sliding window and new into lookahead */
            slidingWindow[windowHead] = uncodedLookahead[uncodedHead];
            uncodedLookahead[uncodedHead] = c;
            windowHead = (windowHead + 1) % WINDOW_SIZE;
            uncodedHead = (uncodedHead + 1) % MAX_CODED;
            i++;
        }

        /* handle case where we hit EOF before filling lookahead */
        while (i < matchData.length)
        {
            slidingWindow[windowHead] = uncodedLookahead[uncodedHead];
            /* nothing to add to lookahead here */
            windowHead = (windowHead + 1) % WINDOW_SIZE;
            uncodedHead = (uncodedHead + 1) % MAX_CODED;
            len--;
            i++;
        }

        /* find match for the remaining characters */
        matchData = FindMatch(windowHead, uncodedHead);
    }

    /* write out any remaining encoded data */
    if (nextEncoded != 0)
    {
        putc(flags, outFile);

        for (i = 0; i < nextEncoded; i++)
        {
            putc(encodedData[i], outFile);
        }
    }
    printf("done!");
    fclose(inFile);
    fclose(outFile);


}

void DecodeLZSS(char* inFilePath, char* outFilePath)
{
    //open files
    FILE* inFile;
    FILE* outFile;



    int  i, c;
    unsigned char flags, flagsUsed;     /* encoded/not encoded flag */
    int nextChar;                       /* next char in sliding window */
    encoded_string_t code;              /* offset/length code for string */

    /* initialize variables */
    flags = 0;
    flagsUsed = 7;
    nextChar = 0;



    errno_t error_codeA, error_codeB;
    error_codeA = fopen_s(&inFile, inFilePath, "rb");

    error_codeB = fopen_s(&outFile, outFilePath, "wb");

    for (i = 0; i < WINDOW_SIZE; i++)
    {
        slidingWindow[i] = ' ';
    }

    while (TRUE)
    {
        flags >>= 1;
        flagsUsed++;

        if (flagsUsed == 8)
        {
            /* shifted out all the flag bits, read a new flag */
            if ((c = getc(inFile)) == EOF)
            {
                break;
            }

            flags = c & 0xFF;
            flagsUsed = 0;
        }

        if (flags & 0x01)
        {
            /* uncoded character */
            if ((c = getc(inFile)) == EOF)
            {
                break;
            }

            /* write out byte and put it in sliding window */
            putc(c, outFile);
            slidingWindow[nextChar] = c;
            nextChar = (nextChar + 1) % WINDOW_SIZE;
        }
        else
        {
            /* offset and length */
            if ((code.offset = getc(inFile)) == EOF)
            {
                break;
            }

            if ((code.length = getc(inFile)) == EOF)
            {
                break;
            }

            /* unpack offset and length */
            code.offset <<= 4;
            code.offset |= ((code.length & 0x00F0) >> 4);
            code.length = (code.length & 0x000F) + MAX_UNCODED + 1;

            /****************************************************************
            * Write out decoded string to file and lookahead.  It would be
            * nice to write to the sliding window instead of the lookahead,
            * but we could end up overwriting the matching string with the
            * new string if abs(offset - next char) < match length.
            ****************************************************************/
            for (i = 0; i < code.length; i++)
            {
                c = slidingWindow[(code.offset + i) % WINDOW_SIZE];
                putc(c, outFile);
                uncodedLookahead[i] = c;
            }

            /* write out decoded string to sliding window */
            for (i = 0; i < code.length; i++)
            {
                slidingWindow[(nextChar + i) % WINDOW_SIZE] =
                    uncodedLookahead[i];
            }

            nextChar = (nextChar + code.length) % WINDOW_SIZE;
        }
    }
    printf("done!");
    fclose(inFile);
    fclose(outFile);
}

bool Decrypt(char* inFilePath, char* outFilePath, char* password)
{
    return decryptFile(inFilePath, outFilePath, password);
}
void Encrypt(char* inFilePath, char* outFilePath, char* password)
{
    return encryptFile(inFilePath, outFilePath, password);
}
