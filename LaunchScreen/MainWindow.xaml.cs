using System.Windows;

namespace LaunchScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Launcher.PlayGame("CsharpRPG.exe");
        }

        private void btnWebsite_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchWebsite("http://rogueasp.azurewebsites.net/");
        }
    }
}
