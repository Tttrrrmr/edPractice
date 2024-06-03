using edPractice.ApplicationData;
using edPractice.Models;
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

namespace edPractice
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            tripsList.ItemsSource = AppConnect.model1db.Trip.ToList();
            ComboFilter.Items.Add("от 0 до 550");
            ComboFilter.Items.Add("от 1000 до 1500");
            ComboFilter.Items.Add("от 2000");
            ComboSort.Items.Add("по возрастанию");
            ComboSort.Items.Add("по убыванию");
            tripsList.SelectionChanged += new SelectionChangedEventHandler((s, e) =>
            {
                BtnEdit.IsEnabled = tripsList.SelectedItem != null;
                BtnDelete.IsEnabled = tripsList.SelectedItem != null;
            });
            FocusableChanged += new DependencyPropertyChangedEventHandler((s, e) =>
            {
                tripsList.ItemsSource = AppConnect.model1db.Trip.ToList();
            });

        }
        private void Edit(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Trip)((FrameworkElement)sender).DataContext;
        }

        Trip[] FindTrip()
        {
            var trip = AppConnect.model1db.Trip.ToList();
            var tripall = trip;
            var country = AppConnect.model1db.Country.ToList();

            //поиск по названию страны
            if (TextSearch != null)
            {
                country = country.Where(x => x.Name.ToLower().Contains(TextSearch.Text.ToLower())).ToList();
            }

            //фильтрация по скидки
            if (ComboFilter.SelectedIndex >= 0)
            {
                switch (ComboFilter.SelectedIndex)
                {
                    case 0:
                        trip=trip.Where(x => x.Discount > 0 && x.Discount < 550).ToList();
                        break;
                    case 1:
                        trip=trip.Where(x => x.Discount >= 1000 && x.Discount < 1500).ToList();
                        break;
                    case 2:
                        trip=trip.Where(x => x.Discount >= 2000).ToList();
                        break;
                }
            }

            //сортировка по возврастанию и убыванию цены
            if (ComboSort.SelectedIndex >= 0)
            {
                switch (ComboSort.SelectedIndex)
                {
                    case 0:
                        {
                            trip=trip.OrderBy(x => x.Price).ToList();
                            break;
                        }
                    case 1:
                        {
                            trip=trip.OrderByDescending(x => x.Price).ToList();
                            break;
                        }
                }
            }

            //количество найденных элементов
            if (trip.Count > 0)
            {
                LabelCount.Content = "Найдено " + trip.Count + " из " + tripall.Count;
            }
            else
            {
                LabelCount.Content = "Ничего не найдено ";
            }
            return trip.ToArray(); 
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            tripsList.ItemsSource = FindTrip();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tripsList.ItemsSource = FindTrip();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tripsList.ItemsSource = FindTrip();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Window3 window = new Window3(new Trip());
            window.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window3 window = new Window3((Trip)tripsList.SelectedItem);
            window.Show();

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            AppConnect.model1db.Trip.Remove((Trip)tripsList.SelectedItem);
            AppConnect.model1db.SaveChanges();
            tripsList.ItemsSource = AppConnect.model1db.Trip.ToList();
        }
    }

}

