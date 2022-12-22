#define _CRT_SECURE_NO_WARNINGS
#include "Huffman.h"
#include <string>
#include <fstream>
#define MIN_NUM_ASCII 48
#define MAX_NUM_ASCII 57


// stl priority queue, respects the huffman value of each element
// places it using comapre()
std::priority_queue<HuffNode*, std::vector<HuffNode*>, compare>
treeHeap;

std::map<unsigned char, std::string> codes; //first-> char | second->its huffman code
std::map<unsigned char, int> freq; //first->char second->frequency


//////////////////////////////////////////////////////////////////////////////////////// the data structures above were moved from Huffman.h

void clearPriorityQueue(std::priority_queue<HuffNode*, std::vector<HuffNode*>, compare> treeHeap)
{
    while (!treeHeap.empty())
    {
        treeHeap.pop();
    }
}

/*
* parse the frequency dict in the encoded huffman files
* loads map freq with all chars and thyere frequencies
*/
void parseFreq(std::string str)
{
    freq.clear();
    std::string specific_freq;
    unsigned char chr;
    unsigned char frequency;

    char* c = const_cast<char*>(str.c_str());
    strcpy(c, str.c_str());
    char* freq_str = strtok(c, "==");
    freq_str = strtok(NULL, "==");// get the frequency dict
    while (freq_str = strtok(freq_str, "||"))
    {
        std::cout << freq_str << std::endl;
        while (freq_str = strtok(freq_str, "::"))
        {
            chr = (unsigned char)freq_str;
            freq_str = strtok(NULL, "::");
            frequency = (unsigned char)freq_str;
            freq[chr] = frequency;
        }
        freq_str = strtok(NULL, "||");
    }
}

//checks if given char is a number
bool isCharNum(char chr)
{
    return (chr >= MIN_NUM_ASCII && chr <= MAX_NUM_ASCII);
}


///////////////////////////////////////////////////////////////////////////////////////Helper Funcs

HuffNode::HuffNode(unsigned char data, int freq)
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
    for (std::map<unsigned char, int>::iterator v = freq.begin();
        v != freq.end(); v++)
        treeHeap.push(new HuffNode(v->first, v->second));
    while (treeHeap.size() != 1) {
        left = treeHeap.top();
        treeHeap.pop();
        right = treeHeap.top();
        treeHeap.pop();
        top = new HuffNode('$',
            left->freq + right->freq);
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
    freq.clear();
    for (int i = 0; i < str.size(); i++)
        freq[str[i]]++;
}

/*
* function htat encdoes a given string into a huffman tree and returns said tree
*/
void huffEncode(std::string str, const char* out_path)
{
    std::ofstream myfile;
    std::string encodedString, decodedString;
    std::wstring xaxa;
    calcFreq(str, str.length());
    HuffmanCodes(str.length());

    myfile.open(out_path);
    myfile.clear();
    myfile.close();
    myfile.open(out_path);




    for (auto i : str)
        myfile << codes[i];

    //now we will output the frequncy and char dict to the file
    myfile << "=="; // know when the dict starts

    for (auto itr = freq.begin(); itr != freq.end(); itr++) // insert codes
    {

        std::cout << int(itr->first) << " " << (char)itr->first << " has " << itr->second << " appearances\n";
        myfile << "||";
        myfile << (int)itr->first;
        myfile << "::";
        myfile << itr->second;
        myfile << "||";

    }
    //myfile << "\0";
    //myfile << EOF;
    myfile.close();

}



/*
* decode a huffman tree with a dict in it
*/
std::string decodeHuffman(std::string str)
{
    std::string ans = "";
    //freq.clear(); // shitty code design
    parseFreq(str);

    //now that i have all frequencies and chars - we can build a new hufftree
    HuffmanCodes(freq.size()); // create huff tree

    struct HuffNode* root = treeHeap.top();
    struct HuffNode* curr = root;

    for (int i = 0; i < str.size(); i++) {
        if (str[i] == '0')
            curr = curr->left;
        else
            curr = curr->right;

        // reached leaf node
        if (curr->left == NULL and curr->right == NULL) {
            ans += curr->data;
            curr = root;
        }
    }
    return ans + '\0';



}



