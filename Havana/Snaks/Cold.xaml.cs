﻿using Library.Models.Classes;
using System;
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
using WPF_Library.Models.Classes;

namespace Havana.Snaks
{
    /// <summary>
    /// Interaction logic for Cold.xaml
    /// </summary>
    public partial class Cold : Window
    {
        public Cold()
        {
            InitializeComponent();
            ShowInfo();
        }

        private void ShowInfo() 
        {
            DataAccess dataAccess = new DataAccess();
            List<SnackPhoto> photos = dataAccess.GetAllSnacksPhotos();

            Image[] images = new Image[6];
            for (int i = 0; i < 6; i++)
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

        private void AddButt(object sender, RoutedEventArgs e) 
        {
        }

        private void BackToOrder(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = new NewOrderWindow();
            newOrderWindow.BackToOrderWindow();
            this.Close();
        }
    }
}
