using System.Windows;
using System.Windows.Controls;

namespace Gokarts.Views;

/// <summary>
/// Logika interakcji dla klasy HomeView.xaml
/// </summary>
public partial class HomeView : UserControl
{
    public static readonly DependencyProperty IsModernWindowStyleEnabledProperty = DependencyProperty.Register("IsModernWindowStyleEnabled", typeof(bool), typeof(HomeView), new PropertyMetadata(true));

    public HomeView()
    {
        InitializeComponent();
    }
}
