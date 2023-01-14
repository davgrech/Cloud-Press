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
        //TODO: NEED TO IMPORT THE FUNCTION WITHOUT ERRORS

        [DllImport("dll_compression_decompression.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void EncodeLZSS(char[] inFilePath, char[] outFilePath);

        public addGui()
        {
            InitializeComponent();
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

            String output = "output.txt";


            if (txtFileName.Text == "" || txtFolder.Text == "")
            {
                return;
            }
            //get the name 
         
            String path_name = Path.GetFileName(txtFileName.Text);


            if (!Directory.Exists(txtFolder.Text))
            {
                return;
            }
            //destination path
            String dest_path = Path.Combine(txtFolder.Text, path_name);
            String src_path = txtFileName.Text;
           
            //comrpess source file and store in output file
            EncodeLZSS(src_path.ToCharArray(), output.ToCharArray());
            //create the compressed file
            File.Create(dest_path);
            //copy from the output file to the folder
            File.Copy(output, dest_path);





        }





        private void addGui_Load(object sender, EventArgs e)
        {

        }
    }
}
