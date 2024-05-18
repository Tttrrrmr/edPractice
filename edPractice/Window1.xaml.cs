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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void PasswordBox1_PassworChanged(object sender, RoutedEventArgs e)
        {
            if (psbPass.Password!= psbPass1.Password)
            {
                btnCreate.IsEnabled = false;
                psbPass1.Background=Brushes.LightCoral;
                psbPass1.BorderBrush = Brushes.Red;
            }
            else
            {
                btnCreate.IsEnabled = true;
                psbPass1.Background =Brushes.LightGreen;
                psbPass1.BorderBrush = Brushes.LightGreen;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (AppConnect.model1db.User.Count(x => x.Login==txbLogin.Text)>0)
            {
                MessageBox.Show("пользователь с таким логином есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                User user0bj = new User()
                {
                    Name =txbName.Text,
                    Login =txbLogin.Text,
                    Password=psbPass.Password,
                    ID_role=2
                };
                AppConnect.model1db.User.Add(user0bj);
                AppConnect.model1db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
