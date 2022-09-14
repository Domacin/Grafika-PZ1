using Primer_2._1.Model;
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
using System.Xml;

namespace Primer_2._1
{
    /// <summary>
    /// Interaction logic for Transformacije2.xaml
    /// </summary>
    public partial class Transformacije2 : Window
    {
       
        public double noviX, noviY,finalniX,finalniY;
        public double lon, lan;
        public double najmanjaLon = 45.189725628196662;
        public double najvecaLon  = 45.328735805580521;
        public double najmanjaLat = 19.727275715138394;
        public double najvecaLat  = 19.950944330565722;
        public int load = 1;
        //public double prethodniNodX = 429.6872721122665;//ovo je prvi nod i njegova x koordinata
        //public double prethodniNodY = 378.12106736508377;// ovo je prvi nod i njegova y koordinata
        //public double faktorSkaliranjaX;
        //public double faktorSkaliranjaY;

        public int pomocnaAproximacija = 1;
        public int poslednjiEl = 0;
        public int unDo = 0; // 0 znaci moze da se izvrsi 1 ne moze
        public int reDo = 1; // 0 znaci moze da se izvrsi 1 ne moze
        public int clear = 0; //ako je cledar 0 znaci da clear nije radjen, a ako je 1 znaci da je clear vec uradjen
        public int editElipsaCount = 0;//ako je jedan onda menjaj boju
        //List<double> nodeEntityX = new List<double>();
       // List<double> nodeEntityY = new List<double>();
        //List<Tuple<double, double>> nodeEntityXY = new List<Tuple<double, double>>(); //prvi double  je x, drugi je y

        List<UIElement> pomocni = new System.Collections.Generic.List<UIElement>();
        Dictionary<long, UIElement> sve = new Dictionary<long, UIElement>();
        List<System.Windows.Point> poligon = new List<System.Windows.Point>();
        public bool[,] matrica = new bool[200, 200];
        Ellipse el = new Ellipse();//Klasa elipsa
        public ELipsa elp;
        public EditElipse ee;

        public Transformacije2()
        {
            this.InitializeComponent();       
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private double pozicijaXKoordinate(double x)
        {
            double pozicija = (x - najmanjaLon) / (najvecaLon - najmanjaLon)*800;
            return pozicija;
        }

        private double pozicijaYKoordinate(double y)
        {
            double pozicija = (y - najmanjaLat) / (najvecaLat - najmanjaLat)*800;
            return pozicija;
        }

        private void Glavni_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Poligon.IsChecked)
            {
                 
                    System.Windows.Point Point = Mouse.GetPosition(Glavni);
                    poligon.Add(Point);
                    
            }
            else 
            {
                System.Windows.Point P = Mouse.GetPosition(Glavni);
           
           

            if (Elipsa.IsChecked)
            {
                 
                elp = new ELipsa(); //novi prozor
                elp.ShowDialog();

                    if(elp.TekstualniSadrzaj.Trim() == string.Empty)
                    {
                           
                            el.Width = elp.Sirina1;
                            el.Height = elp.Visina1;
                            el.StrokeThickness = elp.Debljina1;
                            el.Fill = elp.Boja;
                            el.MouseLeftButtonDown += EditELipse;
                        if(editElipsaCount == 1)
                        {
                            el.Fill = ee.BojaElipse;
                        }
                            Canvas.SetTop(el, P.Y);
                            Canvas.SetLeft(el, P.X);
                            Glavni.Children.Add(el);
                            
                            AddText.IsChecked = false;
                            Poligon.IsChecked = false;
                    }
                     else
                     {
                        Grid grid = new Grid();
                        Ellipse el = new Ellipse();//Klasa elipsa
                        el.Width = elp.Sirina1;
                        el.Height = elp.Visina1;
                        el.StrokeThickness = elp.Debljina1;
                        el.Fill = elp.Boja;
                        Label l = new Label();
                        l.Content = elp.TekstualniSadrzaj;
                        l.Foreground = elp.BojaSlova;
                        l.FontSize = 7;
                        l.VerticalAlignment = VerticalAlignment.Center;
                        l.HorizontalAlignment = HorizontalAlignment.Center;
                        Canvas.SetTop(grid, P.Y);
                        Canvas.SetLeft(grid, P.X);
                        grid.Children.Add(el);
                        grid.Children.Add(l);                      
                        Glavni.Children.Add(grid);
                        AddText.IsChecked = false;
                        Poligon.IsChecked = false;
                    }
                

            }
                else if (AddText.IsChecked)
            {
                 
                AddWindow addW = new AddWindow();
                addW.ShowDialog();
                TextBox tb = new TextBox();
                tb.Text = addW.Text;
                tb.FontSize = addW.FontSize1;
                tb.Foreground = addW.BojaTexta;
                Canvas.SetLeft(tb, P.X);
                Canvas.SetTop(tb, P.Y);
                Glavni.Children.Add(tb);
                Poligon.IsChecked = false;
                Elipsa.IsChecked = false;

            }

            }
        }

