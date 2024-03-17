using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using WinUI_XAMPP.Pages;
using WinUI_XAMPP.WindowHelper;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI_XAMPP
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private CustomWindowHelper _wndHlpr;

        public MainWindow()
        {
            InitializeComponent();
            _wndHlpr = new(this);
            ExtendsContentIntoTitleBar = true;
            Title = AppTitle.Text = App.IsAdmin() ? "WinUI XAMPP (Admin)" : "WinUI XAMPP";
            AppSideBar.SelectedItem = Home;
            SetTitleBar(CustomTitleBar);
            SystemBackdrop = new MicaBackdrop()
            {
                Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base
            };

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
