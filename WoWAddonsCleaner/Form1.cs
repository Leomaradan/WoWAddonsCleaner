using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoWAddonsCleaner
{
    public partial class mainForm : Form
    {

        private AddonsScanner scan;

        private int allAddonsSorting = 0;
        private bool allAddonsSortingDesc = false;

        private string currentVersion = "";

        private string[] missingAddonsExceptions;

        private static string kCURRENT_VERSION = "80300";

        public mainForm()
        {
            InitializeComponent();
        }


        private void btnSearchWoW_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtWoWFolder.Text = folderBrowserDialog.SelectedPath;
                //woWFolderChanged();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string path = System.Configuration!System.Configuration.ConfigurationManager
            string path = ConfigurationManager.AppSettings["CurrentPath"];
            string currentVersion = ConfigurationManager.AppSettings["CurrentVersion"];
            string missingAddonsExceptions = ConfigurationManager.AppSettings["MissingAddonsExceptions"];

            if (path == null)
            {
                path = @"C:\Program Files (x86)\World of Warcraft";
            }

            if (currentVersion == null)
            {
                currentVersion = kCURRENT_VERSION;
                updateCurrentVersionConfig(kCURRENT_VERSION);
            }

            if(missingAddonsExceptions != null)
            {
                this.missingAddonsExceptions = missingAddonsExceptions.Split(';');
            } else
            {
                this.missingAddonsExceptions = new List<string>().ToArray();
            }

            txtWoWFolder.Text = path;
            this.currentVersion = currentVersion;
            labelHelpPatch.Text = @"Patch les addons sélectionné pour leur indiquer être fait pour la version "
            + new Version(currentVersion).versionNum
            + ".";
        }

        private void txtWoWFolder_TextChanged(object sender, EventArgs e)
        {
            if (txtWoWFolder.Text != String.Empty)
            {
                if (Directory.Exists(txtWoWFolder.Text))
                {
                    updatePathConfig(txtWoWFolder.Text);
                    btnScanAddons.Enabled = true;
                }
                else
                {
                    txtWoWFolder.Text = String.Empty;
                    updatePathConfig(null);
                    btnScanAddons.Enabled = false;
                }
            }

        }

        private void updatePathConfig(string path)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;

            if (settings["CurrentPath"] == null)
            {
                settings.Add("CurrentPath", path);
            }
            else
            {
                settings["CurrentPath"].Value = path;
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        private void updateCurrentVersionConfig(string version)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;

            if (settings["CurrentVersion"] == null)
            {
                settings.Add("CurrentVersion", version);
            }
            else
            {
                settings["CurrentVersion"].Value = version;
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        private void updateMissingAddonsExceptionsConfig(List<string> iAddons)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            string wAddons = String.Join(";", iAddons.ToArray());

            if (settings["MissingAddonsExceptions"] == null)
            {
                settings.Add("MissingAddonsExceptions", wAddons);
            }
            else
            {
                settings["MissingAddonsExceptions"].Value = wAddons;
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        private void btnScanAddons_Click(object sender, EventArgs e)
        {
            scanAddons();
            tabControl.Enabled = true;
        }

        private void scanAddons()
        {
            scan = new AddonsScanner(txtWoWFolder.Text, new List<string>(this.missingAddonsExceptions), progressBar);

            this.updateListAllAddons();
            this.updateOrphans();
            this.updateCharacterTree();
            this.updateListWTF();
            this.updateMissing();
        }

        private void updateList(ListView iList, ListViewItem[] iItems)
        {
            iList.Items.Clear();

            iList.Items.AddRange(iItems);
            iList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            iList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void updateOrphans()
        {
            ListViewItem[] lItems = scan.orphanAddons.Select(
                X => new ListViewItem(X)
            ).ToArray();

            this.updateList(listOrphanAddons, lItems);
        }

        private void updateMissing()
        {
            List<ListViewItem> lItems = new List<ListViewItem>();

            foreach (var addons in scan.missingAddons)
            {
                ListViewItem lItem = new ListViewItem();
                lItem.Text = addons.Key;
                lItems.Add(lItem);
            }

            lItems.Sort((x, y) => x.Text.CompareTo(y.Text));

            this.updateList(listMissingAddons, lItems.ToArray());
        }

        private void updateCharacterTree()
        {
            treeCharacters.Nodes.Clear();

            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> account in scan.filesWTF)
            {

                TreeNode accountNode = new TreeNode(account.Key);

                foreach (KeyValuePair<string, Dictionary<string, List<string>>> server in account.Value)
                {

                    TreeNode serverNode = new TreeNode(server.Key);

                    foreach (KeyValuePair<string, List<string>> character in server.Value)
                    {
                        serverNode.Nodes.Add(character.Key);

                        //listConfigFile.Items.AddRange(character.Value.Select(X => new ListViewItem(X)).ToArray());
                    }

                    accountNode.Nodes.Add(serverNode);
                }

                treeCharacters.Nodes.Add(accountNode);
            }

            treeCharacters.ExpandAll();
        }

        private void updateListWTF()
        {


            List<ListViewItem> lItems = new List<ListViewItem>();
            foreach (string orphans in scan.orphanSavedVariables)
            {
                ListViewItem lItem = new ListViewItem();
                lItem.Text = orphans;
                lItem.SubItems.Add("Global");
                lItem.Group = listWTF.Groups[0];
                lItems.Add(lItem);
            }

            foreach (string orphans in scan.orphanSavedVariablesPerCharacter)
            {
                ListViewItem lItem = new ListViewItem();
                lItem.Text = orphans;
                lItem.SubItems.Add("Personnage");
                lItem.Group = listWTF.Groups[1];
                lItems.Add(lItem);
            }

            this.updateList(listWTF, lItems.ToArray());
        }
        private void updateListAllAddons()
        {
            var list = scan.addonsVersion.ToList();
            Version version = new Version(currentVersion);

            if (this.allAddonsSorting == 1)
            {
                list.Sort((x, y) => x.Value.version.version.CompareTo(y.Value.version.version));
            }
            else
            {
                list.Sort((x, y) => x.Value.title.CompareTo(y.Value.title));
            }

            if (this.allAddonsSortingDesc)
            {
                list.Reverse();
            }

            List<ListViewItem> lItems = new List<ListViewItem>();
            foreach (KeyValuePair<string, Addon> addons in list)
            {
                ListViewItem lItem = new ListViewItem();
                lItem.Name = addons.Key;
                lItem.Text = addons.Value.title;
                lItem.SubItems.Add(addons.Value.version.versionNum);
                lItems.Add(lItem);
            }
            this.updateList(listAllAddons, lItems.ToArray());

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

        private void btnBackupAddons_Click(object sender, EventArgs e)
        {

        }

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

        private void btnBackupWTF_Click(object sender, EventArgs e)
        {
            //ZipFile.CreateFromDirectory(dirPath, zipFile);
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

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            listWTF.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listWTF.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listWTF_ColumnClick(object sender, ColumnClickEventArgs e)
        {

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

        private void btnSortAllAddonsTxt_Click(object sender, EventArgs e)
        {
            scan.sortAddonsReferences();
            scanAddons();
        }
    }
}
