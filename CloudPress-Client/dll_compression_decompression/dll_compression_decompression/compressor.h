// MathLibrary.h - Contains declarations of math functions
#pragma once

#ifdef COMPRESS_EXPORTS
#define COMPRESS_API __declspec(dllexport)
#else
#define COMPRESS_API __declspec(dllimport)
#endif

#include <stdio.h>
#include <stdlib.h>

// The Fibonacci recurrence relation describes a sequence F
// where F(n) is { n = 0, a
//               { n = 1, b
//               { n > 1, F(n-2) + F(n-1)
// for some initial integral values a and b.
// If the sequence is initialized F(0) = 1, F(1) = 1,
// then this relation produces the well-known Fibonacci
// sequence: 1, 1, 2, 3, 5, 8, 13, 21, 34, ...

// Initialize a Fibonacci relation sequence
// such that F(0) = a, F(1) = b.
// This function must be called before any other function.
extern "C" COMPRESS_API void EncodeLZSS(char* inFilePath, char* outFilePath);
extern "C" COMPRESS_API void DecodeLZSS(char* inFilePath, char* outFilePath);   /* decoding routine */
extern "C" COMPRESS_API bool Decrypt(char* inFilePath, char* outFilePath, char* password);
extern "C" COMPRESS_API void Encrypt(char* inFilePath, char* outFilePath, char* password);