# WPF Navigation & Theming System

## 🎯 Project Overview

This document explains how the WPF navigation and theming system works, with analogies to React concepts for React developers learning WPF.

## 📁 Project Structure

```
├── Themes/
│   ├── Light.xaml          # Light theme colors (CSS variables)
│   └── Dark.xaml           # Dark theme colors
├── Views/
│   ├── Components/
│   │   ├── Header/
│   │   │   ├── Header.xaml          # Header component with theme toggle
│   │   │   └── Header.xaml.cs
│   │   └── NavBar/
│   │       ├── NavBar.xaml          # Navigation container
│   │       ├── NavBar.xaml.cs
│   │       ├── NavButton.xaml       # Individual nav item
│   │       └── NavButton.xaml.cs
│   └── Pages/
│       ├── HomePage.xaml            # Page components
│       ├── AnalysisPage.xaml
│       ├── MachineStatusPage.xaml
│       └── ProductionPage.xaml
├── ThemeManager.cs              # Global theme management
├── MainWindow.xaml              # Main app layout
└── App.xaml.cs                 # App initialization
```

## 🔄 React ↔ WPF Concepts

| React Concept | WPF Equivalent | What It Does |
|---------------|----------------|--------------|
| `useState()` | DependencyProperty | Component state management |
| `useContext()` | Static Resources + ThemeManager | Global state sharing |
| CSS Variables | DynamicResource | Theme values that update at runtime |
| Component Props | Dependency Properties | Passing data to components |
| Event Handlers | RoutedEventHandler | Component communication |
| React Router | Frame.Navigate() | Page navigation |
| useEffect() | Constructor + Event Subscriptions | Component lifecycle |

## 🧩 Component System

### 1. NavButton (Like a React Component)

**React equivalent:**
```jsx
function NavButton({ text, icon, isActive, onClick }) {
  return (
    <button 
      className={isActive ? 'active' : ''}
      onClick={onClick}
    >
      <Icon icon={icon} />
      <span>{text}</span>
    </button>
  );
}
```

**WPF implementation:**
```xml
<!-- NavButton.xaml -->
<UserControl>
  <Border Background="{DynamicResource NavButtonDefaultBrush}">
    <StackPanel>
      <ui:SymbolIcon Symbol="{Binding Icon}" />
      <TextBlock Text="{Binding Text}" />
    </StackPanel>
  </Border>
</UserControl>
```

```csharp
// NavButton.xaml.cs
public static readonly DependencyProperty TextProperty =
    DependencyProperty.Register("Text", typeof(string), typeof(NavButton));

public string Text
{
    get { return (string)GetValue(TextProperty); }
    set { SetValue(TextProperty, value); }
}

public event RoutedEventHandler Click;

private void OnClick(object sender, RoutedEventArgs e)
{
    if (Click != null)
        Click(this, new RoutedEventArgs());
}
```

**Key concepts:**
- **DependencyProperty** = React state (`useState`)
- **Binding** = React props
- **RoutedEventHandler** = React event handlers

### 2. NavBar (Container Component)

**React equivalent:**
```jsx
function NavBar({ currentPage, onNavigate }) {
  return (
    <nav>
      <NavButton 
        text="Home" 
        isActive={currentPage === 'home'}
        onClick={() => onNavigate('home')}
      />
      <NavButton 
        text="Analysis" 
        isActive={currentPage === 'analysis'}
        onClick={() => onNavigate('analysis')}
      />
    </nav>
  );
}
```

**WPF implementation:**
```csharp
public static readonly DependencyProperty CurrentPageProperty =
    DependencyProperty.Register("CurrentPage", typeof(string), typeof(NavBar));

public string CurrentPage
{
    get { return (string)GetValue(CurrentPageProperty); }
    set { SetValue(CurrentPageProperty, value); }
}

public event RoutedEventHandler HomeRequested;
public event RoutedEventHandler AnalysisRequested;

private void Home_Click(object sender, RoutedEventArgs e)
{
    CurrentPage = "Home";
    if (HomeRequested != null)
        HomeRequested(this, e);
}
```

**Key concepts:**
- **DependencyProperty** = React prop + state
- **Events** = Callback functions (`onNavigate`)
- **State management** = Updating CurrentPage triggers UI updates

