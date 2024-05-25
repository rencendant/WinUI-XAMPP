using System;
using System.Collections.Generic;
using System.IO;

using CommunityToolkit.WinUI.Controls;

using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_XAMPP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DebugPage : Page
    {
        public DebugPage()
        {
            InitializeComponent();
            foreach (var item in SearchForXampp())
            {
                XamppDirectoryPanel.Children.Add(new SettingsCard()
                {
                    Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                    Header = "Available XAMPP Directory",
                    Content = item,
                    XamlRoot = XamppDirectoryPanel.XamlRoot
                });
            }
            ReadXamppControlFile();
        }


        List<string> xamppDirectories = [];

        public List<string> SearchForXampp()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    try
                    {
                        xamppDirectories.AddRange(Directory.GetDirectories(drive.RootDirectory.FullName, "XAMPP", SearchOption.TopDirectoryOnly));
                    }
                    catch (UnauthorizedAccessException)
                    {

                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return xamppDirectories;
        }

        public void ReadXamppControlFile()
        {
            foreach (string directory in xamppDirectories)
            {
                if (File.Exists($"{directory}\\xampp-control.ini"))
                {
                    string XamppControlFile = $"{directory}\\xampp-control.ini";
                    string[] fileContents = File.ReadAllLines(XamppControlFile);

                    Dictionary<string, string> keyValuePairs = [];

                    foreach (string line in fileContents)
                    {
                        string[] parts = line.Split('=');

                        if (parts.Length >= 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();
                            keyValuePairs[key] = value;
                        }
                    }

                    foreach (KeyValuePair<string, string> keyValue in keyValuePairs)
                    {
                        XamppDirectoryPanel.Children.Add(new SettingsCard()
                        {
                            Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                            Header = keyValue.Key,
                            Content = keyValue.Value,
                            XamlRoot = XamppDirectoryPanel.XamlRoot
                        });
                    }
                }
            }
        }
    }
}
