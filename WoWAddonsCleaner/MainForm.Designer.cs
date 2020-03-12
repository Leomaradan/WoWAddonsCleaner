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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Global", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Character", System.Windows.Forms.HorizontalAlignment.Left);
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
            this.tabPage4.Controls.Add(this.btnDelete);
            this.tabPage4.Controls.Add(this.btnAutoClean);
            this.tabPage4.Controls.Add(this.lblDescription);
            this.tabPage4.Controls.Add(this.lblAuthor);
            this.tabPage4.Controls.Add(this.lblVersion);
            this.tabPage4.Controls.Add(this.lblTitle);
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
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(625, 291);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(227, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Supprimer";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAutoClean
            // 
            this.btnAutoClean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoClean.Location = new System.Drawing.Point(625, 320);
            this.btnAutoClean.Name = "btnAutoClean";
            this.btnAutoClean.Size = new System.Drawing.Size(227, 23);
            this.btnAutoClean.TabIndex = 8;
            this.btnAutoClean.Text = "AutoClean";
            this.btnAutoClean.UseVisualStyleBackColor = true;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.AutoEllipsis = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(625, 72);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(231, 206);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "Description: ";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAuthor.AutoEllipsis = true;
            this.lblAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.Location = new System.Drawing.Point(625, 52);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(231, 20);
            this.lblAuthor.TabIndex = 6;
            this.lblAuthor.Text = "Auteur: ";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoEllipsis = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(625, 32);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(231, 20);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "Version: ";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(625, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(231, 25);
            this.lblTitle.TabIndex = 4;
            // 
            // labelHelpPatch
            // 
            this.labelHelpPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelpPatch.AutoEllipsis = true;
            this.labelHelpPatch.Location = new System.Drawing.Point(626, 375);
            this.labelHelpPatch.Name = "labelHelpPatch";
            this.labelHelpPatch.Size = new System.Drawing.Size(227, 33);
            this.labelHelpPatch.TabIndex = 3;
            this.labelHelpPatch.Text = "Patch les addons sélectionné pour leur indiquer être fait pour la dernière versio" +
    "n";
            // 
            // btnPatchVersion
            // 
            this.btnPatchVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatchVersion.Location = new System.Drawing.Point(625, 349);
            this.btnPatchVersion.Name = "btnPatchVersion";
            this.btnPatchVersion.Size = new System.Drawing.Size(227, 23);
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
            this.allAddonsName,
            this.allAddonsVersion,
            this.allAddonsAutoClean});
            this.listAllAddons.ContextMenuStrip = this.ctmAllAddons;
            this.listAllAddons.FullRowSelect = true;
            this.listAllAddons.HideSelection = false;
            this.listAllAddons.Location = new System.Drawing.Point(7, 7);
            this.listAllAddons.Name = "listAllAddons";
            this.listAllAddons.Size = new System.Drawing.Size(612, 401);
            this.listAllAddons.TabIndex = 0;
            this.listAllAddons.UseCompatibleStateImageBehavior = false;
            this.listAllAddons.View = System.Windows.Forms.View.Details;
            this.listAllAddons.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listAllAddons_ColumnClick);
            this.listAllAddons.SelectedIndexChanged += new System.EventHandler(this.listAllAddons_SelectedIndexChanged);
            // 
            // allAddonsName
            // 
            this.allAddonsName.Text = "Addons";
            // 
            // allAddonsVersion
            // 
            this.allAddonsVersion.Text = "Version";
            // 
            // allAddonsAutoClean
            // 
            this.allAddonsAutoClean.Text = "Auto-Clean Actif ?";
            this.allAddonsAutoClean.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ctmAllAddons
            // 
            this.ctmAllAddons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPatch,
            this.tsmiAddToDelete,
            this.tsmiRemoveToDelete});
            this.ctmAllAddons.Name = "ctmAddonReference";
            this.ctmAllAddons.Size = new System.Drawing.Size(246, 70);
            this.ctmAllAddons.Opening += new System.ComponentModel.CancelEventHandler(this.ctmAllAddons_Opening);
            // 
            // tsmiPatch
            // 
            this.tsmiPatch.Name = "tsmiPatch";
            this.tsmiPatch.Size = new System.Drawing.Size(245, 22);
            this.tsmiPatch.Text = "Patcher";
            // 
            // tsmiAddToDelete
            // 
            this.tsmiAddToDelete.Name = "tsmiAddToDelete";
            this.tsmiAddToDelete.Size = new System.Drawing.Size(245, 22);
            this.tsmiAddToDelete.Text = "Ajouter aux Addons à supprimer";
            this.tsmiAddToDelete.Click += new System.EventHandler(this.tsmiAddToDelete_Click);
            // 
            // tsmiRemoveToDelete
            // 
            this.tsmiRemoveToDelete.Name = "tsmiRemoveToDelete";
            this.tsmiRemoveToDelete.Size = new System.Drawing.Size(245, 22);
            this.tsmiRemoveToDelete.Text = "Enlever des Addons à supprimer";
            this.tsmiRemoveToDelete.Visible = false;
            this.tsmiRemoveToDelete.Click += new System.EventHandler(this.tsmiRemoveToDelete_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listOrphanAddons);
            this.tabPage1.Controls.Add(this.btnDeleteOrphanAddons);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(862, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Addons orphelins";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listOrphanAddons
            // 
            this.listOrphanAddons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listOrphanAddons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Addon});
            this.listOrphanAddons.FullRowSelect = true;
            this.listOrphanAddons.HideSelection = false;
            this.listOrphanAddons.Location = new System.Drawing.Point(7, 7);
            this.listOrphanAddons.Name = "listOrphanAddons";
            this.listOrphanAddons.Size = new System.Drawing.Size(716, 401);
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
            this.btnDeleteOrphanAddons.Location = new System.Drawing.Point(729, 7);
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
            this.tabPage2.Size = new System.Drawing.Size(862, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Personnages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCharacter
            // 
            this.btnDeleteCharacter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteCharacter.Enabled = false;
            this.btnDeleteCharacter.Location = new System.Drawing.Point(729, 6);
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
            this.treeCharacters.Size = new System.Drawing.Size(716, 402);
            this.treeCharacters.TabIndex = 0;
            this.treeCharacters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCharacter_AfterSelect);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnOrderWTF);
            this.tabPage3.Controls.Add(this.btnDeleteBakFile);
            this.tabPage3.Controls.Add(this.listWTF);
            this.tabPage3.Controls.Add(this.btnDeleteOrphanWTF);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(862, 411);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Fichiers de config orphelins";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnOrderWTF
            // 
            this.btnOrderWTF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderWTF.Location = new System.Drawing.Point(729, 64);
            this.btnOrderWTF.Name = "btnOrderWTF";
            this.btnOrderWTF.Size = new System.Drawing.Size(127, 23);
            this.btnOrderWTF.TabIndex = 6;
            this.btnOrderWTF.Text = "Ordonner les variables";
            this.btnOrderWTF.UseVisualStyleBackColor = true;
            this.btnOrderWTF.Visible = false;
            // 
            // btnDeleteBakFile
            // 
            this.btnDeleteBakFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteBakFile.Location = new System.Drawing.Point(729, 35);
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
            this.listWTF.FullRowSelect = true;
            listViewGroup1.Header = "Global";
            listViewGroup1.Name = "listViewGroupGlobal";
            listViewGroup2.Header = "Character";
            listViewGroup2.Name = "listViewGroupCharacter";
            this.listWTF.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listWTF.HideSelection = false;
            this.listWTF.Location = new System.Drawing.Point(7, 7);
            this.listWTF.Name = "listWTF";
            this.listWTF.Size = new System.Drawing.Size(716, 398);
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
            // btnDeleteOrphanWTF
            // 
            this.btnDeleteOrphanWTF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteOrphanWTF.Location = new System.Drawing.Point(729, 6);
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
            this.listMissingAddons.ContextMenuStrip = this.ctmAddonReference;
            this.listMissingAddons.FullRowSelect = true;
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
            // ctmAddonReference
            // 
            this.ctmAddonReference.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExclude});
            this.ctmAddonReference.Name = "ctmAddonReference";
            this.ctmAddonReference.Size = new System.Drawing.Size(169, 26);
            // 
            // tsmiExclude
            // 
            this.tsmiExclude.Name = "tsmiExclude";
            this.tsmiExclude.Size = new System.Drawing.Size(168, 22);
            this.tsmiExclude.Text = "Ignorer cet addon";
            this.tsmiExclude.Click += new System.EventHandler(this.tsmiExclude_Click);
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
            // frmMainForm
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
            this.Name = "frmMainForm";
            this.Text = "WoW Addons Cleaner";
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
    }
}

