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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ListaStudentow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [XmlArray("studenci"), XmlArrayItem(typeof(List<Student>), ElementName = "Student")]
        public static List<Student> studenci = new List<Student>();
        public MainWindow()
        {
            InitializeComponent();
            Window1 win2 = new Window1();
            win2.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            uniStudents.ItemsSource = null;
            uniStudents.ItemsSource = studenci;
        }
        
        private void Button_Click2(object sender, RoutedEventArgs e)
        {

            XmlSerializer ser = new XmlSerializer(typeof(List<Student>));

            using (FileStream fs = new FileStream("../../xml/sertest.xml", FileMode.Create))
            {
                    ser.Serialize(fs, studenci);
            }
        }

        private void listView_Click(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedIndex;
            if (item > -1)
            {
                Window2 win3 = new Window2(item);
                win3.Show();
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var mySerializer = new XmlSerializer(typeof(List<Student>));
                var myFileStream = new FileStream(openFileDialog.FileName, FileMode.Open);
                studenci = (List<Student>)mySerializer.Deserialize(myFileStream);
                uniStudents.ItemsSource = null;
                uniStudents.ItemsSource = studenci;
            }
        }
    }
}
public class Student
{
    public string Imie { get; set; }
    public string Wiek { get; set; }
    public string Pesel { get; set; }
    public string AvatarSrc { get; set; }
}