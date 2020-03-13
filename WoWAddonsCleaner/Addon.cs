using System.Collections.Generic;

namespace WoWAddonsCleaner
{
    internal class Addon
    {
        public string filename;
        public string title;
        public string notes; // Notes, Notes-frFR
        public string author; // Author, Autor
        public List<string> optionalDependencies; // OptionalDependencies, OptionalDep* 
        public List<string> dependencies; // Dependencies, RequiredDeps, Dep*
        public List<string> savedVariables;
        public List<string> savedVariablesPerCharacter;
        public Version version;

        public Dictionary<string, Addon> subAddons;

        public Addon()
        {
            optionalDependencies = new List<string>();
            dependencies = new List<string>();
            savedVariables = new List<string>();
            savedVariablesPerCharacter = new List<string>();

            subAddons = new Dictionary<string, Addon>();
        }
    }

    internal class Version
    {

        public Version(string iVersion)
        {
            version = iVersion;
        }

        public string version;
        public string versionNum
        {
            get
            {
                string major = version.Substring(0, 1);
                string minor = version.Substring(2, 1);
                string patch = version.Substring(4, 1);

                return major + "." + minor + "." + patch;
            }
        }

        public string toString()
        {
            return versionNum;
        }
    }
}
