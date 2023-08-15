#include "EncryptionHandler.h"

const std::vector<unsigned char> read_file(const std::string& filename) {
    std::ifstream file(filename, std::ios::binary);
    if (!file.is_open()) {
        std::cout << "Error: Could not open file " << filename << std::endl;
        return {};
    }

    // Get the size of the file
    file.seekg(0, std::ios::end);
    size_t size = file.tellg();
    file.seekg(0, std::ios::beg);

    // Allocate memory for the buffer
    std::vector<unsigned char> buffer(size);

    // Read the contents of the file into the buffer
    file.read(reinterpret_cast<char*>(buffer.data()), size);

    if (!file) {
        std::cout << "Error: Could not read file " << filename << std::endl;
        return {};
    }

    file.close();
    return buffer;
}

void writeToFile(unsigned char* input, unsigned int length, const char* path)
{
    std::string data(reinterpret_cast<char*>(input));
    std::ofstream file(path);
    for (int i = 0; i < length; i++) // sizes are problematic here
    {
        file << data[i];
    }
    file.close();
}

void encryptFile(const char * input_path, const char* out_path, const char* password)
{
    AES Encryptor;

    std::string key_str = KEY;
    const unsigned char* input_key = reinterpret_cast<const unsigned char*>(key_str.c_str()); 
    
    //read content from file and parse it to fitting type
    std::ifstream file(input_path, std::ios::binary);
    std::vector<unsigned char> buffer((std::istreambuf_iterator<char>(file)), std::istreambuf_iterator<char>()); 
    file.close();

    //parse it even more lol
    unsigned int length = buffer.size();
    std::string data_str;
    int count = 0;
    for (auto it : buffer)
    {
        count += 1;
        data_str += it;
    }
    while (count % BLOCK_SIZE_LEN != 0) // update the padding
    {
        count += 1;
    }
    // all these are type-casting input and output:(
    const unsigned char* data = reinterpret_cast<const unsigned char*>(data_str.c_str()); 
    std::string str_data_from_file(data, data + count);

    unsigned char* output = Encryptor.EncryptECB(data, count, input_key); // run the encryption

    std::basic_string<unsigned char> ustr(output, output + count);
    std::string str_output(ustr.begin(), ustr.end()); // copy encryption output to string

    std::string pass_and_data = md5(password) + str_output;

    unsigned char* uchar_ptr = new unsigned char[pass_and_data.length() + 1];
    memcpy(uchar_ptr, pass_and_data.data(), pass_and_data.length());
    uchar_ptr[pass_and_data.length()] = '\0';


    writeToFile(uchar_ptr, MD5_LENGTH + count, out_path);
}
/*
* returns true if the two passwords match
*/
bool checkPassword(const char* given_password, const char* actual_password) // md5 hash gives string of 32 chars, im comparing hashes here
{
    return !(strcmp(given_password, actual_password) ? true : false);
}
/*
* get the file's password from the file
*/
const char* getPasswordFromFile(std::vector<unsigned char> buffer)
{
    std::string lol(buffer.begin(), buffer.begin() + 32);
    const char* cstr = lol.c_str();
    return cstr;
}
void decryptFile(const char* input_path, const char* out_path, const char* password)
{
    AES Decryptor;
    std::ifstream file(input_path, std::ios::binary);
    std::vector<unsigned char> buffer((std::istreambuf_iterator<char>(file)), std::istreambuf_iterator<char>()); // read file to buffer
    file.close();

    /////////////////////////////password checking
    std::string hashed_password = md5(password); 
    password = hashed_password.c_str();

    std::string pass_from_file(buffer.begin(), buffer.begin() + 32);
    const char* c_pass_from_file = pass_from_file.c_str();

    if (!checkPassword(c_pass_from_file, password))
    {
        std::cerr << "Passwords do no match";
        //throw("passwords do not match");
        return;
    }
    buffer.erase(buffer.begin(), buffer.begin() + 32); //delete the password from buffer

    ////////////////////password checking 

    const unsigned char* data = buffer.data();
    unsigned int length = buffer.size();

   
    std::string keyinput = KEY; // key is constant to avoid headaches lol
    const unsigned char* fitting_key = reinterpret_cast<const unsigned char*>(keyinput.c_str());


    unsigned char* output = Decryptor.DecryptECB(data, length, fitting_key);
    writeToFile(output, length, out_path);
}

/*
* main call handler for file encryption and decryption
* takes input file path to encrypt (assumes is a single file) - const char* input_path
* takes output file, overwrites the file if exists, creates a new one if doesnt - const char* output_path
* unhashed password - const char* password
* boolean value that checks if user wants to encrypt or decrypt: encrypt : true | decrpyt : false - bool encryptOrDecrypt
*/
void mainHandlerCall(const char* input_path, const char* output_path, const char* password, bool encryptOrDecrypt)
{
    if (encryptOrDecrypt) // encrypt file
    {
        encryptFile(input_path, output_path, password);
        std::cout << "encryption done!" << std::endl;
    }
    else if (!encryptOrDecrypt)
    {
        decryptFile(input_path, output_path, password);
        std::cout << "decryption done!" << std::endl;
    }
    else
    {
        std::cout << "idk how this went wrong";
    }
}
int main()
{
    const char* input = "D:\\lol.txt";
    const char* output = "D:\\output.txt";
    mainHandlerCall(input, output, "gay", true);

}



