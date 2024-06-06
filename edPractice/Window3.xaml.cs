using edPractice.ApplicationData;
using edPractice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace edPractice
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private Trip currentTrip;

        private List<Country> countryLis = AppConnect.model1db.Country.ToList();
        private List<Client> clientLis = AppConnect.model1db.Client.ToList();
        private List<Trip> tripLis = AppConnect.model1db.Trip.ToList();

        public Window3(Trip e)
        {
            InitializeComponent();
            currentTrip = e;
            DataContext = currentTrip;

            StartDatePicker.SelectedDate = currentTrip.Trip_start;
            EndDatePicker.SelectedDate = currentTrip.Trip_end;

            ComboCountry.Items.Add("Выберите страну");

            
            var selectedCountry = 0;

            foreach (var item in countryLis) 
            {
                if (item.ID_country==currentTrip.ID_country)
                { 
                    selectedCountry = countryLis.IndexOf(item) + 1; 
                }
                ComboCountry.Items.Add(item.Name);
            }
            ComboCountry.SelectedIndex = selectedCountry;

            
            var selectedClient = 0;

            ComboClient.Items.Add("Выберите клиента");
            foreach (var item in clientLis)
            {
                ComboClient.Items.Add($"{item.Surname} {item.Name} {item.Patronymic}");
                if (item.ID_client==currentTrip.ID_client)
                {
                    selectedClient = clientLis.IndexOf(item) + 1;
                }
            }
            ComboClient.SelectedIndex = selectedClient;
           
           var selectedImage = 0;

            ComboImage.Items.Add("Выберите картинку");
            foreach (var item in tripLis)
            {
                if (item.Image==currentTrip.Image)
                {
                    selectedImage = tripLis.IndexOf(item) + 1;
                }
                ComboImage.Items.Add(item.Image);
            }
            ComboImage.SelectedIndex = selectedImage;

            ComboCountry.SelectionChanged += new SelectionChangedEventHandler(OnCountrySelected);
            ComboClient.SelectionChanged += new SelectionChangedEventHandler(OnClientSelected);
            ComboImage.SelectionChanged += new SelectionChangedEventHandler(OnImageSelected);
            StartDatePicker.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(OnSelectDate);
            EndDatePicker.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(OnSelectEndDate);
        }

        private void OnCountrySelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentTrip.ID_country = countryLis[ComboCountry.SelectedIndex - 1].ID_country;
            } 
            catch (Exception)
            {
                currentTrip.ID_country = 0;
            }
        }

        private void OnClientSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentTrip.ID_client = clientLis[ComboClient.SelectedIndex - 1].ID_client;
            }
            catch(Exception)
            {
                currentTrip.ID_client = 0;  
            }
        }

        private void OnSelectDate(object sender, SelectionChangedEventArgs e)
        {
            if (StartDatePicker.SelectedDate != null)
            {
                currentTrip.Trip_start = (DateTime)StartDatePicker.SelectedDate;
            }
        }

        private void OnSelectEndDate(object sender, SelectionChangedEventArgs e)
        {
            if (EndDatePicker.SelectedDate != null)
            {
                currentTrip.Trip_end = (DateTime)EndDatePicker.SelectedDate;
            }
        }
        private void OnImageSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentTrip.Image = (String)ComboImage.SelectedItem;
            }
            catch (Exception)
            {
                currentTrip.Image = " ";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (currentTrip.Price == 0)
                errors.AppendLine("Укажите стоимость поездки");
            if (currentTrip.Discount == 0)
                errors.AppendLine("Укажите скидку для поездки");
            if (currentTrip.Trip_start == System.DateTime.MinValue)
                errors.AppendLine("Укажите дату начала поездки");
            if (currentTrip.Trip_end == System.DateTime.MinValue)
                errors.AppendLine("Укажите дату конца поездки");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                AppConnect.model1db.Trip.AddOrUpdate(currentTrip);
                AppConnect.model1db.SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
