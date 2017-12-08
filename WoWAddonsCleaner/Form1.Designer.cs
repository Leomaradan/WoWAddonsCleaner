namespace WoWAddonsCleaner
{
    partial class Form1
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtWoWFolder = new System.Windows.Forms.TextBox();
            this.btnSearchWoW = new System.Windows.Forms.Button();
            this.btnScanAddons = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteCharacter = new System.Windows.Forms.Button();
            this.treeCharacters = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnDeleteOrphanAddons = new System.Windows.Forms.Button();
            this.listAddons = new System.Windows.Forms.ListView();
            this.Addon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnBackupAddons = new System.Windows.Forms.Button();
            this.btnDeleteOrphanWTF = new System.Windows.Forms.Button();
            this.btnBackupWTF = new System.Windows.Forms.Button();
            this.listWTF = new System.Windows.Forms.ListView();
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // txtWoWFolder
            // 
            this.txtWoWFolder.Location = new System.Drawing.Point(12, 12);
            this.txtWoWFolder.Name = "txtWoWFolder";
            this.txtWoWFolder.Size = new System.Drawing.Size(317, 20);
            this.txtWoWFolder.TabIndex = 0;
            this.txtWoWFolder.TextChanged += new System.EventHandler(this.txtWoWFolder_TextChanged);
            // 
            // btnSearchWoW
            // 
            this.btnSearchWoW.Location = new System.Drawing.Point(335, 12);
            this.btnSearchWoW.Name = "btnSearchWoW";
            this.btnSearchWoW.Size = new System.Drawing.Size(128, 20);
            this.btnSearchWoW.TabIndex = 1;
            this.btnSearchWoW.Text = "Choisir le dossier";
            this.btnSearchWoW.UseVisualStyleBackColor = true;
            this.btnSearchWoW.Click += new System.EventHandler(this.btnSearchWoW_Click);
            // 
            // btnScanAddons
            // 
            this.btnScanAddons.Enabled = false;
            this.btnScanAddons.Location = new System.Drawing.Point(335, 38);
            this.btnScanAddons.Name = "btnScanAddons";
            this.btnScanAddons.Size = new System.Drawing.Size(128, 23);
            this.btnScanAddons.TabIndex = 2;
            this.btnScanAddons.Text = "Scan des addons";
            this.btnScanAddons.UseVisualStyleBackColor = true;
            this.btnScanAddons.Click += new System.EventHandler(this.btnScanAddons_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(13, 68);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(450, 343);
            this.tabControl.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnBackupAddons);
            this.tabPage1.Controls.Add(this.listAddons);
            this.tabPage1.Controls.Add(this.btnDeleteOrphanAddons);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(442, 317);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Addons orphelins";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteCharacter);
            this.tabPage2.Controls.Add(this.treeCharacters);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(442, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Personnages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCharacter
            // 
            this.btnDeleteCharacter.Enabled = false;
            this.btnDeleteCharacter.Location = new System.Drawing.Point(309, 7);
            this.btnDeleteCharacter.Name = "btnDeleteCharacter";
            this.btnDeleteCharacter.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteCharacter.TabIndex = 1;
            this.btnDeleteCharacter.Text = "Supprimer";
            this.btnDeleteCharacter.UseVisualStyleBackColor = true;
            this.btnDeleteCharacter.Click += new System.EventHandler(this.btnDeleteCharacter_Click);
            // 
            // treeCharacters
            // 
            this.treeCharacters.FullRowSelect = true;
            this.treeCharacters.HideSelection = false;
            this.treeCharacters.Location = new System.Drawing.Point(7, 7);
            this.treeCharacters.Name = "treeCharacters";
            this.treeCharacters.Size = new System.Drawing.Size(296, 305);
            this.treeCharacters.TabIndex = 0;
            this.treeCharacters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCharacter_AfterSelect);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listWTF);
            this.tabPage3.Controls.Add(this.btnBackupWTF);
            this.tabPage3.Controls.Add(this.btnDeleteOrphanWTF);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(442, 317);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Fichiers de config orphelins";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnDeleteOrphanAddons
            // 
            this.btnDeleteOrphanAddons.Location = new System.Drawing.Point(309, 7);
            this.btnDeleteOrphanAddons.Name = "btnDeleteOrphanAddons";
            this.btnDeleteOrphanAddons.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteOrphanAddons.TabIndex = 4;
            this.btnDeleteOrphanAddons.Text = "Supprimer";
            this.btnDeleteOrphanAddons.UseVisualStyleBackColor = true;
            this.btnDeleteOrphanAddons.Click += new System.EventHandler(this.btnDeleteOrphanAddons_Click);
            // 
            // listAddons
            // 
            this.listAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Addon});
            this.listAddons.Location = new System.Drawing.Point(7, 7);
            this.listAddons.Name = "listAddons";
            this.listAddons.Size = new System.Drawing.Size(296, 304);
            this.listAddons.TabIndex = 5;
            this.listAddons.UseCompatibleStateImageBehavior = false;
            this.listAddons.View = System.Windows.Forms.View.Details;
            // 
            // Addon
            // 
            this.Addon.Text = "Addons";
            this.Addon.Width = 268;
            // 
            // btnBackupAddons
            // 
            this.btnBackupAddons.Location = new System.Drawing.Point(309, 36);
            this.btnBackupAddons.Name = "btnBackupAddons";
            this.btnBackupAddons.Size = new System.Drawing.Size(127, 23);
            this.btnBackupAddons.TabIndex = 6;
            this.btnBackupAddons.Text = "Backup des addons";
            this.btnBackupAddons.UseVisualStyleBackColor = true;
            this.btnBackupAddons.Visible = false;
            this.btnBackupAddons.Click += new System.EventHandler(this.btnBackupAddons_Click);
            // 
            // btnDeleteOrphanWTF
            // 
            this.btnDeleteOrphanWTF.Location = new System.Drawing.Point(309, 7);
            this.btnDeleteOrphanWTF.Name = "btnDeleteOrphanWTF";
            this.btnDeleteOrphanWTF.Size = new System.Drawing.Size(127, 23);
            this.btnDeleteOrphanWTF.TabIndex = 1;
            this.btnDeleteOrphanWTF.Text = "Supprimer";
            this.btnDeleteOrphanWTF.UseVisualStyleBackColor = true;
            this.btnDeleteOrphanWTF.Click += new System.EventHandler(this.btnDeleteOrphanWTF_Click);
            // 
            // btnBackupWTF
            // 
            this.btnBackupWTF.Location = new System.Drawing.Point(309, 36);
            this.btnBackupWTF.Name = "btnBackupWTF";
            this.btnBackupWTF.Size = new System.Drawing.Size(127, 23);
            this.btnBackupWTF.TabIndex = 3;
            this.btnBackupWTF.Text = "Backup de WTF";
            this.btnBackupWTF.UseVisualStyleBackColor = true;
            this.btnBackupWTF.Visible = false;
            this.btnBackupWTF.Click += new System.EventHandler(this.btnBackupWTF_Click);
            // 
            // listWTF
            // 
            this.listWTF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.file,
            this.typeFile});
            this.listWTF.Location = new System.Drawing.Point(7, 7);
            this.listWTF.Name = "listWTF";
            this.listWTF.Size = new System.Drawing.Size(296, 304);
            this.listWTF.TabIndex = 4;
            this.listWTF.UseCompatibleStateImageBehavior = false;
            this.listWTF.View = System.Windows.Forms.View.Details;
            // 
            // file
            // 
            this.file.Text = "Fichier";
            // 
            // typeFile
            // 
            this.typeFile.Text = "Type";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 423);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnScanAddons);
            this.Controls.Add(this.btnSearchWoW);
            this.Controls.Add(this.txtWoWFolder);
            this.Name = "Form1";
            this.Text = "WoW Addons Cleaner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
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
        private System.Windows.Forms.ListView listAddons;
        private System.Windows.Forms.ColumnHeader Addon;
        private System.Windows.Forms.Button btnBackupAddons;
        private System.Windows.Forms.Button btnBackupWTF;
        private System.Windows.Forms.Button btnDeleteOrphanWTF;
        private System.Windows.Forms.ListView listWTF;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ColumnHeader typeFile;
    }
}

