using System.Collections.Generic;

namespace WoWAddonsCleaner
{
    class Addon
    {
        public string title;
        public string notes;
        public string author; // Author, Autor
        public string optionalDependencies; // OptionalDependencies, OptionalDep* 
        public string dependencies; // Dependencies, RequiredDeps, Dep*
        public List<string> savedVariables;
        public List<string> savedVariablesPerCharacter;
        public Version version;
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
