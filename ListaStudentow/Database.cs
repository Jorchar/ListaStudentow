using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

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
            //command = new SqlCommand();

            //command.CommandText = "Select Avg([Wiek]) AS Srednia_wieku from studenci";

            command = new SqlCommand();
            command.CommandText = "EXEC dbo.DOPISZ 'Jordan', 12, 99988877766, 'asd', 'asd'";
            usingBase();
        }
        public static void lecturerBase()
        {
            command = new SqlCommand();

            command.CommandText = "Select studenci.[Imie], studenci.[Kierunek], wykladowcy.[Imie] AS Imie_wykladowcy from studenci Inner Join wykladowcy On studenci.[Kierunek]=wykladowcy.[Kierunek]";
            usingBase();
        }
        public static void saveBase(string dana1, string dana2, string dana3, string dana4, string indeks, string update)
        {
            command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = update;
            command.Connection = cnn;

            SqlParameter pms0 = new SqlParameter();
            SqlParameter pms1 = new SqlParameter();
            SqlParameter pms2 = new SqlParameter();
            SqlParameter pms3 = new SqlParameter();
            SqlParameter pms4 = new SqlParameter();

            pms0.Value = dana1;
            pms0.Direction = ParameterDirection.Input;
            pms0.DbType = DbType.String;
            pms0.ParameterName = "@dana1";


            pms1.Value = dana2;
            pms1.Direction = ParameterDirection.Input;
            pms1.DbType = DbType.String;
            pms1.ParameterName = "@dana2";

            pms2.Value = dana3;
            pms2.Direction = ParameterDirection.Input;
            pms2.DbType = DbType.String;
            pms2.ParameterName = "@dana3";

            pms3.Value = dana4;
            pms3.Direction = ParameterDirection.Input;
            pms3.DbType = DbType.String;
            pms3.ParameterName = "@dana4";

            pms4.Value = indeks;
            pms4.Direction = ParameterDirection.Input;
            pms4.DbType = DbType.String;
            pms4.ParameterName = "@indeks";

            command.Parameters.Add(pms0);
            command.Parameters.Add(pms1);
            command.Parameters.Add(pms2);
            command.Parameters.Add(pms3);
            command.Parameters.Add(pms4);

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
