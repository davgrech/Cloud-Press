This branch is the development branch of the encryption alg in the prokect
the encryption featues AES-256 file encryption with a constant key(written in one of the files)
passwords are saved as hashes in the first 32 bytes of the file itself
EncryptionHandler contains the handler for the ecryption - curenty has a main function for testing

*#TODO - REMOVE MAIN FUNC FOR FURTHER DEVELOPMENT*

*Handler usage as follows:*
void mainHandlerCall(const char* input_path, const char* output_path, const char* password, bool encryptOrDecrypt)

const char* input_path - input path: file to encrypt(will overwrite the existing data in that file)

const char* output_path - output path: file to decrypt (will overwrite the existing data in the file if it exits. if it doesnt, it will create a new one)

const char* password - password used for locking an archive/attemp at opening archive

bool encryptOrDecrypt - boolean value that checks if user wants to encrypt or decrypt file. true: encrypts | false: decrypts
