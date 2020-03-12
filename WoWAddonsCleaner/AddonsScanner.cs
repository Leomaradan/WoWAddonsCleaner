using System;
using System.Collections.Generic;
using System.IO;
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
                return this.oBasePath + Path.DirectorySeparatorChar + "WTF" + Path.DirectorySeparatorChar + "Account" + Path.DirectorySeparatorChar;
            }
        }

        private string pathInterface
        {
            get
            {
                return this.oBasePath + Path.DirectorySeparatorChar + "Interface" + Path.DirectorySeparatorChar + "Addons" + Path.DirectorySeparatorChar;
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
            DirectoryInfo wtfFolder = new DirectoryInfo(this.pathWTF);

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

            foreach (DirectoryInfo accountDir in wtfFolder.GetDirectories())
            {

                Dictionary<string, Dictionary<string, List<string>>> servers = new Dictionary<string, Dictionary<string, List<string>>>();

                foreach (DirectoryInfo serverDir in accountDir.GetDirectories())
                {

                    string servname = serverDir.Name.Replace('\'', '’');

                    if (!servname.Equals("SavedVariables"))
                    {

                        Dictionary<string, List<string>> characters = new Dictionary<string, List<string>>();

                        foreach (DirectoryInfo characterDir in serverDir.GetDirectories())
                        {

                            List<string> files = new List<string>();

                            DirectoryInfo[] savedVariableFolder = characterDir.GetDirectories("SavedVariables");

                            if (savedVariableFolder.Length == 1)
                            {
                                foreach (FileInfo wtfFile in savedVariableFolder[0].GetFiles("*.lua"))
                                {
                                    files.Add(wtfFile.FullName);
                                    string filename = wtfFile.Name.Substring(0, wtfFile.Name.Length - 4).ToLower();
                                    if (!oSavedVariablesPerCharacter.Contains(filename) && !oOrphanSavedVariablesPerCharacter.Contains(filename) && !filename.StartsWith("blizzard_"))
                                    {
                                        oOrphanSavedVariablesPerCharacter.Add(filename);
                                    }
                                }

                                foreach (FileInfo bakFile in savedVariableFolder[0].GetFiles("*.bak"))
                                {
                                    oBakFiles.Add(bakFile.FullName);
                                }
                            }

                            foreach (FileInfo file in characterDir.GetFiles("AddOns.txt"))
                            {
                                var lines = File.ReadAllLines(file.FullName);

                                oAddonsTxt.Add(file.FullName);

                                foreach (string line in lines)
                                {
                                    string addonName = line.Replace(": enabled", "").Replace(": disabled", "").ToLower().Trim();
                                    if (!oAddons.ContainsKey(addonName) && !this.oMissingAddonsExceptions.Contains(addonName))
                                    {
                                        if (!oMissingAddons.ContainsKey(addonName))
                                        {
                                            oMissingAddons.Add(addonName, new Dictionary<string, Dictionary<string, List<string>>>());
                                        }

                                        if (!oMissingAddons[addonName].ContainsKey(accountDir.Name))
                                        {
                                            oMissingAddons[addonName].Add(accountDir.Name, new Dictionary<string, List<string>>());
                                        }

                                        if (!oMissingAddons[addonName][accountDir.Name].ContainsKey(servname))
                                        {
                                            oMissingAddons[addonName][accountDir.Name].Add(servname, new List<string>());
                                        }

                                        oMissingAddons[addonName][accountDir.Name][servname].Add(characterDir.Name);
                                    }
                                }
                            }

                            characters.Add(characterDir.Name, files);
                        }

                        if (servers.ContainsKey(servname))
                        {

                            foreach (KeyValuePair<string, List<string>> c in characters)
                            {
                                if (!servers[servname].ContainsKey(c.Key))
                                {
                                    servers[servname].Add(c.Key, c.Value);
                                }
                            }

                        }
                        else
                        {
                            servers.Add(servname, characters);
                        }

                    }
                    else
                    {
                        foreach (FileInfo wtfFile in serverDir.GetFiles("*.lua"))
                        {
                            string filename = wtfFile.Name.Substring(0, wtfFile.Name.Length - 4).ToLower();
                            if (!oSavedVariables.Contains(filename) && !oOrphanSavedVariables.Contains(filename) && !filename.StartsWith("blizzard_"))
                            {
                                oOrphanSavedVariables.Add(filename);
                            }
                        }
                        foreach (FileInfo bakFile in serverDir.GetFiles("*.bak"))
                        {
                            oBakFiles.Add(bakFile.FullName);
                        }
                    }
                }

                oFilesWTF.Add(accountDir.Name, servers);
            }
        }

        private string cleanString(string source)
        {
            source = Regex.Replace(source, @"\|c[0-9a-fA-F]{8}", "");
            source = Regex.Replace(source, @"\|r", "");
            source = Regex.Replace(source, @"\<|\>", "");

            return source.Trim();
        }
        private void scanAddonFolder()
        {
            DirectoryInfo addonsFolder = new DirectoryInfo(pathInterface);
            var spliter = new char[] { ',', ' ' };


            //oMainAddons.Clear();
            //oSubAddons.Clear();
            oAddons.Clear();
            //oOrphanAddons.Clear();

            oSavedVariables.Clear();
            oSavedVariablesPerCharacter.Clear();


            foreach (DirectoryInfo dir in addonsFolder.GetDirectories())
            {
                foreach (FileInfo file in dir.GetFiles("*.toc"))
                {
                    Addon wAddon = new Addon();

                    var lines = File.ReadAllLines(file.FullName);
                    string title = null;
                    string titleLocalized = null;
                    string plainTitle = null;

                    string notes = null;
                    string notesLocalized = null;

                    string filename = file.Name.Substring(0, file.Name.Length - 4).ToLower();
                    foreach (var line in lines)
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

                    // Clean empty values
                    /*wAddon.dependencies = wAddon.dependencies.Where(x => x != "").ToList();
                    wAddon.optionalDependencies = wAddon.optionalDependencies.Where(x => x != "").ToList();
                    wAddon.savedVariables = wAddon.savedVariables.Where(x => x != "").ToList();
                    wAddon.savedVariablesPerCharacter = wAddon.savedVariablesPerCharacter.Where(x => x != "").ToList();*/

                    wAddon.filename = filename;

                    oAddons.Add(filename, wAddon);
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
            foreach (string bakFile in oBakFiles)
            {
                File.Delete(bakFile);
                this.progressBar.Value += 1;
            }
            this.progressBar.Visible = false;
            return oBakFiles.Count;
        }

        public void deleteCharacterVariables(string account, string server, string character)
        {
            DirectoryInfo wtfFolder;
            string pathServerA = this.pathWTF +
                account + Path.DirectorySeparatorChar +
                server + Path.DirectorySeparatorChar +
                character;
            string pathServerB = this.pathWTF +
                account + Path.DirectorySeparatorChar +
                server.Replace('’', '\'') + Path.DirectorySeparatorChar +
                character;

            if (Directory.Exists(pathServerA))
            {
                wtfFolder = new DirectoryInfo(pathServerA);

                if (wtfFolder.Parent.GetDirectories().Length == 1)
                {
                    wtfFolder.Parent.Delete(true);
                }
                else
                {
                    wtfFolder.Delete(true);
                }
            }

            if (Directory.Exists(pathServerB))
            {
                wtfFolder = new DirectoryInfo(pathServerB);

                if (wtfFolder.Parent.GetDirectories().Length == 1)
                {
                    wtfFolder.Parent.Delete(true);
                }
                else
                {
                    wtfFolder.Delete(true);
                }
            }

        }

        public void deleteServerVariables(string iAccount, string iServer)
        {

            string pathServerA = this.pathWTF +
                iAccount + Path.DirectorySeparatorChar +
                iServer;
            string pathServerB = this.pathWTF +
                iAccount + Path.DirectorySeparatorChar +
                iServer.Replace('’', '\'');

            if (Directory.Exists(pathServerA))
            {
                Directory.Delete(pathServerA, true);
            }

            if (Directory.Exists(pathServerB))
            {
                Directory.Delete(pathServerB, true);
            }
        }

        public void deleteAddons(List<string> iAddons)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = iAddons.Count;
            this.progressBar.Value = 0;

            foreach (string wAddon in iAddons)
            {
                Directory.Delete(pathInterface + wAddon, true);
                this.progressBar.Value += 1;
            }

            this.progressBar.Visible = false;

        }

        public void patchAddonsVersion(List<string> iAddons, string iVersion)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = iAddons.Count;
            this.progressBar.Value = 0;

            foreach (string wAddon in iAddons)
            {
                FileInfo fi = new FileInfo(this.pathInterface + wAddon + Path.DirectorySeparatorChar + wAddon + ".toc");

                if (fi.Exists)
                {
                    string[] lines = File.ReadAllLines(fi.FullName);
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

                    File.WriteAllLines(fi.FullName, newLines.ToArray());
                    this.progressBar.Value += 1;
                }
            }

            this.progressBar.Visible = false;
        }

        public void removeMissingAddonsReferences(List<string> iAddons, bool iSort = false)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = 1;
            this.progressBar.Value = 0;

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
                this.progressBar.Value += 1;

                if (iSort)
                {
                    purge.Value.Sort((x, y) => x.CompareTo(y));
                }

                File.WriteAllLines(this.pathWTF + purge.Key, purge.Value.ToArray());
            }
            this.progressBar.Visible = false;
        }

        public void sortAddonsReferences()
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = this.oAddonsTxt.Count;
            this.progressBar.Value = 0;

            foreach (string addonPath in this.oAddonsTxt)
            {
                List<string> lines = new List<string>(File.ReadAllLines(addonPath));

                lines.Sort((x, y) => x.CompareTo(y));

                // File.WriteAllLines(addonPath, lines.ToArray());
                this.progressBar.Value += 1;
            }
            this.progressBar.Visible = false;
        }

        private void prepareRemovingMissingAddonsReferences(string iAccount, string iServer, string iCharacter, string iAddon)
        {
            string filename = iAccount
                + Path.DirectorySeparatorChar
                + iServer
                + Path.DirectorySeparatorChar
                + iCharacter
                + Path.DirectorySeparatorChar
                + "AddOns.txt";

            FileInfo fi = new FileInfo(this.pathWTF + filename);

            if (fi.Exists)
            {
                if (!oPurgeAddons.ContainsKey(filename))
                {
                    oPurgeAddons.Add(filename, new List<string>(File.ReadAllLines(this.pathWTF + filename)));
                }

                List<string> newLines = new List<string>();

                foreach (string line in oPurgeAddons[filename])
                {
                    string addonName = line.Replace(": enabled", "").Replace(": disabled", "").ToLower().Trim();
                    if (addonName != iAddon)
                    {
                        newLines.Add(line);
                    }
                }

                oPurgeAddons[filename] = newLines;
            }
        }

        public void deleteWTFFiles(Dictionary<string, string> iWTFFiles)
        {
            this.progressBar.Visible = true;
            this.progressBar.Maximum = iWTFFiles.Count * 2;
            this.progressBar.Value = 0;

            DirectoryInfo di = new DirectoryInfo(this.pathWTF);

            foreach (KeyValuePair<string, string> wtf in iWTFFiles)
            {
                //var accounts = di.GetDirectories();
                foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> account in oFilesWTF)
                {

                    if (wtf.Value == "Global")
                    {
                        File.Delete(this.pathWTF + account.Key + @"\SavedVariables\" + wtf.Key + ".lua");
                        File.Delete(this.pathWTF + account.Key + @"\SavedVariables\" + wtf.Key + ".lua.bak");
                        this.progressBar.Value += 2;
                    }
                    else
                    {

                        foreach (KeyValuePair<string, Dictionary<string, List<string>>> server in account.Value)
                        {
                            foreach (KeyValuePair<string, List<string>> character in server.Value)
                            {
                                this.progressBar.Maximum += 4;

                                string pathServerA = this.pathWTF + account.Key +
                                    Path.DirectorySeparatorChar + server.Key +
                                    Path.DirectorySeparatorChar + character.Key +
                                    @"\SavedVariables\";

                                string pathServerB = this.pathWTF + account.Key +
                                    Path.DirectorySeparatorChar + server.Key.Replace('’', '\'') +
                                    Path.DirectorySeparatorChar + character.Key +
                                    @"\SavedVariables\";

                                if (Directory.Exists(pathServerA))
                                {
                                    File.Delete(pathServerA + wtf.Key + ".lua");
                                    File.Delete(pathServerA + wtf.Key + ".lua.bak");
                                }
                                this.progressBar.Value += 2;

                                if (Directory.Exists(pathServerB))
                                {
                                    File.Delete(pathServerB + wtf.Key + ".lua");
                                    File.Delete(pathServerB + wtf.Key + ".lua.bak");
                                }
                                this.progressBar.Value += 2;
                            }
                        }
                    }
                }
            }
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