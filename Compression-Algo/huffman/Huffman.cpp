#include "Huffman.h"
#include "lzss.h"

HuffNode::HuffNode(char data, int freq)
{
    left = right = NULL;
    this->data = data;
    this->freq = freq;
}
/*
* func that prints the huffman tree recursively from its root
* input: tree root, str to print(the value the node holds)
* output: tree values from root
*/
void printCodes(HuffNode* root, std::string str)
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
/*
* function that stores it's values(char) with theyre huffman
* values in a hash table
* input: huff tree root, char value as string
* output: stores char and its huffman value in codes (works as a table - but is a map)
*/
void storeCodes(struct HuffNode* root, std::string str)
{
	if (root == NULL)
		return;
	if (root->data != '$')
		codes[root->data] = str;
	storeCodes(root->left, str + "0");
	storeCodes(root->right, str + "1");
}

/*
* Func that builds the huffman tree and puts it in the priority queue
* input: size of said tree
* output: - (but stores all data in a priority queue)
*/
void HuffmanCodes(int size)
{
    struct HuffNode* left, * right, * top;
    for (std::map<char, int>::iterator v = freq.begin(); v != freq.end(); v++)
    {
        treeHeap.push(new HuffNode(v->first, v->second));
    }
    while (treeHeap.size() != 1) {
        left = treeHeap.top();
        treeHeap.pop();
        right = treeHeap.top();
        treeHeap.pop();
        top = new HuffNode('$',
            left->freq + right->freq); //root's data is $ and value is all frequencies
        top->left = left;
        top->right = right;
        treeHeap.push(top);
    }
    storeCodes(treeHeap.top(), "");
}

/*
* func that calculates the frequency of each char
* stores all frequencies in freq map
*/
void calcFreq(std::string str, int n)
{
    for (int i = 0; i < str.size(); i++)
        freq[str[i]]++;
}
