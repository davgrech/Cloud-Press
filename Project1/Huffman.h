#include <iostream>
#include <map>


#define MAX_TREE_HT 256

std::map<char, std::string> codes;
std::map<char, int> freq;

struct HuffNode {
private:
	char data;
	int freq;
	HuffNode* left, * right;
public:
	HuffNode();


};
