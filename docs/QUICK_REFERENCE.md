# Quick Reference Guide

## 🎯 Core Concepts

### Dependency Property (useState + PropTypes)
```csharp
public static readonly DependencyProperty MyPropertyProperty =
    DependencyProperty.Register("MyProperty", typeof(string), typeof(MyControl));

public string MyProperty
{
    get { return (string)GetValue(MyPropertyProperty); }
    set { SetValue(MyPropertyProperty, value); }
}
```

### Event Handling (Callbacks)
```csharp
// Define
public event RoutedEventHandler MyEvent;

// Trigger
if (MyEvent != null) MyEvent(this, e);

// Handle
myControl.MyEvent += (s, e) => { /* handle */ };
```

### Data Binding (Props)
```xml
<!-- One-way -->
<TextBlock Text="{Binding PropertyName}" />

<!-- Two-way -->
<TextBox Text="{Binding PropertyName, Mode=TwoWay}" />

<!-- Resources -->
<Control Template="{StaticResource MyTemplate}" />
<Control Background="{DynamicResource MyBrush}" />
```

## 🧩 Component Patterns

### 1. Basic Component
```xml
<!-- MyComponent.xaml -->
<UserControl x:Class="MyApp.MyComponent">
    <Grid>
        <TextBlock Text="{Binding Title}" />
    </Grid>
</UserControl>
```

```csharp
// MyComponent.xaml.cs
public partial class MyComponent : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(MyComponent));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public MyComponent()
    {
        InitializeComponent();
        DataContext = this; // Enable binding to own properties
    }
}
```

### 2. Container Component
```csharp
public partial class Container : UserControl
{
    public event RoutedEventHandler ItemSelected;

    private void OnItemClick(object sender, RoutedEventArgs e)
    {
        // Update internal state
        SelectedItem = ((Button)sender).Content.ToString();
        
        // Notify parent
        if (ItemSelected != null)
            ItemSelected(this, e);
    }
}
```

## 🎨 Theme Patterns

### Resource Dictionary
```xml
<ResourceDictionary>
    <!-- Colors -->
    <Color x:Key="Primary">#0078D4</Color>
    <Color x:Key="Background">#FFFFFF</Color>
    
    <!-- Brushes -->
    <SolidColorBrush x:Key="PrimaryBrush" Color="{StaticResource Primary}"/>
    <SolidColorBrush x:Key="BackgroundBrush" Color="{StaticResource Background}"/>
</ResourceDictionary>
```

### Theme Manager
```csharp
public static class ThemeManager
{
    public static void SetTheme(string themeName)
    {
        var uri = new Uri($"Themes/{themeName}.xaml", UriKind.Relative);
        var dict = new ResourceDictionary { Source = uri };
        
        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(dict);
    }
}
```

## 🧭 Navigation Patterns

### Frame Navigation
```csharp
// Navigate
frame.Navigate(new TargetPage());

// Go back  
if (frame.CanGoBack) frame.GoBack();

// Get current page
var currentPage = frame.Content as Page;
```

### Event-Driven Navigation
```csharp
// In navigation component
public event RoutedEventHandler NavigateToPage;

private void OnButtonClick(object sender, RoutedEventArgs e)
{
    if (NavigateToPage != null)
        NavigateToPage(sender, e);
}

// In main window
navBar.NavigateToPage += (s, e) => {
    var button = (Button)s;
    switch (button.Name) {
        case "HomeBtn": frame.Navigate(new HomePage()); break;
        case "SettingsBtn": frame.Navigate(new SettingsPage()); break;
    }
};
```

## 🔄 Common Patterns

### Data Triggers (Conditional Styling)
```xml
<Style TargetType="Border">
    <Style.Triggers>
        <DataTrigger Binding="{Binding IsActive}" Value="True">
            <Setter Property="Background" Value="Blue"/>
        </DataTrigger>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter Property="Background" Value="LightGray"/>
        </Trigger>
    </Style.Triggers>
</Style>
```

### Value Converters (Data Transformation)
```csharp
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Visibility)value == Visibility.Visible;
    }
}
```

### Commands (Action Binding)
```csharp
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
    public void Execute(object parameter) => _execute(parameter);
}
```

## 🎯 React → WPF Cheat Sheet

| React | WPF |
|--------|------|
| `useState()` | DependencyProperty |
| `useEffect()` | Constructor + Event subscriptions |
| `useContext()` | Static resources + events |
| CSS Variables | DynamicResource |
| Component props | Dependency properties |
| Event callbacks | RoutedEventHandler |
| Conditional rendering | DataTriggers, Visibility |
| Router | Frame.Navigate() |
| PropTypes | Property metadata |
| useMemo() | Cached resources |

## 🚀 Tips for React Developers

1. **Think Declaratively**: XAML is like JSX - describe what you want
2. **Use Dependency Properties**: They're your state management system
3. **Leverage Resources**: They're your styling system
4. **Event-Driven Communication**: Use events instead of direct method calls
5. **Data Binding**: It's like two-way props
6. **Separate Concerns**: XAML for UI, C# for logic
7. **Use MVVM Pattern**: ViewModel ≈ React component state + methods