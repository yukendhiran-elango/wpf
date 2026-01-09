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

namespace Master.Views.Components.Header
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
            UpdateThemeIcon();
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        private void ThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ToggleTheme();
        }

        private void OnThemeChanged(ThemeType newTheme)
        {
            UpdateThemeIcon();
        }

        private void UpdateThemeIcon()
        {
            if (ThemeManager.CurrentTheme == ThemeType.Light)
            {
                ThemeIcon.Symbol = Wpf.Ui.Controls.SymbolRegular.WeatherMoon20;
            }
            else
            {
                ThemeIcon.Symbol = Wpf.Ui.Controls.SymbolRegular.WeatherSunny20;
            }
        }
    }
}