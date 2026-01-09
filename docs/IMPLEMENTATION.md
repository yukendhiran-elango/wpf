# Implementation Details

## 🎯 What Was Built

### 1. Navigation System
- **Component-based navigation** similar to React Router
- **Active state highlighting** for current page
- **Event-driven architecture** for loose coupling
- **Frame-based content switching**

### 2. Theming System  
- **CSS variable equivalent** using Resource Dictionaries
- **Light/Dark theme support** with runtime switching
- **Global theme manager** for centralized state
- **Dynamic resource binding** for automatic UI updates

### 3. Component Architecture
- **Reusable NavButton** component
- **NavBar container** with state management
- **Header** with theme toggle functionality
- **Page components** with theme integration

## 🏗️ How It Works

### Navigation Flow

1. **Initial Setup**
   ```csharp
   // MainWindow constructor
   InitializeComponent();
   MainFrame.Navigate(new HomePage());        // Set initial page
   NavigationBar.CurrentPage = "Home";        // Set nav state
   ```

2. **User Interaction**
   ```csharp
   // User clicks nav button
   private void Home_Click(object sender, RoutedEventArgs e)
   {
       CurrentPage = "Home";                   // Update internal state
       if (HomeRequested != null)               // Fire event to parent
           HomeRequested(this, e);
   }
   ```

3. **Parent Handling**
   ```csharp
   // MainWindow receives event
   private void OnHomeRequested(object sender, RoutedEventArgs e)
   {
       MainFrame.Navigate(new HomePage());        // Navigate to page
       NavigationBar.CurrentPage = "Home";        // Sync nav state
   }
   ```

4. **Visual Update**
   ```csharp
   // CurrentPage property change triggers
   private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   {
       ((NavBar)d).UpdateActiveStates();          // Update button states
   }
   ```

### Theme Switching Flow

1. **User Action**
   ```csharp
   // Theme toggle button clicked
   private void ThemeToggle_Click(object sender, RoutedEventArgs e)
   {
       ThemeManager.ToggleTheme();                  // Request theme change
   }
   ```

2. **Theme Manager**
   ```csharp
   public static void ToggleTheme()
   {
       var newTheme = currentTheme == ThemeType.Light ? ThemeType.Dark : ThemeType.Light;
       SetTheme(newTheme);
   }

   public static void SetTheme(ThemeType theme)
   {
       currentTheme = theme;
       
       // Switch resource dictionary (CSS variables)
       var app = Application.Current;
       app.Resources.MergedDictionaries.RemoveAt(0);
       
       var themeDict = new ResourceDictionary 
       { 
           Source = theme == ThemeType.Light 
               ? new Uri("Themes/Light.xaml", UriKind.Relative)
               : new Uri("Themes/Dark.xaml", UriKind.Relative)
       };
       app.Resources.MergedDictionaries.Insert(0, themeDict);

       if (ThemeChanged != null)                 // Notify subscribers
           ThemeChanged(theme);
   }
   ```

3. **Automatic UI Updates**
   ```xml
   <!-- DynamicResource automatically updates when theme changes -->
   <Border Background="{DynamicResource BackgroundBrush}">
     <TextBlock Foreground="{DynamicResource TextBrush}" />
   </Border>
   ```

### Component State Management

1. **Dependency Properties** (React useState)
   ```csharp
   // NavButton active state
   public static readonly DependencyProperty IsActiveProperty =
       DependencyProperty.Register(
           "IsActive",                    // Property name
           typeof(bool),                   // Type
           typeof(NavButton),             // Owner
           new PropertyMetadata(false)      // Default value
       );

   public bool IsActive
   {
       get { return (bool)GetValue(IsActiveProperty); }
       set { SetValue(IsActiveProperty, value); }   // Triggers UI update
   }
   ```

2. **Event Communication** (React callbacks)
   ```csharp
   // Define event interface
   public event RoutedEventHandler HomeRequested;

   // Component raises event
   private void OnClick(object sender, RoutedEventArgs e)
   {
       CurrentPage = "Home";                   // Internal state update
       if (HomeRequested != null)               // Parent notification
           HomeRequested(this, e);
   }
   ```

