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
    public partial class addGui : Form
    {
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

        
    }
}
