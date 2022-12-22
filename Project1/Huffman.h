#pragma once
#include <iostream>
#include <map>
#include <queue>
#define _CRT_SECURE_NO_WARNINGS



#define MAX_TREE_HT 256




struct HuffNode {

    unsigned char data;
    int freq;
    HuffNode* left, * right;
public:
    HuffNode(unsigned char data, int freq);


};

struct compare
{
    // simply compares frequencies between two nodes lol - will use it for priority queue
    bool operator()(HuffNode* left, HuffNode* right)
    {
        return(left->freq > right->freq);
    }
};




//void huffEncode(std::string str);
void printCodes(HuffNode* root, std::string str);
void storeCodes(struct HuffNode* root, std::string str);
void clearPriorityQueue(std::priority_queue<HuffNode*, std::vector<HuffNode*>, compare> treeHeap);
void HuffmanCodes(int size);
void calcFreq(std::string str, int n);
void huffEncode(std::string str, const char* out_path);
bool isCharNum(char chr);
void parseFreq(std::string str);
std::string decodeHuffman(std::string str);