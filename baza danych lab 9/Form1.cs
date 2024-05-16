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
        public string userdata;
        public int userid;
        public string usernumerAlbumu;
        public string userimieINazwisko;
        public string userkierunekIStopien;
        public string userprzedmiot;
        public string userpunkty;
        public string userprowadzacy;
        public string useruzasadnienie;
        public string usersemestrIRok;

        public Form1()
        {
            InitializeComponent();
        }

        public class DatabaseManager
        {
            private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=""C:\USERS\JULIA\SOURCE\REPOS\BAZA DANYCH LAB 9\BAZA DANYCH LAB 9\DATABASE1.MDF"";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            public void ReadData()
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Lol";
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Check if there are rows returned
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int rId = reader.GetInt32(0);
                                string rdata = reader.GetValue(1).ToString();
                                string rnumerAlbumu = reader.GetValue(2).ToString();
                                string rimieINazwisko = reader.GetValue(3).ToString();
                                string rkierunekIStopien = reader.GetValue(4).ToString();
                                string rprzedmiot = reader.GetValue(5).ToString();
                                string rpunkty = reader.GetValue(6).ToString();
                                string rprowadzacy = reader.GetValue(7).ToString();
                                string ruzasadnienie = reader.GetValue(8).ToString();
                                string rsemestrIRok = reader.GetValue(9).ToString();

                                MessageBox.Show($"User Id: {rId}, data: {rdata}, Numer Albumu: {rnumerAlbumu}, Imie i Nazwisko: {rimieINazwisko}, Kierunek i Stopieñ: {rkierunekIStopien}, Przedmiot: {rprzedmiot}, Punkty: {rpunkty}, Prowadz¹cy: {rprowadzacy}, Uzasadnienie: {ruzasadnienie}, Semestr i Rok: {rsemestrIRok}");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No data found.");
                        }

                        reader.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("SQL Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }



            public void WriteData(int userId, string userdata, string usernumerAlbumu, string userimieINazwisko, string userkierunekIStopien, string userprzedmiot, string userpunkty, string userprowadzacy, string useruzasadnienie, string usersemestrIRok)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Lol (Id, data, numerAlbumu, imieINazwisko, kierunekIStopien, przedmiot, punkty, prowadzacy, uzasadnienie, semestrIRok) VALUES (@idd, @dataa, @numeralbumuu, @imieiinazwisko, @kierunekiistopien, @przedmiott, @punktyy, @prowadzacyy, @uzasadnieniee, @semestriirok)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idd", userId);
                    command.Parameters.AddWithValue("@dataa", userdata);
                    command.Parameters.AddWithValue("@numeralbumuu", usernumerAlbumu);
                    command.Parameters.AddWithValue("@imieiinazwisko", userimieINazwisko);
                    command.Parameters.AddWithValue("@kierunekiistopien", userkierunekIStopien);
                    command.Parameters.AddWithValue("@przedmiott", userprzedmiot);
                    command.Parameters.AddWithValue("@punktyy", userpunkty);
                    command.Parameters.AddWithValue("@prowadzacyy", userprowadzacy);
                    command.Parameters.AddWithValue("@uzasadnieniee", useruzasadnienie);
                    command.Parameters.AddWithValue("@semestriirok", usersemestrIRok);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) inserted.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            userdata = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            usernumerAlbumu = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            userimieINazwisko = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            userid = Convert.ToInt32(textBox4.Text);
        }

        //zapis button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                DatabaseManager db = new DatabaseManager();
                db.WriteData(userid, userdata, usernumerAlbumu, userimieINazwisko, userkierunekIStopien, userprzedmiot, userpunkty, userprowadzacy, useruzasadnienie, usersemestrIRok);
                MessageBox.Show("ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }
        //odczyt
        private void button2_Click(object sender, EventArgs e)
        {
            DatabaseManager db = new DatabaseManager();
            db.ReadData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=""C:\USERS\JULIA\SOURCE\REPOS\BAZA DANYCH LAB 9\BAZA DANYCH LAB 9\DATABASE1.MDF"";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        MessageBox.Show("udalo sie polaczyc z baza danych");
                    }
                    else
                    {
                        MessageBox.Show("nie udalo sie polaczyc z baza danych");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            userkierunekIStopien = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            userprzedmiot = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            userpunkty = textBox7.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            userprowadzacy = textBox8.Text;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            useruzasadnienie = textBox9.Text;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            usersemestrIRok = textBox10.Text;
        }
    }
}
