using System.Diagnostics;

namespace WinUI_XAMPP.Core
{
    public class CoreClass
    {
        private string[] appDirectories = new string[5];

        public string SearchXAMPPFolder(string[] commonPaths)
        {
            for (int i = 0; i < commonPaths.Length; i++)
            {
                if (Directory.Exists(commonPaths[i]))
                {
                    appDirectories[i] = commonPaths[i];
                    Debug.WriteLine($"Path Found: {appDirectories[i]}");
                }
            }

            string defaultInstance = appDirectories[0];

            return defaultInstance;
        }

        public string[] SearchXAMPPFolders(string[] commonPaths)
        {
            for (int i = 0; i < commonPaths.Length; i++)
            {
                if (Directory.Exists(commonPaths[i]))
                {
                    appDirectories[i] = commonPaths[i];
                    Debug.WriteLine($"Path Found: {appDirectories[i]}");
                }
            }

            return appDirectories;
        }

        private int GetProcessID(string processName, string directory)
        {
            var processes = Process.GetProcessesByName(processName);
            processes = processes.Where(p => p.MainModule != null && p.MainModule.FileName.StartsWith(directory, StringComparison.OrdinalIgnoreCase)).ToArray();
            return processes.Length > 0 ? processes[0].Id : -1;
        }

        public List<ProcessInfo> SearchProcesses()
        {
            ProcessInfo[] processes =
            [
                new ProcessInfo { FriendlyName = "Apache 2.4", Name = "httpd", Port = 80 },
                new ProcessInfo { FriendlyName = "MySQL", Name = "mysqld", Port = 3306 },
                new ProcessInfo { FriendlyName = "FileZilla FTP Server", Name = "FileZillaServer" },
                new ProcessInfo { FriendlyName = "Mercury", Name = "mercury" }
            ];

            List<ProcessInfo> processInfoList = processes.Select(p => new ProcessInfo
            {
                Name = p.Name,
                Port = p.Port,
                ProcessId = GetProcessID(p.Name, appDirectories[0])
            })
                .ToList();

            return processInfoList;
        }
    }
}
