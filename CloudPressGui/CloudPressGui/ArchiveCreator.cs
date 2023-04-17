using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SevenZip;
using System.Diagnostics;
using System.Windows.Forms;

namespace CloudPressGui
{
    internal   class ArchiveCreator
    {

       
        static string TEMP_FOLDER_OF_PROJECT = Path.Combine(Environment.CurrentDirectory, "temp");
        static string VIEW_FOLDER_OF_PROJECT = Path.Combine(Environment.CurrentDirectory, "view");

        [DllImport(@"dll_compression_decompression.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void EncodeLZSS(string inFilePath, string outFilePath);

        [DllImport(@"dll_compression_decompression.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void DecodeLZSS(string inFilePath, string outFilePath);


        //decrypt encrpyt
        [DllImport(@"dll_compression_decompression.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern bool Decrpyt(string input_path, string out_path, string password);

        [DllImport(@"dll_compression_decompression.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void Encrypt(string input_path, string out_path, string password);

        public static void ZipFiles(string[] filePaths, string outputFilePath, string password = null)
        {
            var tmp = new SevenZipCompressor();
            tmp.CompressionLevel = CompressionLevel.None;
            tmp.ScanOnlyWritable = true;
            tmp.CompressFilesEncrypted(outputFilePath, password, filePaths);
            
            
        }
        public static void ZipDirectory(string DirectroryPath, string outputFilePath, string password = null)
        {
            var tmp = new SevenZipCompressor();
            tmp.CompressionLevel = CompressionLevel.None;
            tmp.ScanOnlyWritable = true;
            
          
            tmp.CompressDirectory(DirectroryPath, outputFilePath);
           
        }

        public static void createArchive(string pathOfArchive, List<string> pathsToEncode, string password = null)
        {


            //create temp folder
            createDirectory(TEMP_FOLDER_OF_PROJECT);


            string tempOutputPath = "";

            foreach (string path in pathsToEncode)
            {

                //if directory
                if (Directory.Exists(path))
                {

                    //get all the files and childdirectories files
                    string[] fileEntries = Directory.GetFiles(path, "*", SearchOption.AllDirectories); //search in all directories
                    foreach (string fileEntry in fileEntries)
                    {
                        

                        string relativePath = GetRelativePath(Path.GetDirectoryName(path), fileEntry);
                        string parentDirectory = Path.GetDirectoryName(relativePath);  // relative directory 
                        tempOutputPath = Path.Combine(TEMP_FOLDER_OF_PROJECT, parentDirectory, Path.GetFileName(fileEntry)); // to compress directories
                        createDirectory(Path.Combine(TEMP_FOLDER_OF_PROJECT, parentDirectory));

                        //compress
                        EncodeLZSS(fileEntry, tempOutputPath);
                        //encrypt if password set
                        EncryptArchive(tempOutputPath, password);

                        ZipDirectory(TEMP_FOLDER_OF_PROJECT, pathOfArchive);
                    }

                }
                else
                {
                    

                    tempOutputPath = Path.Combine(TEMP_FOLDER_OF_PROJECT, Path.GetFileName(path));
                    //compress
                    EncodeLZSS(path, tempOutputPath);
                    //encrypt if password set
                    EncryptArchive(tempOutputPath, password);

                    ZipFiles(Directory.GetFiles(TEMP_FOLDER_OF_PROJECT), pathOfArchive); // get the files from temp folder and add it to archive 
                }






            }

            //after finish the temp folder
            //










            //delete the temp folder after archive created successfully
            deleteDirectory(TEMP_FOLDER_OF_PROJECT);





        }
        //this is getting the unique parent directoreis from pathA to pathB
        public static string GetRelativePath(string fromPath, string toPath)
        {
            var fromUri = new Uri(fromPath);
            var toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme)
            {
                return toPath;
            }

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (string.IsNullOrEmpty(relativePath))
            {
                return ".";
            }

            if (relativePath.IndexOf('/') >= 0)
            {
                relativePath = relativePath.Substring(relativePath.IndexOf('/') + 1);
            }

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }


        public static void extractArchive(string pathOfArchive, string destination)
        {
            ExtractFile(pathOfArchive, destination);

        }
        public static bool EncryptArchive(string pathToEncrypt, string password = null)
        {
           
            if(password == null)
            {
                return false;
            }
            string tempFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            Encrypt(pathToEncrypt, "C:\\Users\\user\\Desktop\\lmdasd.txt", password);
            
            File.Replace(tempFile, pathToEncrypt, Path.Combine(Path.GetTempPath(), Path.GetTempFileName()));

            //add the encrypt flag
            addEncryptedAttribute(pathToEncrypt);



            return true;
        }
        private static void addEncryptedAttribute(string filePath)
        {
            FileAttributes attributes  = File.GetAttributes(filePath);
            if ((attributes & FileAttributes.Encrypted) != FileAttributes.Encrypted)
            {
                attributes |= FileAttributes.Encrypted; 
                File.SetAttributes(filePath, attributes);
                MessageBox.Show("The encrypted flag has been added", "!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The encrypted flag is already raised", "!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private static void ExtractFile(string archivePath, string destinationPath, string password = null)
        {
            string tempPath;
            
          
           
            SevenZipExtractor extractor = new SevenZipExtractor(archivePath);
            extractor.ExtractArchive(destinationPath);
      
            

            foreach(var file in extractor.ArchiveFileData)
            {
                string fullpath = Path.Combine(destinationPath, file.FileName);
             

                if (!Directory.Exists(fullpath))
                {
                    
                    //decoded file here
                    tempPath = generateTempPath();

                    //decrypt
                    Decrpyt(fullpath, tempPath, password);

                    CopyFile(tempPath, fullpath);



                    tempPath = generateTempPath();
                    //original content in fullpath
                    DecodeLZSS(fullpath, tempPath);

                    //create the extracted directory
                    createDirectory(Path.GetDirectoryName(fullpath));

                    CopyFile(tempPath, fullpath);

                }
            }
        
        }
        private static string generateTempPath()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        }
        private static void CopyFile(string sourceFile, string destinationFile)
        {
            byte[] fileBytes = File.ReadAllBytes(sourceFile);
            File.WriteAllBytes(destinationFile, fileBytes);
        }
       


        /*
         this function exists to decompress a specific file so the person can view it whether he clicked an archive 
        */
        public static  void decompressFileAndOpen(string pathExract, string pathToArchive)
        {

            //create our temp directory 
            

            createDirectory(VIEW_FOLDER_OF_PROJECT);
            createDirectory(TEMP_FOLDER_OF_PROJECT);

            
            //putting encoded file in temp foldeer
            string tempOutputPath = Path.Combine(TEMP_FOLDER_OF_PROJECT, Path.GetFileName(pathExract));
            using (SevenZipExtractor extractor = new SevenZipExtractor(pathToArchive))
            {
                using (Stream stream = new FileStream(tempOutputPath, FileMode.Create))
                {

                    //putting the encoded file in temp
                    extractor.ExtractFile(pathExract, stream);
                    
                }
                
            }

            
            string viewFile = Path.Combine(VIEW_FOLDER_OF_PROJECT, Path.GetFileName(pathExract));

            //putting the decoded file in view so ppl can process it
            DecodeLZSS(tempOutputPath, viewFile);


            FileAttributes attributes = File.GetAttributes(viewFile);
            if ((attributes & FileAttributes.Encrypted) == FileAttributes.Encrypted)
            {
                //dialog to ask for password
                string password = "ask";
 
                string tempFile = Path.Combine(Path.GetTempPath(), Path.GetTempPath());
                //check if the password is the currect
                if(ArchiveCreator.Decrpyt(viewFile, tempFile, password))
                {
                    //put in view file the decrypted file
                    File.Replace(tempFile, viewFile, Path.Combine(Path.GetTempPath(), Path.GetTempPath()));
                    try
                    {
                        Process process = new Process();
                        process.StartInfo.FileName = viewFile;
                        process.EnableRaisingEvents = true;
                        process.Exited += Process_Exited;
                        process.Start();
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    //password is worng
                    MessageBox.Show("error occurred", "passwords is wrong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                //run the process without ddecryption
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = viewFile;
                    process.EnableRaisingEvents = true;
                    process.Exited += Process_Exited;
                    process.Start();
                }
                catch (Exception ex)
                {

                }
            }

            

            deleteDirectory(TEMP_FOLDER_OF_PROJECT);
        
           

        }
        static void Process_Exited(object sender, EventArgs e)
        {
            if(Directory.Exists(VIEW_FOLDER_OF_PROJECT)) 
                deleteDirectory(VIEW_FOLDER_OF_PROJECT);




        }

        private static void deleteDirectory(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
          
        }
        private static  void createDirectory(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }


    }
   
}
