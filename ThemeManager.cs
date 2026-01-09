using System;
using System.Windows;
using System.Windows.Interop;

namespace Master
{
    public enum ThemeType
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        private static ThemeType currentTheme = ThemeType.Light;
        public static ThemeType CurrentTheme { get { return currentTheme; } }

        public static event Action<ThemeType> ThemeChanged;

        public static void SetTheme(ThemeType theme)
        {
            if (currentTheme == theme) return;

            currentTheme = theme;
            
            // Get the application's resource dictionary
            var app = Application.Current;
            Uri themeUri;
            if (theme == ThemeType.Light)
            {
                themeUri = new Uri("Themes/Light.xaml", UriKind.Relative);
            }
            else
            {
                themeUri = new Uri("Themes/Dark.xaml", UriKind.Relative);
            }

            // Remove existing theme resources
            if (app.Resources.MergedDictionaries.Count > 0)
            {
                app.Resources.MergedDictionaries.RemoveAt(0);
            }

            // Add new theme resources
            var themeDict = new ResourceDictionary();
            themeDict.Source = themeUri;
            app.Resources.MergedDictionaries.Insert(0, themeDict);

            // Apply WPF UI theme
            ApplyWpfUiTheme(theme);

            // Notify listeners
            if (ThemeChanged != null)
            {
                ThemeChanged(theme);
            }
        }

        public static void ToggleTheme()
        {
            if (currentTheme == ThemeType.Light)
            {
                SetTheme(ThemeType.Dark);
            }
            else
            {
                SetTheme(ThemeType.Light);
            }
        }

        private static void ApplyWpfUiTheme(ThemeType theme)
        {
            // Apply WPF UI theme to windows
            var windows = Application.Current.Windows;
            foreach (Window window in windows)
            {
                var helper = new WindowInteropHelper(window);
                var handle = helper.Handle;
                
                if (handle != IntPtr.Zero)
                {
                    // You can use Wpf.Ui's theme system here
                    // For now, we'll rely on our custom theme resources
                }
            }
        }

        public static void Initialize()
        {
            // Set initial theme
            SetTheme(ThemeType.Light);
        }
    }
}