#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>


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

void EncodeLZSS(FILE* inFile, FILE* outFile);   /* encoding routine */
void DecodeLZSS(FILE* inFile, FILE* outFile);   /* decoding routine */

