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
    /// Interaction logic for Transformacije.xaml
    /// </summary>
    public partial class Transformacije : Window
    {
        private TransformGroup group = new TransformGroup();
        private RotateTransform rotate = new RotateTransform(0);
        private ScaleTransform scale = new ScaleTransform(1, 1);
        private SkewTransform skew = new SkewTransform(0, 0);
        private TranslateTransform trans = new TranslateTransform(0, 0);

        public Transformacije()
        {
            this.InitializeComponent();

            // add the transformations to the group
            group.Children.Add(rotate);
            group.Children.Add(scale);
            group.Children.Add(skew);
            group.Children.Add(trans);
        }

        private void OnSceneLoaded(object sender,
                                   System.Windows.RoutedEventArgs e)
        {
            // set the transformation property of the TextBox to the
            // transformations group
            TextBox.RenderTransform = group;
        }

        private void OnAngleChanged(object sender,
           System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            // change the angle
            rotate.Angle = e.NewValue;
        }

        private void OnScaleChanged(object sender,
           System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            // change the values of ratio on the two axes
            scale.ScaleX = e.NewValue / 100;
            scale.ScaleY = e.NewValue / 100;
        }

        private void OnSkewXChanged(object sender,
           System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            skew.AngleX = e.NewValue;
        }

        private void OnSkewYChanged(object sender,
           System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            skew.AngleY = e.NewValue;
        }

        private void OnTranslateX(object sender,
           System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            trans.X = e.NewValue;
        }

        private void OnTranslateY(object sender,
           System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            trans.Y = e.NewValue;
        }
    }
}