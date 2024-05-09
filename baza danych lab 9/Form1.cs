using System;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Windows.Forms;
//using System.Data.SqlClient;

using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Drawing;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.Identity.Client;

namespace baza_danych_lab_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class DatabaseManager
        {
            private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            public void ReadData()
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Table";
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(0); // Assuming Id is the first column
                            string userdata = reader.GetString(1);
                            string usernumerAlbumu = reader.GetString(2);
                            string imieINazwisko = reader.GetString(3);
                            // Assuming Name is the second column
                            Console.WriteLine($"User Id: {userId}, data: {userdata}");
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            public void WriteData(int userId, string userdata, string usernumerAlbumu, string userimieINazwisko)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Table (Id, data, numerAlbumu, imieINazwisko) VALUES (@userId, @userdata, @usernumerAlbumu, @userimieINazwisko)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", userId);
                    command.Parameters.AddWithValue("@data", userdata);
                    command.Parameters.AddWithValue("@numerAlbumu", usernumerAlbumu);
                    command.Parameters.AddWithValue("@imieINazwisko", userimieINazwisko);
                    command.ExecuteNonQuery();
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }

        //zapis
        private void button1_Click(object sender, EventArgs e)
        {

            WriteData(2, "aaa", "bbb", "ccc");
        }
        //odczyt
        private void button2_Click(object sender, EventArgs e)
        {

        }


    }
}
