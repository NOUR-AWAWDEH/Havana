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
using System.Windows.Controls;
using System.Windows;
using static Library.Models.Classes.DataAccess;
using System.CodeDom.Compiler;
using System.Collections;
using System.Security.Cryptography;
using System.Data.Common;
using System.Reflection;
using System.Linq;
using System.Globalization;

namespace Library.Models.Classes
{
    public class DataAccess
    {

        public readonly string cnnString = ConfigurationManager.ConnectionStrings["Havana.Properties.Settings.HavanaConnectionString"].ConnectionString;

        public int InsertBuyer(Buyer buyer)
        {
            int insertedId = 0;
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    cnn.Open();

                    string sql = "INSERT INTO Buyer (name) VALUES (@name) SELECT @id = SCOPE_IDENTITY() ";
                    SqlCommand cmd = new SqlCommand(sql, cnn);


                    cmd.Parameters.AddWithValue("@name", buyer.Name);

                    SqlParameter idParameter = new SqlParameter("@id", SqlDbType.Int);
                    idParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idParameter);

                    cmd.ExecuteNonQuery();
                    if (idParameter.Value != DBNull.Value)
                    {
                        insertedId = Convert.ToInt32(idParameter.Value);
                    }
                    cnn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return insertedId;
        }


        //TypOFDrink
        public List<TypeOfDrink> GetDrinksType()
        {
            List<TypeOfDrink> typeOfDrinks = new List<TypeOfDrink>();

            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return typeOfDrinks;
        }


        //Snacks Type
        public List<TypeOfSnack> GetSnacksType()
        {
            List<TypeOfSnack> typeOfSnacks = new List<TypeOfSnack>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    cnn.Open();
                    string sql = "SELECT  id, name FROM TypeOfSnack";
                    SqlCommand cmd = new SqlCommand(sql, cnn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TypeOfSnack typeOfSnack = new TypeOfSnack(reader.GetInt32(0), reader.GetString(1));
                        typeOfSnacks.Add(typeOfSnack);
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return typeOfSnacks;
        }

        //Drinks : 
        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = new List<Drink>();
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return drinks;
        }

        public int InsertDrink(Drink drink)
        {
            int insertedId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();

                    string insertDrinkSql = "INSERT INTO Drink (name, id_type_dr, cost, volume) " +
                            "VALUES (@name, @id_type_dr, @cost, @volume); " +
                            "SELECT @id = SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(insertDrinkSql, connection))
                    {
                        command.Parameters.AddWithValue("@name", drink.Name);
                        command.Parameters.AddWithValue("@id_type_dr", drink.TypeOfDrinkId);
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
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return insertedId;
        }

        public void DeleteDrink(int idDrink)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateDrink(Drink drink)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();
                    string query = "UPDATE Drink SET Name = @Name, Cost = @Cost, Volume = @Volume, id_type_dr = @TypeOfDrinkId WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", drink.Name);
                        command.Parameters.AddWithValue("@Cost", drink.Cost);
                        command.Parameters.AddWithValue("@Volume", drink.Volume);
                        command.Parameters.AddWithValue("@TypeOfDrinkId", drink.TypeOfDrinkId);
                        command.Parameters.AddWithValue("@Id", drink.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<DrinkPhoto> GetDrinksPhotos()
        {
            List<DrinkPhoto> drinkPhotos = new List<DrinkPhoto>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();
                    string query = "Select DP.id, DP.photo ,D.Id, D.name, D.id_type_dr ,D.cost, D.volume From DrinkPhotos DP inner join Drink D on D.id = DP.id_Drink";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int DrinkPhotoId = reader.GetInt32(0);
                            byte[] photoData = reader.GetFieldValue<byte[]>(reader.GetOrdinal("photo"));

                            Image ImageSource = new Image();
                            using (MemoryStream stream = new MemoryStream(photoData))
                            {
                                ImageSource.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

                                int drinkId = reader.GetInt32(2);
                                string drinkName = reader.GetString(3);
                                int idTypeOFDrink = reader.GetInt32(4);
                                decimal drinkCost = reader.GetDecimal(5);
                                double drinkVolume = reader.GetDouble(6);

                                Drink drink = new Drink(drinkId, drinkName, drinkCost, drinkVolume, idTypeOFDrink);
                                DrinkPhoto drinkPhoto = new DrinkPhoto(DrinkPhotoId, ImageSource.Source, drink);

                                drinkPhotos.Add(drinkPhoto);
                            }
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return drinkPhotos;
        }

        public void InsertDrinkPhoto(string filePath, int idDrink)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public Drink GetDrinkByName(string selectedDrinkName)
        {
            Drink drink = null;

            try
            {
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
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return drink;
        }



        //Snacks :
        public List<Snack> GetSnacks()
        {
            List<Snack> snacks = new List<Snack>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    cnn.Open();
                    string request = "SELECT id, name, cost , weight FROM dbo.Snack";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK);
            }

            return snacks;
        }

