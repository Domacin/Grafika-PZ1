using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Primer_2._1
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private string text = "";
        private double fontSize = 0;
        private SolidColorBrush bojaTexta;
        public AddWindow()
        {
            InitializeComponent();
        }

        public string Text { get => text; set => text = value; }
        public double FontSize1 { get => fontSize; set => fontSize = value; }
        public SolidColorBrush BojaTexta { get => bojaTexta; set => bojaTexta = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Text = TextContent.Text;
            FontSize1 = Convert.ToDouble(FontSizeContent.Text);
            this.Close();
        }

        private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (cp.SelectedColor.HasValue)
            {
                BojaTexta = new SolidColorBrush(cp.SelectedColor.Value);
            }
        }

        private void TextContent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
