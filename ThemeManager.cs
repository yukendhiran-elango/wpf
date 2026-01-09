using System;
using System.Linq;
using System.Windows;

namespace Master
{
    public enum ThemeType
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        private const string ThemeDictionaryKey = "ThemeDictionary";

        public static ThemeType CurrentTheme { get; private set; } = ThemeType.Light;

        public static event Action<ThemeType> ThemeChanged;

        public static void Initialize(ThemeType initialTheme = ThemeType.Light)
        {
            SetTheme(initialTheme);
        }

        public static void ToggleTheme()
        {
            SetTheme(CurrentTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light);
        }

        public static void SetTheme(ThemeType theme)
        {
            if (CurrentTheme == theme)
                return;

            CurrentTheme = theme;

            var app = Application.Current;
            if (app == null)
                return;

            // Remove existing theme dictionary (by key, not index)
            var existingTheme = app.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Contains(ThemeDictionaryKey));

            if (existingTheme != null)
                app.Resources.MergedDictionaries.Remove(existingTheme);

            // Load new theme dictionary
            var themeDictionary = new ResourceDictionary
            {
                Source = new Uri(
                    theme == ThemeType.Light
                        ? "Themes/Light.xaml"
                        : "Themes/Dark.xaml",
                    UriKind.Relative
                )
            };

            // Marker key so we can find/remove it later
            themeDictionary[ThemeDictionaryKey] = true;

            // Insert at top to ensure highest priority
            app.Resources.MergedDictionaries.Insert(0, themeDictionary);

            //app.Resources.MergedDictionaries.Add(new ResourceDictionary
            //{
            //    Source = new Uri("Themes/Common.xaml", UriKind.Relative)
            //});
            ThemeChanged?.Invoke(theme);
        }
    }
}
