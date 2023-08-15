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
using Microsoft.VisualBasic;
namespace CloudPressGui
{
  

    public partial class addGui : Form
    {


        List<string> PATHS_TO_COMPRESS = new List<string>();

        //TODO: NEED TO IMPORT THE FUNCTION WITHOUT ERRORS
        [DllImport(@"dll_compression_decompression.dll", CallingConvention =  CallingConvention.Cdecl)]
        static extern void EncodeLZSS(string inFilePath, string outFilePath);

        static string ARCHIVE_PATH;
    

        public addGui(List<string> paths, string archive)
        {
            InitializeComponent();
            //all the passes the user selected to add
           

           //init global variables
            PATHS_TO_COMPRESS = paths;
            ARCHIVE_PATH = archive;
            

            if (paths == null) Application.Exit();
            if (PATHS_TO_COMPRESS.Count > 0)
            {
                if (PATHS_TO_COMPRESS.Count == 1)
                    txtArchiveName.Text = Path.Combine(ARCHIVE_PATH, Path.GetFileNameWithoutExtension(PATHS_TO_COMPRESS[0])) + ".7z";
                else
                    txtArchiveName.Text = Path.Combine(ARCHIVE_PATH, Path.GetFileNameWithoutExtension(ARCHIVE_PATH)) + ".7z";

               
            }
            else
            {
                return;
            }
           
           

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

       



        private void compress_Click(object sender, EventArgs e)
        {
         

            //check fields
            //if (txtArchiveName.Text == "" || txtFolder.Text == "")
            //{
            //    return;
            //}

            //get name  of future rar archive
            String nameOfArchive;
            
            
           


            //if (!Directory.Exists(txtFolder.Text))
            //{
            //    return;
            //}

            //destination path for our future archive
            //string archivePath = Path.Combine(txtFolder.Text, nameOfArchive);
            ArchiveCreator.createArchive(txtArchiveName.Text, PATHS_TO_COMPRESS);


            this.Close();




            

                

            




        }
        public void ZipFiles(List<string> filePaths, string outputFilePath, string password = null)
        {
            var tmp = new SevenZipCompressor();
            tmp.CompressionLevel = CompressionLevel.None;
            tmp.ScanOnlyWritable = true;
            tmp.CompressFilesEncrypted(outputFilePath, password, filePaths.ToArray());
        }
        private void addGui_Load(object sender, EventArgs e)
        {

        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
           
            PasswordForm testDialog = new PasswordForm(txtArchiveName.Text, PATHS_TO_COMPRESS);
            testDialog.Show();

            
            
        }
    }
}