## 🎨 Theming System

### CSS Variables vs DynamicResource

**CSS (React):**
```css
:root {
  --primary-color: #0078D4;
  --background-color: #FFFFFF;
  --text-color: #323130;
}

.button {
  background: var(--primary-color);
  color: var(--text-color);
}
```

**WPF XAML:**
```xml
<!-- Light.xaml -->
<Color x:Key="PrimaryColor">#0078D4</Color>
<Color x:Key="BackgroundColor">#FFFFFF</Color>
<Color x:Key="TextColor">#323130</Color>

<SolidColorBrush x:Key="PrimaryBrush" Color="{StaticResource PrimaryColor}"/>
<SolidColorBrush x:Key="BackgroundBrush" Color="{StaticResource BackgroundColor}"/>
<SolidColorBrush x:Key="TextBrush" Color="{StaticResource TextColor}"/>

<!-- Usage -->
<Border Background="{DynamicResource BackgroundBrush}">
  <TextBlock Text="Hello" Foreground="{DynamicResource TextBrush}"/>
</Border>
```

### ThemeManager (Global State)

**React equivalent:**
```jsx
const ThemeContext = createContext();

function ThemeProvider({ children }) {
  const [theme, setTheme] = useState('light');
  
  const toggleTheme = () => setTheme(theme === 'light' ? 'dark' : 'light');
  
  return (
    <ThemeContext.Provider value={{ theme, toggleTheme }}>
      {children}
    </ThemeContext.Provider>
  );
}
```

**WPF implementation:**
```csharp
public static class ThemeManager
{
    private static ThemeType currentTheme = ThemeType.Light;
    public static ThemeType CurrentTheme { get { return currentTheme; } }

    public static event Action<ThemeType> ThemeChanged;

    public static void SetTheme(ThemeType theme)
    {
        currentTheme = theme;
        
        // Load theme resource dictionary
        var themeUri = theme == ThemeType.Light 
            ? new Uri("Themes/Light.xaml", UriKind.Relative)
            : new Uri("Themes/Dark.xaml", UriKind.Relative);

        var app = Application.Current;
        app.Resources.MergedDictionaries.RemoveAt(0);
        
        var themeDict = new ResourceDictionary { Source = themeUri };
        app.Resources.MergedDictionaries.Insert(0, themeDict);

        if (ThemeChanged != null)
            ThemeChanged(theme);
    }

    public static void ToggleTheme()
    {
        SetTheme(currentTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light);
    }
}
```

**Key concepts:**
- **Static class** = React context
- **MergedDictionaries** = CSS variable injection
- **Events** = Context consumer updates
- **DynamicResource** = Automatic UI updates when theme changes

## 🧭 Navigation System

### React Router vs WPF Frame

**React Router:**
```jsx
function App() {
  const [currentPage, setCurrentPage] = useState('home');

  return (
    <div>
      <NavBar currentPage={currentPage} onNavigate={setCurrentPage} />
      <Routes>
        <Route path="/home" element={<HomePage />} />
        <Route path="/analysis" element={<AnalysisPage />} />
      </Routes>
    </div>
  );
}
```

**WPF implementation:**
```xml
<!-- MainWindow.xaml -->
<Grid>
  <Grid.RowDefinitions>
    <RowDefinition Height="*" />
    <RowDefinition Height="Auto" />
  </Grid.RowDefinitions>

  <!-- Content area -->
  <Frame x:Name="MainFrame" Grid.Row="0" NavigationUIVisibility="Hidden" />
  
  <!-- Navigation -->
  <NavBar x:Name="NavigationBar" Grid.Row="1"
           HomeRequested="OnHomeRequested"
           AnalysisRequested="OnAnalysisRequested" />
</Grid>
```

```csharp
// MainWindow.xaml.cs
public partial class MainWindow 
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new HomePage()); // Initial route
    }

    private void OnHomeRequested(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new HomePage()); // Navigate to page
        NavigationBar.CurrentPage = "Home"; // Update navigation state
    }

    private void OnAnalysisRequested(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new AnalysisPage());
        NavigationBar.CurrentPage = "Analysis";
    }
}
```

