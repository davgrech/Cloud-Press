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


namespace CloudPressGui
{
  

    public partial class addGui : Form
    {
        string[] PATHS_TO_COMPRESS; 
        //TODO: NEED TO IMPORT THE FUNCTION WITHOUT ERRORS
        [DllImport(@"C:\\Users\\user\\Desktop\\Dolev\\magshimimProjects\\Projects_2022\\ashkelon-1206-compressor\\dll_compression_decompression\\x64\\Debug\\dll_compression_decompression.dll", CallingConvention =  CallingConvention.Cdecl)]
        static extern void EncodeLZSS(string inFilePath, string outFilePath);

        public addGui(String[] paths)
        {
            InitializeComponent();
            //all the passes the user selected to add
            PATHS_TO_COMPRESS = paths;
        }

        private void fileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "All files |*.*", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtFileName.Text = ofd.FileName;
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
            //the temo file we will 
            String output = "output.txt";


            if (txtFileName.Text == "" || txtFolder.Text == "")
            {
                return;
            }
            
            //get name  of future rar archive
            String nameOfArchive = Path.GetFileNameWithoutExtension(txtFileName.Text)+".rar";
           

            if (!Directory.Exists(txtFolder.Text))
            {
                return;
            }
            //destination path for our future archive
            string dest_path = Path.Combine(txtFolder.Text, nameOfArchive);
            string src_path = txtFileName.Text;




            //algorithm --->
            //comrpess source file and store in output file
            EncodeLZSS(src_path, "C:\\Users\\user\\Desktop\\abc.txt");
            





        }





        private void addGui_Load(object sender, EventArgs e)
        {

        }
    }
}
