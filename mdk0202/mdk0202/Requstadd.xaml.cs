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

namespace mdk0202
{
    /// <summary>
    /// Логика взаимодействия для Requstadd.xaml
    /// </summary>
    public partial class Requstadd : Window
    {
        Entities db = new Entities();
        Request Request;
        double? RequestSum;
        public Requstadd()
        {
            InitializeComponent();
            // Инициализация выпадающих списков данными из базы
            cbPartner.ItemsSource = db.Partner.ToList();
            cbProduct.ItemsSource = db.Product.ToList();
        }
        //фильтрация
        private void tbAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextAllowed(e.Text);
        }
        public static bool TextAllowed(string text)
        {
            return text.All(char.IsDigit);
        }

        private void cbPartner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Заполнение полей формы данными выбранного партнера
                Partner selPart = cbPartner.SelectedItem as Partner;

                cbPartnerType.Text = selPart.PartnerType.title;
                tbTitle.Text = selPart.title;
                tbFIODir.Text = selPart.director;
                tbAddress.Text = selPart.address;
                tbRating.Text = selPart.rating.ToString();
                tbPhone.Text = selPart.phone;
                tbEmail.Text = selPart.email;
            }
            catch { MessageBox.Show("Error!"); }

        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = Convert.ToInt32(tbAmount.Text);
                Product selProd = cbProduct.SelectedItem as Product;
                // Расчет суммы для текущего продукта
                double? requestSum = amount * selProd.price;
                if (RequestSum != null)
                {
                    RequestSum += requestSum;
                }
                else
                {
                    RequestSum = 0;
                    RequestSum += requestSum;
                }


                listProducts.Items.Add(selProd.title);
                Request_products rp = new Request_products
                {
                    id_product = selProd.ID,
                    amount = amount,
                    id_request = Request.ID
                };
                db.Request_products.Add(rp);
                db.SaveChanges();
            }
            catch { MessageBox.Show("Error!"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание новой заявки при загрузке окна
                Request newRequest = new Request();
                db.Request.Add(newRequest);
                db.SaveChanges();

                Request = newRequest;
            }
            catch { MessageBox.Show("Error!"); }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Завершение оформления заявки
                Partner selPart = cbPartner.SelectedItem as Partner;

                Request request = Request;
                request.id_partner = selPart.ID;
                request.request_date = DateTime.Today; // Установка текущей даты
                request.price = RequestSum;// Сохранение общей суммы

                db.SaveChanges();
                MessageBox.Show("Заявка успешно добавлена!");
            }
            catch { MessageBox.Show("Error!"); }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //переход назад
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {

        }
    }
    }

