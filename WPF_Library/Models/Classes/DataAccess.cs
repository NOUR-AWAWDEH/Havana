using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using System.Data;

namespace Library.Models.Classes
{
    public class DataAccess
    {
        
        public readonly string cnnString = ConfigurationManager.ConnectionStrings["Havana.Properties.Settings.HavanaConnectionString"].ConnectionString;
        
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

        public List<TypeOfDrink> GetDrinkType()
        {
            List<TypeOfDrink> typeOfDrinks = new List<TypeOfDrink>();
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                string sql = "SELECT  id, name FROM TypeOfDrink";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TypeOfDrink typeOfDrink = new TypeOfDrink(reader.GetInt32(0), reader.GetString(1));
                    typeOfDrinks.Add(typeOfDrink);
                }

                cnn.Close();
            }
            return typeOfDrinks;
        }

        //Drinks : 
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
                cnn.Close();
            }
            return drinks;
        }

        public int InsertDrink(Drink drink)
        {
            int insertedId = 0;

            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

                string insertDrinkSql = "INSERT INTO Drink (name, id_type_dr, cost, volume) " +
                        "VALUES (@name, @id_type_dr, @cost, @volume); " +
                        "SELECT @id = SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(insertDrinkSql, connection))
                {
                    command.Parameters.AddWithValue("@name", drink.Name);
                    command.Parameters.AddWithValue("@id_type_dr", drink.IdTypeOFDrink);
                    command.Parameters.AddWithValue("@cost", drink.Cost);
                    command.Parameters.AddWithValue("@volume", drink.Volume);

                    SqlParameter idParameter = new SqlParameter("@id", SqlDbType.Int);
                    idParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(idParameter);

                    command.ExecuteNonQuery();

                    if (idParameter.Value != DBNull.Value)
                    {
                        insertedId = Convert.ToInt32(idParameter.Value);
                    }
                }
            }

            return insertedId;
        }

        public void DeleteDrink(int idDrink) 
        {
            using (SqlConnection connection = new SqlConnection(cnnString)) 
            {
                connection.Open();
                string query = "DELETE  FROM Drink  WHERE Drink.id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id", idDrink);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            command.ExecuteReader().Close();
                        }
                    }
                }
                connection.Close();
            }
        }
      
        public void UpdateDrink(Drink drink) 
        {

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
                connection.Close ();
            }

            return imageSource;
        }
        
        public List<DrinkPhoto> GetDrinksPhotos()
        {
            List<DrinkPhoto> drinkPhotos = new List<DrinkPhoto>();
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();
                string query = "Select DP.id, DP.photo ,D.Id, D.name, D.cost, D.volume From DrinkPhotos DP inner join Drink D on D.id = DP.id_Drink";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int DrinkPhotoId = reader.GetInt32(0);
                        byte[] photoData = reader.GetFieldValue<byte[]>(reader.GetOrdinal("photo"));

                        using (MemoryStream stream = new MemoryStream(photoData))
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            

                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = stream;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.EndInit();
                            ImageSource imageSource = bitmapImage;

                            int drinkId = reader.GetInt32(2);
                            string drinkName = reader.GetString(3);
                            decimal drinkCost = reader.GetDecimal(4);
                            double drinkVolume = reader.GetDouble(5);

                            Drink drink = new Drink(drinkId, drinkName, drinkCost, drinkVolume);
                            DrinkPhoto drinkPhoto = new DrinkPhoto(DrinkPhotoId, imageSource, drink);

                            drinkPhotos.Add(drinkPhoto);
                        }
                    }
                }
                connection.Close();
            }
            return drinkPhotos;
        }
        
        public void InsertDrinkPhoto(string filePath, int idDrink)
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
                connection.Close();
            }

        }
       
        //Snacks :
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
                        Snack snack = new Snack(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDouble(3));
                        snacks.Add(snack);
                    }
                }
                cnn.Close();
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
                    decimal cost = reader.GetDecimal(2);
                    double weight = reader.GetDouble(3);
                    snack = new Snack(Id, name, cost, weight);

                }
                cnn.Close( );
            }
            return snack;
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
                connection.Close();
            }

            return imageSource;
        }
        
        public List<SnackPhoto> GetSnacksPhotos()
        {
            List<SnackPhoto> photos = new List<SnackPhoto>();

            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();
                string query = "Select Sp.id, Sp.photo ,S.Id, S.name, S.cost, S.weigth From SnackPhotos SP inner join Snack S on S.id = SP.id_Snack";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int snackPhotoId = reader.GetInt32(0);
                        byte[] photoData = reader.GetFieldValue<byte[]>(reader.GetOrdinal("photo"));

                        using (MemoryStream stream = new MemoryStream(photoData))
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = stream;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.EndInit();

                            ImageSource imageSource = bitmapImage;

                            int snackId = reader.GetInt32(2);
                            string snackName = reader.GetString(3);
                            decimal snackCost = reader.GetDecimal(4);
                            double snackWeight = reader.GetDouble(5);
                            Snack snack = new Snack(snackId, snackName, snackCost, snackWeight);
                            SnackPhoto snackPhoto = new SnackPhoto(snackPhotoId, imageSource, snack);

                            photos.Add(snackPhoto);
                        }
                    }
                }
                connection.Close();
            }

            return photos;
        }
        
        public void InsertSnackPhoto(string filePath, int snackId)
        {
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
                connection.Close(); 
            }

        }

        public Drink GetDrinkByName(string selectedDrinkName)
        {
            Drink drink = null;

            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();
                string query = "SELECT * FROM Drink WHERE Name = @selectedDrinkName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@selectedDrinkName", selectedDrinkName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            decimal cost = reader.GetDecimal(reader.GetOrdinal("Cost"));
                            double volume = reader.GetDouble(reader.GetOrdinal("Volume"));

                            drink = new Drink(id, name, cost, volume);
                        }
                    }
                }
                connection.Close ();
            }

            return drink;
        }
    }
}
