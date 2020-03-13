using System;
using System.Collections.Generic;
using System.IO;

namespace WoWAddonsCleaner
{

    public delegate void TaskSteps();

    class FileOperations
    {
        private List<string> oDeleteFileOperations;
        private Dictionary<string, bool> oDeleteDirOperations;
        private Dictionary<string, string[]> oReplaceOperations;
        private TaskSteps oStep;

        public FileOperations()
        {
            this.oDeleteFileOperations = new List<string>();
            this.oDeleteDirOperations = new Dictionary<string, bool>();
            this.oReplaceOperations = new Dictionary<string, string[]>();
            this.FailedOperations = new List<string>();
            this.oStep = () => { }; // Empty callback
        }

        public FileOperations(TaskSteps iStep)
        {
            this.oDeleteFileOperations = new List<string>();
            this.oDeleteDirOperations = new Dictionary<string, bool>();
            this.oReplaceOperations = new Dictionary<string, string[]>();
            this.FailedOperations = new List<string>();
            this.oStep = iStep;

        }

        public static string ResolvePath(params string[] iPaths)
        {
            return String.Join(Path.DirectorySeparatorChar.ToString(), iPaths);
        }

        public void deleteDirectory(string iPath, bool iRecursive = false)
        {
            if (!this.oDeleteDirOperations.ContainsKey(iPath))
            {
                this.oDeleteDirOperations.Add(iPath, iRecursive);
            }
            else if (iRecursive)
            {
                this.oDeleteDirOperations[iPath] = true;
            }

        }

        public void deleteFile(string iPath)
        {
            if (!this.oDeleteFileOperations.Contains(iPath))
            {
                this.oDeleteFileOperations.Add(iPath);
            }
        }

        public void replaceFile(string iPath, List<string> iContent)
        {
            this.replaceFile(iPath, iContent.ToArray());
        }

        public void replaceFile(string iPath, string[] iContent)
        {
            if (!this.oReplaceOperations.ContainsKey(iPath))
            {
                this.oReplaceOperations.Add(iPath, iContent);
            }
            else
            {
                this.FailedOperations.Add("REPLACE DUPLICATE " + iPath);
            }
        }

        public static string[] ReadFile(string iPath)
        {
            try
            {
                return File.ReadAllLines(iPath);
            }
            catch
            {
                // Nothing to do
            }

            return null;
        }

        public static bool FileExists(string iPath)
        {
            FileInfo wFI = new FileInfo(iPath);

            return wFI.Exists;
        }

        public static bool DirectoryExists(string iPath)
        {
            DirectoryInfo wDI = new DirectoryInfo(iPath);

            return wDI.Exists;
        }

        public static List<string> GetDirectories(string iPath)
        {
            DirectoryInfo wDI = new DirectoryInfo(iPath);
            List<string> wPaths = new List<string>();

            foreach (DirectoryInfo wSubDir in wDI.GetDirectories())
            {
                wPaths.Add(wSubDir.FullName);
            }

            return wPaths;
        }

        public static List<string> GetFiles(string iPath, string iPattern = null)
        {
            DirectoryInfo wDI = new DirectoryInfo(iPath);
            List<string> wPaths = new List<string>();

            foreach (FileInfo wFile in wDI.GetFiles(iPattern))
            {
                wPaths.Add(wFile.FullName);
            }

            return wPaths;
        }

        public static string ExtractFileName(string iPath)
        {
            FileInfo wFI = new FileInfo(iPath);

            if (wFI.Exists)
            {
                return wFI.Name;
            }

            DirectoryInfo wDI = new DirectoryInfo(iPath);

            return wDI.Name;
        }

        public static string GetParent(string iPath)
        {

            DirectoryInfo wDI = new DirectoryInfo(iPath);

            return wDI.Parent.FullName;
        }

        public int execute()
        {
            foreach (KeyValuePair<string, string[]> wOperation in this.oReplaceOperations)
            {
                try
                {
                    File.WriteAllLines(wOperation.Key, wOperation.Value);
                }
                catch
                {
                    this.FailedOperations.Add("REPLACE " + wOperation.Key);
                }
                finally
                {
                    this.oStep();
                }
            }

            foreach (string wOperation in this.oDeleteFileOperations)
            {
                try
                {
                    File.Delete(wOperation);
                }
                catch
                {
                    this.FailedOperations.Add("DELETE FILE " + wOperation);
                }
                finally
                {
                    this.oStep();
                }
            }

            foreach (KeyValuePair<string, bool> wOperation in this.oDeleteDirOperations)
            {
                try
                {
                    Directory.Delete(wOperation.Key, wOperation.Value);
                }
                catch
                {
                    this.FailedOperations.Add("DELETE DIR " + wOperation.Key);
                }
                finally
                {
                    this.oStep();
                }
            }

            return this.FailedOperations.Count;
        }

        public List<string> FailedOperations { get; }
    }
}
