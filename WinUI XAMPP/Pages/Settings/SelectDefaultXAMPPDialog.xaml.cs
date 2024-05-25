using Microsoft.UI.Xaml.Controls;

namespace WinUI_XAMPP.Pages.Settings
{
    public sealed partial class SelectDefaultXAMPPDialog : ContentDialog
    {
        public SelectDefaultXAMPPDialog()
        {
            InitializeComponent();
            DialogParent.Children.Add(new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                Children = {
                    new ProgressRing()
                    {
                        IsIndeterminate = true,
                        IsActive = true,
                        HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                        Margin = new Microsoft.UI.Xaml.Thickness(0,0,10,0)
                    },
                    new TextBlock()
                    {
                        Text = "Searching XAMPP Versions",
                        HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
                        VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center
                    }
                }
            });
        }
    }
}
