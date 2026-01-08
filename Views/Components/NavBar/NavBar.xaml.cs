using Master.Views.Pages;
using System;
using System.Collections.Generic;
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
using Wpf.Ui.Controls;


namespace Master.Views.Components.NavBar
{
    /// <summary>
    /// Interaction logic for NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler HomeRequested;
        public event RoutedEventHandler AnalysisRequested;
        public event RoutedEventHandler MachineStatusRequested;
        public event RoutedEventHandler ProductionRequested;
        public event RoutedEventHandler ReportRequested;

        private void Home_Click(object sender, RoutedEventArgs e)
            => HomeRequested?.Invoke(this, e);

      

        private void Analysis_Click(object sender, RoutedEventArgs e)
            => AnalysisRequested?.Invoke(this, e);

        private void MachineStatus_Clik(object sender, RoutedEventArgs e)   => MachineStatusRequested?.
            Invoke(this, e);

        private void Production_Clik(object sender, RoutedEventArgs e) => ProductionRequested?.
          Invoke(this, e);

        private void Report_Clik(object sender, RoutedEventArgs e) => ReportRequested?.
          Invoke(this, e);

    }
}
