using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using WinUI_XAMPP.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_XAMPP
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            Title = "WinUI XAMPP";
            AppSideBar.SelectedItem = Home;
        }

        private void AppSideBar_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == (object)Home)
            {
                AppFrame.Navigate(typeof(HomePage));
            }
            else if (sender.SelectedItem == sender.SettingsItem)
            {
                AppFrame.Navigate(typeof(SettingsPage));
            }
        }
    }
}
