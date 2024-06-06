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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {

        // Конструктор класса
        public Window4(int role, int id)
        {
            InitializeComponent();
            ID = id;
            Role = role;
            ShowCart();
        }

        public int ID { get; set; }
        public int Role { get; set; }

        private void BtnBack_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 window = new Window2(Role, ID);
            window.Show();
            this.Close();
        }

        private void BtnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            var deletedOrders = AppConnect.model1db.Order.ToList().Where(o => o.ID_client == ID);
            AppConnect.model1db.Order.RemoveRange(deletedOrders);
            AppConnect.model1db.SaveChanges();
            ShowCart();
        }

        private void ShowCart()
        {
            var orders = AppConnect.model1db.Order.ToList();
            listCart.ItemsSource = orders;

            int sumPrice = 0;
            int DiscountPrice = 0;

            for (int i = 0; i < orders.Count; i++)
            {
                sumPrice += orders[i].Trip.Price * orders[i].Count;
            }
            LabelSum.Content = sumPrice.ToString();

            for (int i = 0; i < orders.Count; i++)
            {
                DiscountPrice += orders[i].Trip.Discount * orders[i].Count;
            }
            LabelDiscount.Content = ((sumPrice - DiscountPrice)).ToString();
        }

        private void delTrip_Click(object sender, RoutedEventArgs e)
        {
            if ((Order)listCart.SelectedItem != null)
            {
                Order del = (Order)listCart.SelectedItem;
                delCart(del);
            }
        }

        private void delCart(Order del)
        {
            var res = MessageBox.Show($"Вы действительно хотите удалить эту путевку? ", "Уведомление", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.model1db.Order.Remove(del);
                    AppConnect.model1db.SaveChanges();
                    ShowCart();
                }
                catch (Exception)
                {
                    MessageBox.Show("Что то пошло не так!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void upNum_Click(object sender, RoutedEventArgs e)
        {
            if ((Order)listCart.SelectedItem != null)
            {
                Order red = (Order)listCart.SelectedItem;
                if (red.Count < 100)
                {
                    var res = MessageBox.Show("Вы действительно хотите увеличить это число?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (res == MessageBoxResult.Yes)
                    {
                        try
                        {
                            red.Count = red.Count + 1;
                            AppConnect.model1db.SaveChanges();
                            ShowCart();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void downNum_Click(object sender, RoutedEventArgs e)
        {
            if ((Order)listCart.SelectedItem != null)
            {
                Order red = (Order)listCart.SelectedItem;
                if (red.Count > 1)
                {
                    var res = MessageBox.Show("Вы действительно хотите убавить это число?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (res == MessageBoxResult.Yes)
                    {
                        try
                        {
                            red.Count = red.Count - 1; 
                            AppConnect.model1db.SaveChanges();
                            ShowCart();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
    }
}
