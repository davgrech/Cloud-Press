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
using System.Diagnostics; // for process execution

using SevenZip;


namespace CloudPressGui
{
    public partial class MainMenu : Form
    {

        const string STARTERPOSITION = @"C:\Users\user\";
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
            DirectoryInfo info = new DirectoryInfo(STARTERPOSITION + @"Desktop");
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

                editItemsListView(dir.Name, dir.LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.Folder);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                if (file.Name.EndsWith(".7z"))
                {
                    editItemsListView(file.Name, file.LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.Zip);
                    
                }
                else
                {

                    editItemsListView(file.Name, file.LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.File);

                }


            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }



        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            //if to handle multy select in a list view
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

            //"C:\Users\user\Desktop\CodeBlocks.rar"
            PATH_OF_ARCHIVE = STARTERPOSITION + treeView1.SelectedNode.FullPath;

            if (PATH_OF_ARCHIVE == null)
            {
                return new List<string>();
            }

            var selectedItemsOfListView = listView1.SelectedItems;
            for (int i = 0; i < selectedItemsOfListView.Count; i++) // for every selected item get his path 
            {
                string tempPath = selectedItemsOfListView[i].SubItems[0].Text;//loop through each item
                paths.Add(Path.Combine(PATH_OF_ARCHIVE, tempPath)); //adding to our list of strings
            }
            foreach (string path in paths)
            {
                MessageBox.Show(path, "lol", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return paths;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string fileSelectedName = "";
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedDoubleClick = listView1.SelectedItems[0];
                fileSelectedName = selectedDoubleClick.SubItems[0].Text;
            }

            //global variable to store the path of slected archive
            PATH_OF_ARCHIVE = STARTERPOSITION + treeView1.SelectedNode.FullPath;

            //selected path
            string pathOfSelected = Path.Combine(PATH_OF_ARCHIVE, fileSelectedName);
            if (Path.GetExtension(pathOfSelected) == ".7z")
            {
                PATH_OF_ARCHIVE = pathOfSelected;
                using (SevenZipExtractor extractor = new SevenZipExtractor(pathOfSelected))
                {

                    putArchiveFilesToListView(extractor);



                }
            }
            else
            {
                //runs the selected process
                Process process = new Process();
                process.StartInfo.FileName = pathOfSelected;
                process.Start();
            }

        }
        private void putArchiveFilesToListView(SevenZipExtractor extractor)
        {
            listView1.Items.Clear();
            for (int i = 0; i < extractor.ArchiveFileData.Count; i++)
            {
              
                if (Path.GetExtension(extractor.ArchiveFileData[i].FileName) == ".7z")
                {
                    editItemsListView(extractor.ArchiveFileData[i].FileName, extractor.ArchiveFileData[i].LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.Zip);
                    

                }
                else if (extractor.ArchiveFileData[i].IsDirectory)
                {
                    editItemsListView(extractor.ArchiveFileData[i].FileName, extractor.ArchiveFileData[i].LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.Folder);
                }
                else
                {
                    editItemsListView(extractor.ArchiveFileData[i].FileName, extractor.ArchiveFileData[i].LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.File);
                }


            }
            
           
        }
        /*
         * type map
         * 0. folder
         * 1. text
         * 2. zip
         */
        private void editItemsListView(string fileName, string lastAcessTime, int typeOfFile)
        {
            //check if the type of file is valid our enum is stored in fileTypeMap
            if (!isInRageOfTypeMapEnum(typeOfFile))
            {
                return;
            }
            
            ListViewItem item = new ListViewItem(fileName, typeOfFile);
            ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[]
            { new ListViewItem.ListViewSubItem(item, Enum.GetName(typeof(fileTypeMap.TypeMap), typeOfFile)),
                    new ListViewItem.ListViewSubItem(item,
                      lastAcessTime)};
            item.SubItems.AddRange(subItems);
            listView1.Items.Add(item);
        }

        private bool isInRageOfTypeMapEnum(int value)
        {
            Array values = Enum.GetValues(typeof(fileTypeMap.TypeMap));
            int minValue = (int)values.GetValue(0);
            int maxValue = (int)values.GetValue(values.Length - 1);

            return value >= minValue && value <= maxValue;
        }
    }
    
        
}
