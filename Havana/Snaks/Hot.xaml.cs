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
using Havana.HavanaDataSetTableAdapters;
using Library.Models.Classes;
using Library.Models.Interfaces.Snacks;
using Library.Models.Products.Snacks.Hot;
using Microsoft.SqlServer.Server;
using WPF_Library.Models.Classes;


namespace Havana.Snaks
{
    /// <summary>
    /// Interaction logic for Hot.xaml
    /// </summary>
    public partial class Hot : Window
    {
        public Hot()
        {
            InitializeComponent();
            ShowInfo();

        }

        private void ShowInfo()
        {
            DataAccess dataAccess = new DataAccess();
            List<SnackPhoto> photos = dataAccess.GetHotSnackPhotos();

            Image[] images = new Image[4];
            for (int i = 0; i < 4; i++)
            {
                string imageName = "Image" + (i + 1);
                Image image = (Image)FindName(imageName);
                if (image != null)
                {
                    images[i] = image;
                }
            }
           
            int endPos = Math.Min(photos.Count, images.Length);

            for (int i = 0; i < endPos; i++)
            {
                int snackId = photos[i].Snack.Id;
                images[i].Source = dataAccess.GetSnackPhoto(snackId);

                string textBlockName = "TextLable" + (i + 1);
                TextBlock textBlock = (TextBlock)FindName(textBlockName);
                if (textBlock != null)
                {
                    textBlock.Text = photos[i].Snack.Name;
                }
            }
        }

        private void BackToOrder(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = new NewOrderWindow();
            newOrderWindow.BackToOrderWindow();
            this.Close();
        }

        private void AddButt(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int buttonIndex = int.Parse(button.Tag.ToString());

            DataAccess dataAccess = new DataAccess();
            List<SnackPhoto> photos = dataAccess.GetHotSnackPhotos();
            SnackPhoto snackPhoto = photos.ElementAtOrDefault(buttonIndex - 1);

            if (snackPhoto != null)
            {
                NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
                newOrderWindow?.AddSnackItem(snackPhoto.Snack.Id);
            }
        }

    }
}


