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
        public extractGui()
        {
            InitializeComponent();
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

       
    }
}
