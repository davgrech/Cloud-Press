#include "lzss.h"
/****************************************************************************
*   Function   : FindMatch
*   Description: This function will search through the slidingWindow
*                dictionary for the longest sequence matching the MAX_CODED
*                long string stored in uncodedLookahed.
*   Parameters : windowHead - head of sliding window
*                uncodedHead - head of uncoded lookahead buffer
*   Effects    : NONE
*   Returned   : The sliding window index where the match starts and the
*                length of the match.  If there is no match a length of
*                zero will be returned.
****************************************************************************/
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