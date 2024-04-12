using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Win32.SafeHandles;

namespace Library.Models.Classes
{
    public class DataAcces
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
                        Drink drink = new Drink(Reader.GetInt32(0), Reader.GetString(1), Reader.GetFloat(2), Reader.GetFloat(3));
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
                string Request = "select * from dbo.Snack";
                SqlCommand cmd = new SqlCommand(Request, cnn);
                SqlDataReader Reader = cmd.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Snack snack = new Snack(Reader.GetInt32(0), Reader.GetString(1), Reader.GetFloat(2), Reader.GetFloat(3));
                        snacks.Add(snack);
                    }
                }
                Reader.Close();
            }
            return snacks;
        }

        public void Insert(Drink drink) 
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

        public void Delete(Drink drink) 
        {

        }

        public void Update(Drink drink) 
        {

        }

        public void Insert(Snack snak) 
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

        public void Delete(Snack snak) 
        {

        }

        public void Update(Snack snack) 
        {

        }

    }
}
