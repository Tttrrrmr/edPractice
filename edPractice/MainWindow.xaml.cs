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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace edPractice
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.model1db = new educational_practiceEntities2();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user0bj = AppConnect.model1db.User.FirstOrDefault(x => x.Login == txbLogin.Text && x.Password == psbPassword.Password);
                if (user0bj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (user0bj.ID_role)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте, пользователь " + user0bj.Name + "!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, администратор " + user0bj.Name + "!",
                               "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Window2 window = new Window2();
            window.Show();
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Show();
            this.Close();
        }
    }
}
