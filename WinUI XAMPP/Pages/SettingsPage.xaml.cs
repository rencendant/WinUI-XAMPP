using System;
using System.Collections.Generic;

using Microsoft.UI.Xaml.Controls;

using Windows.ApplicationModel;

using WinUI_XAMPP.Pages.Settings;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_XAMPP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public record BreadcrumbItem(string value);


    public sealed partial class SettingsPage : Page
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
            AppTitle.Text = Package.Current.DisplayName;
            AppDescription.Text = $"Created by {Package.Current.PublisherDisplayName}";
            AppVersion.Text = $"Version {Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor} Build {Package.Current.Id.Version.Build}";
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
    }
}
