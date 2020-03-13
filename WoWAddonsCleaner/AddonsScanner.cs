using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WoWAddonsCleaner
{

    internal class AddonsScanner
    {
        string oBasePath;

        //List<string> addons
        bool oHasScan = false;

        //private IndexedHashSet oMainAddons;

        private HashSet<string> oOrphanAddons;

        //private Dictionary<string, string[]> oSubAddons;

        private Dictionary<string, List<string>> oPurgeAddons;
        private Dictionary<string, Addon> oAddons;
        private Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> oFilesWTF;
        private Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> oMissingAddons;

        private List<string> oSavedVariables;
        private List<string> oSavedVariablesPerCharacter;
        private List<string> oOrphanSavedVariables;
        private List<string> oOrphanSavedVariablesPerCharacter;
        private List<string> oBakFiles;
        private List<string> oAddonsTxt;
        private List<string> oMissingAddonsExceptions;

        private List<string> oAllProperties;

        private ToolStripProgressBar progressBar;

        private string lang;

        internal AddonsScanner(string iPath, List<string> missingAddonsExceptions, string lang, ToolStripProgressBar iProgressBar)
        {
            this.oBasePath = iPath;

            //this.oMainAddons = new IndexedHashSet();
            this.oOrphanAddons = new HashSet<string>();

            //this.oSubAddons = new Dictionary<string, string[]>();

            this.oPurgeAddons = new Dictionary<string, List<string>>();
            this.oAddons = new Dictionary<string, Addon>();
            this.oFilesWTF = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();
            this.oMissingAddons = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();

            this.oSavedVariables = new List<string>();
            this.oSavedVariablesPerCharacter = new List<string>();
            this.oOrphanSavedVariables = new List<string>();
            this.oOrphanSavedVariablesPerCharacter = new List<string>();
            this.oBakFiles = new List<string>();
            this.oAddonsTxt = new List<string>();
            this.oMissingAddonsExceptions = missingAddonsExceptions;

            this.oAllProperties = new List<string>();

            this.progressBar = iProgressBar;

            this.lang = lang;
        }

        #region Getter
        private string pathWTF
        {
            get
            {
                return FileOperations.ResolvePath(this.oBasePath, "WTF", "Account");
            }
        }

        private string pathInterface
        {
            get
            {
                return FileOperations.ResolvePath(this.oBasePath, "Interface", "Addons");
            }
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> filesWTF
        {
            get
            {
                if (!oHasScan)
                {
                    doScan();
                }

                return oFilesWTF;
            }
        }

        public HashSet<string> orphanAddons
        {
            get
            {
                if (!oHasScan)
                {
                    doScan();
                }

                return oOrphanAddons;
            }
        }

        public Dictionary<string, Addon> addons
        {
            get
            {
                if (!oHasScan)
                {
                    doScan();
                }

                return oAddons;
            }
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> missingAddonReferences
        {
            get
            {
                if (!oHasScan)
                {
                    doScan();
                }

                return oMissingAddons;
            }
        }
        public List<string> orphanSavedVariables
        {
            get
            {
                if (!oHasScan)
                {
                    doScan();
                }

                oOrphanSavedVariables.Sort();

                return oOrphanSavedVariables;
            }
        }

        public List<string> orphanSavedVariablesPerCharacter
        {
            get
            {
                if (!oHasScan)
                {
                    doScan();
                }

                oOrphanSavedVariablesPerCharacter.Sort();

                return oOrphanSavedVariablesPerCharacter;
            }
        }
        #endregion

        #region Scan
        private void scanWTFFolder()
        {
            if (oAddons.Count == 0)
            {
                throw new Exception("scanWTFFolder called before populating Addon List");
            }

            oOrphanSavedVariables.Clear();
            oOrphanSavedVariablesPerCharacter.Clear();
            oMissingAddons.Clear();

            oFilesWTF.Clear();
            oBakFiles.Clear();
            oAddonsTxt.Clear();

            foreach (string wAccountDirPath in FileOperations.GetDirectories(this.pathWTF))
            {
                string wAccountDirName = FileOperations.ExtractFileName(wAccountDirPath);
                Dictionary<string, Dictionary<string, List<string>>> servers = new Dictionary<string, Dictionary<string, List<string>>>();

                foreach (string wServerDirPath in FileOperations.GetDirectories(wAccountDirPath))
                {

                    string wServerDirName = FileOperations.ExtractFileName(wServerDirPath).Replace('\'', '’');

                    if (!wServerDirName.Equals("SavedVariables"))
                    {

                        Dictionary<string, List<string>> wCharacters = new Dictionary<string, List<string>>();

                        foreach (string wCharacterDirPath in FileOperations.GetDirectories(wServerDirPath))
                        {
                            string wCharacterDirName = FileOperations.ExtractFileName(wCharacterDirPath);

                            List<string> wLuaFiles = new List<string>();

                            string wSavedVariablePath = FileOperations.ResolvePath(wCharacterDirPath, "SavedVariables");

                            if (FileOperations.DirectoryExists(wSavedVariablePath))
                            {
                                foreach (string wLuaPath in FileOperations.GetFiles(wSavedVariablePath, "*.lua"))
                                {
                                    wLuaFiles.Add(wLuaPath);
                                    string wFilename = FileOperations.ExtractFileName(wLuaPath);
                                    wFilename = wFilename.Substring(0, wFilename.Length - 4).ToLower();
                                    if (!oSavedVariablesPerCharacter.Contains(wFilename) && !oOrphanSavedVariablesPerCharacter.Contains(wFilename) && !wFilename.StartsWith("blizzard_"))
                                    {
                                        oOrphanSavedVariablesPerCharacter.Add(wFilename);
                                    }
                                }

                                foreach (string wBakFiles in FileOperations.GetFiles(wSavedVariablePath, "*.bak"))
                                {
                                    oBakFiles.Add(wBakFiles);
                                }
                            }

                            foreach (string wAddonsTxtPath in FileOperations.GetFiles(wCharacterDirPath, "AddOns.txt"))
                            {


                                var wLines = FileOperations.ReadFile(wAddonsTxtPath);

                                if (wLines != null)
                                {
                                    oAddonsTxt.Add(FileOperations.ExtractFileName(wAddonsTxtPath));

                                    foreach (string wLine in wLines)
                                    {
                                        string wAddonName = wLine.Replace(": enabled", "").Replace(": disabled", "").ToLower().Trim();
                                        if (!oAddons.ContainsKey(wAddonName) && !this.oMissingAddonsExceptions.Contains(wAddonName))
                                        {
                                            if (!oMissingAddons.ContainsKey(wAddonName))
                                            {
                                                oMissingAddons.Add(wAddonName, new Dictionary<string, Dictionary<string, List<string>>>());
                                            }

                                            if (!oMissingAddons[wAddonName].ContainsKey(wAccountDirName))
                                            {
                                                oMissingAddons[wAddonName].Add(wAccountDirName, new Dictionary<string, List<string>>());
                                            }

                                            if (!oMissingAddons[wAddonName][wAccountDirName].ContainsKey(wServerDirName))
                                            {
                                                oMissingAddons[wAddonName][wAccountDirName].Add(wServerDirName, new List<string>());
                                            }

                                            oMissingAddons[wAddonName][wAccountDirName][wServerDirName].Add(wCharacterDirName);
                                        }
                                    }
                                }


                            }

                            wCharacters.Add(wCharacterDirName, wLuaFiles);
                        }

                        if (servers.ContainsKey(wServerDirName))
                        {

                            foreach (KeyValuePair<string, List<string>> wCharacter in wCharacters)
                            {
                                if (!servers[wServerDirName].ContainsKey(wCharacter.Key))
                                {
                                    servers[wServerDirName].Add(wCharacter.Key, wCharacter.Value);
                                }
                            }

                        }
                        else
                        {
                            servers.Add(wServerDirName, wCharacters);
                        }

                    }
                    else
                    {
                        foreach (string wLuaPath in FileOperations.GetFiles(wServerDirPath, "*.lua"))
                        {
                            string wFilename = FileOperations.ExtractFileName(wLuaPath);
                            string wLuaName = wFilename.Substring(0, wFilename.Length - 4).ToLower();
                            if (!oSavedVariables.Contains(wLuaName) && !oOrphanSavedVariables.Contains(wLuaName) && !wLuaName.StartsWith("blizzard_"))
                            {
                                oOrphanSavedVariables.Add(wLuaName);
                            }
                        }
                        foreach (string wBakPath in FileOperations.GetFiles(wServerDirPath, "*.bak"))
                        {
                            oBakFiles.Add(wBakPath);
                        }
                    }
                }

                oFilesWTF.Add(wAccountDirName, servers);
            }
        }

        private string cleanString(string wSource)
        {
            wSource = Regex.Replace(wSource, @"\|c[0-9a-fA-F]{8}", "");
            wSource = Regex.Replace(wSource, @"\|r", "");
            wSource = Regex.Replace(wSource, @"\<|\>", "");

            return wSource.Trim();
        }
        private void scanAddonFolder()
        {
            //DirectoryInfo addonsFolder = new DirectoryInfo(pathInterface);
            var spliter = new char[] { ',', ' ' };


            //oMainAddons.Clear();
            //oSubAddons.Clear();
            oAddons.Clear();
            //oOrphanAddons.Clear();

            oSavedVariables.Clear();
            oSavedVariablesPerCharacter.Clear();


            foreach (string wInterfacePath in FileOperations.GetDirectories(pathInterface))
            {
                foreach (string wTocPath in FileOperations.GetFiles(wInterfacePath, "*.toc"))
                {
                    Addon wAddon = new Addon();

                    string wTocName = FileOperations.ExtractFileName(wTocPath);
                    var wLines = FileOperations.ReadFile(wTocPath);
                    string title = null;
                    string titleLocalized = null;
                    string plainTitle = null;

                    string notes = null;
                    string notesLocalized = null;

                    string filename = wTocName.Substring(0, wTocName.Length - 4).ToLower();
                    if (wLines != null)
                    {
                        foreach (var line in wLines)
                        {
                            int wDotPos = line.IndexOf(':') + 1;

                            if (line.StartsWith("## Interface:"))
                            {
                                wAddon.version = new Version(line.Substring(wDotPos).ToLower().Trim());
                            }

                            if (line.StartsWith("## Plain Title:"))
                            {
                                plainTitle = cleanString(line.Substring(wDotPos));
                            }

                            if (line.StartsWith("## Title:"))
                            {
                                title = cleanString(line.Substring(wDotPos));
                            }

                            if (line.StartsWith("## Title-" + this.lang + ":"))
                            {
                                titleLocalized = cleanString(line.Substring(wDotPos));
                            }

                            if (line.StartsWith("## Notes:"))
                            {
                                notes = cleanString(line.Substring(wDotPos));
                            }

                            if (line.StartsWith("## Notes-" + this.lang + ":"))
                            {
                                notesLocalized = cleanString(line.Substring(wDotPos));
                            }

                            if (line.StartsWith("## Dep") || line.StartsWith("## RequiredDeps")) //Dependencies, RequiredDeps, Dep*
                            {
                                /*if (oSubAddons.ContainsKey(filename))
                                {
                                    List<string> arr = new List<string>(oSubAddons[filename]);
                                    arr.AddRange(line.Substring(16).ToLower().Trim().Split(','));
                                    oSubAddons[filename] = arr.ToArray();
                                }
                                else
                                {
                                    oSubAddons.Add(filename, line.Substring(17).ToLower().Split(','));
                                }*/

                                //new char[] { ']', '[' }, StringSplitOptions.RemoveEmptyEntries
                                wAddon.dependencies.AddRange(line.Substring(wDotPos).ToLower().Trim().Split(spliter, StringSplitOptions.RemoveEmptyEntries));

                            }

                            if (line.StartsWith("## OptionalDependencies") || line.StartsWith("## OptionalDep")) //Dependencies, RequiredDeps, Dep*
                            {
                                wAddon.optionalDependencies.AddRange(line.Substring(wDotPos).ToLower().Trim().Split(spliter, StringSplitOptions.RemoveEmptyEntries));
                            }

                            if (line.StartsWith("## Author") || line.StartsWith("## Autor")) //Dependencies, RequiredDeps, Dep*
                            {
                                wAddon.author = line.Substring(wDotPos).ToLower().Trim();
                            }
                            /*else if (line.StartsWith("RequiredDeps:"))
                            {
                                oSubAddons.Add(filename, line.Substring(13).ToLower().Trim().Split(','));
                            }*/
                            else
                            {
                                //oMainAddons.AddOrUpdate(filename, new HashSet<string>());
                            }

                            if (line.StartsWith("## SavedVariables:"))
                            {
                                oSavedVariables.Add(filename);
                                wAddon.savedVariables.AddRange(line.Substring(wDotPos).ToLower().Trim().Split(spliter, StringSplitOptions.RemoveEmptyEntries));
                            }
                            else if (line.StartsWith("## SavedVariablesPerCharacter:"))
                            {
                                oSavedVariablesPerCharacter.Add(filename);
                                wAddon.savedVariablesPerCharacter.AddRange(line.Substring(wDotPos).ToLower().Trim().Split(spliter, StringSplitOptions.RemoveEmptyEntries));
                            }

                        }

                        if (title != null || plainTitle != null || titleLocalized != null)
                        {
                            if (titleLocalized != null)
                            {
                                wAddon.title = titleLocalized;
                            }
                            else if (plainTitle != null)
                            {
                                wAddon.title = plainTitle;

                            }
                            else
                            {
                                wAddon.title = title;
                            }

                        }

                        if (notes != null || notesLocalized != null)
                        {
                            wAddon.notes = notesLocalized ?? notes;
                        }

                        wAddon.filename = filename;

                        oAddons.Add(filename, wAddon);
                    }

                }

            }
        }

        private void checkOrphanAddons()
        {
            foreach (KeyValuePair<string, Addon> sub in oAddons) // child, parent
            {
                Addon wAddon = sub.Value;
                for (int i = 0; i < wAddon.dependencies.Count; i++)
                {
                    string key = wAddon.dependencies[i].Trim();
                    if (oAddons.ContainsKey(key))
                    {
                        oAddons[key].subAddons.Add(sub.Key, this.oAddons[sub.Key]);
                    }
                    else if (key.StartsWith("blizzard_"))
                    {
                        // nothing to do
                    }
                    else
                    {
                        oOrphanAddons.Add(sub.Key);
                    }
                }

            }
        }


        #endregion

        #region Actions
        public void clearScan()
        {
            this.oHasScan = false;
        }

        public void doScan()
        {
            this.scanAddonFolder();
            this.scanWTFFolder();
            this.checkOrphanAddons();
            this.oHasScan = true;
        }

        public int deleteBakFiles()
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = oBakFiles.Count;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            foreach (string bakFile in oBakFiles)
            {
                wFI.deleteFile(bakFile);

            }

            wFI.execute();

            this.progressBar.Visible = false;
            return oBakFiles.Count;
        }

        public void deleteCharacterVariables(string iAccount, string iServer, string iCharacter)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = 1;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            string wServerAPath = FileOperations.ResolvePath(this.pathWTF, iAccount, iServer, iCharacter);
            string wServerBPath = FileOperations.ResolvePath(this.pathWTF, iAccount, iServer.Replace('’', '\''), iCharacter);

            if (FileOperations.DirectoryExists(wServerAPath))
            {

                this.progressBar.Maximum += 1;
                string wParentPath = FileOperations.GetParent(wServerAPath);

                if (FileOperations.GetDirectories(wParentPath).Count == 1)
                {
                    wFI.deleteDirectory(wParentPath, true);
                }
                else
                {
                    wFI.deleteDirectory(wServerAPath, true);
                }
            }

            if (FileOperations.DirectoryExists(wServerBPath))
            {

                this.progressBar.Maximum += 1;
                string wParentPath = FileOperations.GetParent(wServerBPath);

                if (FileOperations.GetDirectories(wParentPath).Count == 1)
                {
                    wFI.deleteDirectory(wParentPath, true);
                }
                else
                {
                    wFI.deleteDirectory(wServerBPath, true);
                }
            }

            wFI.execute();
            this.progressBar.Visible = false;

        }

        public void deleteServerVariables(string iAccount, string iServer)
        {

            this.progressBar.Visible = true;
            this.progressBar.Maximum = 1;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            string pathServerA = FileOperations.ResolvePath(this.pathWTF, iAccount, iServer);
            string pathServerB = FileOperations.ResolvePath(this.pathWTF, iAccount, iServer.Replace('’', '\''));

            if (FileOperations.DirectoryExists(pathServerA))
            {
                this.progressBar.Maximum += 1;
                wFI.deleteDirectory(pathServerA, true);
            }

            if (FileOperations.DirectoryExists(pathServerB))
            {
                this.progressBar.Maximum += 1;
                wFI.deleteDirectory(pathServerB, true);
            }

            wFI.execute();
            this.progressBar.Visible = false;
        }

        public void deleteAddons(List<string> iAddons)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = iAddons.Count;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            foreach (string wAddon in iAddons)
            {
                wFI.deleteDirectory(FileOperations.ResolvePath(this.pathInterface, wAddon), true);
            }

            wFI.execute();

            this.progressBar.Visible = false;

        }

        public void patchAddonsVersion(List<string> iAddons, string iVersion)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = iAddons.Count;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            foreach (string wAddon in iAddons)
            {
                //FileInfo fi = new FileInfo();
                string wFullPath = FileOperations.ResolvePath(this.pathInterface, wAddon, wAddon + ".toc");

                if (FileOperations.FileExists(wFullPath))
                {
                    string[] lines = FileOperations.ReadFile(wFullPath);

                    if (lines != null)
                    {
                        List<string> newLines = new List<string>();

                        foreach (string line in lines)
                        {
                            if (line.StartsWith("## Interface:"))
                            {
                                newLines.Add("## Interface: " + iVersion);
                                newLines.Add("# WoWAddonsCleaner-Patch: Patched Version from " + line.Substring(13).ToLower().Trim());
                            }
                            else if (line.StartsWith("# WoWAddonsCleaner-Patch: "))
                            {
                                // Skip the line
                            }
                            else
                            {
                                newLines.Add(line);
                            }
                        }

                        wFI.replaceFile(wFullPath, newLines);
                    }
                }
            }

            wFI.execute();
            this.progressBar.Visible = false;
        }

        public void removeMissingAddonsReferences(List<string> iAddons, bool iSort = false)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = 1;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            foreach (var wAddon in oMissingAddons)
            {
                if (iAddons.Contains(wAddon.Key))
                {
                    foreach (var wAccount in wAddon.Value)
                    {
                        foreach (var wServer in wAccount.Value)
                        {
                            foreach (var wCharacter in wServer.Value)
                            {

                                string cleanServerName = wServer.Key.Replace('’', '\'');
                                this.progressBar.Maximum += 2;
                                prepareRemovingMissingAddonsReferences(wAccount.Key, cleanServerName, wCharacter, wAddon.Key.ToLower());
                                prepareRemovingMissingAddonsReferences(wAccount.Key, wServer.Key.Replace('\'', '’'), wCharacter, wAddon.Key.ToLower());

                            }
                        }
                    }
                }
            }

            this.progressBar.Value = this.progressBar.Maximum - oPurgeAddons.Count;

            foreach (KeyValuePair<string, List<string>> purge in oPurgeAddons)
            {

                if (iSort)
                {
                    purge.Value.Sort((x, y) => x.CompareTo(y));
                }

                wFI.replaceFile(FileOperations.ResolvePath(this.pathWTF, purge.Key), purge.Value);

            }

            wFI.execute();
            this.progressBar.Visible = false;
        }

        public void sortAddonsReferences()
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = this.oAddonsTxt.Count;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            foreach (string addonPath in this.oAddonsTxt)
            {
                string[] wLines = FileOperations.ReadFile(addonPath);
                if (wLines != null)
                {
                    List<string> wListLines = new List<string>(wLines);
                    wListLines.Sort((x, y) => x.CompareTo(y));
                    // wFI.replaceFile(addonPath, wListLines);
                }
            }

            wFI.execute();
            this.progressBar.Visible = false;

        }

        private void prepareRemovingMissingAddonsReferences(string iAccount, string iServer, string iCharacter, string iAddon)
        {
            string wFilename = FileOperations.ResolvePath(iAccount, iServer, iCharacter, "AddOns.txt");
            string wPath = FileOperations.ResolvePath(this.pathWTF, wFilename);

            if (FileOperations.FileExists(wPath))
            {
                if (!oPurgeAddons.ContainsKey(wFilename))
                {
                    string[] wLines = FileOperations.ReadFile(wPath);
                    if (wLines != null)
                    {
                        oPurgeAddons.Add(wFilename, new List<string>(wLines));
                    }

                }

                List<string> wNewLines = new List<string>();

                foreach (string wLine in oPurgeAddons[wFilename])
                {
                    string wAddonName = wLine.Replace(": enabled", "").Replace(": disabled", "").ToLower().Trim();
                    if (wAddonName != iAddon)
                    {
                        wNewLines.Add(wLine);
                    }
                }

                oPurgeAddons[wFilename] = wNewLines;
            }
        }

        public void deleteWTFFiles(Dictionary<string, string> iWTFFiles)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = iWTFFiles.Count * 2;
            this.progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                this.progressBar.Value += 1;
            });

            foreach (KeyValuePair<string, string> wtf in iWTFFiles)
            {
                foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> account in oFilesWTF)
                {

                    if (wtf.Value == "Global")
                    {
                        wFI.deleteFile(FileOperations.ResolvePath(this.pathWTF, account.Key, "SavedVariables", wtf.Key + ".lua"));
                        wFI.deleteFile(FileOperations.ResolvePath(this.pathWTF, account.Key, "SavedVariables", wtf.Key + ".lua.bak"));
                    }
                    else
                    {

                        foreach (KeyValuePair<string, Dictionary<string, List<string>>> server in account.Value)
                        {
                            foreach (KeyValuePair<string, List<string>> character in server.Value)
                            {

                                string pathServerA = FileOperations.ResolvePath(this.pathWTF, account.Key, server.Key, character.Key, "SavedVariables");
                                string pathServerB = FileOperations.ResolvePath(this.pathWTF, account.Key, server.Key.Replace('’', '\''), character.Key, "SavedVariables");

                                if (FileOperations.DirectoryExists(pathServerA))
                                {
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerA, wtf.Key + ".lua"));
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerA, wtf.Key + ".lua.bak"));
                                }
                                this.progressBar.Value += 2;

                                if (FileOperations.DirectoryExists(pathServerB))
                                {
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerB, wtf.Key + ".lua"));
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerB, wtf.Key + ".lua.bak"));
                                }
                                this.progressBar.Value += 2;
                            }
                        }
                    }
                }
            }

            wFI.execute();
            this.progressBar.Visible = false;
        }

        #endregion

    }

    internal class IndexedHashSet : Dictionary<string, HashSet<string>>
    {
        public void AddOrUpdate(string key, HashSet<string> value)
        {
            if (this.ContainsKey(key))
            {
                this[key].UnionWith(value);
            }
            else
            {
                this.Add(key, value);
            }
        }

        public void AddOrUpdate(string key, string value)
        {
            if (this.ContainsKey(key))
            {
                if (!this[key].Contains(value))
                {
                    this[key].Add(value);
                }

            }
            else
            {
                this.Add(key, new HashSet<string>());
                this[key].Add(value);
            }
        }
    }
}