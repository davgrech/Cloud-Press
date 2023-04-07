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
    public partial class PasswordForm : Form
    {
        private string ARCHIVE_NAME;
        private List<string> PATHS_TO_ARCHIVE;
        private string PASSWORD = ""; 

        public PasswordForm(string archive, List<string> paths)
        {
            InitializeComponent();
            //init
            ARCHIVE_NAME = archive;
            PATHS_TO_ARCHIVE = paths;
            

        }

        private void okButton_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text.CompareTo(retxtPassowrd.Text) == 0)
            {
                ArchiveCreator.createArchive(ARCHIVE_NAME, PATHS_TO_ARCHIVE, txtPassword.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("error occurred", "passwords doesnt match", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
