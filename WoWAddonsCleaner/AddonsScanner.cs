using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WoWAddonsCleaner
{

    internal class AddonsScanner
    {
        private readonly string oBasePath;

        //List<string> addons
        private bool oHasScan = false;

        //private IndexedHashSet oMainAddons;

        private readonly HashSet<string> oOrphanAddons;

        //private Dictionary<string, string[]> oSubAddons;

        private readonly Dictionary<string, List<string>> oPurgeAddons;
        private readonly Dictionary<string, Addon> oAddons;
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> oFilesWTF;
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> oMissingAddons;

        private readonly List<string> oSavedVariables;
        private readonly List<string> oSavedVariablesPerCharacter;
        private readonly List<string> oOrphanSavedVariables;
        private readonly List<string> oOrphanSavedVariablesPerCharacter;
        private readonly List<string> oBakFiles;
        private readonly List<string> oAddonsTxt;
        private readonly List<string> oMissingAddonsExceptions;

        private readonly List<string> oAllProperties;

        private readonly ToolStripProgressBar progressBar;

        private readonly string lang;

        internal AddonsScanner(string iPath, List<string> missingAddonsExceptions, string lang, ToolStripProgressBar iProgressBar)
        {
            oBasePath = iPath;

            //this.oMainAddons = new IndexedHashSet();
            oOrphanAddons = new HashSet<string>();

            //this.oSubAddons = new Dictionary<string, string[]>();

            oPurgeAddons = new Dictionary<string, List<string>>();
            oAddons = new Dictionary<string, Addon>();
            oFilesWTF = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();
            oMissingAddons = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();

            oSavedVariables = new List<string>();
            oSavedVariablesPerCharacter = new List<string>();
            oOrphanSavedVariables = new List<string>();
            oOrphanSavedVariablesPerCharacter = new List<string>();
            oBakFiles = new List<string>();
            oAddonsTxt = new List<string>();
            oMissingAddonsExceptions = missingAddonsExceptions;

            oAllProperties = new List<string>();

            progressBar = iProgressBar;

            this.lang = lang;
        }

        #region Getter
        private string pathWTF => FileOperations.ResolvePath(oBasePath, "WTF", "Account");

        private string pathInterface => FileOperations.ResolvePath(oBasePath, "Interface", "Addons");

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

            foreach (string wAccountDirPath in FileOperations.GetDirectories(pathWTF))
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


                                string[] wLines = FileOperations.ReadFile(wAddonsTxtPath);

                                if (wLines != null)
                                {
                                    oAddonsTxt.Add(FileOperations.ExtractFileName(wAddonsTxtPath));

                                    foreach (string wLine in wLines)
                                    {
                                        string wAddonName = wLine.Replace(": enabled", "").Replace(": disabled", "").ToLower().Trim();
                                        if (!oAddons.ContainsKey(wAddonName) && !oMissingAddonsExceptions.Contains(wAddonName))
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
            char[] spliter = new char[] { ',', ' ' };


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
                    string[] wLines = FileOperations.ReadFile(wTocPath);
                    string title = null;
                    string titleLocalized = null;
                    string plainTitle = null;

                    string notes = null;
                    string notesLocalized = null;

                    string filename = wTocName.Substring(0, wTocName.Length - 4).ToLower();
                    if (wLines != null)
                    {
                        foreach (string line in wLines)
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

                            if (line.StartsWith("## Title-" + lang + ":"))
                            {
                                titleLocalized = cleanString(line.Substring(wDotPos));
                            }

                            if (line.StartsWith("## Notes:"))
                            {
                                notes = cleanString(line.Substring(wDotPos));
                            }

                            if (line.StartsWith("## Notes-" + lang + ":"))
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
                        oAddons[key].subAddons.Add(sub.Key, oAddons[sub.Key]);
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
            oHasScan = false;
        }

        public void doScan()
        {
            scanAddonFolder();
            scanWTFFolder();
            checkOrphanAddons();
            oHasScan = true;
        }

        public int deleteBakFiles()
        {
            progressBar.Visible = true;
            progressBar.Maximum = oBakFiles.Count;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            foreach (string bakFile in oBakFiles)
            {
                wFI.deleteFile(bakFile);

            }

            wFI.execute();

            progressBar.Visible = false;
            return oBakFiles.Count;
        }

        public void deleteCharacterVariables(string iAccount, string iServer, string iCharacter)
        {
            progressBar.Visible = true;
            progressBar.Maximum = 1;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            string wServerAPath = FileOperations.ResolvePath(pathWTF, iAccount, iServer, iCharacter);
            string wServerBPath = FileOperations.ResolvePath(pathWTF, iAccount, iServer.Replace('’', '\''), iCharacter);

            if (FileOperations.DirectoryExists(wServerAPath))
            {

                progressBar.Maximum += 1;
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

                progressBar.Maximum += 1;
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
            progressBar.Visible = false;

        }

        public void deleteServerVariables(string iAccount, string iServer)
        {

            progressBar.Visible = true;
            progressBar.Maximum = 1;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            string pathServerA = FileOperations.ResolvePath(pathWTF, iAccount, iServer);
            string pathServerB = FileOperations.ResolvePath(pathWTF, iAccount, iServer.Replace('’', '\''));

            if (FileOperations.DirectoryExists(pathServerA))
            {
                progressBar.Maximum += 1;
                wFI.deleteDirectory(pathServerA, true);
            }

            if (FileOperations.DirectoryExists(pathServerB))
            {
                progressBar.Maximum += 1;
                wFI.deleteDirectory(pathServerB, true);
            }

            wFI.execute();
            progressBar.Visible = false;
        }

        public void deleteAddons(List<string> iAddons)
        {
            progressBar.Visible = true;
            progressBar.Maximum = iAddons.Count;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            foreach (string wAddon in iAddons)
            {
                wFI.deleteDirectory(FileOperations.ResolvePath(pathInterface, wAddon), true);
            }

            wFI.execute();

            progressBar.Visible = false;

        }

        public void patchAddonsVersion(List<string> iAddons, string iVersion)
        {
            progressBar.Visible = true;
            progressBar.Maximum = iAddons.Count;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            foreach (string wAddon in iAddons)
            {
                //FileInfo fi = new FileInfo();
                string wFullPath = FileOperations.ResolvePath(pathInterface, wAddon, wAddon + ".toc");

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
            progressBar.Visible = false;
        }

        public void removeMissingAddonsReferences(List<string> iAddons, bool iSort = false)
        {
            progressBar.Visible = true;
            progressBar.Maximum = 1;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> wAddon in oMissingAddons)
            {
                if (iAddons.Contains(wAddon.Key))
                {
                    foreach (KeyValuePair<string, Dictionary<string, List<string>>> wAccount in wAddon.Value)
                    {
                        foreach (KeyValuePair<string, List<string>> wServer in wAccount.Value)
                        {
                            foreach (string wCharacter in wServer.Value)
                            {

                                string cleanServerName = wServer.Key.Replace('’', '\'');
                                progressBar.Maximum += 2;
                                prepareRemovingMissingAddonsReferences(wAccount.Key, cleanServerName, wCharacter, wAddon.Key.ToLower());
                                prepareRemovingMissingAddonsReferences(wAccount.Key, wServer.Key.Replace('\'', '’'), wCharacter, wAddon.Key.ToLower());

                            }
                        }
                    }
                }
            }

            progressBar.Value = progressBar.Maximum - oPurgeAddons.Count;

            foreach (KeyValuePair<string, List<string>> purge in oPurgeAddons)
            {

                if (iSort)
                {
                    purge.Value.Sort((x, y) => x.CompareTo(y));
                }

                wFI.replaceFile(FileOperations.ResolvePath(pathWTF, purge.Key), purge.Value);

            }

            wFI.execute();
            progressBar.Visible = false;
        }

        public void sortAddonsReferences()
        {
            progressBar.Visible = true;
            progressBar.Maximum = oAddonsTxt.Count;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            foreach (string addonPath in oAddonsTxt)
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
            progressBar.Visible = false;

        }

        private void prepareRemovingMissingAddonsReferences(string iAccount, string iServer, string iCharacter, string iAddon)
        {
            string wFilename = FileOperations.ResolvePath(iAccount, iServer, iCharacter, "AddOns.txt");
            string wPath = FileOperations.ResolvePath(pathWTF, wFilename);

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
            progressBar.Visible = true;
            progressBar.Maximum = iWTFFiles.Count * 2;
            progressBar.Value = 0;

            FileOperations wFI = new FileOperations(() =>
            {
                progressBar.Value += 1;
            });

            foreach (KeyValuePair<string, string> wtf in iWTFFiles)
            {
                foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> account in oFilesWTF)
                {

                    if (wtf.Value == "Global")
                    {
                        wFI.deleteFile(FileOperations.ResolvePath(pathWTF, account.Key, "SavedVariables", wtf.Key + ".lua"));
                        wFI.deleteFile(FileOperations.ResolvePath(pathWTF, account.Key, "SavedVariables", wtf.Key + ".lua.bak"));
                    }
                    else
                    {

                        foreach (KeyValuePair<string, Dictionary<string, List<string>>> server in account.Value)
                        {
                            foreach (KeyValuePair<string, List<string>> character in server.Value)
                            {

                                string pathServerA = FileOperations.ResolvePath(pathWTF, account.Key, server.Key, character.Key, "SavedVariables");
                                string pathServerB = FileOperations.ResolvePath(pathWTF, account.Key, server.Key.Replace('’', '\''), character.Key, "SavedVariables");

                                if (FileOperations.DirectoryExists(pathServerA))
                                {
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerA, wtf.Key + ".lua"));
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerA, wtf.Key + ".lua.bak"));
                                }
                                progressBar.Value += 2;

                                if (FileOperations.DirectoryExists(pathServerB))
                                {
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerB, wtf.Key + ".lua"));
                                    wFI.deleteFile(FileOperations.ResolvePath(pathServerB, wtf.Key + ".lua.bak"));
                                }
                                progressBar.Value += 2;
                            }
                        }
                    }
                }
            }

            wFI.execute();
            progressBar.Visible = false;
        }

        #endregion

    }

    internal class IndexedHashSet : Dictionary<string, HashSet<string>>
    {
        public void AddOrUpdate(string key, HashSet<string> value)
        {
            if (ContainsKey(key))
            {
                this[key].UnionWith(value);
            }
            else
            {
                Add(key, value);
            }
        }

        public void AddOrUpdate(string key, string value)
        {
            if (ContainsKey(key))
            {
                if (!this[key].Contains(value))
                {
                    this[key].Add(value);
                }

            }
            else
            {
                Add(key, new HashSet<string>());
                this[key].Add(value);
            }
        }
    }
}