        public int InsertSnack(Snack snack)
        {
            int insertedId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();

                    string insertSnackSql = "INSERT INTO Snack (name, id_type_sn, cost, weight) " +
                            "VALUES (@name, @id_type_sn, @cost, @weight); " +
                            "SELECT @id = SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(insertSnackSql, connection))
                    {
                        command.Parameters.AddWithValue("@name", snack.Name);
                        command.Parameters.AddWithValue("@id_type_sn", snack.TypeOfSnakId);
                        command.Parameters.AddWithValue("@cost", snack.Cost);
                        command.Parameters.AddWithValue("@weight", snack.Weight);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return insertedId;
        }

        public void DeleteSnack(int idSnack)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();
                    string query = "DELETE  FROM Snack  WHERE Snack.id = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@id", idSnack);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void UpdateSnack(Snack snack)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();
                    string query = "UPDATE Snack SET Name = @Name, Cost = @Cost, Weight = @Weight, id_type_sn = @TypeOfSnackId WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", snack.Name);
                        command.Parameters.AddWithValue("@Cost", snack.Cost);
                        command.Parameters.AddWithValue("@Weight", snack.Weight);
                        command.Parameters.AddWithValue("@TypeOfSnackId", snack.TypeOfSnakId);
                        command.Parameters.AddWithValue("@Id", snack.Id);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public Snack GetSnack(int id)
        {
            Snack snack = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    cnn.Open();
                    string request = "SELECT id, name, cost, weight FROM dbo.Snack where id =@id";
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
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return snack;
        }

        public ImageSource GetSnackPhoto(int idSnack)
        {
            ImageSource imageSource = null;
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return imageSource;
        }

        public List<SnackPhoto> GetSnacksPhotos()
        {
            List<SnackPhoto> snackPhotos = new List<SnackPhoto>();
            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();
                    string query = "Select SP.id, SP.photo ,S.id, S.name, S.id_type_sn ,S.cost, S.weight From SnackPhotos SP inner join Snack S on S.id = SP.id_Snack";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int SnackPhotoId = reader.GetInt32(0);
                            byte[] photoData = reader.GetFieldValue<byte[]>(reader.GetOrdinal("photo"));

                            Image ImageSource = new Image();
                            using (MemoryStream stream = new MemoryStream(photoData))
                            {
                                ImageSource.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

                                int snackId = reader.GetInt32(2);
                                string snackName = reader.GetString(3);
                                int idTypeOFSnack = reader.GetInt32(4);
                                decimal snackCost = reader.GetDecimal(5);
                                double snackWeight = reader.GetDouble(6);

                                Snack snack = new Snack(snackId, snackName, snackCost, snackWeight, idTypeOFSnack);
                                SnackPhoto drinkPhoto = new SnackPhoto(SnackPhotoId, ImageSource.Source, snack);

                                snackPhotos.Add(drinkPhoto);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return snackPhotos;
        }

        public void InsertSnackPhoto(string filePath, int snackId)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        public Snack GetSnackByName(string selectedSnackName)
        {
            Snack snack = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Snack WHERE Name = @selectedSnackName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@selectedSnackName", selectedSnackName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                string name = reader.GetString(reader.GetOrdinal("Name"));
                                decimal cost = reader.GetDecimal(reader.GetOrdinal("Cost"));
                                double weight = reader.GetDouble(reader.GetOrdinal("Weight"));

                                snack = new Snack(id, name, cost, weight);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return snack;
        }



        public void InsertOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

                // Insert the buyer into the Buyers table
                string buyerQuery = "INSERT INTO Buyer (name) VALUES (@BuyerName); SELECT SCOPE_IDENTITY();";
                using (SqlCommand buyerCmd = new SqlCommand(buyerQuery, connection))
                {
                    buyerCmd.Parameters.AddWithValue("@BuyerName", order.BuyerName.Name);
                    order.BuyerName.Id = Convert.ToInt32(buyerCmd.ExecuteScalar());
                }

                // Insert the order into the Orders table
                string orderQuery = "INSERT INTO Orders (name, DateTime, id_buyer) VALUES (@Name, @DateTime, @BuyerId); SELECT SCOPE_IDENTITY();";
                using (SqlCommand orderCmd = new SqlCommand(orderQuery, connection))
                {
                    orderCmd.Parameters.AddWithValue("@Name", order.Name);
                    orderCmd.Parameters.AddWithValue("@DateTime", order.DateTime);
                    orderCmd.Parameters.AddWithValue("@BuyerId", order.BuyerName.Id);

                    order.Id = Convert.ToInt32(orderCmd.ExecuteScalar());
                }

                // Insert the drinks into the OrderItems table
                if (order.DrinksList.Drinks != null && order.DrinksList.Drinks.Count > 0)
                {
                    string drinkQuery =
                        @"
                             INSERT INTO ListOfDrinks (id_order, id_drink, count)
                            VALUES (@OrderId, @DrinkId, @DrinkCount);
                        ";

                    foreach (Drink drink in order.DrinksList.Drinks)
                    {
                        using (SqlCommand drinkCmd = new SqlCommand(drinkQuery, connection))
                        {
                            drinkCmd.Parameters.AddWithValue("@OrderId", order.Id);
                            drinkCmd.Parameters.AddWithValue("@DrinkId", drink.Id);
                            drinkCmd.Parameters.AddWithValue("@DrinkCount", order.DrinksList.Count);

                            drinkCmd.ExecuteNonQuery();
                        }
                    }
                }

                // Insert the snacks into the OrderItems table
                if (order.SnacksList.Snacks != null && order.SnacksList.Snacks.Count > 0)
                {
                    string snackQuery =
                        @"
                            INSERT INTO ListOfSnacks (id_order, id_snack, count)
                            VALUES (@OrderId, @SnackId, @SnackCount);
                        ";

                    foreach (Snack snack in order.SnacksList.Snacks)
                    {
                        using (SqlCommand snackCmd = new SqlCommand(snackQuery, connection))
                        {
                            snackCmd.Parameters.AddWithValue("@OrderId", order.Id);
                            snackCmd.Parameters.AddWithValue("@SnackId", snack.Id);
                            snackCmd.Parameters.AddWithValue("@SnackCount", order.SnacksList.Count);
                            snackCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }



        public List<string> GetOrdersDates()
        {
            List<string> ordersDates = new List<string>();

            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();
                string sqlQuery = @"SELECT DateTime FROM Orders";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime orderDate = reader.GetDateTime(0);
                            string formattedDate = orderDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            ordersDates.Add(formattedDate);
                        }
                    }
                }
            }

            return ordersDates;
        }


        public List<Order> GetAllOrders(string startDate, string endDate)
        {
            var orders = new List<Order>();

            try
            {
                using (SqlConnection connection = new SqlConnection(cnnString))
                {
                    connection.Open();

                    string sqlQuery = @"
                SELECT 
                    O.id AS OrderID,
                    O.Name AS OrderName,
                    O.DateTime AS OrderDate,
                    B.id AS BuyerID,
                    B.name AS BuyerName,
                    D.id AS DrinkId,
                    D.name AS DrinkName,
                    D.cost AS DrinkCost,
                    D.volume AS DrinkVolume,
                    LD.id AS ListOfDrinksID,
                    LD.count AS ListOfDrinkCount,
                    S.id AS SnackId,
                    S.name AS SnackName,
                    S.cost AS SnackCost,
                    S.weight AS SnackWeight,
                    LS.id AS ListOfSnacksID,
                    LS.count AS ListOfSnacksCount
                FROM Orders O
                INNER JOIN Buyer B ON O.id_buyer = B.id
                LEFT JOIN ListOfDrinks LD ON O.id = LD.id_order
                LEFT JOIN Drink D ON D.id = LD.id_drink
                LEFT JOIN ListOfSnacks LS ON O.id = LS.id_order
                LEFT JOIN Snack S ON S.id = LS.id_snack
                WHERE (LD.id_order IS NOT NULL OR LS.id_order IS NOT NULL)
                AND O.DateTime BETWEEN @StartDate AND @EndDate ;";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int orderIdIndex = reader.GetOrdinal("OrderID");
                                if (!reader.IsDBNull(orderIdIndex))
                                {
                                    int orderId = reader.GetInt32(orderIdIndex);
                                    Order order = orders.FirstOrDefault(o => o.Id == orderId);

                                    if (order == null)
                                    {
                                        order = new Order
                                        {
                                            Id = orderId,
                                            Name = reader.IsDBNull(reader.GetOrdinal("OrderName")) ? null : reader.GetString(reader.GetOrdinal("OrderName")),
                                            DateTime = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                                            BuyerName = reader.IsDBNull(reader.GetOrdinal("BuyerID")) || reader.IsDBNull(reader.GetOrdinal("BuyerName")) ? null : new Buyer
                                            {
                                                Id = reader.GetInt32(reader.GetOrdinal("BuyerID")),
                                                Name = reader.GetString(reader.GetOrdinal("BuyerName"))
                                            },
                                            DrinksList = !reader.IsDBNull(reader.GetOrdinal("ListOfDrinksID")) ? new ListOfDrinks
                                            {
                                                Id = reader.GetInt32(reader.GetOrdinal("ListOfDrinksID")),
                                                Count = reader.GetInt32(reader.GetOrdinal("ListOfDrinkCount")),
                                                Drinks = new List<Drink>()
                                            } : null,
                                            SnacksList = !reader.IsDBNull(reader.GetOrdinal("ListOfSnacksID")) ? new ListOfSnacks
                                            {
                                                Id = reader.GetInt32(reader.GetOrdinal("ListOfSnacksID")),
                                                Count = reader.GetInt32(reader.GetOrdinal("ListOfSnacksCount")),
                                                Snacks = new List<Snack>()
                                            } : null
                                        };

                                        orders.Add(order);
                                    }

                                    if (!reader.IsDBNull(reader.GetOrdinal("DrinkId")))
                                    {
                                        int drinkId = reader.GetInt32(reader.GetOrdinal("DrinkId"));
                                        string drinkName = reader.GetString(reader.GetOrdinal("DrinkName"));
                                        decimal drinkCost = reader.GetDecimal(reader.GetOrdinal("DrinkCost"));
                                        double drinkVolume = reader.GetDouble(reader.GetOrdinal("DrinkVolume"));

                                        Drink drink = new Drink(drinkId, drinkName, drinkCost, drinkVolume);
                                        order.DrinksList.Drinks.Add(drink);
                                    }

                                    if (!reader.IsDBNull(reader.GetOrdinal("SnackId")))
                                    {
                                        int snackId = reader.GetInt32(reader.GetOrdinal("SnackId"));
                                        string snackName = reader.GetString(reader.GetOrdinal("SnackName"));
                                        decimal snackCost = reader.GetDecimal(reader.GetOrdinal("SnackCost"));
                                        double snackWeight = reader.GetDouble(reader.GetOrdinal("SnackWeight"));

                                        Snack snack = new Snack(snackId, snackName, snackCost, snackWeight);
                                        order.SnacksList.Snacks.Add(snack);
                                    }

                                    order.TotalCost = CalculateTotalCost(order);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                MessageBox.Show(ex.Message, "An error occurred while retrieving orders: ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return orders;
        }

        private decimal CalculateTotalCost(Order order)
        {
            decimal totalCost = 0;

            if (order.DrinksList != null && order.DrinksList.Drinks != null)
            {
                foreach (var drink in order.DrinksList.Drinks)
                {
                    totalCost += drink.Cost;
                }
            }

            if (order.SnacksList != null && order.SnacksList.Snacks != null)
            {
                foreach (var snack in order.SnacksList.Snacks)
                {
                    totalCost += snack.Cost;
                }
            }

            return totalCost;
        }



    }
}
