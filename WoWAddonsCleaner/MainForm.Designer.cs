namespace WoWAddonsCleaner
{
    partial class frmMainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtWoWFolder = new System.Windows.Forms.TextBox();
            this.btnSearchWoW = new System.Windows.Forms.Button();
            this.btnScanAddons = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAutoClean = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.labelHelpPatch = new System.Windows.Forms.Label();
            this.btnPatchVersion = new System.Windows.Forms.Button();
            this.listAllAddons = new System.Windows.Forms.ListView();
            this.allAddonsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.allAddonsVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.allAddonsAutoClean = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctmAllAddons = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPatch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddToDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveToDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listOrphanAddons = new System.Windows.Forms.ListView();
            this.Addon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteOrphanAddons = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteCharacter = new System.Windows.Forms.Button();
            this.treeCharacters = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnOrderWTF = new System.Windows.Forms.Button();
            this.btnDeleteBakFile = new System.Windows.Forms.Button();
            this.listWTF = new System.Windows.Forms.ListView();
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteOrphanWTF = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnShowExceptions = new System.Windows.Forms.Button();
            this.btnSortAllAddonsTxt = new System.Windows.Forms.Button();
            this.chkSortAddonsTxt = new System.Windows.Forms.CheckBox();
            this.listMissingAddons = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctmAddonReference = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiExclude = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteMissingAddons = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControl.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.ctmAllAddons.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.ctmAddonReference.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // txtWoWFolder
            // 
            resources.ApplyResources(this.txtWoWFolder, "txtWoWFolder");
            this.txtWoWFolder.Name = "txtWoWFolder";
            this.txtWoWFolder.TextChanged += new System.EventHandler(this.txtWoWFolder_TextChanged);
            // 
            // btnSearchWoW
            // 
            resources.ApplyResources(this.btnSearchWoW, "btnSearchWoW");
            this.btnSearchWoW.Name = "btnSearchWoW";
            this.btnSearchWoW.UseVisualStyleBackColor = true;
            this.btnSearchWoW.Click += new System.EventHandler(this.btnSearchWoW_Click);
            // 
            // btnScanAddons
            // 
            resources.ApplyResources(this.btnScanAddons, "btnScanAddons");
            this.btnScanAddons.Name = "btnScanAddons";
            this.btnScanAddons.UseVisualStyleBackColor = true;
            this.btnScanAddons.Click += new System.EventHandler(this.btnScanAddons_Click);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnDelete);
            this.tabPage4.Controls.Add(this.btnAutoClean);
            this.tabPage4.Controls.Add(this.lblDescription);
            this.tabPage4.Controls.Add(this.lblAuthor);
            this.tabPage4.Controls.Add(this.lblVersion);
            this.tabPage4.Controls.Add(this.lblTitle);
            this.tabPage4.Controls.Add(this.labelHelpPatch);
            this.tabPage4.Controls.Add(this.btnPatchVersion);
            this.tabPage4.Controls.Add(this.listAllAddons);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAutoClean
            // 
            resources.ApplyResources(this.btnAutoClean, "btnAutoClean");
            this.btnAutoClean.Name = "btnAutoClean";
            this.btnAutoClean.UseVisualStyleBackColor = true;
            this.btnAutoClean.Click += new System.EventHandler(this.btnAutoClean_Click);
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.AutoEllipsis = true;
            this.lblDescription.Name = "lblDescription";
            // 
            // lblAuthor
            // 
            resources.ApplyResources(this.lblAuthor, "lblAuthor");
            this.lblAuthor.AutoEllipsis = true;
            this.lblAuthor.Name = "lblAuthor";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.AutoEllipsis = true;
            this.lblVersion.Name = "lblVersion";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Name = "lblTitle";
            // 
            // labelHelpPatch
            // 
            resources.ApplyResources(this.labelHelpPatch, "labelHelpPatch");
            this.labelHelpPatch.AutoEllipsis = true;
            this.labelHelpPatch.Name = "labelHelpPatch";
            // 
            // btnPatchVersion
            // 
            resources.ApplyResources(this.btnPatchVersion, "btnPatchVersion");
            this.btnPatchVersion.Name = "btnPatchVersion";
            this.btnPatchVersion.UseVisualStyleBackColor = true;
            this.btnPatchVersion.Click += new System.EventHandler(this.btnPatchVersion_Click);
            // 
            // listAllAddons
            // 
            resources.ApplyResources(this.listAllAddons, "listAllAddons");
            this.listAllAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.allAddonsName,
            this.allAddonsVersion,
            this.allAddonsAutoClean});
            this.listAllAddons.ContextMenuStrip = this.ctmAllAddons;
            this.listAllAddons.FullRowSelect = true;
            this.listAllAddons.HideSelection = false;
            this.listAllAddons.Name = "listAllAddons";
            this.listAllAddons.UseCompatibleStateImageBehavior = false;
            this.listAllAddons.View = System.Windows.Forms.View.Details;
            this.listAllAddons.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listAllAddons_ColumnClick);
            this.listAllAddons.SelectedIndexChanged += new System.EventHandler(this.listAllAddons_SelectedIndexChanged);
            // 
            // allAddonsName
            // 
            resources.ApplyResources(this.allAddonsName, "allAddonsName");
            // 
            // allAddonsVersion
            // 
            resources.ApplyResources(this.allAddonsVersion, "allAddonsVersion");
            // 
            // allAddonsAutoClean
            // 
            resources.ApplyResources(this.allAddonsAutoClean, "allAddonsAutoClean");
            // 
            // ctmAllAddons
            // 
            this.ctmAllAddons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPatch,
            this.tsmiAddToDelete,
            this.tsmiRemoveToDelete});
            this.ctmAllAddons.Name = "ctmAddonReference";
            resources.ApplyResources(this.ctmAllAddons, "ctmAllAddons");
            this.ctmAllAddons.Opening += new System.ComponentModel.CancelEventHandler(this.ctmAllAddons_Opening);
            // 
            // tsmiPatch
            // 
            this.tsmiPatch.Name = "tsmiPatch";
            resources.ApplyResources(this.tsmiPatch, "tsmiPatch");
            this.tsmiPatch.Click += new System.EventHandler(this.tsmiPatch_Click);
            // 
            // tsmiAddToDelete
            // 
            this.tsmiAddToDelete.Name = "tsmiAddToDelete";
            resources.ApplyResources(this.tsmiAddToDelete, "tsmiAddToDelete");
            this.tsmiAddToDelete.Click += new System.EventHandler(this.tsmiAddToDelete_Click);
            // 
            // tsmiRemoveToDelete
            // 
            this.tsmiRemoveToDelete.Name = "tsmiRemoveToDelete";
            resources.ApplyResources(this.tsmiRemoveToDelete, "tsmiRemoveToDelete");
            this.tsmiRemoveToDelete.Click += new System.EventHandler(this.tsmiRemoveToDelete_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listOrphanAddons);
            this.tabPage1.Controls.Add(this.btnDeleteOrphanAddons);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listOrphanAddons
            // 
            resources.ApplyResources(this.listOrphanAddons, "listOrphanAddons");
            this.listOrphanAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Addon});
            this.listOrphanAddons.FullRowSelect = true;
            this.listOrphanAddons.HideSelection = false;
            this.listOrphanAddons.Name = "listOrphanAddons";
            this.listOrphanAddons.UseCompatibleStateImageBehavior = false;
            this.listOrphanAddons.View = System.Windows.Forms.View.Details;
            // 
            // Addon
            // 
            resources.ApplyResources(this.Addon, "Addon");
            // 
            // btnDeleteOrphanAddons
            // 
            resources.ApplyResources(this.btnDeleteOrphanAddons, "btnDeleteOrphanAddons");
            this.btnDeleteOrphanAddons.Name = "btnDeleteOrphanAddons";
            this.btnDeleteOrphanAddons.UseVisualStyleBackColor = true;
            this.btnDeleteOrphanAddons.Click += new System.EventHandler(this.btnDeleteOrphanAddons_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteCharacter);
            this.tabPage2.Controls.Add(this.treeCharacters);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCharacter
            // 
            resources.ApplyResources(this.btnDeleteCharacter, "btnDeleteCharacter");
            this.btnDeleteCharacter.Name = "btnDeleteCharacter";
            this.btnDeleteCharacter.UseVisualStyleBackColor = true;
            this.btnDeleteCharacter.Click += new System.EventHandler(this.btnDeleteCharacter_Click);
            // 
            // treeCharacters
            // 
            resources.ApplyResources(this.treeCharacters, "treeCharacters");
            this.treeCharacters.FullRowSelect = true;
            this.treeCharacters.HideSelection = false;
            this.treeCharacters.Name = "treeCharacters";
            this.treeCharacters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCharacter_AfterSelect);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnOrderWTF);
            this.tabPage3.Controls.Add(this.btnDeleteBakFile);
            this.tabPage3.Controls.Add(this.listWTF);
            this.tabPage3.Controls.Add(this.btnDeleteOrphanWTF);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnOrderWTF
            // 
            resources.ApplyResources(this.btnOrderWTF, "btnOrderWTF");
            this.btnOrderWTF.Name = "btnOrderWTF";
            this.btnOrderWTF.UseVisualStyleBackColor = true;
            // 
            // btnDeleteBakFile
            // 
            resources.ApplyResources(this.btnDeleteBakFile, "btnDeleteBakFile");
            this.btnDeleteBakFile.Name = "btnDeleteBakFile";
            this.btnDeleteBakFile.UseVisualStyleBackColor = true;
            this.btnDeleteBakFile.Click += new System.EventHandler(this.btnDeleteBakFile_Click);
            // 
            // listWTF
            // 
            resources.ApplyResources(this.listWTF, "listWTF");
            this.listWTF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.file,
            this.typeFile});
            this.listWTF.FullRowSelect = true;
            this.listWTF.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listWTF.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listWTF.Groups1")))});
            this.listWTF.HideSelection = false;
            this.listWTF.Name = "listWTF";
            this.listWTF.UseCompatibleStateImageBehavior = false;
            this.listWTF.View = System.Windows.Forms.View.Details;
            // 
            // file
            // 
            resources.ApplyResources(this.file, "file");
            // 
            // typeFile
            // 
            resources.ApplyResources(this.typeFile, "typeFile");
            // 
            // btnDeleteOrphanWTF
            // 
            resources.ApplyResources(this.btnDeleteOrphanWTF, "btnDeleteOrphanWTF");
            this.btnDeleteOrphanWTF.Name = "btnDeleteOrphanWTF";
            this.btnDeleteOrphanWTF.UseVisualStyleBackColor = true;
            this.btnDeleteOrphanWTF.Click += new System.EventHandler(this.btnDeleteOrphanWTF_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnShowExceptions);
            this.tabPage5.Controls.Add(this.btnSortAllAddonsTxt);
            this.tabPage5.Controls.Add(this.chkSortAddonsTxt);
            this.tabPage5.Controls.Add(this.listMissingAddons);
            this.tabPage5.Controls.Add(this.btnDeleteMissingAddons);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnShowExceptions
            // 
            resources.ApplyResources(this.btnShowExceptions, "btnShowExceptions");
            this.btnShowExceptions.Name = "btnShowExceptions";
            this.btnShowExceptions.UseVisualStyleBackColor = true;
            this.btnShowExceptions.Click += new System.EventHandler(this.btnShowExceptions_Click);
            // 
            // btnSortAllAddonsTxt
            // 
            resources.ApplyResources(this.btnSortAllAddonsTxt, "btnSortAllAddonsTxt");
            this.btnSortAllAddonsTxt.Name = "btnSortAllAddonsTxt";
            this.btnSortAllAddonsTxt.UseVisualStyleBackColor = true;
            this.btnSortAllAddonsTxt.Click += new System.EventHandler(this.btnSortAllAddonsTxt_Click);
            // 
            // chkSortAddonsTxt
            // 
            resources.ApplyResources(this.chkSortAddonsTxt, "chkSortAddonsTxt");
            this.chkSortAddonsTxt.Name = "chkSortAddonsTxt";
            this.chkSortAddonsTxt.UseVisualStyleBackColor = true;
            // 
            // listMissingAddons
            // 
            resources.ApplyResources(this.listMissingAddons, "listMissingAddons");
            this.listMissingAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listMissingAddons.ContextMenuStrip = this.ctmAddonReference;
            this.listMissingAddons.FullRowSelect = true;
            this.listMissingAddons.HideSelection = false;
            this.listMissingAddons.Name = "listMissingAddons";
            this.listMissingAddons.UseCompatibleStateImageBehavior = false;
            this.listMissingAddons.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // ctmAddonReference
            // 
            this.ctmAddonReference.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExclude});
            this.ctmAddonReference.Name = "ctmAddonReference";
            resources.ApplyResources(this.ctmAddonReference, "ctmAddonReference");
            // 
            // tsmiExclude
            // 
            this.tsmiExclude.Name = "tsmiExclude";
            resources.ApplyResources(this.tsmiExclude, "tsmiExclude");
            this.tsmiExclude.Click += new System.EventHandler(this.tsmiExclude_Click);
            // 
            // btnDeleteMissingAddons
            // 
            resources.ApplyResources(this.btnDeleteMissingAddons, "btnDeleteMissingAddons");
            this.btnDeleteMissingAddons.Name = "btnDeleteMissingAddons";
            this.btnDeleteMissingAddons.UseVisualStyleBackColor = true;
            this.btnDeleteMissingAddons.Click += new System.EventHandler(this.btnDeleteMissingAddons_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Step = 1;
            // 
            // frmMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnScanAddons);
            this.Controls.Add(this.btnSearchWoW);
            this.Controls.Add(this.txtWoWFolder);
            this.Name = "frmMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.SizeChanged += new System.EventHandler(this.frmMainForm_SizeChanged);
            this.Resize += new System.EventHandler(this.frmMainForm_SizeChanged);
            this.tabControl.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ctmAllAddons.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ctmAddonReference.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnDeleteOrphanWTF;
        private System.Windows.Forms.ListView listWTF;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ColumnHeader typeFile;
        private System.Windows.Forms.Button btnDeleteBakFile;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listAllAddons;
        private System.Windows.Forms.ColumnHeader allAddonsName;
        private System.Windows.Forms.ColumnHeader allAddonsVersion;
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
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnOrderWTF;
        private System.Windows.Forms.ContextMenuStrip ctmAddonReference;
        private System.Windows.Forms.ToolStripMenuItem tsmiExclude;
        private System.Windows.Forms.ContextMenuStrip ctmAllAddons;
        private System.Windows.Forms.ToolStripMenuItem tsmiPatch;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddToDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveToDelete;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAutoClean;
        private System.Windows.Forms.ColumnHeader allAddonsAutoClean;
        private System.Windows.Forms.Button btnShowExceptions;
    }
}

