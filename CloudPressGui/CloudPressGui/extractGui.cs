using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudPressGui
{
    public partial class extractGui : Form
    {
        List<string> PATH_OF_ARCHIVE;
        
        public extractGui(List<string> archivePath, string destination)
        {
            InitializeComponent();


            //init global variable
            PATH_OF_ARCHIVE = archivePath;

            txtPath.Text = destination;

        }

        private void chooseBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog() { Description = "Select your path." })
            {

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void extractGui_Load(object sender, EventArgs e)
        {

        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PATH_OF_ARCHIVE.Count; i++)
            {
                ArchiveCreator.extractArchive(PATH_OF_ARCHIVE[i], txtPath.Text);

            }
            this.Close();
        }

        
    }
}
