using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Octokit;

using Windows.ApplicationModel;
using Windows.UI.Popups;

using WinUI_XAMPP.Pages.Settings;

using WinUIEx.Messaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_XAMPP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public record BreadcrumbItem(string value);


    public sealed partial class SettingsPage : Microsoft.UI.Xaml.Controls.Page
    {
        private List<BreadcrumbItem> SettingsBreadcrumbItems = new();

        public SettingsPage()
        {
            InitializeComponent();
            LoadApplicationInfo();
            SettingsBreadcrumbItems.Add(new BreadcrumbItem("Settings"));
        }

        private void LoadApplicationInfo()
        {
            AppTitle.Text = Windows.ApplicationModel.Package.Current.DisplayName;
            AppDescription.Text = $"Created by {Windows.ApplicationModel.Package.Current.PublisherDisplayName}";
            AppVersion.Text = $"Version {Windows.ApplicationModel.Package.Current.Id.Version.Major}.{Windows.ApplicationModel.Package.Current.Id.Version.Minor} Build {Windows.ApplicationModel.Package.Current.Id.Version.Build}";
        }

        private void SettingsBreadcrumbBar_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

        }

        private async void SelectXAMPPDirButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            await new SelectDefaultXAMPPDialog()
            {
                XamlRoot = Content.XamlRoot
            }.ShowAsync();
        }

        private async void CheckUpdateButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            CheckUpdateButton.Content = new ProgressRing
            {
                IsActive = true,
                IsIndeterminate = true,
                HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                Height = 14.0,
                Width = 14.0
            };

            if (CheckUpdateButton.Content.GetType() == typeof(ProgressRing))
            {
                string apiToken = "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";
                string owner = "rencendant";
                string repository = "WinUI-XAMPP";

                var updateResult =  await GetUpdates(apiToken, owner, repository);

                if (updateResult != null)
                {

                }
                else
                {
                    await new ContentDialog()
                    {
                        Title = "Check for Updates",
                        Content = "No Updates Found",
                        Width = 500,
                        Height = 200,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        DefaultButton = ContentDialogButton.Close,
                        CloseButtonText = "Close",
                        XamlRoot = this.XamlRoot
                    }.ShowAsync();

                    CheckUpdateButton.Content = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        Children =
                        {
                            new FontIcon()
                            {
                                Glyph = "\uE895",
                                FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Segoe Fluent Icons"),
                                FontSize = 12,
                                Margin = new Thickness(0,0,10,0)
                            },
                            new TextBlock()
                            {
                                Text = "Check for Updates",
                            }
                        }
                    };
                }
            }
        }

        private async Task<Release> GetUpdates(string apiToken, string owner, string repository)
        {
            Windows.ApplicationModel.PackageVersion currentVersion = Windows.ApplicationModel.Package.Current.Id.Version;

            GitHubClient clientRepository = new(new ProductHeaderValue("WinUI-XAMPP-Updater"));
            clientRepository.Credentials = new Credentials(apiToken);

            if (clientRepository.Repository != null)
            {
                if (clientRepository.Repository.Release.GetAll(owner, repository).Result.Count != 0)
                {
                    var releaseVersion = await clientRepository.Repository.Release.GetLatest(owner, repository);
                    return releaseVersion;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
