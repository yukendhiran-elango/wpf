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

namespace Master.Views.Components.HomePage
{
    /// <summary>
    /// Interaction logic for InfoRow.xaml
    /// </summary>
    public partial class InfoRow : UserControl
    {
        public InfoRow()
        {
            InitializeComponent();
        }
        // 1. Register the "Label" Property
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(InfoRow),
                new PropertyMetadata(string.Empty));

        // 2. Register the "Value" Property
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(string),
                typeof(InfoRow),
                new PropertyMetadata(string.Empty));

        // C# Wrappers (The "Props" you access in code)
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}
