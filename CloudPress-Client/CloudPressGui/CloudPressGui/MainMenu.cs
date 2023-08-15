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
using System.Collections.ObjectModel;

namespace CloudPressGui
{
    public partial class MainMenu : Form
    {
        static string TEMP_FOLDER_OF_PROJECT = Path.Combine(Environment.CurrentDirectory, "temp");
        static string VIEW_FOLDER_OF_PROJECT = Path.Combine(Environment.CurrentDirectory, "view");

        //globals to navigate
        const string STARTERPOSITION = @"C:\Users\user\";
        string CURRENT_PATH = STARTERPOSITION + @"Desktop";
        string SELECTED_PATH = "";
        string PATH_OF_ARCHIVE = "";
        string RELATIVE_ROOT_ARCHIVE = "";
        bool IS_IN_ARCHIVE_MODE = false;

        public MainMenu(string PATH)
        {
            InitializeComponent();
            SELECTED_PATH = PATH;
           
            

            txtCurrentPath.Text = CURRENT_PATH;




            refreshListView(CURRENT_PATH);


            
            
            
            
        }
        

        private void EventHandler(object sender, EventArgs e)
        {
            refreshListView(CURRENT_PATH);
        }

        private void browserBtn_Click(object sender, EventArgs e)
        {

        }

        private void backBtn_Click(object sender, EventArgs e)
        {

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
        


        

        /*
         * multy select function.
         */
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
             
                //string path = selectedNode.FullPath;
                // MessageBox.Show("test", "hello",MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void ad_Click(object sender, EventArgs e)
        {


            //archive
            

            List<string> listOfPaths = getPathsToArchiveFromGui();

            
            //create + compress the files 
            if (PATH_OF_ARCHIVE == "") return;

            addGui addWindows = new addGui(listOfPaths, PATH_OF_ARCHIVE);
            addWindows.FormClosed += AddWindows_FormClosed;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;


            addWindows.ShowDialog();

            refreshListView(CURRENT_PATH);





        }

        private void AddWindows_FormClosed(object sender, FormClosedEventArgs e)
        {
           this.FormBorderStyle= FormBorderStyle.Sizable;
            this.ControlBox = true;
        }

        private List<string> getPathsToArchiveFromGui()
        {
            PATH_OF_ARCHIVE = CURRENT_PATH;
            List<string> paths = new List<string>();

            //"C:\Users\user\Desktop\CodeBlocks.rar"
           
            

            var selectedItemsOfListView = listView1.SelectedItems;
            if(selectedItemsOfListView.Count > 0)
            {
               
               

                if (PATH_OF_ARCHIVE == null)
                    return new List<string>();
                
            }
            

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
            string fileSelectedName  = "";
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedDoubleClick = listView1.SelectedItems[0];
                fileSelectedName = selectedDoubleClick.SubItems[0].Text;
            }

            //global variable to store the path of slected archive
            string pathOfSelectedWithoutName = CURRENT_PATH;

            //selected path
            string pathOfSelected = Path.Combine(pathOfSelectedWithoutName, fileSelectedName);
            if (Path.GetExtension(pathOfSelected) == ".7z")
            {
                PATH_OF_ARCHIVE = pathOfSelected;
                using (SevenZipExtractor extractor = new SevenZipExtractor(pathOfSelected))
                {

                    putArchiveFilesToListView(extractor);


                    //determine im in archive mode = double click file it opens with decode

                    IS_IN_ARCHIVE_MODE = true;



                }
            
               
            }
            else
            {
                //if in archive mode
                if(IS_IN_ARCHIVE_MODE)
                {

                    //if its directory add to relative path of archive the parent directory
                    if(Directory.Exists(pathOfSelected))
                    {
                        RELATIVE_ROOT_ARCHIVE = Path.Combine(RELATIVE_ROOT_ARCHIVE, fileSelectedName);
                        directoryDoubleClick(pathOfSelected);
                    }
                    //else its a file and we cna open it with the relative path
                    else
                    {
                        RELATIVE_ROOT_ARCHIVE = Path.Combine(RELATIVE_ROOT_ARCHIVE, fileSelectedName);
                        ArchiveCreator.decompressFileAndOpen(RELATIVE_ROOT_ARCHIVE, PATH_OF_ARCHIVE);

                        //after we open a file we are back to the relative root archive
                        RELATIVE_ROOT_ARCHIVE = Path.GetDirectoryName(RELATIVE_ROOT_ARCHIVE);
                    }
                    
                   
                    
                }
                else
                {
                    if (Directory.Exists(pathOfSelected))
                    {

                        directoryDoubleClick(pathOfSelected);

                    }
                    else
                    {
                        Process process = new Process();
                        process.StartInfo.FileName = pathOfSelected;
                        try
                        {
                            process.Start();
                        }
                        catch (Exception ex)
                        {
                            //Error
                        }
                    }
                   
                    
                }
                //runs the selected process
               
            }

        }
        private void directoryDoubleClick(string path)
        {
            CURRENT_PATH = path;
            txtCurrentPath.Text = CURRENT_PATH;
            refreshListView(CURRENT_PATH);
        }
       
