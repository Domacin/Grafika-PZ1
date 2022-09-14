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
    /// Interaction logic for ELipsa.xaml
    /// </summary>
    public partial class ELipsa : Window
    {
       

        private  int sirina1 = 0;
        private  int visina1 = 0;
        private double debljina1 = 0;
        private SolidColorBrush boja;
        private SolidColorBrush bojaSlova;
        private string tekstualniSadrzaj = "";


        public  int Sirina1 { get => sirina1; set => sirina1 = value; }
        public  int Visina1 { get => visina1; set => visina1 = value; }
        public  double Debljina1 { get => debljina1; set => debljina1 = value; }
        public SolidColorBrush Boja { get => boja; set => boja = value; }
        public string TekstualniSadrzaj { get => tekstualniSadrzaj; set => tekstualniSadrzaj = value; }
        public SolidColorBrush BojaSlova { get => bojaSlova; set => bojaSlova = value; }

        public ELipsa()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if(Sirina.Text.Trim() == string.Empty || Visina.Text.Trim() == string.Empty || Debljina.Text.Trim() == string.Empty || Boja == null)
            {
                MessageBox.Show("Sva polja moraju biti popunjena! (Osim texta) i mora se izabrati boja!");
                return;
            }
              
            Sirina1 = Convert.ToInt32(Sirina.Text);
            Visina1 = Convert.ToInt32(Visina.Text);
            Debljina1 = Convert.ToDouble(Debljina.Text);
            TekstualniSadrzaj = SadrzajTexta.Text;
            if(TekstualniSadrzaj.Trim() != string.Empty && BojaSlova == null)
            {
                MessageBox.Show("Morate izabrati boju slova!");
                return;
            }
            this.Close();
        }

        private void cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (cp.SelectedColor.HasValue)
            {
                Boja = new SolidColorBrush(cp.SelectedColor.Value);
            }
        }

        private void Visina_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
 

        private void cpSlova_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (cpSlova.SelectedColor.HasValue)
            {
                BojaSlova = new SolidColorBrush(cpSlova.SelectedColor.Value);
            }
        }
    }
}
