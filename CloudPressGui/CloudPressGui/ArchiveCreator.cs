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
            
           


     
      
            ZipFiles(Directory.GetFiles(TEMP_FOLDER_OF_PROJECT), pathOfArchive); // get the files from temp folder and add it to archive 



            //delete the temp folder after archive created successfully
            deleteDirectory(TEMP_FOLDER_OF_PROJECT);




        }
        public static void extractArchive(string pathOfArchive, string destination)
        {
            ExtractFile(pathOfArchive, destination);

        }

        private static void ExtractFile(string archivePath, string destinationPath)
        {
            SevenZipExtractor extractor = new SevenZipExtractor(archivePath);
           
            extractor.ExtractArchive(destinationPath);

            

        }

        /*
         this function exists to decompress a specific file so the person can view it whether he clicked an archive 
        */
        public static  void decompressFileAndOpen(string pathExract, string pathToArchive)
        {

            //create our temp directory 
            

            createDirectory(VIEW_FOLDER_OF_PROJECT);
            createDirectory(TEMP_FOLDER_OF_PROJECT);

            
            string tempOutputPath = Path.Combine(TEMP_FOLDER_OF_PROJECT, Path.GetFileName(pathExract));
            using (SevenZipExtractor extractor = new SevenZipExtractor(pathToArchive))
            {
                using (Stream stream = new FileStream(tempOutputPath, FileMode.Create))
                {

                    //putting the encoded file in temp
                    extractor.ExtractFile(Path.GetFileName(pathExract), stream);
                }
                
            }

            
            string viewFile = Path.Combine(VIEW_FOLDER_OF_PROJECT, Path.GetFileName(pathExract));

            //putting the decoded file in view
            DecodeLZSS(tempOutputPath, viewFile);

            Process process = new Process();
            process.StartInfo.FileName = viewFile;
            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;
            process.Start();
            

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
