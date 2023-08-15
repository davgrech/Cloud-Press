namespace CloudPressGui
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveArchiveCopyAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesToArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToASpecifiedFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInfromationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTopicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ad = new System.Windows.Forms.Button();
            this.extractButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Filee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtCurrentPath = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.commandsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(809, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openArchiveToolStripMenuItem,
            this.saveArchiveCopyAsToolStripMenuItem,
            this.changeDriveToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openArchiveToolStripMenuItem
            // 
            this.openArchiveToolStripMenuItem.Name = "openArchiveToolStripMenuItem";
            this.openArchiveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openArchiveToolStripMenuItem.Text = "Open archive";
            // 
            // saveArchiveCopyAsToolStripMenuItem
            // 
            this.saveArchiveCopyAsToolStripMenuItem.Name = "saveArchiveCopyAsToolStripMenuItem";
            this.saveArchiveCopyAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveArchiveCopyAsToolStripMenuItem.Text = "Save archive copy as";
            // 
            // changeDriveToolStripMenuItem
            // 
            this.changeDriveToolStripMenuItem.Name = "changeDriveToolStripMenuItem";
            this.changeDriveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.changeDriveToolStripMenuItem.Text = "Set  defualt password";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilesToArchiveToolStripMenuItem,
            this.extractToASpecifiedFolderToolStripMenuItem,
            this.lookArchiveToolStripMenuItem});
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.commandsToolStripMenuItem.Text = "Commands";
            // 
            // addFilesToArchiveToolStripMenuItem
            // 
            this.addFilesToArchiveToolStripMenuItem.Name = "addFilesToArchiveToolStripMenuItem";
            this.addFilesToArchiveToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.addFilesToArchiveToolStripMenuItem.Text = "Add files to archive";
            // 
            // extractToASpecifiedFolderToolStripMenuItem
            // 
            this.extractToASpecifiedFolderToolStripMenuItem.Name = "extractToASpecifiedFolderToolStripMenuItem";
            this.extractToASpecifiedFolderToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.extractToASpecifiedFolderToolStripMenuItem.Text = "Extract to a specified folder";
            // 
            // lookArchiveToolStripMenuItem
            // 
            this.lookArchiveToolStripMenuItem.Name = "lookArchiveToolStripMenuItem";
            this.lookArchiveToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.lookArchiveToolStripMenuItem.Text = "Look archive";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findFilesToolStripMenuItem,
            this.showInfromationToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // findFilesToolStripMenuItem
            // 
            this.findFilesToolStripMenuItem.Name = "findFilesToolStripMenuItem";
            this.findFilesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.findFilesToolStripMenuItem.Text = "Find files";
            // 
            // showInfromationToolStripMenuItem
            // 
            this.showInfromationToolStripMenuItem.Name = "showInfromationToolStripMenuItem";
            this.showInfromationToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.showInfromationToolStripMenuItem.Text = "Show infromation";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpTopicsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpTopicsToolStripMenuItem
            // 
            this.helpTopicsToolStripMenuItem.Name = "helpTopicsToolStripMenuItem";
            this.helpTopicsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.helpTopicsToolStripMenuItem.Text = "Help topics";
            // 
            // ad
            // 
            this.ad.Location = new System.Drawing.Point(13, 27);
            this.ad.Name = "ad";
            this.ad.Size = new System.Drawing.Size(60, 65);
            this.ad.TabIndex = 1;
            this.ad.Text = "Add";
            this.ad.UseVisualStyleBackColor = true;
            this.ad.Click += new System.EventHandler(this.ad_Click);
            // 
            // extractButton
            // 
            this.extractButton.Location = new System.Drawing.Point(79, 27);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(60, 65);
            this.extractButton.TabIndex = 2;
            this.extractButton.Text = "Extract";
            this.extractButton.UseVisualStyleBackColor = true;
            this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(145, 27);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(60, 65);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Delete";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(211, 27);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 65);
            this.button4.TabIndex = 4;
            this.button4.Text = "Find";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(277, 27);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 65);
            this.button5.TabIndex = 5;
            this.button5.Text = "Info";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(343, 27);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(60, 65);
            this.button6.TabIndex = 6;
            this.button6.Text = "PRESS";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 145);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(809, 337);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 10;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filee,
            this.Type,
            this.LastModified});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(645, 337);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "Text.png");
            this.imageList1.Images.SetKeyName(2, "Zip.jpg");
            // 
            // txtCurrentPath
            // 
            this.txtCurrentPath.Location = new System.Drawing.Point(66, 120);
            this.txtCurrentPath.Name = "txtCurrentPath";
            this.txtCurrentPath.Size = new System.Drawing.Size(731, 20);
            this.txtCurrentPath.TabIndex = 11;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(0, 120);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(60, 23);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 516);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtCurrentPath);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.extractButton);
            this.Controls.Add(this.ad);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.Button ad;
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ToolStripMenuItem openArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveArchiveCopyAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesToArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToASpecifiedFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInfromationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpTopicsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Filee;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ColumnHeader LastModified;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtCurrentPath;
        private System.Windows.Forms.Button btnBack;
    }
}