using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Database.readBase();
            myDataGrid.ItemsSource = Database.dataTable.DefaultView;
        }

        private void Zapisz(object sender, RoutedEventArgs e)
        {
            string dana1 = "";
            string dana2 = "";
            string dana3 = "";
            string dana4 = "";
            string index = "";
            string update = "";
            int x = 0;

            while(true)
            {
                try
                {
                    DataRowView rows = (DataRowView)myDataGrid.Items[x];
                    index = rows.Row[2].ToString();
                    dana1 = rows.Row[0].ToString();
                    dana2 = rows.Row[1].ToString();
                    dana3 = rows.Row[3].ToString();
                    dana4 = rows.Row[4].ToString();
                }
                catch (System.InvalidCastException)
                {
                    break;
                }

                update = "INSERT INTO studenci([Imie], [Wiek], [Kierunek], [Pesel], [AvatarSrc]) VALUES ('" + dana1 + "', '" + dana2 + "', '" + dana3 + "', '" + index + "', '" + dana4 + "')";

                try
                {
                    Database.saveBase(update);
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    update = "UPDATE studenci SET [Imie] = '" + dana1 + "', [Wiek] = '" + dana2 + "', [Kierunek] = '" + dana3 + "', [AvatarSrc] = '" + dana4 + "' from studenci WHERE [Pesel]='" + index + "'";
                    Database.saveBase(update);
                }
                x++;
            }
            Database.readBase();
            myDataGrid.ItemsSource = Database.dataTable.DefaultView;
        }

        private void Kierunki(object sender, RoutedEventArgs e)
        {
            Database.countBase();

            myDataGrid.ItemsSource = Database.dataTable.DefaultView;
        }

        private void Sredni(object sender, RoutedEventArgs e)
        {
            Database.averageBase();

            myDataGrid.ItemsSource = Database.dataTable.DefaultView;
        }

        private void Wykladowcy(object sender, RoutedEventArgs e)
        {
            Database.lecturerBase();

            myDataGrid.ItemsSource = Database.dataTable.DefaultView;
        }

        private void Wroc(object sender, RoutedEventArgs e)
        {
            Database.readBase();

            myDataGrid.ItemsSource = Database.dataTable.DefaultView;
        }
    }
}
