using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace WoWAddonsCleaner
{
    public partial class Form1 : Form
    {

        private AddonsScanner scan;

        public Form1()
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

            if(path == null)
            {
                path = @"C:\Program Files (x86)\World of Warcraft";
            }

            txtWoWFolder.Text = path;
            //woWFolderChanged();

        }

        private void txtWoWFolder_TextChanged(object sender, EventArgs e)
        {
            if(txtWoWFolder.Text != String.Empty)
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

        private void btnScanAddons_Click(object sender, EventArgs e)
        {
            scanAddons();
        }

        private void scanAddons()
        {
            scan = new AddonsScanner(txtWoWFolder.Text);

            listAddons.Items.Clear();
            treeCharacters.Nodes.Clear();
            listWTF.Items.Clear();


            ListViewItem[] lItems = scan.listOrphanAddons().Select(
                                                      X => new ListViewItem(X)
                                                ).ToArray();

            listAddons.Items.AddRange(lItems);
            listAddons.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listAddons.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> charlist = scan.listCharacters();

            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> account in charlist)
            {

                TreeNode accountNode = new TreeNode(account.Key);

                foreach (KeyValuePair<string, Dictionary<string, List<string>>> server in account.Value)
                {

                    TreeNode serverNode = new TreeNode(server.Key);

                    foreach (KeyValuePair <string, List<string>> character in server.Value)
                    {
                        serverNode.Nodes.Add(character.Key);
                        
                        //listConfigFile.Items.AddRange(character.Value.Select(X => new ListViewItem(X)).ToArray());
                    }

                    accountNode.Nodes.Add(serverNode);
                }

                treeCharacters.Nodes.Add(accountNode);
            }

            ListViewItem lItem;
            foreach (string orphans in scan.listOrphanSavedVariables())
            {
                lItem = new ListViewItem();
                lItem.Text = orphans;
                lItem.SubItems.Add("Global");
                listWTF.Items.Add(lItem);
            }

            foreach (string orphans in scan.listOrphanSavedVariablesPerCharacter())
            {
                lItem = new ListViewItem();
                lItem.Text = orphans;
                lItem.SubItems.Add("Personnage");
                listWTF.Items.Add(lItem);
            }

            listWTF.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listWTF.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            /*lItem = scan.listOrphanSavedVariables().Select(
                                          //X => new ListViewItem(X)
                                          X => new ListViewItem(X.Key)
                                    ).ToArray();*/

            treeCharacters.ExpandAll();
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
                    scan.removeCharacter(elem[0], elem[1], elem[2]);
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
                    scan.removeServer(elem[0], elem[1]);
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
            if (listAddons.SelectedItems.Count <= 0)
            {
                return;
            }
            var confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) l'addon(s) sélectionné(s) ?",
                                 "Confirmation de suppression",
                                 MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                List<string> items = new List<string>();
                foreach (ListViewItem Item in listAddons.SelectedItems)
                {
                    items.Add(Item.Text.ToString());
                }

                scan.removeAddons(items);
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

                scan.removeWTF(items);
                scanAddons();
            }
        }

        private void btnBackupWTF_Click(object sender, EventArgs e)
        {
            //ZipFile.CreateFromDirectory(dirPath, zipFile);
        }
    }
}
