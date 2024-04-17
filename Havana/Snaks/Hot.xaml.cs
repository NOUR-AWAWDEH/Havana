﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Havana;
using Library.Models.Classes;
using Library.Models.Products.Snacks.Hot;
using Microsoft.SqlServer.Server;


namespace Havana.Snaks
{
    /// <summary>
    /// Interaction logic for Hot.xaml
    /// </summary>
    public partial class Hot : Window
    {
        public  static bool isAdd { get; set; }
        public Hot()
        {
            InitializeComponent();
            ShowImages();

        }

        private void ShowImages() 
        {
            
            DataAccess dataAccess = new DataAccess();

            //Grilled Chicken Sandwich Image
            int idSnack = 7;
            ImageSource photo1 = dataAccess.GetSnackPhoto(idSnack);
            GrilledChickenSandwichImage.Source = photo1;

            
            //Grilled Chicken Sandwich Image
            idSnack = 10;
            ImageSource photo2 = dataAccess.GetSnackPhoto(idSnack);
            HotDogSandwichImage.Source = photo2;

            //Grilled Chicken Sandwich Image
            idSnack = 9;
            ImageSource photo3 = dataAccess.GetSnackPhoto(idSnack);
            GrilledMozzarellaSandwichImage.Source = photo3;

            //Cheese And Tomato Sandwich Image
            idSnack = 11;
            ImageSource photo4 = dataAccess.GetSnackPhoto(idSnack);
            CheeseAndTomatoSandwichImage.Source = photo4;

        }
        

        private void BackToOrder(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = new NewOrderWindow();
            newOrderWindow.BackToOrderWindow();
            this.Close();
        }

        private void AddGrilledChickenButt(object sender, RoutedEventArgs e)
        {

            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(7);

        }

        private void AddGrilledMozzarellaButt(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(9);

        }

        private void AddChesseAndTomatoButt(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(11);
        }

        private void AddHotDogButt(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(10);
        }
       
        
            
        

    }
}


