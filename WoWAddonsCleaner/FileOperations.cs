using System.Collections.Generic;
using System.IO;

namespace WoWAddonsCleaner
{

    public delegate void TaskSteps();

    internal class FileOperations
    {
        private readonly List<string> oDeleteFileOperations;
        private readonly Dictionary<string, bool> oDeleteDirOperations;
        private readonly Dictionary<string, string[]> oReplaceOperations;
        private readonly TaskSteps oStep;

        public FileOperations()
        {
            oDeleteFileOperations = new List<string>();
            oDeleteDirOperations = new Dictionary<string, bool>();
            oReplaceOperations = new Dictionary<string, string[]>();
            FailedOperations = new List<string>();
            oStep = () => { }; // Empty callback
        }

        public FileOperations(TaskSteps iStep)
        {
            oDeleteFileOperations = new List<string>();
            oDeleteDirOperations = new Dictionary<string, bool>();
            oReplaceOperations = new Dictionary<string, string[]>();
            FailedOperations = new List<string>();
            oStep = iStep;

        }

        public static string ResolvePath(params string[] iPaths)
        {
            return string.Join(Path.DirectorySeparatorChar.ToString(), iPaths);
        }

        public void deleteDirectory(string iPath, bool iRecursive = false)
        {
            if (!oDeleteDirOperations.ContainsKey(iPath))
            {
                oDeleteDirOperations.Add(iPath, iRecursive);
            }
            else if (iRecursive)
            {
                oDeleteDirOperations[iPath] = true;
            }

        }

        public void deleteFile(string iPath)
        {
            if (!oDeleteFileOperations.Contains(iPath))
            {
                oDeleteFileOperations.Add(iPath);
            }
        }

        public void replaceFile(string iPath, List<string> iContent)
        {
            replaceFile(iPath, iContent.ToArray());
        }

        public void replaceFile(string iPath, string[] iContent)
        {
            if (!oReplaceOperations.ContainsKey(iPath))
            {
                oReplaceOperations.Add(iPath, iContent);
            }
            else
            {
                FailedOperations.Add("REPLACE DUPLICATE " + iPath);
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
            foreach (KeyValuePair<string, string[]> wOperation in oReplaceOperations)
            {
                try
                {
                    File.WriteAllLines(wOperation.Key, wOperation.Value);
                }
                catch
                {
                    FailedOperations.Add("REPLACE " + wOperation.Key);
                }
                finally
                {
                    oStep();
                }
            }

            foreach (string wOperation in oDeleteFileOperations)
            {
                try
                {
                    File.Delete(wOperation);
                }
                catch
                {
                    FailedOperations.Add("DELETE FILE " + wOperation);
                }
                finally
                {
                    oStep();
                }
            }

            foreach (KeyValuePair<string, bool> wOperation in oDeleteDirOperations)
            {
                try
                {
                    Directory.Delete(wOperation.Key, wOperation.Value);
                }
                catch
                {
                    FailedOperations.Add("DELETE DIR " + wOperation.Key);
                }
                finally
                {
                    oStep();
                }
            }

            return FailedOperations.Count;
        }

        public List<string> FailedOperations { get; }
    }
}
