#include "lzss.h"
/*
* This function will search through the slidingWindow
*       dictionary for the longest sequence matching the MAX_CODED
*       long string stored in uncodedLookahed.
* input: windowHead - head of sliding window
*       uncodedHead - head of uncoded lookahead buffer
* output: The sliding window index where the match starts and the
*       length of the match.  If there is no match a length of
*       zero will be returned.
*/
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
                uncodedLookahead[(uncodedHead + j) % MAX_CODED]) // look for more matches
            {
                if (j >= MAX_CODED) // if sliding window match is longer them max code len
                {
                    break;
                }
                j++;
            };

            if (j > matchData.length) // if we dound a longer match (better match) - KMP algorithem
            {
                matchData.length = j;
                matchData.offset = i; // update matches and offset
            }
        }

        if (j >= MAX_CODED) // if the the matches found is the max we can store
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
/*
* This function will read an input file and write an output
*       file encoded using a slight modification to the LZss
*       algorithm. This algorithm encodes strings as 16 bits (a 12
*       bit offset + a 4 bit length).
* Input : inFile - file to encode
*         outFile - file to write encoded output
* Output: inFile is encoded and written to outFile
*/
void EncodeLZSS(FILE* inFile, FILE* outFile)
{
    /* 8 code flags and encoded strings */
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

    /*
    * fill up the sliding window with known values
    * if common values are used there is a better chance of matching in earlier strings
    */
    for (i = 0; i < WINDOW_SIZE; i++)
    {
        slidingWindow[i] = ' ';
    }
    /*
    * load MAX_CODED bytes forom input to the lookahead buffer
    */
    for (len = 0; len < MAX_CODED && (c = getc(inFile)) != EOF; len++)
    {
        uncodedLookahead[len] = c;
    }

    if (len == 0)
    {
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

        /* not long enough match.  write uncoded byte */
        if (matchData.length <= MAX_UNCODED)
        {
            matchData.length = 1;   /* set to 1 for 1 byte uncoded */
            flags |= flagPos;       /* mark with uncoded byte flag */
            encodedData[nextEncoded++] = uncodedLookahead[uncodedHead]; // gg go next
        }
        else
        {
            /* match length > MAX_UNCODED.  Encode as offset and length. */
            encodedData[nextEncoded++] =
                (unsigned char)((matchData.offset & 0x0FFF) >> 4); // 0x0FFF = 11111111 12 bits
            encodedData[nextEncoded++] =
                (unsigned char)(((matchData.offset & 0x000F) << 4) | // 0x000F = 1111 4 bits
                    (matchData.length - (MAX_UNCODED + 1)));
        }//binary AND & operator copies a bit to the result if it exists in both operands
         //binaro OR | operator copies a bit if it exists in neither operand

        if (flagPos == 0x80)
        {
            /* we have 8 code flags, write out flags and code buffer */
            putc(flags, outFile);

            for (i = 0; i < nextEncoded; i++)
            {
                /* send at most 8 units of code together */
                putc(encodedData[i], outFile);
                putc('c', outFile);
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

        /*
        * Replace the matchData.length worth of bytes we've matched in the
        * sliding window with new bytes from the input file.
        */
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
}

/*
* This function will read an LZss encoded input file and
*       write an output file. This algorithm
*       encodes strings as 16 bits (a 12bit offset + a 4 bit length).
*   Input : inFile - file to decode
*                outFile - file to write decoded output
*   Output: inFile is decoded and written to outFile
*/
void DecodeLZSS(FILE* inFile, FILE* outFile)
{
    int  i, c;
    unsigned char flags, flagsUsed;     /* encoded/not encoded flag */
    int nextChar;                       /* next char in sliding window */
    encoded_string_t code;              /* offset/length code for string */

    /* initialize variables */
    flags = 0;
    flagsUsed = 7;
    nextChar = 0;

    /*
    * Fill the sliding window buffer with some known vales.
    * If common characters are used, there's an
    * increased chance of matching to the earlier strings.
    */
    for (i = 0; i < WINDOW_SIZE; i++)
    {
        slidingWindow[i] = ' ';
    }

    while (TRUE)
    {
        flags >>= 1; // right shift flags by 1
        flagsUsed++;

        if (flagsUsed == 8)
        {
            /* shift out all flag bits, read a new flag */
            if ((c = getc(inFile)) == EOF) 
            {
                break;
            }

            flags = c & 0xFF; // get lowest 8 bits (get all flags)
            flagsUsed = 0;
        }

        if (flags & 0x01) // if there isn't any encoded data
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
            if ((code.offset = getc(inFile)) == EOF) // get the match offset
            {
                break;
            }

            if ((code.length = getc(inFile)) == EOF) // get the length
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
                c = slidingWindow[(code.offset + i) % WINDOW_SIZE]; // get the string and 
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
} 