3. **Data Triggers** (Conditional CSS)
   ```xml
   <!-- Like className={isActive ? 'active' : ''} -->
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

## 🎨 Theme Resource System

### Resource Dictionary Structure
```xml
<!-- Like CSS custom properties -->
<ResourceDictionary>
    <!-- Color tokens -->
    <Color x:Key="PrimaryColor">#0078D4</Color>
    <Color x:Key="BackgroundColor">#FFFFFF</Color>
    <Color x:Key="TextColor">#323130</Color>
    
    <!-- Brush tokens -->
    <SolidColorBrush x:Key="PrimaryBrush" Color="{StaticResource PrimaryColor}"/>
    <SolidColorBrush x:Key="BackgroundBrush" Color="{StaticResource BackgroundColor}"/>
    <SolidColorBrush x:Key="TextBrush" Color="{StaticResource TextColor}"/>
    
    <!-- Component-specific tokens -->
    <SolidColorBrush x:Key="NavButtonActiveBrush" Color="{StaticResource PrimaryColor}"/>
    <SolidColorBrush x:Key="NavIconActiveBrush" Color="#FFFFFF"/>
</ResourceDictionary>
```

### Usage Patterns
```xml
<!-- Component uses theme resources -->
<Border Background="{DynamicResource NavButtonDefaultBrush}">
    <ui:SymbolIcon 
        Symbol="{Binding Icon}" 
        Foreground="{DynamicResource NavIconDefaultBrush}">
        <ui:SymbolIcon.Style>
            <Style TargetType="ui:SymbolIcon">
                <Style.Triggers>
                    <DataTrigger Binding="{Binding IsActive}" Value="True">
                        <Setter Property="Foreground" Value="{DynamicResource NavIconActiveBrush}"/>
                    </DataTrigger>
                </Style.Triggers>
            </Style>
        </ui:SymbolIcon.Style>
    </ui:SymbolIcon>
</Border>
```

## 🔄 State Synchronization

### Navigation State
1. **NavBar.CurrentPage** - Internal navigation state
2. **MainFrame.Content** - Current displayed page  
3. **Synchronization** - Events keep them in sync

### Theme State  
1. **ThemeManager.currentTheme** - Global theme enum
2. **ResourceDictionary** - Current theme values
3. **UI Bindings** - Automatic updates via DynamicResource

### Component State
1. **NavButton.IsActive** - Visual active state
2. **Data Binding** - Synchronizes with CurrentPage
3. **Triggers** - Updates visual appearance

## 🎯 Key Architectural Decisions

### 1. Event-Driven Communication
- **Benefits**: Loose coupling, testability, scalability
- **Implementation**: RoutedEventHandler pattern
- **React analogy**: Props callbacks

### 2. Centralized Theme Management
- **Benefits**: Single source of truth, global consistency
- **Implementation**: Static ThemeManager class
- **React analogy**: Context provider

### 3. Resource-Based Theming
- **Benefits**: Runtime switching, CSS-like variables
- **Implementation**: DynamicResource + ResourceDictionary
- **React analogy**: CSS custom properties

### 4. Component-Based Architecture
- **Benefits**: Reusability, maintainability
- **Implementation**: UserControl + DependencyProperties
- **React analogy**: Functional components

## 🚀 Performance Optimizations

### 1. Efficient Resource Loading
```csharp
// Clear and insert at index 0 for performance
app.Resources.MergedDictionaries.RemoveAt(0);
app.Resources.MergedDictionaries.Insert(0, themeDict);
```

### 2. Minimal Visual Tree Traversal
```csharp
// Use specific naming instead of tree walking
var activeButton = CurrentPage switch
{
    "Home" => FindButtonByText("Home"),
    "Analysis" => FindButtonByText("Analysis"),
    _ => null
};
```

### 3. Static Event Pattern
```csharp
// Static events for global subscription
public static event Action<ThemeType> ThemeChanged;
```

## 🎉 Result

This implementation provides:
- **React-like developer experience** with familiar patterns
- **Modern theming system** equivalent to CSS variables
- **Component reusability** and maintainability
- **Runtime theme switching** without application restart
- **Navigation state management** with visual feedback
- **Loose coupling** between components

The system demonstrates how WPF concepts map to React patterns, making it easier for React developers to learn WPF while building production-ready applications.