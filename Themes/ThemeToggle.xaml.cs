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

namespace Master.Themes
{
    /// <summary>
    /// Interaction logic for ThemeToggle.xaml
    /// </summary>
    public partial class ThemeToggle : UserControl
    {
        public ThemeToggle()
        {
            InitializeComponent();
            // Initial icon state
            UpdateIcon(ThemeManager.CurrentTheme);

            // Listen for theme changes
            ThemeManager.ThemeChanged += OnThemeChanged;

            // Cleanup to avoid memory leaks
            Unloaded += OnUnloaded;
        }

        private void ThemeToggle_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ToggleTheme();
        }


        private void OnThemeChanged(ThemeType theme)
        {
            UpdateIcon(theme);
        }

        private void UpdateIcon(ThemeType theme)
        {
            ThemeIcon.Symbol = theme == ThemeType.Dark
                ? SymbolRegular.WeatherSunny20
                : SymbolRegular.WeatherMoon20;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

    }
}