        private void MenuItem_Click_Clear(object sender, RoutedEventArgs e)
        {
            foreach (UIElement item in Glavni.Children)
            {
                pomocni.Add(item);
            }
            Glavni.Children.Clear();
            clear = 1;
            unDo = 1;
            reDo = 1;
           
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
             
            for (int i = 0; i <= Glavni.Children.Count; i++)
            {
                poslednjiEl = i;
            }

            if(clear == 1)
            {
                foreach (UIElement item in pomocni)
                {
                    Glavni.Children.Add(item);
                }
                clear = 0;
            }

            if(unDo == 0)
            {
                pomocni.Add(Glavni.Children[poslednjiEl - 1]);
                Glavni.Children.Remove(Glavni.Children[poslednjiEl - 1]);
            }
                

            unDo = 1;
            reDo = 0;
            load++;
        }

        private void ReDoButton_Click(object sender, RoutedEventArgs e)
        {
            if(reDo == 0)
            {
                Glavni.Children.Add(pomocni[0]);
                pomocni.Clear();
            }
            reDo = 1;
            unDo = 0;
            load++;
           
        }

        //ovo se aktivira kad se obiljezi crtanje poligona
        private void Glavni_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PointCollection sveTackePoligona = new PointCollection();
            if (Poligon.IsChecked)
            {

                AddPoligon AddP = new AddPoligon();
                AddP.ShowDialog();
                sveTackePoligona.Clear();
                if (AddP.SadrzajTextaUPoligonu.Trim() == string.Empty)
                {
                    Polygon pol = new Polygon();
                    pol.Fill = AddP.BojaPoligona;
                   
                    
                    foreach (System.Windows.Point pt in poligon)
                    {
                        sveTackePoligona.Add(new System.Windows.Point(pt.X, pt.Y));
                    }
                    foreach (var item in sveTackePoligona)
                    {
                        pol.Points.Add(item);
                    }
                     
                    Glavni.Children.Add(pol);
                    poligon.Clear();
                    AddText.IsChecked = false;
                    Elipsa.IsChecked = false;
                }
                else
                {
                    Grid grid = new Grid();
                    Polygon pol = new Polygon();
                    pol.Fill = AddP.BojaPoligona;

                   
                    foreach (System.Windows.Point pt in poligon)
                    {
                        sveTackePoligona.Add(new System.Windows.Point(pt.X, pt.Y));
                    }

                    

                    Label l = new Label();
                    l.Content = AddP.SadrzajTextaUPoligonu;
                    l.FontSize = 7;
                    l.Foreground = AddP.BojaSlovaPoligona;
                    l.HorizontalAlignment = HorizontalAlignment.Center;
                    l.VerticalAlignment = VerticalAlignment.Center;
                     
                    
                    pol.Points = sveTackePoligona;
                    grid.Children.Add(pol);
                    grid.Children.Add(l);
                    Glavni.Children.Add(grid);
                    poligon.Clear();
                    AddText.IsChecked = false;
                    Elipsa.IsChecked = false;
                }

                
            }   
        }

        private void DugmeLoad_Click(object sender, RoutedEventArgs e)
        {
            if(load == 1)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Geographic.xml");

                XmlNodeList nodeList;
                SubstationEntity sub = new SubstationEntity();

                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");
                Rectangle[] r = new Rectangle[5000];
                List<Rectangle> listaEntiteta = new List<Rectangle>();
                int i = 1;


                //ovo je ok, nista se ne preklapa
                foreach (XmlNode node in nodeList)
                {

                    r[i] = new Rectangle();
                    sub.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    sub.Name = node.SelectSingleNode("Name").InnerText;
                    sub.X = double.Parse(node.SelectSingleNode("X").InnerText);
                    sub.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                    ToLatLon(sub.X, sub.Y, 34, out noviX, out noviY);

                    noviX = pozicijaXKoordinate(noviX);
                    noviY = pozicijaYKoordinate(noviY);

                    int matricaX = Convert.ToInt32(Math.Round(noviX / 4));
                    int matricaY = Convert.ToInt32(Math.Round(noviY / 4));


                    if (matricaX == 200)
                        matricaX = 199;
                    else if (matricaY == 200)
                        matricaY = 199;

                    if (matrica[matricaX, matricaY] == false)
                    {
                        matrica[matricaX, matricaY] = true;
                        finalniX = matricaX * 4;
                        finalniY = matricaY * 4;
                    }
                    else
                    {
                        SlobodnaPozicija(matricaX, matricaY, noviX, noviY, out finalniX, out finalniY);
                    }


                    r[i].Height = 4;
                    r[i].Width = 4;
                    r[i].Stroke = Brushes.Black;
                    r[i].Fill = Brushes.Green;
                    r[i].StrokeThickness = 0.1;
                    r[i].ToolTip = "ID: " + sub.Id.ToString() + "\nName: " + sub.Name.ToString();

                    Canvas.SetBottom(r[i], finalniX);
                    Canvas.SetLeft(r[i], finalniY);
                    Glavni.Children.Add(r[i]);
                    listaEntiteta.Add(r[i]);
                    sve.Add(sub.Id, r[i]);

                    i++;
                }


                NodeEntity nodeobj = new NodeEntity();

                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Nodes/NodeEntity");

                //ovi se malo preklapaju
                foreach (XmlNode node in nodeList)
                {
                    r[i] = new Rectangle();
                    nodeobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    nodeobj.Name = node.SelectSingleNode("Name").InnerText;
                    nodeobj.X = double.Parse(node.SelectSingleNode("X").InnerText);
                    nodeobj.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                    ToLatLon(nodeobj.X, nodeobj.Y, 34, out noviX, out noviY);
                    noviX = pozicijaXKoordinate(noviX);
                    noviY = pozicijaYKoordinate(noviY);

                    int matricaX = Convert.ToInt32(Math.Round(noviX / 4));
                    int matricaY = Convert.ToInt32(Math.Round(noviY / 4));


                    if (matricaX == 200)
                        matricaX = 199;
                    else if (matricaY == 200)
                        matricaY = 199;

                    if (matrica[matricaX, matricaY] == false)
                    {
                        matrica[matricaX, matricaY] = true;
                        finalniX = matricaX * 4;
                        finalniY = matricaY * 4;
                    }
                    else
                    {
                        SlobodnaPozicija(matricaX, matricaY, noviX, noviY, out finalniX, out finalniY);
                    }




                    r[i].Height = 4;
                    r[i].Width = 4;
                    r[i].Stroke = Brushes.Black;
                    r[i].Fill = Brushes.Red;
                    r[i].StrokeThickness = 0.1;
                    r[i].ToolTip = "ID: " + nodeobj.Id.ToString() + "\nName: " + nodeobj.Name.ToString();

                    Canvas.SetBottom(r[i], finalniX);
                    Canvas.SetLeft(r[i], finalniY);
                    Glavni.Children.Add(r[i]);
                    listaEntiteta.Add(r[i]);
                    sve.Add(nodeobj.Id, r[i]);
 
                    i++;
                }

                SwitchEntity switchobj = new SwitchEntity();

                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Switches/SwitchEntity");
                //ovi se malo preklapaju
                foreach (XmlNode node in nodeList)
                {
                    r[i] = new Rectangle();
                    switchobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    switchobj.Name = node.SelectSingleNode("Name").InnerText;
                    switchobj.X = double.Parse(node.SelectSingleNode("X").InnerText);
                    switchobj.Y = double.Parse(node.SelectSingleNode("Y").InnerText);
                    switchobj.Status = node.SelectSingleNode("Status").InnerText;

                    ToLatLon(switchobj.X, switchobj.Y, 34, out noviX, out noviY);
                    noviX = pozicijaXKoordinate(noviX);
                    noviY = pozicijaYKoordinate(noviY);

                    int matricaX = Convert.ToInt32(Math.Round(noviX / 4));
                    int matricaY = Convert.ToInt32(Math.Round(noviY / 4));


                    if (matricaX == 200)
                        matricaX = 199;
                    else if (matricaY == 200)
                        matricaY = 199;

                    if (matrica[matricaX, matricaY] == false)
                    {
                        matrica[matricaX, matricaY] = true;
                        finalniX = matricaX * 4;
                        finalniY = matricaY * 4;
                    }
                    else
                    {
                        SlobodnaPozicija(matricaX, matricaY, noviX, noviY, out finalniX, out finalniY);
                    }


                    r[i].Height = 4;
                    r[i].Width = 4;
                    r[i].Stroke = Brushes.Black;
                    r[i].Fill = Brushes.Blue;
                    r[i].StrokeThickness = 0.1;
                    r[i].ToolTip = "ID: " + switchobj.Id.ToString() + "\nName: " + switchobj.Name.ToString();

                    Canvas.SetBottom(r[i], finalniX);
                    Canvas.SetLeft(r[i], finalniY);
                    Glavni.Children.Add(r[i]);
                    listaEntiteta.Add(r[i]);
                    sve.Add(switchobj.Id, r[i]);


                    i++;
                }


                LineEntity l = new LineEntity();
                nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Lines/LineEntity");

                foreach(XmlNode node in nodeList)
                {
                    l.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                    l.Name = node.SelectSingleNode("Name").InnerText;
                    if (node.SelectSingleNode("IsUnderground").InnerText.Equals("true"))
                    {
                        l.IsUnderground = true;
                    }
                    else
                    {
                        l.IsUnderground = false;
                    }
                    l.R = float.Parse(node.SelectSingleNode("R").InnerText);
                    l.ConductorMaterial = node.SelectSingleNode("ConductorMaterial").InnerText;
                    l.LineType = node.SelectSingleNode("LineType").InnerText;
                    l.ThermalConstantHeat = long.Parse(node.SelectSingleNode("ThermalConstantHeat").InnerText);
                    l.FirstEnd = long.Parse(node.SelectSingleNode("FirstEnd").InnerText);
                    l.SecondEnd = long.Parse(node.SelectSingleNode("SecondEnd").InnerText);

                    PointCollection tacke = new PointCollection();
                    if(sve.ContainsKey(l.FirstEnd) && sve.ContainsKey(l.SecondEnd))
                    {
                        System.Windows.Point A = new System.Windows.Point(Canvas.GetLeft(sve[l.FirstEnd]) + 2, 800 - Canvas.GetBottom(sve[l.FirstEnd]) - 2);
                        System.Windows.Point B = new System.Windows.Point(Canvas.GetLeft(sve[l.SecondEnd]) + 2, 800 - Canvas.GetBottom(sve[l.SecondEnd]) - 2);

                        tacke.Add(A);
                        if(A.X != B.X && A.Y != B.Y)
                        {
                            System.Windows.Point C = TackaZaSkretanje(A, B);
                            tacke.Add(C);
                        }

                        tacke.Add(B);

                        SolidColorBrush bojaLinije = new SolidColorBrush();
                        bojaLinije.Color = Colors.DeepPink;
                        Polyline polyline = new Polyline { Points = tacke, Stroke = bojaLinije, StrokeThickness = 1, ToolTip = "ID: " + l.Id + "\nName: " + l.Name};
                        Glavni.Children.Add(polyline);
                        polyline.MouseLeftButtonDown += PolylineClick;
                    }
                }
            }

            load++;

        }

        public void EditELipse(object sender,MouseButtonEventArgs e)
        {
            ee = new EditElipse();
            ee.ShowDialog();
            elp.Boja = ee.BojaElipse;
            editElipsaCount = 1;
        }
       public void PolylineClick(object sender, MouseButtonEventArgs e)
        {
            Polyline p = (Polyline)sender;
            p.Stroke = Brushes.Green;
        }

        public static void ToLatLon(double utmX, double utmY, int zoneUTM, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = true;

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = zoneUTM;
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }

        public void SlobodnaPozicija(int matricaX,int matricaY,double x,double y,out double noviX, out double noviY)
        {
            int radiusPretrage = 2;
            if(matricaX < (x / 4))
            {
                if(matricaY < (y / 4))
                {
                    for(int g = matricaX - radiusPretrage; g<= matricaX + radiusPretrage; g++)
                    {
                        for(int k = matricaY - radiusPretrage; k<= matricaY + radiusPretrage; k++)
                        {
                            if(g >= 0 && g<200 && k>= 0 && k < 200)
                            {
                                if(matrica[g,k] == false)
                                {
                                    matrica[g, k] = true;
                                    noviX = g * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if(g == 200)
                            {
                                if(matrica[199,k] == false)
                                {
                                    matrica[199, k] = true;
                                    noviX = 199 * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if(k == 200)
                            {
                                if(matrica[g,199] == false)
                                {
                                    matrica[g, 199] = true;
                                    noviX = g * 4;
                                    noviY = 199 * 4;
                                    return; 
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int g = matricaX - radiusPretrage; g <= matricaX + radiusPretrage; g++)
                    {
                        for (int k = matricaY + radiusPretrage; k >= matricaY - radiusPretrage; k--)
                        {
                            if (g >= 0 && g < 200 && k >= 0 && k < 200)
                            {
                                if (matrica[g, k] == false)
                                {
                                    matrica[g, k] = true;
                                    noviX = g * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if (g == 200)
                            {
                                if (matrica[199, k] == false)
                                {
                                    matrica[199, k] = true;
                                    noviX = 199 * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if (k == 200)
                            {
                                if (matrica[g, 199] == false)
                                {
                                    matrica[g, 199] = true;
                                    noviX = g * 4;
                                    noviY = 199 * 4;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if(matricaY< (y / 4))
                {
                    for (int g = matricaX + radiusPretrage; g >= matricaX - radiusPretrage; g--)
                    {
                        for (int k = matricaY - radiusPretrage; k <= matricaY + radiusPretrage; k++)
                        {
                            if (g >= 0 && g < 200 && k >= 0 && k < 200)
                            {
                                if (matrica[g, k] == false)
                                {
                                    matrica[g, k] = true;
                                    noviX = g * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if (g == 200)
                            {
                                if (matrica[199, k] == false)
                                {
                                    matrica[199, k] = true;
                                    noviX = 199 * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if (k == 200)
                            {
                                if (matrica[g, 199] == false)
                                {
                                    matrica[g, 199] = true;
                                    noviX = g * 4;
                                    noviY = 199 * 4;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int g = matricaX + radiusPretrage; g >= matricaX - radiusPretrage; g--)
                    {
                        for (int k = matricaY + radiusPretrage; k >= matricaY - radiusPretrage; k--)
                        {
                            if (g >= 0 && g < 200 && k >= 0 && k < 200)
                            {
                                if (matrica[g, k] == false)
                                {
                                    matrica[g, k] = true;
                                    noviX = g * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if (g == 200)
                            {
                                if (matrica[199, k] == false)
                                {
                                    matrica[199, k] = true;
                                    noviX = 199 * 4;
                                    noviY = k * 4;
                                    return;
                                }
                            }
                            else if (k == 200)
                            {
                                if (matrica[g,199] == false)
                                {
                                    matrica[g, 199] = true;
                                    noviX = g * 4;
                                    noviY = 199 * 4;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            noviX = matricaX * 4;
            noviY = matricaY * 4;
           
        }

        public System.Windows.Point TackaZaSkretanje(System.Windows.Point a, System.Windows.Point b)
        {
            if (a.X < b.X)
                return new System.Windows.Point(b.X, a.Y);
            else
                return new System.Windows.Point(a.X, b.Y);
        }
    }
}
