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
                            string key = $"{parts[0].Trim()}";
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

                    //if (keyValuePairs.ContainsKey("Editor"))
                    //{
                    //    XamppDirectoryPanel.Children.Add(new SettingsCard()
                    //    {
                    //        Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                    //        Header = "Default Editor",
                    //        Content = keyValuePairs["Editor"],
                    //        XamlRoot = XamppDirectoryPanel.XamlRoot
                    //    });
                    //}

                    //if (keyValuePairs.ContainsKey("Browser"))
                    //{
                    //    XamppDirectoryPanel.Children.Add(new SettingsCard()
                    //    {
                    //        Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                    //        Header = "Default Browser",
                    //        Content = keyValuePairs["Browser"] != null && keyValuePairs["Browser"] != "" ? keyValuePairs["Browser"] : "No Default Browser Set",
                    //        XamlRoot = XamppDirectoryPanel.XamlRoot
                    //    });
                    //}

                    //if (keyValuePairs.ContainsKey("Debug"))
                    //{
                    //    XamppDirectoryPanel.Children.Add(new SettingsCard()
                    //    {
                    //        Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                    //        Header = "Debug",
                    //        Content = keyValuePairs["Debug"] != null && keyValuePairs["Debug"] != "" ? keyValuePairs["Debug"] : "No Debug Set",
                    //        XamlRoot = XamppDirectoryPanel.XamlRoot
                    //    });
                    //}

                    //if (keyValuePairs.ContainsKey("Debuglevel"))
                    //{
                    //    XamppDirectoryPanel.Children.Add(new SettingsCard()
                    //    {
                    //        Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                    //        Header = "Debug Level",
                    //        Content = keyValuePairs["Debuglevel"] != null && keyValuePairs["Debuglevel"] != "" ? keyValuePairs["Debuglevel"] : "No Debug Level Set",
                    //        XamlRoot = XamppDirectoryPanel.XamlRoot
                    //    });
                    //}

                    //if (keyValuePairs.ContainsKey("TomcatVisible"))
                    //{
                    //    XamppDirectoryPanel.Children.Add(new SettingsCard()
                    //    {
                    //        Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                    //        Header = "Tomcat UI Visibility",
                    //        Content = keyValuePairs["TomcatVisible"] != null && keyValuePairs["TomcatVisible"] != "" ? keyValuePairs["TomcatVisible"] : "No Tomcat UI Visibility Set",
                    //        XamlRoot = XamppDirectoryPanel.XamlRoot
                    //    });
                    //}

                    //if (keyValuePairs.ContainsKey("Language"))
                    //{
                    //    XamppDirectoryPanel.Children.Add(new SettingsCard()
                    //    {
                    //        Margin = new Microsoft.UI.Xaml.Thickness(10, 5, 10, 5),
                    //        Header = "Tomcat UI Visibility",
                    //        Content = keyValuePairs["Language"] != null && keyValuePairs["Language"] != "" ? keyValuePairs["Language"] == "en" : "No Tomcat UI Visibility Set",
                    //        XamlRoot = XamppDirectoryPanel.XamlRoot
                    //    });
                    //}
                }
            }
        }
    }
}
