using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WoWAddonsCleaner
{
    internal class AddonsScanner
    {
        string basePath;

        //List<string> addons
        bool hasScan = false;

        IndexedHashSet mainAddons;
        Dictionary<string, string[]> subAddons;
        //IndexedHashSet libAddons;
        HashSet<string> orphanAddons;
        //List<string> blizzardAddons;

        Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> wtfFiles;

        List<string> savedVariables;
        List<string> savedVariablesPerCharacter;
        List<string> orphanSavedVariables;
        List<string> orphanSavedVariablesPerCharacter;

        internal AddonsScanner(string path)
        {
            basePath = path;
            //addons = new List<string>();

            mainAddons = new IndexedHashSet();
            subAddons = new Dictionary<string, string[]>();
            //libAddons = new IndexedHashSet();
            orphanAddons = new HashSet<string>();
            //blizzardAddons = new List<string>();

            wtfFiles = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();

            savedVariables = new List<string>();
            savedVariablesPerCharacter = new List<string>();
            orphanSavedVariables = new List<string>();
            orphanSavedVariablesPerCharacter = new List<string>();


        }

        public void clearScan()
        {
            hasScan = false;
        }

        public void doScan()
        {
            hasScan = true;


            scanAddonFolder();

            checkOrphanAddons();

            scanWTFFolder();

        }

        private void scanWTFFolder()
        {
            DirectoryInfo wtfFolder = new DirectoryInfo(basePath + Path.DirectorySeparatorChar + @"WTF\Account");

            orphanSavedVariables.Clear();
            orphanSavedVariablesPerCharacter.Clear();

            wtfFiles.Clear();

            //wtf = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();


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
                                    if (!savedVariablesPerCharacter.Contains(filename) && !orphanSavedVariablesPerCharacter.Contains(filename)) {
                                        orphanSavedVariablesPerCharacter.Add(filename);
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

                    } else
                    {
                        foreach (FileInfo wtfFile in serverDir.GetFiles("*.lua"))
                        {
                            string filename = wtfFile.Name.Substring(0, wtfFile.Name.Length - 4).ToLower();
                            if (!savedVariables.Contains(filename) && !orphanSavedVariables.Contains(filename))
                            {
                                orphanSavedVariables.Add(filename);
                            }
                        }
                    }
                    //Distinct

                }

                wtfFiles.Add(accountDir.Name, servers);
            }
        }

        private void checkOrphanAddons()
        {
            Dictionary<string, string[]> copySubAddons = new Dictionary<string, string[]>(subAddons);
            foreach (KeyValuePair<string, string[]> sub in copySubAddons) // child, parent
            {
                for (int i = 0; i < sub.Value.Length; i++)
                {
                    string key = sub.Value[i].Trim();
                    if (mainAddons.ContainsKey(key))
                    {
                        mainAddons[key].Add(sub.Key);
                    }
                    else if (subAddons.ContainsKey(key))
                    {
                        mainAddons.AddOrUpdate(key, new HashSet<string>());
                        mainAddons[key].Add(sub.Key);
                    }
                    else if (key.StartsWith("blizzard_"))
                    {
                        // nothing to do
                    }
                    else
                    {
                        orphanAddons.Add(sub.Key);
                        subAddons.Remove(sub.Key);
                    }
                }

            }
        }

        private void scanAddonFolder()
        {
            //scan addon folder
            DirectoryInfo addonsFolder = new DirectoryInfo(basePath + Path.DirectorySeparatorChar + @"interface\addons");

            mainAddons.Clear();
            subAddons.Clear();
            orphanAddons.Clear();
            //blizzardAddons.Clear();

            savedVariables.Clear();
            savedVariablesPerCharacter.Clear();


            foreach (DirectoryInfo dir in addonsFolder.GetDirectories())
            {
                //dir.Delete(true);
                //addons.Add(dir.Name);
                foreach (FileInfo file in dir.GetFiles("*.toc"))
                {
                    var lines = File.ReadAllLines(file.FullName);
                    foreach (var line in lines)
                    {
                        string filename = file.Name.Substring(0, file.Name.Length - 4).ToLower();
                        /*if (line.StartsWith("## X-Category: Library"))
                        {
                            libAddons.AddOrUpdate(filename, new HashSet<string>());
                        } else*/
                        if (line.StartsWith("## Dependencies: "))
                        {
                            if(subAddons.ContainsKey(filename))
                            {
                                List<string> arr = new List<string>(subAddons[filename]);
                                arr.AddRange(line.Substring(17).ToLower().Split(','));
                                subAddons[filename] = arr.ToArray();
                            } else
                            {
                                subAddons.Add(filename, line.Substring(17).ToLower().Split(','));
                            }
                            
                        }
                        else if (line.StartsWith("RequiredDeps: "))
                        {
                            subAddons.Add(filename, line.Substring(14).ToLower().Split(','));
                        }
                        else
                        {
                            mainAddons.AddOrUpdate(filename, new HashSet<string>());
                        }

                        if (line.StartsWith("## SavedVariables: "))
                        {
                            savedVariables.Add(filename);
                            /*string[] savedVariablesFiles = line.Substring("## SavedVariables: ".Length - 1).Split(',');

                            foreach (string s in savedVariablesFiles)
                            {
                                savedVariables.Add(s);
                            }*/

                        }
                        else if (line.StartsWith("## SavedVariablesPerCharacter: "))
                        {
                            savedVariablesPerCharacter.Add(filename);
                            /*string[] savedVariablesFiles = line.Substring("## SavedVariablesPerCharacter: ".Length - 1).Split(',');

                            foreach (string s in savedVariablesFiles)
                            {
                                savedVariablesPerCharacter.Add(s);
                            }*/
                        }
                    }
                }

                /*foreach (FileInfo file in dir.GetFiles("Blizzard_*.pub"))
                {
                    blizzardAddons.Add(dir.Name.ToLower());
                }*/
            }
        }

        public HashSet<string> listOrphanAddons()
        {
            if (!hasScan)
            {
                doScan();
            }

            return orphanAddons;
        }

        public List<string> listOrphanSavedVariables()
        {
            if (!hasScan)
            {
                doScan();
            }

            orphanSavedVariables.Sort();

            return orphanSavedVariables;
        }

        public List<string> listOrphanSavedVariablesPerCharacter()
        {
            if (!hasScan)
            {
                doScan();
            }

            orphanSavedVariablesPerCharacter.Sort();

            return orphanSavedVariablesPerCharacter;
        }

        /*        public TreeNode listLibsAddons()
                {
                    if(libAddons == null)
                    {
                        doScan();
                    }

                    TreeNode response = new TreeNode("Librairies");

                    foreach(KeyValuePair<string, HashSet<string>> addon in libAddons)
                    {
                        response.Nodes.Add(addon.Key);
                    }

                    return response;
                }

                public TreeNode listMainAddons()
                {
                    if (mainAddons == null)
                    {
                        doScan();
                    }

                    List<TreeNode> nodes = new List<TreeNode>();

                    foreach (KeyValuePair<string, HashSet<string>> addon in mainAddons)
                    {
                        TreeNode node = new TreeNode(addon.Key);

                        foreach (string subaddon in addon.Value)
                        {
                            node.Nodes.Add(subaddon);
                        }

                        nodes.Add(node);
                    }

                    return new TreeNode("Addons principaux", nodes.ToArray());
                }
        */
        /*public List<string> listAddons()
        {
            if(addons == null)
            {
                doScan();
            }


            return addons;
        }*/

        public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> listCharacters()
        {

            if (!hasScan)
            {
                doScan();
            }

            return wtfFiles;
        }

        public void removeCharacter(string account, string server, string character)
        {
            DirectoryInfo wtfFolder;
            string pathServerA = basePath + Path.DirectorySeparatorChar + @"WTF\Account\" +
                account + Path.DirectorySeparatorChar +
                server + Path.DirectorySeparatorChar +
                character;
            string pathServerB = basePath + Path.DirectorySeparatorChar + @"WTF\Account\" +
                account + Path.DirectorySeparatorChar +
                server.Replace('’', '\'') + Path.DirectorySeparatorChar +
                character;

            if (Directory.Exists(pathServerA))
            {
                wtfFolder = new DirectoryInfo(pathServerA);

                if(wtfFolder.Parent.GetDirectories().Length == 1)
                {
                    wtfFolder.Parent.Delete(true);
                } else
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

        public void removeServer(string account, string server)
        {

            string pathServerA = basePath + Path.DirectorySeparatorChar + @"WTF\Account\" +
                account + Path.DirectorySeparatorChar +
                server;
            string pathServerB = basePath + Path.DirectorySeparatorChar + @"WTF\Account\" +
                account + Path.DirectorySeparatorChar +
                server.Replace('’', '\'');

            if (Directory.Exists(pathServerA))
            {
                Directory.Delete(pathServerA, true);
            }

            if (Directory.Exists(pathServerB))
            {
                Directory.Delete(pathServerB, true);
            }
        }

        public void removeAddons(List<string> addons)
        {
            foreach(string addon in addons)
            {
                Directory.Delete(basePath + Path.DirectorySeparatorChar + @"interface\addons\" + addon, true);
            }

        }

        public void removeWTF(Dictionary<string, string> wtfs)
        {

            DirectoryInfo di = new DirectoryInfo(basePath + Path.DirectorySeparatorChar + @"WTF\Account\");

            foreach (KeyValuePair<string, string> wtf in wtfs)
            {
                //var accounts = di.GetDirectories();
                foreach(KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> account in wtfFiles)
                {

                    if (wtf.Value == "Global")
                    {
                        File.Delete(basePath + Path.DirectorySeparatorChar + @"WTF\Account\" + account.Key + @"\SavedVariables\" + wtf.Key + ".lua");
                        File.Delete(basePath + Path.DirectorySeparatorChar + @"WTF\Account\" + account.Key + @"\SavedVariables\" + wtf.Key + ".lua.bak");
                    } else
                    {
                        foreach (KeyValuePair<string, Dictionary<string, List<string>>> server in account.Value)
                        {
                            foreach(KeyValuePair<string, List<string>> character in server.Value)
                            {
                                string pathServerA = basePath + Path.DirectorySeparatorChar + @"WTF\Account\" + account.Key +
                                    Path.DirectorySeparatorChar + server.Key +
                                    Path.DirectorySeparatorChar + character.Key +
                                    @"\SavedVariables\";

                                string pathServerB = basePath + Path.DirectorySeparatorChar + @"WTF\Account\" + account.Key +
                                    Path.DirectorySeparatorChar + server.Key.Replace('’', '\'') +
                                    Path.DirectorySeparatorChar + character.Key +
                                    @"\SavedVariables\";

                                if (Directory.Exists(pathServerA))
                                {
                                    File.Delete(pathServerA + wtf.Key + ".lua");
                                    File.Delete(pathServerA + wtf.Key + ".lua.bak");
                                }

                                if (Directory.Exists(pathServerB))
                                {
                                    File.Delete(pathServerB + wtf.Key + ".lua");
                                    File.Delete(pathServerB + wtf.Key + ".lua.bak");
                                }
                            }
                        }
                    }                      
                }
            }
        }
        
    }

    internal class IndexedHashSet : Dictionary<string, HashSet<string>>
    {
        public void AddOrUpdate(string key, HashSet<string> value)
        {
            if(this.ContainsKey(key))
            {
                this[key].UnionWith(value);
            } else
            {
                this.Add(key, value);
            }
        }

        public void AddOrUpdate(string key, string value)
        {
            if (this.ContainsKey(key))
            {
                if(!this[key].Contains(value))
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