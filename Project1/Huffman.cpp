#include "Huffman.h"
#include "lzss.h"

HuffNode::HuffNode(char data, int freq)
{
    left = right = NULL;
    this->data = data;
    this->freq = freq;
}

// simply compares frequencies between two nodes lol - will use it for priority queue
bool HuffNode::operator()(HuffNode* left, HuffNode* right)
{
    return(left->freq > right->freq);
}
/*
* func that prints the huffman tree recursively from its root
* input: tree root, str to print
* output: tree values from root
*/
void HuffNode::printCodes(HuffNode* root, std::string str)
{
	{
		if (!root) // fuck the root fr - if root is empty then tf we doin
		{
			return;
		}
		if (root->data != '$')
		{
			std::cout << root->data << ": " << str << "\n";
			printCodes(root->left, str + "0");
			printCodes(root->right, str + "1");
		}

	}
}
