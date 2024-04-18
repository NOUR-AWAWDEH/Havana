using System;
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
using WPF_Library.Models.Classes;
using System.Collections;

namespace Library.Models.Classes
{
    public class DataAccess
    {
        
        readonly string cnnString = ConfigurationManager.ConnectionStrings["Havana.Properties.Settings.HavanaConnectionString"].ToString();

        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = new List<Drink>();
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                string Request = "select id, name, cost, volume from dbo.Drink";
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

        public List<Snack> GetSnacks()
        {
            List<Snack> snacks = new List<Snack>();
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                string request = "SELECT id, name, cost , weigth FROM dbo.Snack";
                SqlCommand cmd = new SqlCommand(request, cnn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Snack snack = new Snack(reader.GetInt32(0), reader.GetString(1) ,reader.GetDecimal(2), reader.GetDouble(3));
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

        public void InseartSnackPhoto(string filePath, int snackId)
        {
            // Assuming you have an open database connection named "connection"
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

                byte[] photoData = File.ReadAllBytes(filePath);

                string query = "INSERT INTO SnackPhotos (photo, id_Snack) VALUES (@photo, @id_Snack)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                   
                    command.Parameters.AddWithValue("@photo", photoData);
                    command.Parameters.AddWithValue("@id_Snack", snackId);
                   
                    command.ExecuteNonQuery();
                }
            }

        }

        public void InseartDrinkPhoto(string filePath, int idDrink)
        {
            
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

                byte[] photoData = File.ReadAllBytes(filePath);

                string query = "INSERT INTO DrinkPhotos (photo, id_Drink) VALUES (@photo, @id_Drink)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@photo", photoData);
                    command.Parameters.AddWithValue("@id_Drink", idDrink);

                    command.ExecuteNonQuery();
                }
            }

        }

        public ImageSource GetDrinkPhoto(int idDrink)
        {
            ImageSource imageSource = null;
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();


                string query = "SELECT photo FROM DrinkPhotos WHERE id_Drink = @idSnack";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id_Drink", idDrink);
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

        public ImageSource GetSnackPhoto(int idSnack)
        {
            ImageSource imageSource = null;
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();


                string query = "SELECT photo FROM SnackPhotos WHERE id_Snack = @id_Snack";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id_Snack", idSnack);
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

        public List<SnackPhoto> GetSnackPhotoInfo() 
        {
            List <SnackPhoto> photosInfo = new List<SnackPhoto>();    
            
            using (SqlConnection connection = new SqlConnection(cnnString)) 
            {
                connection.Open();
                string query = "select SP.id, Snack.id, Snack.name, Snack.cost, Snack.weigth from SnackPhotos SP inner join Snack on Sp.id_Snack = Snack.id";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read()) 
                    {
                        SnackPhoto photoSnack = new SnackPhoto(reader.GetInt32(0),
                            new Snack(reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetDouble(4)));

                        photosInfo.Add(photoSnack);
                    }   
                          
                }  
                
            }
            return photosInfo;
        }

    }
}