**Key concepts:**
- **Frame control** = React Router outlet
- **Navigate()** = Programmatic navigation
- **Event system** = Route change handlers
- **State synchronization** = Keeping navigation and content in sync

## 🔄 Data Flow

### 1. Navigation Flow
```
User clicks NavButton 
→ Click event fires 
→ NavBar sets CurrentPage 
→ NavBar raises RoutedEvent 
→ MainWindow receives event 
→ MainWindow.Frame.Navigate() 
→ New page displays 
→ Navigation state updated
```

### 2. Theme Switch Flow
```
User clicks theme toggle 
→ Header calls ThemeManager.ToggleTheme() 
→ ThemeManager switches ResourceDictionary 
→ All {DynamicResource} bindings update 
→ UI instantly reflects new theme
```

### 3. Active State Flow
```
CurrentPage property changes 
→ DependencyProperty change notification 
→ UpdateActiveStates() method called 
→ Iterates through NavButtons 
→ Sets IsActive based on CurrentPage 
→ Visual state updates via DataTriggers
```

## 🎯 Key WPF Concepts for React Developers

### 1. Dependency Properties
```csharp
// Like useState + prop validation
public static readonly DependencyProperty IsActiveProperty =
    DependencyProperty.Register(
        "IsActive",                    // Property name
        typeof(bool),                   // Type
        typeof(NavButton),              // Owner type
        new PropertyMetadata(false)       // Default value
    );

public bool IsActive
{
    get { return (bool)GetValue(IsActiveProperty); }
    set { SetValue(IsActiveProperty, value); }
}
```

### 2. Data Binding
```xml
<!-- One-way binding (like props) -->
<TextBlock Text="{Binding Text}" />

<!-- Two-way binding (like controlled component) -->
<TextBox Text="{Binding UserName, Mode=TwoWay}" />

<!-- Dynamic resource binding (like CSS variables) -->
<Border Background="{DynamicResource BackgroundBrush}" />
```

### 3. Events
```csharp
// Define event
public event RoutedEventHandler HomeRequested;

// Raise event (like callback)
if (HomeRequested != null)
    HomeRequested(this, e);

// Handle event (like event listener)
HomeRequested += OnHomeRequested;
```

### 4. Data Triggers
```xml
<!-- Like conditional CSS classes -->
<Style TargetType="Border">
    <Style.Triggers>
        <DataTrigger Binding="{Binding IsActive}" Value="True">
            <Setter Property="Background" Value="{DynamicResource NavButtonActiveBrush}"/>
        </DataTrigger>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter Property="Background" Value="{DynamicResource NavButtonHoverBrush}"/>
        </Trigger>
    </Style.Triggers>
</Style>
```

## 🚀 Best Practices

### 1. Component Design
- **Single Responsibility**: Each component does one thing well
- **Dependency Properties**: For component state and props
- **Events**: For parent-child communication
- **Resources**: For reusable styles and themes

### 2. Theme Management
- **Resource Dictionaries**: Separate theme files
- **DynamicResource**: For runtime theme switching
- **Centralized Manager**: Single source of truth for theme state
- **Event-driven Updates**: Notify components of theme changes

### 3. Navigation Architecture
- **Frame-based**: Use Frame for page content
- **Event-driven**: Use events for navigation triggers
- **State Synchronization**: Keep navigation and content in sync
- **Loose Coupling**: Components don't depend on specific pages

## 📚 Next Steps

### 1. Enhanced Navigation
- Add breadcrumb navigation
- Implement page transitions
- Add keyboard shortcuts
- Support deep linking

### 2. Advanced Theming
- Add custom theme creation
- Implement theme persistence
- Add accessibility themes
- Support automatic theme switching

### 3. Component Library
- Create reusable UI components
- Implement component documentation
- Add component testing
- Create design system guidelines

## 🎉 Conclusion

This WPF implementation follows React patterns while leveraging WPF's strengths:
- **Declarative UI** through XAML
- **Component-based architecture** 
- **Global state management** with ThemeManager
- **Event-driven communication**
- **Resource-based theming** like CSS variables

The result is a modern, maintainable WPF application that feels familiar to React developers!