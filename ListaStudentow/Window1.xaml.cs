using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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



namespace ListaStudentow
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string dane1;
        private string dane2;
        private string dane3;
        private string dane4;
        public Window1()
        {
            InitializeComponent();
        }

        private void DodajStudenta(object sender, RoutedEventArgs e)
        {
            dane1 = Imie.Text;
            dane2 = Wiek.Text;
            dane3 = PESEL.Text;

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Avatar.Source as BitmapImage));
            encoder.QualityLevel = 100;
            using (var stream = new FileStream("src\\" + MainWindow.studenci.Count + ".jpg", FileMode.Create))
            {
                encoder.Save(stream);
                stream.Close();
            }
            string zrobienieTegoWymagaloDuzoPracy = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dane4 = zrobienieTegoWymagaloDuzoPracy + "\\src\\" + MainWindow.studenci.Count + ".jpg";

            if (dane2.All(char.IsNumber) && dane3.All(char.IsNumber) && dane1.All(char.IsLetter)){
                MainWindow.studenci.Add(new Student() { Imie = dane1, Wiek = dane2, Pesel = dane3, AvatarSrc = dane4 });
            }
            else
            {
                MessageBox.Show("Aby dodać studenta, w polu wiek/PESEL podaj liczby, a w polu imie podaj litery");
            }
        }

        private void DodajObraz(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                Avatar.Source = new BitmapImage(fileUri);
            }
        }
    }
}
