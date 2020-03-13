using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoWAddonsCleaner
{
    internal enum Configs
    {
        Version,
        Path,
        Lang,
        AddonsExceptions,
        AutoClean
    }
    public partial class frmMainForm : Form
    {

        private AddonsScanner oAddonsScanner;

        private int oAllAddonsSorting = 0;
        private bool oAllAddonsSortingDesc = false;

        private string oCurrentVersion = "";
        private string oLang;
        private bool oShowAddonsException = false;
        private List<ListViewItem> olItemsMissingReference;
        private List<string> olMissingAddonsExceptions;
        private List<string> olAutoClean;

        private static readonly string kCURRENT_VERSION = "80300";
        private static readonly string kCURRENT_PATH = "";
        private static readonly string kCURRENT_LANG = "frFR";
        private static readonly string kCURRENT_ADDONS_EXCEPTIONS = "";

        public frmMainForm()
        {
            InitializeComponent();
        }

        #region Main Form
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings[resolveConfig(Configs.Path)];
            string currentVersion = ConfigurationManager.AppSettings[resolveConfig(Configs.Version)];
            string missingAddonsExceptions = ConfigurationManager.AppSettings[resolveConfig(Configs.AddonsExceptions)];
            string lang = ConfigurationManager.AppSettings[resolveConfig(Configs.Lang)];
            string autoclean = ConfigurationManager.AppSettings[resolveConfig(Configs.AutoClean)];

            if (path == null)
            {
                path = "";
                this.updateConfig(Configs.Path, "");
            }

            if (currentVersion == null)
            {
                currentVersion = kCURRENT_VERSION;
                this.updateConfig(Configs.Version, kCURRENT_VERSION);
            }

            if (missingAddonsExceptions == null)
            {
                missingAddonsExceptions = "";
                this.updateConfig(Configs.AddonsExceptions, "");
            }

            if (lang == null)
            {
                lang = kCURRENT_LANG;
                this.updateConfig(Configs.Lang, kCURRENT_LANG);
            }

            if (autoclean == null)
            {
                autoclean = "";
                this.updateConfig(Configs.AutoClean, "");
            }

            this.txtWoWFolder.Text = path;
            this.oCurrentVersion = currentVersion;
            this.olMissingAddonsExceptions = new List<string>(missingAddonsExceptions.Split(';'));
            this.olAutoClean = new List<string>(autoclean.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            this.oLang = lang;

            this.labelHelpPatch.Text = @"Patch les addons sélectionné pour leur indiquer être fait pour la version "
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
            DialogResult wResult = this.folderBrowserDialog.ShowDialog();
            if (wResult == DialogResult.OK)
            {
                this.txtWoWFolder.Text = this.folderBrowserDialog.SelectedPath;
            }
        }

        private void txtWoWFolder_TextChanged(object sender, EventArgs e)
        {
            if (this.txtWoWFolder.Text != string.Empty)
            {
                if (Directory.Exists(this.txtWoWFolder.Text))
                {
                    this.updateConfig(Configs.Path, this.txtWoWFolder.Text);
                    this.btnScanAddons.Enabled = true;
                }
                else
                {
                    this.txtWoWFolder.Text = string.Empty;
                    this.updateConfig(Configs.Path, null);
                    this.btnScanAddons.Enabled = false;
                }
            }

        }

        private void btnScanAddons_Click(object sender, EventArgs e)
        {
            this.scanAddons();
            this.tabControl.Enabled = true;
        }

        #endregion

        #region All Addons Tabs

        private void listAllAddons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listAllAddons.SelectedItems.Count >= 1)
            {
                Addon wSelectedAddon = this.oAddonsScanner.addons[this.listAllAddons.SelectedItems[0].Name];

                this.lblTitle.Text = wSelectedAddon.title;
                this.lblAuthor.Text = "Auteur: " + wSelectedAddon.author;
                this.lblVersion.Text = "Version: " + wSelectedAddon.version.versionNum;
                this.lblDescription.Text = "Description: " + wSelectedAddon.notes;
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
            int wNewSorting = e.Column;

            if (this.oAllAddonsSorting == wNewSorting)
            {
                this.oAllAddonsSortingDesc = !this.oAllAddonsSortingDesc;
            }
            else
            {
                this.oAllAddonsSorting = wNewSorting;
                this.oAllAddonsSortingDesc = false;
            }

            this.updateListAllAddons();
        }

        private void btnPatchVersion_Click(object sender, EventArgs e)
        {
            if (this.listAllAddons.SelectedItems.Count <= 0)
            {
                return;
            }

            DialogResult wConfirmResult = MessageBox.Show("Êtes-vous sur de vouloir patcher le(s) fichier(s) sélectionné(s) ?",
                                 "Confirmation de modification",
                                 MessageBoxButtons.YesNo);

            if (wConfirmResult == DialogResult.Yes)
            {
                List<string> wItems = new List<string>();
                foreach (ListViewItem wItem in this.listAllAddons.SelectedItems)
                {
                    wItems.Add(wItem.Name);
                }

                this.oAddonsScanner.patchAddonsVersion(wItems, this.oCurrentVersion);
                this.scanAddons();
            }
        }

        private void btnAutoClean_Click(object sender, EventArgs e)
        {
            if (this.olAutoClean.Count <= 0)
            {
                return;
            }

            DialogResult wConfirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) fichier(s) en mode auto-clean ?",
                                 "Confirmation de modification",
                                 MessageBoxButtons.YesNo);

            if (wConfirmResult == DialogResult.Yes)
            {
                this.oAddonsScanner.deleteAddons(this.olAutoClean);
                this.scanAddons();
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
                        if (this.olAutoClean.Contains(wItem.Name))
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
                    if (!this.olAutoClean.Contains(wItem.Name))
                    {
                        this.olAutoClean.Add(wItem.Name);
                    }
                }

                this.btnAutoClean.Enabled = this.olAutoClean.Count > 0;
                this.updateConfig(Configs.AutoClean, string.Join(";", this.olAutoClean.ToArray()));
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
                    if (this.olAutoClean.Contains(wItem.Name))
                    {
                        this.olAutoClean.Remove(wItem.Name);
                    }
                }

                this.btnAutoClean.Enabled = this.olAutoClean.Count > 0;
                this.updateConfig(Configs.AutoClean, string.Join(";", this.olAutoClean.ToArray()));
                this.updateListAllAddons();
            }
        }

        private void tsmiPatch_Click(object sender, EventArgs e)
        {
            this.btnPatchVersion_Click(sender, e);
        }

        #endregion

        #region Orphan Tabs
        private void btnDeleteOrphanAddons_Click(object sender, EventArgs e)
        {
            if (this.listOrphanAddons.SelectedItems.Count <= 0)
            {
                return;
            }

            DialogResult wConfirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) l'addon(s) sélectionné(s) ?",
                                 "Confirmation de suppression",
                                 MessageBoxButtons.YesNo);

            if (wConfirmResult == DialogResult.Yes)
            {
                List<string> wlItems = new List<string>();
                foreach (ListViewItem wItem in this.listOrphanAddons.SelectedItems)
                {
                    wlItems.Add(wItem.Text.ToString());
                }

                this.oAddonsScanner.deleteAddons(wlItems);
                this.scanAddons();
            }
        }
        #endregion

        #region Characters Tabs

        private void treeCharacter_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeCharacters.SelectedNode.Level > 0)
            {
                this.btnDeleteCharacter.Enabled = true;
            }
            else
            {
                this.btnDeleteCharacter.Enabled = false;
            }
        }
        private void btnDeleteCharacter_Click(object sender, EventArgs e)
        {
            TreeNode wCurrent = this.treeCharacters.SelectedNode;
            string[] wElems = wCurrent.FullPath.Split('\\');

            if (wCurrent.Level == 2)
            {
                DialogResult wConfirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer " + wElems[2] + "-" + wElems[1] + " ?",
                                     "Confirmation de suppression",
                                     MessageBoxButtons.YesNo);

                if (wConfirmResult == DialogResult.Yes)
                {
                    this.oAddonsScanner.deleteCharacterVariables(wElems[0], wElems[1], wElems[2]);
                    this.scanAddons();
                }
            }
            else if (wCurrent.Level == 1)
            {
                DialogResult wConfirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le serveur " + wElems[1] + " ?",
                                     "Confirmation de suppression",
                                     MessageBoxButtons.YesNo);

                if (wConfirmResult == DialogResult.Yes)
                {
                    this.oAddonsScanner.deleteServerVariables(wElems[0], wElems[1]);
                    this.scanAddons();
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
            DialogResult confirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) fichier(s) sélectionné(s) ?",
                                 "Confirmation de suppression",
                                 MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                Dictionary<string, string> items = new Dictionary<string, string>();
                foreach (ListViewItem Item in listWTF.SelectedItems)
                {
                    items.Add(Item.Text.ToString(), Item.SubItems[1].Text.ToString());
                }

                oAddonsScanner.deleteWTFFiles(items);
                scanAddons();
            }
        }

        private void btnDeleteBakFile_Click(object sender, EventArgs e)
        {
            DialogResult wConfirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer le(s) fichier(s) *.bak ?",
                     "Confirmation de suppression",
                     MessageBoxButtons.YesNo);

            if (wConfirmResult == DialogResult.Yes)
            {
                int wCount = this.oAddonsScanner.deleteBakFiles();
                MessageBox.Show(wCount + " fichier(s) *.bak ont été supprimé");
                this.scanAddons();
            }

        }
        #endregion

        #region Addons References Tabs

        private void btnSortAllAddonsTxt_Click(object sender, EventArgs e)
        {
            this.oAddonsScanner.sortAddonsReferences();
            this.scanAddons();
        }

        private void updateMissingExceptionList()
        {
            List<ListViewItem> wExceptionsItems = new List<ListViewItem>();
            foreach (string wExceptions in this.olMissingAddonsExceptions)
            {
                ListViewItem wlItem = new ListViewItem
                {
                    Text = wExceptions
                };
                wExceptionsItems.Add(wlItem);
            }
            this.updateList(this.listMissingAddons, wExceptionsItems.ToArray());
        }

        private void btnShowExceptions_Click(object sender, EventArgs e)
        {
            if (!this.oShowAddonsException)
            {
                this.btnShowExceptions.Text = "Voir les addons";
                this.updateMissingExceptionList();

            }
            else
            {
                this.btnShowExceptions.Text = "Voir les exceptions";
                this.updateList(listMissingAddons, this.olItemsMissingReference.ToArray());
            }

            this.oShowAddonsException = !this.oShowAddonsException;
        }

        private void tsmiExclude_Click(object sender, EventArgs e)
        {

            ListView wListView = this.getControl(sender) as ListView;

            if (wListView != null)
            {
                foreach (ListViewItem wItem in wListView.SelectedItems)
                {
                    if (!this.olMissingAddonsExceptions.Contains(wItem.Text))
                    {
                        this.olMissingAddonsExceptions.Add(wItem.Text);
                    }
                }

                this.updateConfig(Configs.AddonsExceptions, string.Join(";", this.olMissingAddonsExceptions.ToArray()));
                this.scanAddons();
            }
        }
        private void btnDeleteMissingAddons_Click(object sender, EventArgs e)
        {

            if (this.oShowAddonsException)
            {
                foreach (ListViewItem wItem in this.listMissingAddons.SelectedItems)
                {
                    if (this.olMissingAddonsExceptions.Contains(wItem.Text))
                    {
                        this.olMissingAddonsExceptions.Remove(wItem.Text);
                    }
                }

                this.updateConfig(Configs.AddonsExceptions, string.Join(";", this.olMissingAddonsExceptions.ToArray()));
                this.updateMissingExceptionList();
            }
            else
            {
                List<string> wlItems = new List<string>();
                foreach (ListViewItem wItem in this.listMissingAddons.SelectedItems)
                {
                    wlItems.Add(wItem.Text);
                }

                DialogResult wConfirmResult = MessageBox.Show("Êtes-vous sur de vouloir supprimer " + wlItems.Count + " références d'addons ?",
                         "Confirmation de suppression",
                         MessageBoxButtons.YesNo);

                if (wConfirmResult == DialogResult.Yes)
                {
                    this.oAddonsScanner.removeMissingAddonsReferences(wlItems, this.chkSortAddonsTxt.Checked);
                    this.scanAddons();
                }
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
            string wConfigKey = this.resolveConfig(iKey);

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
            this.oAddonsScanner = new AddonsScanner(this.txtWoWFolder.Text, new List<string>(this.olMissingAddonsExceptions), this.oLang, this.progressBar);

            this.updateListAllAddons();
            this.updateOrphans();
            this.updateCharacterTree();
            this.updateListWTF();
            this.updateMissing();

            this.btnAutoClean.Enabled = this.olAutoClean.Count > 0;
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
            ListViewItem[] wlItems = this.oAddonsScanner.orphanAddons.Select(
                X => new ListViewItem(X)
            ).ToArray();

            this.updateList(this.listOrphanAddons, wlItems);
        }

        private void updateMissing()
        {
            this.olItemsMissingReference = new List<ListViewItem>();

            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> wAddons in this.oAddonsScanner.missingAddonReferences)
            {
                ListViewItem wlItem = new ListViewItem
                {
                    Text = wAddons.Key
                };
                this.olItemsMissingReference.Add(wlItem);
            }

            this.olItemsMissingReference.Sort((x, y) => x.Text.CompareTo(y.Text));

            this.updateList(this.listMissingAddons, this.olItemsMissingReference.ToArray());
        }

        private void updateCharacterTree()
        {
            this.treeCharacters.Nodes.Clear();

            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> wAccount in this.oAddonsScanner.filesWTF)
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

                this.treeCharacters.Nodes.Add(wAccountNode);
            }

            this.treeCharacters.ExpandAll();
        }

        private void updateListWTF()
        {

            List<ListViewItem> wlItems = new List<ListViewItem>();
            foreach (string wOrphans in this.oAddonsScanner.orphanSavedVariables)
            {
                ListViewItem lItem = new ListViewItem
                {
                    Text = wOrphans
                };
                lItem.SubItems.Add("Global");
                lItem.Group = this.listWTF.Groups[0];
                wlItems.Add(lItem);
            }

            foreach (string wOrphans in this.oAddonsScanner.orphanSavedVariablesPerCharacter)
            {
                ListViewItem lItem = new ListViewItem
                {
                    Text = wOrphans
                };
                lItem.SubItems.Add("Personnage");
                lItem.Group = this.listWTF.Groups[1];
                wlItems.Add(lItem);
            }

            updateList(this.listWTF, wlItems.ToArray());
        }
        private void updateListAllAddons()
        {
            List<KeyValuePair<string, Addon>> wList = this.oAddonsScanner.addons.ToList();
            Version wVersion = new Version(this.oCurrentVersion);

            switch (this.oAllAddonsSorting)
            {
                case 1:
                    wList.Sort((x, y) => x.Value.version.version.CompareTo(y.Value.version.version));
                    break;
                case 2:
                    wList.Sort((x, y) => x.Value.title.CompareTo(y.Value.title));
                    wList.Sort((x, y) =>
                    {
                        int acX = this.olAutoClean.Contains(x.Key) ? 1 : 0;
                        int acY = this.olAutoClean.Contains(y.Key) ? 1 : 0;

                        return acX - acY;
                    });
                    // Order by autoclean
                    break;

                default:
                    wList.Sort((x, y) => x.Value.title.CompareTo(y.Value.title));
                    break;
            }

            if (this.oAllAddonsSortingDesc)
            {
                wList.Reverse();
            }

            List<ListViewItem> wlItems = new List<ListViewItem>();
            foreach (KeyValuePair<string, Addon> wAddons in wList)
            {
                ListViewItem wlItem = new ListViewItem
                {
                    Name = wAddons.Key,
                    BackColor = this.olAutoClean.Contains(wAddons.Key) ? Color.LightSalmon : Color.LightBlue,
                    Text = wAddons.Value.title
                };
                wlItem.SubItems.Add(wAddons.Value.version.versionNum);
                wlItem.SubItems.Add(this.olAutoClean.Contains(wAddons.Key) ? "Oui" : "Non");
                wlItems.Add(wlItem);
            }
            updateList(this.listAllAddons, wlItems.ToArray());

        }



        #endregion
    }
}
