namespace WoWAddonsCleaner
{
    partial class mainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Global", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Character", System.Windows.Forms.HorizontalAlignment.Left);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtWoWFolder = new System.Windows.Forms.TextBox();
            this.btnSearchWoW = new System.Windows.Forms.Button();
            this.btnScanAddons = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.labelHelpPatch = new System.Windows.Forms.Label();
            this.btnPatchVersion = new System.Windows.Forms.Button();
            this.listAllAddons = new System.Windows.Forms.ListView();
            this.addonsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addonsVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnBackupAddons = new System.Windows.Forms.Button();
            this.listOrphanAddons = new System.Windows.Forms.ListView();
            this.Addon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteOrphanAddons = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteCharacter = new System.Windows.Forms.Button();
            this.treeCharacters = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDeleteBakFile = new System.Windows.Forms.Button();
            this.listWTF = new System.Windows.Forms.ListView();
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnBackupWTF = new System.Windows.Forms.Button();
            this.btnDeleteOrphanWTF = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnSortAllAddonsTxt = new System.Windows.Forms.Button();
            this.chkSortAddonsTxt = new System.Windows.Forms.CheckBox();
            this.listMissingAddons = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteMissingAddons = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // txtWoWFolder
            // 
            this.txtWoWFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWoWFolder.Location = new System.Drawing.Point(12, 12);
            this.txtWoWFolder.Name = "txtWoWFolder";
            this.txtWoWFolder.Size = new System.Drawing.Size(737, 20);
            this.txtWoWFolder.TabIndex = 0;
            this.txtWoWFolder.TextChanged += new System.EventHandler(this.txtWoWFolder_TextChanged);
            // 
            // btnSearchWoW
            // 
            this.btnSearchWoW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchWoW.Location = new System.Drawing.Point(755, 12);
            this.btnSearchWoW.Name = "btnSearchWoW";
            this.btnSearchWoW.Size = new System.Drawing.Size(128, 20);
            this.btnSearchWoW.TabIndex = 1;
            this.btnSearchWoW.Text = "Choisir le dossier";
            this.btnSearchWoW.UseVisualStyleBackColor = true;
            this.btnSearchWoW.Click += new System.EventHandler(this.btnSearchWoW_Click);
            // 
            // btnScanAddons
            // 
            this.btnScanAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScanAddons.Enabled = false;
            this.btnScanAddons.Location = new System.Drawing.Point(755, 38);
            this.btnScanAddons.Name = "btnScanAddons";
            this.btnScanAddons.Size = new System.Drawing.Size(128, 23);
            this.btnScanAddons.TabIndex = 2;
            this.btnScanAddons.Text = "Scan des addons";
            this.btnScanAddons.UseVisualStyleBackColor = true;
            this.btnScanAddons.Click += new System.EventHandler(this.btnScanAddons_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(13, 68);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(870, 437);
            this.tabControl.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.labelHelpPatch);
            this.tabPage4.Controls.Add(this.btnPatchVersion);
            this.tabPage4.Controls.Add(this.listAllAddons);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(862, 411);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tout les addons";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // labelHelpPatch
            // 
            this.labelHelpPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelpPatch.AutoEllipsis = true;
            this.labelHelpPatch.Location = new System.Drawing.Point(730, 37);
            this.labelHelpPatch.Name = "labelHelpPatch";
            this.labelHelpPatch.Size = new System.Drawing.Size(126, 101);
            this.labelHelpPatch.TabIndex = 3;
            this.labelHelpPatch.Text = "Patch les addons sélectionné pour leur indiquer être fait pour la dernière versio" +
    "n";
            // 
            // btnPatchVersion
            // 
            this.btnPatchVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatchVersion.Location = new System.Drawing.Point(729, 7);
            this.btnPatchVersion.Name = "btnPatchVersion";
            this.btnPatchVersion.Size = new System.Drawing.Size(127, 23);
            this.btnPatchVersion.TabIndex = 2;
            this.btnPatchVersion.Text = "Patcher";
            this.btnPatchVersion.UseVisualStyleBackColor = true;
            this.btnPatchVersion.Click += new System.EventHandler(this.btnPatchVersion_Click);
            // 
            // listAllAddons
            // 
            this.listAllAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listAllAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.addonsName,
            this.addonsVersion});
            this.listAllAddons.HideSelection = false;
            this.listAllAddons.Location = new System.Drawing.Point(7, 7);
            this.listAllAddons.Name = "listAllAddons";
            this.listAllAddons.Size = new System.Drawing.Size(716, 401);
            this.listAllAddons.TabIndex = 0;
            this.listAllAddons.UseCompatibleStateImageBehavior = false;
            this.listAllAddons.View = System.Windows.Forms.View.Details;
            this.listAllAddons.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listAllAddons_ColumnClick);
            // 
            // addonsName
            // 
            this.addonsName.Text = "Addons";
            // 
            // addonsVersion
            // 
            this.addonsVersion.Text = "Version";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnBackupAddons);
            this.tabPage1.Controls.Add(this.listOrphanAddons);
            this.tabPage1.Controls.Add(this.btnDeleteOrphanAddons);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(533, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Addons orphelins";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnBackupAddons
            // 
            this.btnBackupAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupAddons.Location = new System.Drawing.Point(400, 36);
            this.btnBackupAddons.Name = "btnBackupAddons";
            this.btnBackupAddons.Size = new System.Drawing.Size(127, 23);
            this.btnBackupAddons.TabIndex = 6;
            this.btnBackupAddons.Text = "Backup des addons";
            this.btnBackupAddons.UseVisualStyleBackColor = true;
            this.btnBackupAddons.Visible = false;
            this.btnBackupAddons.Click += new System.EventHandler(this.btnBackupAddons_Click);
            // 
            // listOrphanAddons
            // 
            this.listOrphanAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listOrphanAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Addon});
            this.listOrphanAddons.HideSelection = false;
            this.listOrphanAddons.Location = new System.Drawing.Point(7, 7);
            this.listOrphanAddons.Name = "listOrphanAddons";
            this.listOrphanAddons.Size = new System.Drawing.Size(387, 304);
            this.listOrphanAddons.TabIndex = 5;
            this.listOrphanAddons.UseCompatibleStateImageBehavior = false;
            this.listOrphanAddons.View = System.Windows.Forms.View.Details;
            // 
            // Addon
            // 
            this.Addon.Text = "Addons";
            this.Addon.Width = 268;
            // 
            // btnDeleteOrphanAddons
            // 
            this.btnDeleteOrphanAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteOrphanAddons.Location = new System.Drawing.Point(400, 7);
            this.btnDeleteOrphanAddons.Name = "btnDeleteOrphanAddons";
            this.btnDeleteOrphanAddons.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteOrphanAddons.TabIndex = 4;
            this.btnDeleteOrphanAddons.Text = "Supprimer";
            this.btnDeleteOrphanAddons.UseVisualStyleBackColor = true;
            this.btnDeleteOrphanAddons.Click += new System.EventHandler(this.btnDeleteOrphanAddons_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteCharacter);
            this.tabPage2.Controls.Add(this.treeCharacters);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(533, 314);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Personnages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCharacter
            // 
            this.btnDeleteCharacter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteCharacter.Enabled = false;
            this.btnDeleteCharacter.Location = new System.Drawing.Point(400, 6);
            this.btnDeleteCharacter.Name = "btnDeleteCharacter";
            this.btnDeleteCharacter.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteCharacter.TabIndex = 1;
            this.btnDeleteCharacter.Text = "Supprimer";
            this.btnDeleteCharacter.UseVisualStyleBackColor = true;
            this.btnDeleteCharacter.Click += new System.EventHandler(this.btnDeleteCharacter_Click);
            // 
            // treeCharacters
            // 
            this.treeCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCharacters.FullRowSelect = true;
            this.treeCharacters.HideSelection = false;
            this.treeCharacters.Location = new System.Drawing.Point(7, 7);
            this.treeCharacters.Name = "treeCharacters";
            this.treeCharacters.Size = new System.Drawing.Size(387, 305);
            this.treeCharacters.TabIndex = 0;
            this.treeCharacters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCharacter_AfterSelect);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnDeleteBakFile);
            this.tabPage3.Controls.Add(this.listWTF);
            this.tabPage3.Controls.Add(this.btnBackupWTF);
            this.tabPage3.Controls.Add(this.btnDeleteOrphanWTF);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(533, 314);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Fichiers de config orphelins";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDeleteBakFile
            // 
            this.btnDeleteBakFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteBakFile.Location = new System.Drawing.Point(400, 64);
            this.btnDeleteBakFile.Name = "btnDeleteBakFile";
            this.btnDeleteBakFile.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteBakFile.TabIndex = 5;
            this.btnDeleteBakFile.Text = "Suppression des *.bak";
            this.btnDeleteBakFile.UseVisualStyleBackColor = true;
            this.btnDeleteBakFile.Click += new System.EventHandler(this.btnDeleteBakFile_Click);
            // 
            // listWTF
            // 
            this.listWTF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listWTF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.file,
            this.typeFile});
            listViewGroup3.Header = "Global";
            listViewGroup3.Name = "listViewGroupGlobal";
            listViewGroup4.Header = "Character";
            listViewGroup4.Name = "listViewGroupCharacter";
            this.listWTF.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this.listWTF.HideSelection = false;
            this.listWTF.Location = new System.Drawing.Point(7, 7);
            this.listWTF.Name = "listWTF";
            this.listWTF.Size = new System.Drawing.Size(387, 301);
            this.listWTF.TabIndex = 4;
            this.listWTF.UseCompatibleStateImageBehavior = false;
            this.listWTF.View = System.Windows.Forms.View.Details;
            this.listWTF.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listWTF_ColumnClick);
            // 
            // file
            // 
            this.file.Text = "Fichier";
            // 
            // typeFile
            // 
            this.typeFile.Text = "Type";
            // 
            // btnBackupWTF
            // 
            this.btnBackupWTF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupWTF.Enabled = false;
            this.btnBackupWTF.Location = new System.Drawing.Point(400, 35);
            this.btnBackupWTF.Name = "btnBackupWTF";
            this.btnBackupWTF.Size = new System.Drawing.Size(127, 23);
            this.btnBackupWTF.TabIndex = 3;
            this.btnBackupWTF.Text = "Backup de WTF";
            this.btnBackupWTF.UseVisualStyleBackColor = true;
            this.btnBackupWTF.Click += new System.EventHandler(this.btnBackupWTF_Click);
            // 
            // btnDeleteOrphanWTF
            // 
            this.btnDeleteOrphanWTF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteOrphanWTF.Location = new System.Drawing.Point(400, 6);
            this.btnDeleteOrphanWTF.Name = "btnDeleteOrphanWTF";
            this.btnDeleteOrphanWTF.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteOrphanWTF.TabIndex = 1;
            this.btnDeleteOrphanWTF.Text = "Supprimer";
            this.btnDeleteOrphanWTF.UseVisualStyleBackColor = true;
            this.btnDeleteOrphanWTF.Click += new System.EventHandler(this.btnDeleteOrphanWTF_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnSortAllAddonsTxt);
            this.tabPage5.Controls.Add(this.chkSortAddonsTxt);
            this.tabPage5.Controls.Add(this.listMissingAddons);
            this.tabPage5.Controls.Add(this.btnDeleteMissingAddons);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(862, 411);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Addons manquant";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnSortAllAddonsTxt
            // 
            this.btnSortAllAddonsTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSortAllAddonsTxt.Location = new System.Drawing.Point(728, 59);
            this.btnSortAllAddonsTxt.Name = "btnSortAllAddonsTxt";
            this.btnSortAllAddonsTxt.Size = new System.Drawing.Size(127, 23);
            this.btnSortAllAddonsTxt.TabIndex = 8;
            this.btnSortAllAddonsTxt.Text = "Trier tout les fichiers";
            this.btnSortAllAddonsTxt.UseVisualStyleBackColor = true;
            this.btnSortAllAddonsTxt.Click += new System.EventHandler(this.btnSortAllAddonsTxt_Click);
            // 
            // chkSortAddonsTxt
            // 
            this.chkSortAddonsTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSortAddonsTxt.AutoSize = true;
            this.chkSortAddonsTxt.Location = new System.Drawing.Point(731, 36);
            this.chkSortAddonsTxt.Name = "chkSortAddonsTxt";
            this.chkSortAddonsTxt.Size = new System.Drawing.Size(101, 17);
            this.chkSortAddonsTxt.TabIndex = 7;
            this.chkSortAddonsTxt.Text = "Trier les addons";
            this.chkSortAddonsTxt.UseVisualStyleBackColor = true;
            // 
            // listMissingAddons
            // 
            this.listMissingAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMissingAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listMissingAddons.HideSelection = false;
            this.listMissingAddons.Location = new System.Drawing.Point(6, 6);
            this.listMissingAddons.Name = "listMissingAddons";
            this.listMissingAddons.Size = new System.Drawing.Size(716, 398);
            this.listMissingAddons.TabIndex = 6;
            this.listMissingAddons.UseCompatibleStateImageBehavior = false;
            this.listMissingAddons.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Addons";
            // 
            // btnDeleteMissingAddons
            // 
            this.btnDeleteMissingAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteMissingAddons.Location = new System.Drawing.Point(728, 6);
            this.btnDeleteMissingAddons.Name = "btnDeleteMissingAddons";
            this.btnDeleteMissingAddons.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteMissingAddons.TabIndex = 5;
            this.btnDeleteMissingAddons.Text = "Supprimer";
            this.btnDeleteMissingAddons.UseVisualStyleBackColor = true;
            this.btnDeleteMissingAddons.Click += new System.EventHandler(this.btnDeleteMissingAddons_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 514);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(895, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Step = 1;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 536);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnScanAddons);
            this.Controls.Add(this.btnSearchWoW);
            this.Controls.Add(this.txtWoWFolder);
            this.MinimumSize = new System.Drawing.Size(491, 462);
            this.Name = "mainForm";
            this.Text = "WoW Addons Cleaner";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tabControl.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox txtWoWFolder;
        private System.Windows.Forms.Button btnSearchWoW;
        private System.Windows.Forms.Button btnScanAddons;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeCharacters;
        private System.Windows.Forms.Button btnDeleteCharacter;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnDeleteOrphanAddons;
        private System.Windows.Forms.ListView listOrphanAddons;
        private System.Windows.Forms.ColumnHeader Addon;
        private System.Windows.Forms.Button btnBackupAddons;
        private System.Windows.Forms.Button btnBackupWTF;
        private System.Windows.Forms.Button btnDeleteOrphanWTF;
        private System.Windows.Forms.ListView listWTF;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ColumnHeader typeFile;
        private System.Windows.Forms.Button btnDeleteBakFile;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listAllAddons;
        private System.Windows.Forms.ColumnHeader addonsName;
        private System.Windows.Forms.ColumnHeader addonsVersion;
        private System.Windows.Forms.Button btnPatchVersion;
        private System.Windows.Forms.Label labelHelpPatch;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnDeleteMissingAddons;
        private System.Windows.Forms.ListView listMissingAddons;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox chkSortAddonsTxt;
        private System.Windows.Forms.Button btnSortAllAddonsTxt;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
    }
}

