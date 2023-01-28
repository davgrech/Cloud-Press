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
    public partial class MainMenu : Form
    {

        private Rectangle webbrowserRectangle;
        private Rectangle orginialFormSize;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void browserBtn_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog dialog = new FolderBrowserDialog() { Description="Select your path." })
            {

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    webBrowser.Url = new Uri(dialog.SelectedPath);
                    txtPath.Text = dialog.SelectedPath;

                    
                }
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            if(webBrowser.CanGoBack)
                webBrowser.GoBack();
            
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