        private void putArchiveFilesToListView(SevenZipExtractor extractor)
        {
            listView1.Items.Clear(); //delete all the items from listview
            for (int i = 0; i < extractor.ArchiveFileData.Count; i++)
            {
               
                if(extractor.ArchiveFileData[i].IsDirectory)
                {
                    if(!isChildPath(extractor.ArchiveFileNames, extractor.ArchiveFileData[i].FileName))
                        editItemsListView(extractor.ArchiveFileData[i].FileName, extractor.ArchiveFileData[i].LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.Folder);
                }
                else
                {
                    if(Path.GetDirectoryName(extractor.ArchiveFileData[i].FileName) == "")
                        editItemsListView(extractor.ArchiveFileData[i].FileName, extractor.ArchiveFileData[i].LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.File);
                }

                



      


            }


}

      

        private bool isChildPath(ReadOnlyCollection<string> paths, string target)
        {
            bool isChild = false;

            string parentDirectory = Path.GetDirectoryName(target);

            foreach (string otherPath in paths)
            {
                if (otherPath != target && parentDirectory == otherPath)
                {
                    return true;
                }
            }

            return false;
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



        //deconstructor
        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Directory.Exists(TEMP_FOLDER_OF_PROJECT))
                Directory.Delete(TEMP_FOLDER_OF_PROJECT);
            if (Directory.Exists(VIEW_FOLDER_OF_PROJECT))
                Directory.Delete(VIEW_FOLDER_OF_PROJECT);

        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            //archive after selected
            //PATH_OF_ARCHIVE = STARTERPOSITION + treeView1.SelectedNode.FullPath + listView1.Sel.SubItems[0].Text;
            List<string> paths_to_extract = new List<string>();
            if(IS_IN_ARCHIVE_MODE)
            {
                paths_to_extract.Add(PATH_OF_ARCHIVE);
                extractGui extractWindows = new extractGui(paths_to_extract, Path.Combine(CURRENT_PATH, Path.GetFileNameWithoutExtension(CURRENT_PATH)));

                extractWindows.Show();
            }
            else
            {
                if(listView1.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    {
                        if (Path.GetExtension(listView1.SelectedItems[i].Text) == ".7z")
                        {
                            PATH_OF_ARCHIVE = Path.Combine(CURRENT_PATH, listView1.SelectedItems[i].SubItems[0].Text);
                            paths_to_extract.Add(PATH_OF_ARCHIVE);
                        }
                    }

                   
                    extractGui extractWindows = new extractGui(paths_to_extract, Path.Combine(CURRENT_PATH, Path.GetFileNameWithoutExtension(CURRENT_PATH)));

                    extractWindows.Show();

                }




            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(IS_IN_ARCHIVE_MODE)
            {
                //if no relative directory
                if (RELATIVE_ROOT_ARCHIVE == "")
                {
                    IS_IN_ARCHIVE_MODE = false;
                    refreshListView(CURRENT_PATH);


                }
                else
                {
                    RELATIVE_ROOT_ARCHIVE = Path.GetDirectoryName(RELATIVE_ROOT_ARCHIVE);
                    CURRENT_PATH = Path.GetDirectoryName(CURRENT_PATH);
                    txtCurrentPath.Text = CURRENT_PATH;
                    if(RELATIVE_ROOT_ARCHIVE == "")
                    {
                        //after i go back from a dictionary in zip i need to display the zip again
                        using (SevenZipExtractor extractor = new SevenZipExtractor(PATH_OF_ARCHIVE))
                        {

                            putArchiveFilesToListView(extractor);


                        }
                    }
                    else
                    {
                        refreshListView(CURRENT_PATH);
                    }
                    
                }
                
                return;
            }
               
            

            DirectoryInfo parentDirectory = Directory.GetParent(CURRENT_PATH);
            //check if we reached the start position if not
            if(!parentDirectory.FullName.Equals(STARTERPOSITION.Substring(0, STARTERPOSITION.Length-1)))
            {
                CURRENT_PATH = parentDirectory.FullName;
                txtCurrentPath.Text = CURRENT_PATH;
                refreshListView(CURRENT_PATH);
            }
               
            
        }

        private void refreshListView(string path)
        {
            CURRENT_PATH = txtCurrentPath.Text;
            listView1.Items.Clear();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileSystemInfo[] fileSystemInfos = directory.GetFileSystemInfos();
            foreach (FileSystemInfo fileSystemInfo in fileSystemInfos)
            {
                if (Path.GetExtension(fileSystemInfo.Name) == ".7z")
                    editItemsListView(fileSystemInfo.Name, fileSystemInfo.LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.Zip);
                else if (Directory.Exists(fileSystemInfo.FullName))
                    editItemsListView(fileSystemInfo.Name, fileSystemInfo.LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.Folder);
                else
                    editItemsListView(fileSystemInfo.Name, fileSystemInfo.LastAccessTime.ToString(), (int)fileTypeMap.TypeMap.File);

            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            refreshListView(CURRENT_PATH);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            List<string> paths_to_delete = getPathsToArchiveFromGui();
            foreach(string path in paths_to_delete)
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }
                else
                {

                    File.Delete(path);
                  

                }
                refreshListView(CURRENT_PATH);
            }
            
        }
    }
    
        
}
