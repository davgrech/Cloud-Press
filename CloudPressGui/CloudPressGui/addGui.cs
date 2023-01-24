using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SevenZip;
using static System.Net.WebRequestMethods;
using SharpCompress.Archives.SevenZip;

namespace CloudPressGui
{
  

    public partial class addGui : Form
    {
        string[] PATHS_TO_COMPRESS = {@"C:\Users\Dolev\Desktop\abc.txt", @"C:\Users\Dolev\Desktop\lmao.txt" };
        //TODO: NEED TO IMPORT THE FUNCTION WITHOUT ERRORS
        [DllImport(@"C:\\Users\\user\\Desktop\\Dolev\\magshimimProjects\\Projects_2022\\ashkelon-1206-compressor\\dll_compression_decompression\\x64\\Debug\\dll_compression_decompression.dll", CallingConvention =  CallingConvention.Cdecl)]
        static extern void EncodeLZSS(string inFilePath, string outFilePath);

     
     

        public addGui(String[] paths)
        {
            InitializeComponent();
            //all the passes the user selected to add

            //PATHS_TO_COMPRESS = paths;
        }

        private void fileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "All files |*.*", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtArchiveName.Text = ofd.FileName;
            }
        }

        private void folderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.Description = "Select your path.";
            if (fdb.ShowDialog() == DialogResult.OK)
                txtFolder.Text = fdb.SelectedPath;
        }



        private void compress_Click(object sender, EventArgs e)
        {
         

            //check fields
            if (txtArchiveName.Text == "" || txtFolder.Text == "")
            {
                return;
            }

            //get name  of future rar archive
            String nameOfArchive = Path.GetFileNameWithoutExtension(txtArchiveName.Text) + ".7z";


            if (!Directory.Exists(txtFolder.Text))
            {
                return;
            }

            //destination path for our future archive
            string archivePath = Path.Combine(txtFolder.Text, nameOfArchive);





            //algorithm --->

            //create 7zip file
        
            
           

            // compress the files 

            SevenZipExtractor.SetLibraryPath(@"7z.dll");

                

      
            ZipFiles(PATHS_TO_COMPRESS, archivePath);




        }
        public void ZipFiles(string[] filePaths, string outputFilePath, string password = null)
        {
            var tmp = new SevenZipCompressor();
            tmp.CompressionLevel = CompressionLevel.None;
            tmp.ScanOnlyWritable = true;
            tmp.CompressFilesEncrypted(outputFilePath, password, filePaths);
        }
        private void addGui_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check if lock archive is checked
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // pop a form asking for a password and a repeater

        }
    }
}
