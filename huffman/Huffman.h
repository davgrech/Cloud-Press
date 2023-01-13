#include <iostream>
#include <map>
#include <queue>


#define MAX_TREE_HT 256


std::map<char, std::string> codes;
std::map<char, int> freq;

struct HuffNode {

	char data;
	int freq;
	HuffNode* left, * right;
public:
	HuffNode(char data, int freq);


};

struct compare
{
	// simply compares frequencies between two nodes lol - will use it for priority queue
	bool operator()(HuffNode* left, HuffNode* right)
	{
		return(left->freq > right->freq);
	}
};
// stl priority queue, respects the huffman value of each element
// places it using comapre()
std::priority_queue<HuffNode*, std::vector<HuffNode*>, compare>
minHeap;

