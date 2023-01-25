
#include "AES.h"
std::string KEY;
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
//generates a a random key
std::stringstream generateKey()
{
    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<unsigned long long> dist(0, (1ull << 64) - 1);

    std::stringstream key;
    std::stringstream iv;
    AES lol;

    for (int i = 0; i < 4; i++) {
        key << std::hex << dist(gen);
        iv << std::hex << dist(gen);
    }
    KEY = key.str();
    return key;
}
void encryptFile(const char * input_path, const char* out_path)
{
    AES Encryptor;

    //generate random key and parse it to fitting type
    std::stringstream key = generateKey();
    std::cout << key.str() << std::endl;
    std::string key_str = key.str();
    const unsigned char* input_key = reinterpret_cast<const unsigned char*>(key_str.c_str()); 
    
    //read content from file and parse it to fitting type
    std::ifstream file(input_path, std::ios::binary);
    std::vector<unsigned char> buffer((std::istreambuf_iterator<char>(file)), std::istreambuf_iterator<char>()); 
    file.close();

    //parse it even more lol
    unsigned int length = buffer.size();
    std::string data_str;
    for (auto it : buffer)
    {
        data_str += it;
    }
    const unsigned char* data = reinterpret_cast<const unsigned char*>(data_str.c_str()); 


    unsigned char* output = Encryptor.EncryptECB(data, length, input_key);
    writeToFile(output, length, out_path);
}
void decryptFile(const char* input_path, const char* out_path, const unsigned char key[])
{
    AES Decryptor;


    std::ifstream file(input_path, std::ios::binary);
    std::vector<unsigned char> buffer((std::istreambuf_iterator<char>(file)), std::istreambuf_iterator<char>()); // read file to buffer
    file.close();

    const unsigned char* data = buffer.data();
    unsigned int length = buffer.size();

   
    
    std::string keyinput = "";
    std::cout << "Enter key: ";
    std::cin >> keyinput;
    const unsigned char* fitting_key = reinterpret_cast<const unsigned char*>(KEY.c_str());
    std::cout << fitting_key << std::endl;


    unsigned char* output = Decryptor.DecryptECB(data, length, fitting_key);
    std::cout << output << std::endl;
    writeToFile(output, length, out_path);
}
int main()
{
    const char* test_input = "D:\\lol.txt";
    const char* test_output = "D:\\output.txt";
    const unsigned char lol[] = "die die die";
    int choice = -1;
    
    while (choice != 3)
    {
        std::cout << "0 - encrypt\n1 - decrypt\n3 - exit\nEnter yout choice: " << std::endl;
        std::cin >> choice;
        if (choice == 0)
        {
            //encrypt
            encryptFile(test_input, test_output);
        }
        else if (choice == 1)
        {
            //decrypt
            decryptFile(test_output, test_output, lol);
        }
        else if (choice == 3)
        {
            exit;
        }
        
    }
    




}



