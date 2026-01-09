using Master.Views.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Appearance;

namespace Master
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationThemeManager.Apply(this);
            MainFrame.Navigate(new Views.Pages.HomePage());
        }

        private void OnHomeRequested(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
            NavigationBar.CurrentPage = "Home";
        }

        private void OnAnalysisRequested(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AnalysisPage());
            NavigationBar.CurrentPage = "Analysis";
        }

        private void OnMachineStatusRequested(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MachineStatusPage());
            NavigationBar.CurrentPage = "MachineStatus";
        }

        private void OnProductionRequested(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductionPage());
            NavigationBar.CurrentPage = "Production";
        }

    }
}
