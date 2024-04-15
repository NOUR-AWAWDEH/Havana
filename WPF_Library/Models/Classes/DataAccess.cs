﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using System.Data.SqlTypes;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Library.Models.Classes
{
    public class DataAccess
    {
        
        string cnnString = ConfigurationManager.ConnectionStrings["Havana.Properties.Settings.HavanaConnectionString"].ToString();

        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = new List<Drink>();
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                string Request = "select * from dbo.Drink";
                SqlCommand cmd = new SqlCommand(Request, cnn);
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Drink drink = new Drink(Reader.GetInt32(0), Reader.GetString(1), Reader.GetDecimal(2), Reader.GetDouble(3));
                        drinks.Add(drink);
                    }
                    Reader.Close();
                }

            }
            return drinks;
        }

        public List<Snack> GetSnacks(int id)
        {
            List<Snack> snacks = new List<Snack>();
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                string request = "SELECT id, name, cost, weigth FROM dbo.Snack WHERE id = @id";
                SqlCommand cmd = new SqlCommand(request, cnn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Snack snack = new Snack(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDouble(3));
                        snacks.Add(snack);
                    }
                }
                reader.Close();
            }
            return snacks;
        }

        public Snack GetSnack(int id)
        {
            Snack snack = null;
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                string request = "SELECT id, name, cost, weigth FROM dbo.Snack where id =@id";
                SqlCommand cmd = new SqlCommand(request, cnn);
                SqlParameter idParametr = new SqlParameter("id", id);
                cmd.Parameters.Add(idParametr);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int Id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    decimal cost =  reader.GetDecimal(2);
                    double weight = reader.GetDouble(3);
                    snack = new Snack(Id, name, cost, weight);
                        
                }
                reader.Close();
            }
            return snack;
        }

        public void InsertDrink(Drink drink) 
        {
            using (SqlConnection cnn = new SqlConnection(cnnString)) 
            {
                cnn.Open();
                string sql = "insreat into dbo.Drink (name, cost, volume) values( @name, @cost, @volume)";
                SqlCommand cmd = new SqlCommand (sql, cnn);

                SqlParameter NameParameter = new SqlParameter("@name", drink.Name);
                SqlParameter CostParameter = new SqlParameter("@cost", drink.Cost);
                SqlParameter ValumeParameter = new SqlParameter("@volume", drink.Volume);
                
                cmd.Parameters.Add(NameParameter);
                cmd.Parameters.Add(CostParameter);
                cmd.Parameters.Add(ValumeParameter);
                cmd.ExecuteReader();
                cnn.Close();
            }
        }

        public void DeleteDrink(Drink drink) 
        {

        }

        public void UpdateDrink(Drink drink) 
        {

        }

        public void InsertSnack(Snack snak) 
        {
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                string sql = "insreat into dbo.Snack (name, cost, weigth) values(@name, @cost, @weigth)";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                SqlParameter NameParameter = new SqlParameter("@name", snak.Name);
                SqlParameter CostParameter = new SqlParameter("@cost", snak.Weigth);
                SqlParameter WeigthParameter = new SqlParameter("@weigth", snak.Weigth);

                cmd.Parameters.Add(NameParameter);
                cmd.Parameters.Add(CostParameter);
                cmd.Parameters.Add(WeigthParameter);
                cmd.ExecuteReader();
                cnn.Close();
            }
        }

        public void DeleteSnack(Snack snak) 
        {

        }

        public void UpdateSnack(Snack snack) 
        {

        }
        
        public void InsertBuyerName(Buyer buyer)
        {
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                    cnn.Open();
                    string sql = "insert into Buyer (name) values( @name)";
                    SqlCommand cmd = new SqlCommand(sql, cnn);

                    SqlParameter NameParameter = new SqlParameter("@name", buyer.Name);
                    cmd.Parameters.Add(NameParameter);
                    cmd.ExecuteReader();
                    cnn.Close();
            }
        }

        public void InseartSnackPhoto(string filePath, string photoName)
        {
            // Assuming you have an open database connection named "connection"
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

             
                byte[] photoData = File.ReadAllBytes(filePath);
                
               
                string query = "INSERT INTO Photo (name, photo, id_Snack) VALUES (@name, @photo, @idSnack)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                    command.Parameters.AddWithValue("@name", photoName);
                    command.Parameters.AddWithValue("@photo", photoData);
                    command.Parameters.AddWithValue("@idSnack", 7);

                   
                    command.ExecuteNonQuery();
                }
            }

        }

        public ImageSource GetPhotoSnack(int idSnack)
        {
            ImageSource imageSource = null;
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

                
                string query = "SELECT photo FROM Photo WHERE id_Snack = @idSnack";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@idSnack", idSnack);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] photoData = (byte[])reader["photo"];
                            using (MemoryStream stream = new MemoryStream(photoData))
                            {
                                BitmapImage bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.StreamSource = stream;
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.EndInit();

                                imageSource = bitmapImage;
                            }
                        }
                    }
                }
            }

            return imageSource;
        }
    }
}
