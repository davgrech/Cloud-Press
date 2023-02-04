using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CloudPressGui
{
    public partial class MainMenu : Form
    {


        string SELECTED_PATH;
        string PATH_OF_ARCHIVE;
        public MainMenu(string PATH)
        {
            InitializeComponent();
            SELECTED_PATH = PATH;
            PopulateTreeView();
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
        }

        private void browserBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void backBtn_Click(object sender, EventArgs e)
        { 
            
        }

      



        private void PopulateTreeView()
        {
            TreeNode rootNode;

            //DirectoryInfo info = new DirectoryInfo(SELECTED_PATH);
            DirectoryInfo info = new DirectoryInfo(@"C:\Users\Dolev\Desktop");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs,
            TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory"),
             new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File"),
             new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var item = listView1.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    if (ModifierKeys == Keys.Control)
                    {
                        if (item.Selected)
                        {
                            item.Selected = false;
                        }
                        else
                        {
                            item.Selected = true;
                        }
                    }
                    else
                    {
                        listView1.SelectedItems.Clear();
                        item.Selected = true;
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                //string path = listView1.SelectedItems[0].SubItems[0].Text;
                TreeNode selectedNode = treeView1.SelectedNode;
                //string path = selectedNode.FullPath;
               // MessageBox.Show("test", "hello",MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
      
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void ad_Click(object sender, EventArgs e)
        {
            List<string> listOfPaths = getPathsToArchiveFromGui();

            //create + compress the files 
            ArchiveCreator.createArchive(PATH_OF_ARCHIVE, listOfPaths);
            
        }
        private List<string> getPathsToArchiveFromGui()
        {
            List<string> paths = new List<string>();
        

        PATH_OF_ARCHIVE = @"C:\Users\Dolev\" + treeView1.SelectedNode.FullPath;
          
            if (PATH_OF_ARCHIVE == null)
            {
                return new List<string>();
            }

            var selectedItemsOfListView = listView1.SelectedItems;
            for(int i = 0; i < selectedItemsOfListView.Count; i++) // for every selected item get his path 
            {
                string tempPath = selectedItemsOfListView[i].SubItems[0].Text;//loop through each item
                paths.Add(Path.Combine(PATH_OF_ARCHIVE, tempPath)); //adding to our list of strings
            }
            foreach (string path in paths)
            {
                MessageBox.Show(path, "lol",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            return paths;
        }
    }
}
