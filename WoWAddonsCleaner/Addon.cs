using System.Collections.Generic;

namespace WoWAddonsCleaner
{
    class Addon
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
            this.optionalDependencies = new List<string>();
            this.dependencies = new List<string>();
            this.savedVariables = new List<string>();
            this.savedVariablesPerCharacter = new List<string>();

            this.subAddons = new Dictionary<string, Addon>();
        }
    }
    class Version
    {

        public Version(string iVersion)
        {
            this.version = iVersion;
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
            return this.versionNum;
        }
    }
}
