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
    internal class ArchiveCreator
    {


        [DllImport(@"dll_compression_decompression.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void EncodeLZSS(string inFilePath, string outFilePath);

        public static void ZipFiles(string[] filePaths, string outputFilePath, string password = null)
        {
            var tmp = new SevenZipCompressor();
            tmp.CompressionLevel = CompressionLevel.None;
            tmp.ScanOnlyWritable = true;
            tmp.CompressFilesEncrypted(outputFilePath, password, filePaths);
        }

        public  static void createArchive(string pathOfArchive, List<string> pathsToEncode)
        {
           
            string projectDirectory = Environment.CurrentDirectory;
            string newFolderPath = Path.Combine(projectDirectory, "temp");
            Directory.CreateDirectory(newFolderPath);
            foreach (string file in pathsToEncode)
            {
                string tempOutputPath = Path.Combine(newFolderPath, Path.GetFileName(file));
                File.Create(tempOutputPath); // create the temp file

                EncodeLZSS(file, tempOutputPath);
              
                


            }
            //after finish the temp folder
            //
            pathOfArchive = Path.Combine(pathOfArchive, Path.GetFileNameWithoutExtension(pathOfArchive)) + ".7z";



      
            ZipFiles(Directory.GetFiles(newFolderPath), pathOfArchive);
         


            //delete the temp folder after archive created successfully
            if (Directory.Exists(newFolderPath))
            {
                Directory.Delete(newFolderPath, true);
            }




        }

       
    }
}
