using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoWAddonsCleaner
{

    enum Configs
    {
        Version,
        Path,
        Lang,
        AddonsExceptions,
        AutoClean
    }
    public partial class frmMainForm : Form
    {

        private AddonsScanner scan;

        private int allAddonsSorting = 0;
        private bool allAddonsSortingDesc = false;

        private string currentVersion = "";
        private string lang;
        private List<string> missingAddonsExceptions;
        private List<string> autoClean;

        private static string kCURRENT_VERSION = "80300";
        private static string kCURRENT_PATH = @"C:\Program Files (x86)\World of Warcraft\_retail_";
        private static string kCURRENT_LANG = "frFR";
        private static string kCURRENT_ADDONS_EXCEPTIONS = "WeakAurasCompanion";



        public frmMainForm()
        {
            InitializeComponent();
        }

        #region Main Form
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            //string path = System.Configuration!System.Configuration.ConfigurationManager
            string path = ConfigurationManager.AppSettings[resolveConfig(Configs.Path)];
            string currentVersion = ConfigurationManager.AppSettings[resolveConfig(Configs.Version)];
            string missingAddonsExceptions = ConfigurationManager.AppSettings[resolveConfig(Configs.AddonsExceptions)];
            string lang = ConfigurationManager.AppSettings[resolveConfig(Configs.Lang)];
            string autoclean = ConfigurationManager.AppSettings[resolveConfig(Configs.AutoClean)];

            if (path == null)
            {
                path = kCURRENT_PATH;
                updateConfig(Configs.Path, kCURRENT_PATH);
            }

            if (currentVersion == null)
            {
                currentVersion = kCURRENT_VERSION;
                updateConfig(Configs.Version, kCURRENT_VERSION);
            }

            if (missingAddonsExceptions == null)
            {
                missingAddonsExceptions = kCURRENT_ADDONS_EXCEPTIONS;
                updateConfig(Configs.AddonsExceptions, kCURRENT_ADDONS_EXCEPTIONS);
            }

            if (lang == null)
            {
                lang = kCURRENT_LANG;
                updateConfig(Configs.Lang, kCURRENT_LANG);
            }

            if (autoclean == null)
            {
                autoclean = "";
                updateConfig(Configs.AutoClean, "");
            }

            txtWoWFolder.Text = path;
            this.currentVersion = currentVersion;
            this.missingAddonsExceptions = new List<string>(missingAddonsExceptions.Split(';'));
            this.autoClean = new List<string>(autoclean.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            this.lang = lang;

            labelHelpPatch.Text = @"Patch les addons sélectionné pour leur indiquer être fait pour la version "
            + new Version(currentVersion).versionNum
            + ".";
        }


        private void frmMainForm_SizeChanged(object sender, EventArgs e)
        {
            this.resizeList(listAllAddons);
            this.resizeList(listOrphanAddons);
            this.resizeList(listWTF);
            this.resizeList(listMissingAddons);
        }
        #endregion

        #region Upper part
        private void btnSearchWoW_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtWoWFolder.Text = folderBrowserDialog.SelectedPath;
                //woWFolderChanged();
            }
        }

        private void txtWoWFolder_TextChanged(object sender, EventArgs e)
        {
            if (txtWoWFolder.Text != String.Empty)
            {
                if (Directory.Exists(txtWoWFolder.Text))
                {
                    updateConfig(Configs.Path, txtWoWFolder.Text);
                    btnScanAddons.Enabled = true;
                }
                else
                {
                    txtWoWFolder.Text = String.Empty;
                    updateConfig(Configs.Path, null);
                    btnScanAddons.Enabled = false;
                }
            }

        }

        private void btnScanAddons_Click(object sender, EventArgs e)
        {
            scanAddons();
            tabControl.Enabled = true;
        }

        #endregion

        #region All Addons Tabs

        private void listAllAddons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listAllAddons.SelectedItems.Count >= 1)
            {
                Addon selectedAddon = scan.addons[listAllAddons.SelectedItems[0].Name];

                this.lblTitle.Text = selectedAddon.title;
                this.lblAuthor.Text = "Auteur: " + selectedAddon.author;
                this.lblVersion.Text = "Version: " + selectedAddon.version.versionNum;
                this.lblDescription.Text = "Description: " + selectedAddon.notes;
            }
            else
            {
                this.lblTitle.Text = "";
                this.lblAuthor.Text = "Auteur: ";
                this.lblVersion.Text = "Version: ";
                this.lblDescription.Text = "Description: ";
            }

        }
        private void listAllAddons_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var newSorting = e.Column;

            if (this.allAddonsSorting == newSorting)
            {
                this.allAddonsSortingDesc = !this.allAddonsSortingDesc;
            }
            else
            {
                this.allAddonsSorting = newSorting;
                this.allAddonsSortingDesc = false;
            }

            this.updateListAllAddons();
        }

        private void btnPatchVersion_Click(object sender, EventArgs e)
        {
            if (listAllAddons.SelectedItems.Count <= 0)
            {
                return;
            }
            var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir patcher le(s) fichier(s) sélectionné(s) ?",
                                 "Confirmation de modification",
                                 MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                List<string> items = new List<string>();
                foreach (ListViewItem Item in listAllAddons.SelectedItems)
                {
                    items.Add(Item.Name);
                }

                scan.patchAddonsVersion(items, this.currentVersion);
                scanAddons();
            }
        }

        private void ctmAllAddons_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ListView wListView = this.listAllAddons;

            if (wListView != null)
            {
                if (wListView.SelectedItems.Count == 1)
                {
                    foreach (ListViewItem wItem in wListView.SelectedItems)
                    {
                        if (this.autoClean.Contains(wItem.Name))
                        {
                            this.tsmiRemoveToDelete.Visible = true;
                            this.tsmiAddToDelete.Visible = false;

                        }
                        else
                        {
                            this.tsmiRemoveToDelete.Visible = false;
                            this.tsmiAddToDelete.Visible = true;
                        }
                    }
                }
                else if (wListView.SelectedItems.Count > 1)
                {
                    this.tsmiRemoveToDelete.Visible = false;
                    this.tsmiAddToDelete.Visible = true;
                }
                else
                {
                    this.tsmiRemoveToDelete.Visible = false;
                    this.tsmiAddToDelete.Visible = false;
                }
            }
        }

        private void tsmiAddToDelete_Click(object sender, EventArgs e)
        {
            ListView wListView = this.getControl(sender) as ListView;

            if (wListView != null)
            {
                foreach (ListViewItem wItem in wListView.SelectedItems)
                {
                    if (!this.autoClean.Contains(wItem.Name))
                    {
                        this.autoClean.Add(wItem.Name);
                    }
                }


                this.btnAutoClean.Enabled = this.autoClean.Count > 0;
                this.updateListAllAddons();
            }
        }
        private void tsmiRemoveToDelete_Click(object sender, EventArgs e)
        {
            ListView wListView = this.getControl(sender) as ListView;

            if (wListView != null)
            {
                foreach (ListViewItem wItem in wListView.SelectedItems)
                {
                    if (this.autoClean.Contains(wItem.Name))
                    {
                        this.autoClean.Remove(wItem.Name);
                    }
                }


                this.btnAutoClean.Enabled = this.autoClean.Count > 0;
                this.updateListAllAddons();
            }
        }

        #endregion

        #region Orphan Tabs
        private void btnDeleteOrphanAddons_Click(object sender, EventArgs e)
        {
            if (listOrphanAddons.SelectedItems.Count <= 0)
            {
                return;
            }
            var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) l'addon(s) sélectionné(s) ?",
                                 "Confirmation de suppression",
                                 MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                List<string> items = new List<string>();
                foreach (ListViewItem Item in listOrphanAddons.SelectedItems)
                {
                    items.Add(Item.Text.ToString());
                }

                scan.deleteAddons(items);
                scanAddons();
            }
        }
        #endregion

        #region Characters Tabs

        private void treeCharacter_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeCharacters.SelectedNode.Level > 0)
            {
                btnDeleteCharacter.Enabled = true;
            }
            else
            {
                btnDeleteCharacter.Enabled = false;
            }
        }
        private void btnDeleteCharacter_Click(object sender, EventArgs e)
        {
            TreeNode current = treeCharacters.SelectedNode;
            string[] elem = current.FullPath.Split('\\');

            if (current.Level == 2)
            {
                var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer " + elem[2] + "-" + elem[1] + " ?",
                                     "Confirmation de suppression",
                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    scan.deleteCharacterVariables(elem[0], elem[1], elem[2]);
                    scanAddons();
                }
            }
            else if (current.Level == 1)
            {
                var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le serveur " + elem[1] + " ?",
                                     "Confirmation de suppression",
                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    scan.deleteServerVariables(elem[0], elem[1]);
                    scanAddons();
                }
            }

        }

        #endregion

        #region WTF Tabs
        private void btnDeleteOrphanWTF_Click(object sender, EventArgs e)
        {
            if (listWTF.SelectedItems.Count <= 0)
            {
                return;
            }
            var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) fichier(s) sélectionné(s) ?",
                                 "Confirmation de suppression",
                                 MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                Dictionary<string, string> items = new Dictionary<string, string>();
                foreach (ListViewItem Item in listWTF.SelectedItems)
                {
                    items.Add(Item.Text.ToString(), Item.SubItems[1].Text.ToString());
                }

                scan.deleteWTFFiles(items);
                scanAddons();
            }
        }

        private void btnDeleteBakFile_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) fichier(s) *.bak ?",
                     "Confirmation de suppression",
                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int count = scan.deleteBakFiles();
                MessageBox.Show(count + " fichier(s) *.bak ont été supprimé");
                scanAddons();
            }

        }
        #endregion

        #region Addons References Tabs

        private void btnSortAllAddonsTxt_Click(object sender, EventArgs e)
        {
            scan.sortAddonsReferences();
            scanAddons();
        }

        private void tsmiExclude_Click(object sender, EventArgs e)
        {

            ListView wListView = this.getControl(sender) as ListView;

            if (wListView != null)
            {
                foreach (ListViewItem wItem in wListView.SelectedItems)
                {
                    if (!this.missingAddonsExceptions.Contains(wItem.Text))
                    {
                        this.missingAddonsExceptions.Add(wItem.Text);
                    }
                }

                this.updateConfig(Configs.AddonsExceptions, String.Join(";", this.missingAddonsExceptions.ToArray()));
                this.scanAddons();
            }
        }
        private void btnDeleteMissingAddons_Click(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            foreach (ListViewItem Item in listMissingAddons.SelectedItems)
            {
                items.Add(Item.Text);
            }

            var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer " + items.Count + " références d'addons ?",
                     "Confirmation de suppression",
                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                scan.removeMissingAddonsReferences(items, chkSortAddonsTxt.Checked);
                scanAddons();
            }

        }


        #endregion

        #region Private methods


        private object getControl(object iSender)
        {
            ToolStripItem wMenuItem = iSender as ToolStripItem;
            if (wMenuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip wOwner = wMenuItem.Owner as ContextMenuStrip;
                if (wOwner != null)
                {
                    // Get the control that is displaying this context menu
                    return wOwner.SourceControl;


                }
            }

            return null;
        }
        private string resolveConfig(Configs iConfig)
        {
            switch (iConfig)
            {
                case Configs.AddonsExceptions: return "MissingAddonsExceptions";
                case Configs.Lang: return "Lang";
                case Configs.Path: return "CurrentPath";
                case Configs.Version: return "CurrentVersion";
                case Configs.AutoClean: return "AutoClean";
            }

            return "";
        }


        private void updateConfig(Configs iKey, string iValue)
        {
            Configuration wConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection wSettings = wConfigFile.AppSettings.Settings;
            string wConfigKey = resolveConfig(iKey);

            if (wSettings[wConfigKey] == null)
            {
                wSettings.Add(wConfigKey, iValue);
            }
            else
            {
                wSettings[wConfigKey].Value = iValue;
            }

            wConfigFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(wConfigFile.AppSettings.SectionInformation.Name);
        }



        private void scanAddons()
        {
            this.scan = new AddonsScanner(txtWoWFolder.Text, new List<string>(this.missingAddonsExceptions), this.lang, progressBar);

            this.updateListAllAddons();
            this.updateOrphans();
            this.updateCharacterTree();
            this.updateListWTF();
            this.updateMissing();

            this.btnAutoClean.Enabled = this.autoClean.Count > 0;
        }

        private void updateList(ListView iList, ListViewItem[] iItems)
        {
            iList.Items.Clear();

            iList.Items.AddRange(iItems);
            this.resizeList(iList);
        }

        private void resizeList(ListView iList)
        {
            iList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            iList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void updateOrphans()
        {
            ListViewItem[] wlItems = scan.orphanAddons.Select(
                X => new ListViewItem(X)
            ).ToArray();

            this.updateList(listOrphanAddons, wlItems);
        }

        private void updateMissing()
        {
            List<ListViewItem> wlItems = new List<ListViewItem>();

            foreach (var wAddons in scan.missingAddonReferences)
            {
                ListViewItem wlItem = new ListViewItem();
                wlItem.Text = wAddons.Key;
                wlItems.Add(wlItem);
            }

            wlItems.Sort((x, y) => x.Text.CompareTo(y.Text));

            this.updateList(listMissingAddons, wlItems.ToArray());
        }

        private void updateCharacterTree()
        {
            treeCharacters.Nodes.Clear();

            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> wAccount in scan.filesWTF)
            {

                TreeNode wAccountNode = new TreeNode(wAccount.Key);

                foreach (KeyValuePair<string, Dictionary<string, List<string>>> wServer in wAccount.Value)
                {

                    TreeNode wServerNode = new TreeNode(wServer.Key);

                    foreach (KeyValuePair<string, List<string>> wCharacter in wServer.Value)
                    {
                        wServerNode.Nodes.Add(wCharacter.Key);
                    }

                    wAccountNode.Nodes.Add(wServerNode);
                }

                treeCharacters.Nodes.Add(wAccountNode);
            }

            treeCharacters.ExpandAll();
        }

        private void updateListWTF()
        {

            List<ListViewItem> wlItems = new List<ListViewItem>();
            foreach (string wOrphans in scan.orphanSavedVariables)
            {
                ListViewItem lItem = new ListViewItem();
                lItem.Text = wOrphans;
                lItem.SubItems.Add("Global");
                lItem.Group = listWTF.Groups[0];
                wlItems.Add(lItem);
            }

            foreach (string wOrphans in scan.orphanSavedVariablesPerCharacter)
            {
                ListViewItem lItem = new ListViewItem();
                lItem.Text = wOrphans;
                lItem.SubItems.Add("Personnage");
                lItem.Group = listWTF.Groups[1];
                wlItems.Add(lItem);
            }

            this.updateList(listWTF, wlItems.ToArray());
        }
        private void updateListAllAddons()
        {
            var wList = scan.addons.ToList();
            Version wVersion = new Version(currentVersion);

            if (this.allAddonsSorting == 1)
            {
                wList.Sort((x, y) => x.Value.version.version.CompareTo(y.Value.version.version));
            }
            else
            {
                wList.Sort((x, y) => x.Value.title.CompareTo(y.Value.title));
            }

            if (this.allAddonsSortingDesc)
            {
                wList.Reverse();
            }

            List<ListViewItem> wlItems = new List<ListViewItem>();
            foreach (KeyValuePair<string, Addon> wAddons in wList)
            {
                ListViewItem wlItem = new ListViewItem();
                wlItem.Name = wAddons.Key;
                wlItem.Text = wAddons.Value.title;
                wlItem.SubItems.Add(wAddons.Value.version.versionNum);
                wlItem.SubItems.Add(this.autoClean.Contains(wAddons.Key) ? "Oui" : "Non");
                wlItems.Add(wlItem);
            }
            this.updateList(listAllAddons, wlItems.ToArray());

        }

        #endregion

    }
}
