using edPractice.ApplicationData;
using edPractice.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using System.Windows.Navigation;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Paragraph = iTextSharp.text.Paragraph;
using Aspose.BarCode.Generation;
using Image = iTextSharp.text.Image;
using System.Windows.Markup;

namespace edPractice
{
    /// <summary>
    /// Логика взаимодействия для Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        public Window6(int role, int id)
        {
            InitializeComponent();
            ID = id;
            Role = role;

            listCart.ItemsSource = FillCart();

        }
        public int ID { get; set; }
        public int Role { get; set; }
        private void BtnBackToWindow_Click(object sender, RoutedEventArgs e)
        {
            Window4 window = new Window4(Role, ID);
            window.Show();
            this.Close();
        }

        Order[] FillCart()
        {
            var productsInCart = AppConnect.model1db.Order.ToList();

            int CountTrip = 0;


            var orders = AppConnect.model1db.Order.ToList();
            listCart.ItemsSource = orders;

            int sumPrice = 0;
            int DiscountPrice = 0;

            for (int i = 0; i < orders.Count; i++)
            {
                sumPrice += orders[i].Trip.Price * orders[i].Count;
            }
            SumPrice.Content = sumPrice.ToString();

            for (int i = 0; i < orders.Count; i++)
            {
                DiscountPrice += orders[i].Trip.Discount * orders[i].Count;
            }
            discountPrice.Content = ((sumPrice - DiscountPrice)).ToString();
            return productsInCart.ToArray();
        }

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            doPDF();
        }

        private void BtnQR_Click(object sender, RoutedEventArgs e)
        {
            QRimg.Source = doQR();
        }

        private void doPDF()
        {
            //новый документ
            Document doc = new Document();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream("..\\..\\Check.pdf", FileMode.Create));
                doc.Open();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 12); Font font1 = new Font(baseFont, 23, 3, BaseColor.BLACK);
                Paragraph line = new Paragraph("--------------------------------------------------------------------", font1);
                line.Alignment = Element.ALIGN_CENTER; doc.Add(line);
                Paragraph title = new Paragraph("ЧЕК", font1);
                title.Alignment = Element.ALIGN_CENTER; doc.Add(title);

                doc.Add(line);
                doQR(); Image Qimg = Image.GetInstance(@"C:\Users\User\Documents\программирование\edPractice\edPractice\QR\" + b.ToString() + "_QR_.png");
                Qimg.ScaleAbsolute(200f, 200f);
                Qimg.Alignment = Element.ALIGN_CENTER; doc.Add(Qimg);
                doc.Add(line);
                int cnt = 0;
                decimal sumW = 0; decimal sumR = 0;
                foreach (var item in listCart.Items)
                {
                    if (item is Order)
                    {
                        Order data = (Order)item;
                        Image img = Image.GetInstance(@"C:\Users\User\Documents\программирование\edPractice\edPractice\Images\" + data.Trip.Image);
                        img.ScaleAbsolute(100f, 100f); doc.Add(img);
                        doc.Add(new Paragraph($"Назание страны: {data.Trip.Country.Name}", font)); 
                        doc.Add(new Paragraph($"Оптовая цена: {data.Trip.Price}", font));
                        doc.Add(new Paragraph($"Скидка: {data.Trip.Discount}", font));
                        doc.Add(new Paragraph($"Количество товара: {data.Count}", font));
                        doc.Add(new Paragraph($"Итоговая цена: {data.Trip.Price * data.Count}", font)); 
                        doc.Add(new Paragraph($"Итоговая цена с учетом скидки: {(data.Trip.Price - data.Trip.Discount) * data.Count}", font));
                        doc.Add(line);
                        cnt += data.Count; sumW += data.Trip.Price * data.Count;
                        sumR += data.Trip.Price * data.Count;
                    }
                }
                
                Paragraph TotalQuantity = new Paragraph($"Общее количество товаров: {cnt}", font);
                Paragraph TotalSumW = new Paragraph($"Цена: {sumW}", font); 
                Paragraph TotalSumR = new Paragraph($"Цена с учетом скидки: {sumR}", font);
                TotalQuantity.Alignment = Element.ALIGN_RIGHT; TotalSumW.Alignment = Element.ALIGN_RIGHT;
                TotalSumR.Alignment = Element.ALIGN_RIGHT;
                doc.Add(TotalQuantity);
                doc.Add(TotalSumW);
                doc.Add(TotalSumR);
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }
            finally
            {
                doc.Close();
            }
        }

        int a = 1; int b = 1;
        private BitmapImage doQR()
        {
            BitmapImage bitmap = new BitmapImage();
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://bom.firpo.ru/Public/86");
            gen.Parameters.Barcode.XDimension.Pixels = 34;

            string dataDir = @"C:\Users\User\Documents\программирование\edPractice\edPractice\QR\";
            //string uuid = Guid.NewGuid().ToString();
            string li = a.ToString() + "_QR_.png";
            if (bitmap != null)
            {
                gen.Save(dataDir + li, BarCodeImageFormat.Png);
            }
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(dataDir + li);
            bitmap.EndInit();
            //QRimg.Source = bitmap;
            b = a; 
            a++;
            return bitmap;
        }
    }
}

