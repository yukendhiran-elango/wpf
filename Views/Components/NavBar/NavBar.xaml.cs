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
    public partial class NavBar : UserControl
    {
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(string), typeof(NavBar), new PropertyMetadata("Home"));

        public string CurrentPage
        {
            get { return (string)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public NavBar()
        {
            InitializeComponent();
            UpdateActiveStates();
        }

        public event RoutedEventHandler HomeRequested;
        public event RoutedEventHandler AnalysisRequested;
        public event RoutedEventHandler MachineStatusRequested;
        public event RoutedEventHandler ProductionRequested;
        public event RoutedEventHandler ReportRequested;

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = "Home";
            if (HomeRequested != null)
                HomeRequested(this, e);
        }

        private void Analysis_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage = "Analysis";
            if (AnalysisRequested != null)
                AnalysisRequested(this, e);
        }

        private void MachineStatus_Clik(object sender, RoutedEventArgs e)
        {
            CurrentPage = "MachineStatus";
            if (MachineStatusRequested != null)
                MachineStatusRequested(this, e);
        }

        private void Production_Clik(object sender, RoutedEventArgs e)
        {
            CurrentPage = "Production";
            if (ProductionRequested != null)
                ProductionRequested(this, e);
        }

        private void Report_Clik(object sender, RoutedEventArgs e)
        {
            CurrentPage = "Reports";
            if (ReportRequested != null)
                ReportRequested(this, e);
        }

        private void UpdateActiveStates()
        {
            // Reset all buttons
            WrapPanel wrapPanel = Content as WrapPanel;
            if (wrapPanel != null)
            {
                foreach (UIElement child in wrapPanel.Children)
                {
                    NavButton navButton = child as NavButton;
                    if (navButton != null)
                    {
                        navButton.IsActive = false;
                    }
                }

                // Set active button based on current page
                NavButton activeButton = null;
                if (CurrentPage == "Home")
                    activeButton = FindButtonByText("Home");
                else if (CurrentPage == "Analysis")
                    activeButton = FindButtonByText("Analysis");
                else if (CurrentPage == "MachineStatus")
                    activeButton = FindButtonByText("Machine Status");
                else if (CurrentPage == "Production")
                    activeButton = FindButtonByText("Production");

                if (activeButton != null)
                {
                    activeButton.IsActive = true;
                }
            }
        }

        private NavButton FindButtonByText(string text)
        {
            WrapPanel wrapPanel = Content as WrapPanel;
            if (wrapPanel != null)
            {
                foreach (UIElement child in wrapPanel.Children)
                {
                    NavButton navButton = child as NavButton;
                    if (navButton != null && navButton.Text == text)
                    {
                        return navButton;
                    }
                }
            }
            return null;
        }
    }
}