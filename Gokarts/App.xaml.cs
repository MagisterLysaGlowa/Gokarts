using System.Windows;
using Gokarts.Stores;
using Gokarts.ViewModels;

namespace Gokarts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            NavigationStore navigationStore = new();

            //navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            
            base.OnStartup(e);
        }
    }

}
