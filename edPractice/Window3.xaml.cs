using edPractice.ApplicationData;
using edPractice.Models;
using System;
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

        public Window3(Trip e)
        {
            InitializeComponent();
            currentTrip = e;
            DataContext = currentTrip;
            ComboCountry.Items.Add("Выберите страну");
            foreach (var item in AppConnect.model1db.Country.ToList()) 
            {
                ComboCountry.Items.Add(item.Name);
            }
            ComboCountry.SelectedIndex = 0;

            ComboClient.Items.Add("Выберите клиента");
            foreach (var item in AppConnect.model1db.Client.ToList())
            {
                ComboClient.Items.Add($"{item.Surname} {item.Name} {item.Patronymic}");
            }
            ComboClient.SelectedIndex = 0;

            ComboImage.Items.Add("Выберите картинку");
            foreach (var item in AppConnect.model1db.Trip.ToList())
            {
                ComboImage.Items.Add(item.Image);
            }
            ComboImage.SelectedIndex = 0;

            //ComboCountry.SelectionChanged += new SelectionChangedEventHandler(OnCountrySelected);
            //ComboClient.SelectionChanged += new SelectionChangedEventHandler(OnClientSelected);
            //ComboImage.SelectionChanged += new SelectionChangedEventHandler(OnImageSelected);
            StartDatePicker.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(OnSelectDate);
            EndDatePicker.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(OnSelectEndDate);
        }

        //private void OnCountrySelected(object sender, SelectionChangedEventArgs e)
        //{
        //    currentTrip.ID_country = ((Country)ComboCountry.SelectedItem).ID_country;
        //}

        //private void OnClientSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    currentTrip.Client = AppConnect.model1db.Client.ToList().ElementAt(ComboClient.SelectedIndex);
        //}

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
        //private void OnImageSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    currentTrip.Image = AppConnect.model1db.Trip.ToList().ElementAt(ComboImage.SelectedIndex).Image;
        //}

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
