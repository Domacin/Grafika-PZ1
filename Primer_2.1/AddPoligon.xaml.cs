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
    /// Interaction logic for AddPoligon.xaml
    /// </summary>
    public partial class AddPoligon : Window
    {
        public double DebljinaPoligona { get; set; }
        public string SadrzajTextaUPoligonu { get; set; }
        public SolidColorBrush BojaPoligona { get; set; }
        public SolidColorBrush BojaSlovaPoligona { get; set; }


        public AddPoligon()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DebljinaPolygona.Text.Trim() == string.Empty || BojaPoligona == null)
            {
                MessageBox.Show("Morate unijeti debljinu! i izabrati boju poligona!");
                return;                  
            }

            if(SadrzajTextaPoligona.Text.Trim() != string.Empty && BojaSlovaPoligona == null)
            {
                MessageBox.Show("Izaberite boju za slova u poligonu!");
            }
            DebljinaPoligona = Convert.ToDouble(DebljinaPolygona.Text);
            SadrzajTextaUPoligonu = SadrzajTextaPoligona.Text;
            this.Close();
        }

        private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            BojaPoligona = new SolidColorBrush(cp.SelectedColor.Value);
        }

        private void cpSlovaPoligon_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            BojaSlovaPoligona = new SolidColorBrush(cpSlovaPoligon.SelectedColor.Value);
        }

        private void DebljinaPolygona_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
