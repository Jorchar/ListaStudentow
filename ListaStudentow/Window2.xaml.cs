using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
        int klucz;
        BitmapImage bi2 = new BitmapImage();
        Image Avatar = new Image();
        public Window2(int item)
        {
            klucz = item;
            InitializeComponent();
            try
            {
                StackPanel1.Children.RemoveAt(0);
            }
            catch (System.ArgumentOutOfRangeException)
            {

            }
            BitmapImage bi = new BitmapImage();
            Imie.Text = MainWindow.studenci[klucz].Imie;
            Wiek.Text = MainWindow.studenci[klucz].Wiek;
            PESEL.Text = MainWindow.studenci[klucz].Pesel;
            string zrobienieTegoWymagaloDuzoPracy = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            bi.UriSource = null;
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(zrobienieTegoWymagaloDuzoPracy +"\\"+ MainWindow.studenci[klucz].AvatarSrc);
            bi.EndInit();
            Avatar.Source = bi;
            StackPanel1.Children.Add(Avatar);
        }

        private void ZmodujStudenta(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(PESEL.Text, @"^\d{11}$") && Regex.IsMatch(Wiek.Text, @"^\p{N}\p{N}*$") && Regex.IsMatch(Imie.Text, @"^\p{L}\p{L}*$"))
            {
                MainWindow.studenci[klucz].Imie = Imie.Text;
                MainWindow.studenci[klucz].Wiek = Wiek.Text;
                MainWindow.studenci[klucz].Pesel = PESEL.Text;
                MainWindow.kurdebele(klucz);
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(Avatar.Source as BitmapImage));
                encoder.QualityLevel = 100;

                StackPanel1.Children.RemoveAt(0);

                GC.Collect();
                try
                {
                    using (var stream = new FileStream("src/" + klucz + ".jpg", FileMode.Create))
                    {
                        encoder.Save(stream);
                        stream.Close();
                    }
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("Obraz jest aktualnie uzywany przez inny program i nie można go skopiowac");
                }
                string zrobienieTegoWymagaloDuzoPracy = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                MainWindow.studenci[klucz].AvatarSrc = "src\\" + klucz + ".jpg";
                this.Close();
            }
            else
            {
                MessageBox.Show("Wprowadz poprawne dane");
            }
        }

        private void DodajObraz(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                bi2.BeginInit();
                bi2.CacheOption = BitmapCacheOption.OnLoad;
                bi2.UriSource = new Uri(openFileDialog.FileName);
                bi2.EndInit();
                this.StackPanel1.Children.RemoveAt(0);
                this.StackPanel1.Children.Add(Avatar);
                Avatar.Source = bi2;
            }
        }

        private void CheckKey2(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"^\p{L}"))
                e.Handled = true;
        }

        private void CheckKey(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"^\p{N}"))
                e.Handled = true;
        }
    }
}