using System;
using System.Collections.Generic;
using System.Diagnostics;

using CommunityToolkit.WinUI.Controls;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

using WinUI_XAMPP.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_XAMPP.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            if (!App.IsAdmin())
            {
                NonElevatedInfoBar.IsOpen = !App.IsAdmin();
            }
        }

        private void AppendXAMPPModules()
        {
            if (!string.IsNullOrEmpty(App.XAMPPDirectory))
            {
                List<ProcessInfo> processes = App.core.SearchProcesses();

                ToggleButton actionButton;

                SettingsExpander mysqlExpander = new()
                {
                    Header = "MySQL",
                    XamlRoot = ModulePanel.XamlRoot,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                SettingsExpander filezillaExpander = new()
                {
                    Header = "FileZilla FTP Server",
                    XamlRoot = ModulePanel.XamlRoot,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                SettingsExpander mercuryExpander = new()
                {
                    Header = "Mercury Mail",
                    XamlRoot = ModulePanel.XamlRoot,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                if (processes.Count != 0 && App.IsAdmin())
                {
                    for (int i = 0; i < processes.Count; i++)
                    {
                        switch (processes[i].Name)
                        {
                            case "httpd":
                                //if (processes[i].ProcessId != -1 && processes[i].Port == 80)
                                //{
                                //    actionButton = new()
                                //    {
                                //        Width = 100
                                //    };
                                //    actionButton.Content = "Stop";
                                //    actionButton.Click += (sender, e) =>
                                //    {
                                //        ProcessStartInfo startInfo = new ProcessStartInfo();
                                //        startInfo.FileName = $"{App.XAMPPDirectory}apache\\bin\\httpd.exe";
                                //        startInfo.Arguments = "-k stop";
                                //        startInfo.CreateNoWindow = true;
                                //        Process.Start(startInfo);
                                //        actionButton.Content = "Start";
                                //    };
                                //    actionButton.IsChecked = true;
                                //    if (actionButton.IsChecked == true)
                                //    {
                                //        actionButton.Click += (sender, e) =>
                                //        {
                                //            ProcessStartInfo startInfo = new ProcessStartInfo();
                                //            startInfo.FileName = $"{App.XAMPPDirectory}apache\\bin\\httpd.exe";
                                //            startInfo.Arguments = "-k start";
                                //            startInfo.CreateNoWindow = true;
                                //            Process.Start(startInfo);
                                //        };
                                //        actionButton.Content = "Stop";
                                //    }
                                //}
                                //else
                                //{
                                //    actionButton = new()
                                //    {
                                //        Width = 100
                                //    };
                                //    actionButton.Content = "Start";
                                //    actionButton.Click += (sender, e) =>
                                //    {
                                //        ProcessStartInfo startInfo = new ProcessStartInfo();
                                //        startInfo.FileName = $"{App.XAMPPDirectory}apache\\bin\\httpd.exe";
                                //        startInfo.Arguments = "-k start";
                                //        startInfo.CreateNoWindow = true;
                                //        Process.Start(startInfo);
                                //        actionButton.Content = "Stop";
                                //    };
                                //    actionButton.IsChecked = false;
                                //    if (actionButton.IsChecked == false)
                                //    {
                                //        actionButton.Click += (sender, e) =>
                                //        {
                                //            ProcessStartInfo startInfo = new ProcessStartInfo();
                                //            startInfo.FileName = $"{App.XAMPPDirectory}apache\\bin\\httpd.exe";
                                //            startInfo.Arguments = "-k stop";
                                //            startInfo.CreateNoWindow = true;
                                //            Process.Start(startInfo);
                                //            actionButton.Content = "Start";
                                //        };
                                //    }
                                //}
                                break;
                            case "mysqld":
                                if (processes[i].ProcessId != -1 && processes[i].Port == 3306)
                                {
                                    actionButton = new()
                                    {
                                        Width = 100
                                    };
                                    actionButton.Content = "Stop";
                                    actionButton.Click += (sender, e) =>
                                    {
                                        ProcessStartInfo startInfo = new ProcessStartInfo();
                                        startInfo.FileName = $"{App.XAMPPDirectory}mysql\\bin\\mysqladmin.exe";
                                        startInfo.Arguments = "-u root shutdown";
                                        startInfo.CreateNoWindow = true;
                                        Process.Start(startInfo);
                                        actionButton.Content = "Start";
                                    };
                                    actionButton.IsChecked = true;
                                    //actionButton.XamlRoot = apacheExpander.XamlRoot;
                                    if (actionButton.IsChecked == true)
                                    {
                                        actionButton.Click += (sender, e) =>
                                        {
                                            ProcessStartInfo startInfo = new ProcessStartInfo();
                                            startInfo.FileName = $"{App.XAMPPDirectory}mysql\\bin\\mysqld.exe";
                                            startInfo.Arguments = "--console";
                                            startInfo.CreateNoWindow = true;
                                            Process.Start(startInfo);
                                            actionButton.Content = "Stop";
                                        };
                                        mysqlExpander.Content = actionButton;
                                    }
                                    mysqlExpander.Content = actionButton;
                                }
                                else
                                {
                                    actionButton = new()
                                    {
                                        Width = 100
                                    };
                                    actionButton.Content = "Start";
                                    actionButton.Click += (sender, e) =>
                                    {
                                        ProcessStartInfo startInfo = new ProcessStartInfo();
                                        startInfo.FileName = $"{App.XAMPPDirectory}mysql\\bin\\mysqld.exe";
                                        startInfo.Arguments = "--console";
                                        startInfo.CreateNoWindow = true;
                                        Process.Start(startInfo);
                                        actionButton.Content = "Stop";
                                    };
                                    actionButton.IsChecked = false;
                                    //actionButton.XamlRoot = apacheExpander.XamlRoot;
                                    if (actionButton.IsChecked == false)
                                    {
                                        actionButton.Click += (sender, e) =>
                                        {
                                            ProcessStartInfo startInfo = new ProcessStartInfo();
                                            startInfo.FileName = $"{App.XAMPPDirectory}mysql\\bin\\mysqladmin.exe";
                                            startInfo.Arguments = "-u root shutdown";
                                            startInfo.CreateNoWindow = true;
                                            Process.Start(startInfo);
                                            actionButton.Content = "Start";
                                        };
                                        mysqlExpander.Content = actionButton;
                                    }
                                    mysqlExpander.Content = actionButton;
                                }
                                ModulePanel.Children.Add(mysqlExpander);
                                break;
                            case "FileZillaServer":
                                actionButton = new()
                                {
                                    Width = 100
                                };
                                actionButton.Content = "Start";
                                filezillaExpander.Content = actionButton;
                                ModulePanel.Children.Add(filezillaExpander);
                                break;
                            case "mercury":
                                actionButton = new()
                                {
                                    Width = 100
                                };
                                actionButton.Content = "Start";
                                mercuryExpander.Content = actionButton;
                                ModulePanel.Children.Add(mercuryExpander);
                                break;
                        }
                    }
                }
            }
        }

        private async void OpenTerminalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Command to open Windows Terminal and set starting directory
                string command = $"wt --startingDirectory {App.XAMPPDirectory}";

                // Start Windows Terminal process
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    CreateNoWindow = true,
                    UseShellExecute = App.IsAdmin()
                });
            }
            catch (Exception ex)
            {
                await new ContentDialog()
                {
                    Title = "Error",
                    Content = ex.Message,
                    XamlRoot = Content.XamlRoot,
                    CloseButtonText = "Close",
                    DefaultButton = ContentDialogButton.Close
                }.ShowAsync();
            }
        }

        private async void OpenServicesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "services.msc",
                    CreateNoWindow = true,
                    UseShellExecute = App.IsAdmin()
                });
            }
            catch (Exception ex)
            {
                await new ContentDialog()
                {
                    Title = "Error",
                    Content = ex.Message,
                    XamlRoot = Content.XamlRoot,
                    CloseButtonText = "Close",
                    DefaultButton = ContentDialogButton.Close
                }.ShowAsync();
            }
        }

        private async void OpenXAMPPFolderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = App.XAMPPDirectory,
                    CreateNoWindow = true,
                    UseShellExecute = App.IsAdmin()
                });
            }
            catch (Exception ex)
            {
                await new ContentDialog()
                {
                    Title = "Error",
                    Content = ex.Message,
                    XamlRoot = Content.XamlRoot,
                    CloseButtonText = "Close",
                    DefaultButton = ContentDialogButton.Close
                }.ShowAsync();
            }
        }
    }
}
