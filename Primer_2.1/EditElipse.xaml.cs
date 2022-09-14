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
using System.Windows.Shapes;

namespace Primer_2._1
{
    /// <summary>
    /// Interaction logic for EditElipse.xaml
    /// </summary>
    public partial class EditElipse : Window
    {
        public SolidColorBrush BojaElipse { get; set; }
        public EditElipse()
        {
            InitializeComponent();
        }

        private void CpElipsa_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            BojaElipse = new SolidColorBrush(CpElipsa.SelectedColor.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }
    }
}
