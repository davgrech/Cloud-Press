using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SevenZip;


namespace CloudPressGui
{
    internal   class ArchiveCreator
    {

       
        static string TEMP_FOLDER_OF_PROJECT = Path.Combine(Environment.CurrentDirectory, "temp");
        [DllImport(@"dll_compression_decompression.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void EncodeLZSS(string inFilePath, string outFilePath);

        public static void ZipFiles(string[] filePaths, string outputFilePath, string password = null)
        {
            var tmp = new SevenZipCompressor();
            tmp.CompressionLevel = CompressionLevel.None;
            tmp.ScanOnlyWritable = true;
            tmp.CompressFilesEncrypted(outputFilePath, password, filePaths);
        }

        public static void createArchive(string pathOfArchive, List<string> pathsToEncode)
        {

            //create temp folder
            createDirectory(TEMP_FOLDER_OF_PROJECT);

            foreach (string filePath in pathsToEncode)
            {
                string tempOutputPath = Path.Combine(TEMP_FOLDER_OF_PROJECT, Path.GetFileName(filePath));
            

                EncodeLZSS(filePath, tempOutputPath);
              



                


            }
            //after finish the temp folder
            //
            pathOfArchive = Path.Combine(pathOfArchive, Path.GetFileNameWithoutExtension(pathOfArchive)) + ".7z";


     
      
            ZipFiles(Directory.GetFiles(TEMP_FOLDER_OF_PROJECT), pathOfArchive);



            //delete the temp folder after archive created successfully
            deleteDirectory(TEMP_FOLDER_OF_PROJECT);




        }
        /*
         this function exists to decompress a specific file so the person can view it whether he clicked an archive 

        */
        public static  void decompressFile(string pathExract, string pathToArchive)
        {

            //create our temp directory 
            createDirectory(TEMP_FOLDER_OF_PROJECT);

            Directory.CreateDirectory(TEMP_FOLDER_OF_PROJECT);

            using (SevenZipExtractor extractor = new SevenZipExtractor(pathToArchive))
            {
                foreach (ArchiveFileInfo fileToExtract in extractor.ArchiveFileData)
                {

                    if (fileToExtract.FileName.Equals(pathExract))
                    {
                        
                        //TODO:: Extract
                        

                        string tempOutputPath = Path.Combine(TEMP_FOLDER_OF_PROJECT, fileToExtract.FileName);

                        EncodeLZSS(pathExract, tempOutputPath);



                       
                        break;
                    }
                   
                }
            }

            deleteDirectory(TEMP_FOLDER_OF_PROJECT);
           

        }

        private static void deleteDirectory(string folderPath)
        {
            if (Directory.Exists(TEMP_FOLDER_OF_PROJECT))
            {
                Directory.Delete(TEMP_FOLDER_OF_PROJECT, true);
            }
          
        }
        private static  void createDirectory(string folderPath)
        {
            Directory.CreateDirectory(TEMP_FOLDER_OF_PROJECT);
        }


    }
   
}
