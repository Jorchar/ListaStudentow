using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2(int item)
        {
            int klucz = item;
            InitializeComponent();
            Imie.Text = MainWindow.studenci[klucz].Imie;
            Wiek.Text = MainWindow.studenci[klucz].Wiek;
            PESEL.Text = MainWindow.studenci[klucz].Pesel;
            BitmapImage zdjecie = new BitmapImage();
            zdjecie.BeginInit();
            zdjecie.UriSource = new Uri(MainWindow.studenci[klucz].AvatarSrc);
            zdjecie.EndInit();
            Avatar.Source = zdjecie;
            x.Content = klucz;
        }

        private void ZmodujStudenta(object sender, RoutedEventArgs e)
        {
            MainWindow.studenci[Convert.ToInt32(x.Content)].Imie = Imie.Text;
            MainWindow.studenci[Convert.ToInt32(x.Content)].Wiek = Wiek.Text;
            MainWindow.studenci[Convert.ToInt32(x.Content)].Pesel = PESEL.Text;
            MainWindow.studenci[Convert.ToInt32(x.Content)].AvatarSrc = Avatar.Source.ToString();
            this.Close();
        }

        private void DodajObraz(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage zdjecie = new BitmapImage(fileUri);
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(zdjecie));
                encoder.QualityLevel = 100;
                using (var stream = new FileStream("src/" + MainWindow.studenci.Count + ".jpg", FileMode.Create))
                {
                    encoder.Save(stream);
                }
                string zrobienieTegoWymagaloDuzoPracy = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Uri fileUri2 = new Uri(zrobienieTegoWymagaloDuzoPracy + "/src/" + MainWindow.studenci.Count + ".jpg");
                Avatar.Source = new BitmapImage(fileUri2);
            }
        }
    }
}
