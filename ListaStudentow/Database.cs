using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ListaStudentow
{
    class Database
    {
        public static string connetionString;
        public static SqlConnection cnn;
        public static SqlCommand command;
        public static SqlDataAdapter adapter = new SqlDataAdapter();
        public static DataTable dataTable = new DataTable();

        public static void openConnection()
        {
            connetionString = @"Data source=DESKTOP-VLVGDFH; database=ListaStudentow; Integrated Security=SSPI;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

        }

        public static void closeConnection()
        {
            cnn.Close();
        }

        public static void readBase()
        {
            command = new SqlCommand();

            command.CommandText = "Select [Imie], [Wiek], [Pesel], [Kierunek], [AvatarSrc] from studenci";
            usingBase();
        }
        public static void countBase()
        {
            command = new SqlCommand();

            command.CommandText = "Select [Kierunek], Count([Pesel]) AS Ilosc_studentow from studenci GROUP BY [Kierunek]";
            usingBase();
        }
        public static void averageBase()
        {
            command = new SqlCommand();

            command.CommandText = "Select Avg([Wiek]) AS Srednia_wieku from studenci";
            usingBase();
        }
        public static void lecturerBase()
        {
            command = new SqlCommand();

            command.CommandText = "Select studenci.[Imie], studenci.[Kierunek], wykladowcy.[Imie] AS Imie_wykladowcy from studenci Inner Join wykladowcy On studenci.[Kierunek]=wykladowcy.[Kierunek]";
            usingBase();
        }
        public static void saveBase(string komenda)
        {
            command = new SqlCommand();

            command.CommandText = komenda;
            command.CommandType = CommandType.Text;
            command.Connection = cnn;

            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable("studenci");
            adapter.Fill(dataTable);
        }
        public static void usingBase()
        {
            command.CommandType = CommandType.Text;
            command.Connection = cnn;

            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable("studenci");
            adapter.Fill(dataTable);
        }
    }
}
