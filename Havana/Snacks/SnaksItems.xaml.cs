using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Havana;
using Havana.HavanaDataSetTableAdapters;
using Library.Models.Classes;




namespace Havana.Snacks
{
    
    /// <summary>
    /// Interaction logic for Hot.xaml
    /// </summary>
    public partial class SnacksItems : Window
    {
        int currentPage = 0;
        int sizePage = 4;
        List<SnackPhoto> photos;
        DataAccess dataAccess = new DataAccess();

        public SnacksItems()
        {
            InitializeComponent();
            photos = dataAccess.GetAllSnacksPhotos();
            ShowInfo(currentPage);
        }

        private void ShowInfo(int page)
        {
            
           
          //  Image[] images = new Image[4];

            
            int startPos = sizePage * page;
            
            int endPos = startPos + sizePage;

            if (endPos > photos.Count)
            {
                endPos = photos.Count;
            }

            // Delete Data
            for (int i = 0; i < 4; i++)
            {
                Image image = (Image)FindName($"Image{i  + 1}");
                TextBlock textBlock = (TextBlock)FindName($"TextLable{i  + 1}");
                image.Source = null;
                textBlock.Text = "";
                Button button = (Button)FindName($"AddButt{i + 1}");
                button.Visibility = Visibility.Hidden;

            }

            //Fill Datat
            for (int i = startPos; i < endPos; i++)
            {
                Image image = (Image)FindName($"Image{i - startPos + 1}");
                if (image != null)
                {
                    image.Source = photos[i].Image;
                }
                TextBlock textBlock = (TextBlock)FindName($"TextLable{i - startPos + 1}");
                Button button = (Button)FindName($"AddButt{i - startPos + 1}");
                if (textBlock != null)
                {
                    textBlock.Text = photos[i].Snack.Name;
                    button.Visibility = Visibility.Visible;
                }
              
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = new NewOrderWindow();
            newOrderWindow.BackToOrderWindow();
            this.Close();
        }

        private void AddButt(object sender, RoutedEventArgs e)
        {
           
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