﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Library.Models.Classes;
using Havana.Orders;




namespace Havana.Snacks
{

    /// <summary>
    /// Interaction logic for Hot.xaml
    /// </summary>
    public partial class SnacksItems : Window
    {
        int currentPage = 0;
        int sizePage = 4;

        
        List<SnackPhoto> photos = null;

        public SnacksItems()
        {
            InitializeComponent();
            DataAccess dataAccess = new DataAccess();
            photos = dataAccess.GetSnacksPhotos();
            ShowInfo(currentPage);
        }

        private void ShowInfo(int page)
        {
            int startPos = sizePage * page;

            int endPos = startPos + sizePage;

            if (endPos > photos.Count)
            {
                endPos = photos.Count;
            }

            //Delete Data
            for (int i = 0; i < 4; i++)
            {
                string ImageName = $"Image{i + 1}";
                Image image = (Image)FindName(ImageName);
                TextBlock textBlock = (TextBlock)FindName($"TextLable{i + 1}");
                image.Source = null;
                textBlock.Text = "";

                string NameButton = $"AddButt{i + 1}";
                Button button = (Button)FindName(NameButton);
                if (image.Source == null)
                {
                    button.Visibility = Visibility.Hidden;
                }

            }

            //Fill Datat
            for (int i = startPos; i < endPos; i++)
            {
                string ImageName = $"Image{i - startPos + 1}";

                Image image = (Image)FindName(ImageName);
                if (image != null)
                {
                    image.Source = photos[i].Image;
                }

                string TexLableName = $"TextLable{i - startPos + 1}";
                TextBlock textBlock = (TextBlock)FindName(TexLableName);

                if (image != null)
                {
                    textBlock.Text = photos[i].Snack.Name;
                }

                string buttonName = $"AddButt{i - startPos + 1}";
                Button button = (Button)FindName(buttonName);
                if (image != null)
                {
                    button.Visibility = Visibility.Visible;
                }
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.BackToOrderWindow(newOrderWindow);
            this.Close();
        }

        private void AddButt(object sender, RoutedEventArgs e)
        {
            int startPos = sizePage * currentPage;
            int endPos = startPos + sizePage;

            if (endPos > photos.Count)
            {
                endPos = photos.Count;
            }

            List<Button> buttons = new List<Button>();

            for (int i = startPos; i < endPos; i++)
            {
                string buttonName = $"AddButt{i - startPos + 1}";
                buttons.Add((Button)FindName(buttonName));
            }

            Button clickedButton = (Button)sender;

            for (int i = startPos; i < endPos; i++)
            {
                if (buttons[i - startPos] == clickedButton)
                {
                    NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
                    Snack snack = photos[i].Snack;
                    newOrderWindow.OrderDataGrid.Items.Add(snack);
                    newOrderWindow.itemsAdded = true;
                    newOrderWindow.ShowOrderButtons();
                    newOrderWindow.Show();
                    break; // Exit the loop once the button is found
                }
            }
        }




        private void NextPage(object sender, RoutedEventArgs e)
        {
         
            currentPage++;
            if (photos.Count < (currentPage * sizePage))
            {
                currentPage--;
            }
            ShowInfo(currentPage);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            currentPage--;
            if (currentPage < 0)
            {
                currentPage = 0;
            }
            ShowInfo(currentPage);
        }
    }
}