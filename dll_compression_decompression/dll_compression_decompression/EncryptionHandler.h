#include "AES.h"
#include "md5.h"
#define _CRT_SECURE_NO_WARNINGS
#define KEY "408a6bdf60ab9545fe02b3b2f07fc3f2668dfd58c57ab55336d60e32a7f62444"
#define MD5_LENGTH 32
#define BLOCK_SIZE_LEN 16
const std::vector<unsigned char> read_file(const std::string& filename);
void writeToFile(unsigned char* input, unsigned int length, const char* path);
void encryptFile(const char* input_path, const char* out_path, const char* password);
bool decryptFile(const char* input_path, const char* out_path, const char* password);
const char* getPasswordFromFile(std::vector<unsigned char> buffer);
bool checkPassword(const char* given_password, const char* actual_password);
const std::vector<unsigned char> read_file(const std::string& filename);
void mainHandlerCall(const char* input_path, const char* output_path, const char* password, bool encryptOrDecrypt);

