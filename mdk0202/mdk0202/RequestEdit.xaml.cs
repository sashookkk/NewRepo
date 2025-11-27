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
    /// Логика взаимодействия для RequestEdit.xaml
    /// </summary>
    public partial class RequestEdit : Window
    {
        Entities db = new Entities();
        Request selectedRequest;
        double? RequestSum;
        public RequestEdit(Request selectedRequest)
        {
            InitializeComponent();
            this.selectedRequest = selectedRequest;
            // Заполняем выпадающие списки данными из базы
            cbPartner.ItemsSource = db.Partner.ToList();
            cbProduct.ItemsSource = db.Product.ToList();
        }

        private void EditReqbtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заявка изменена!");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //переход назад
            MainWindow perehod = new MainWindow();
            perehod.Show();
            this.Close();
        }

        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = Convert.ToInt32(tbAmount.Text);
                Product selProd = cbProduct.SelectedItem as Product;
                // Расчет суммы заказа для выбранного продукта
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
                    id_request = selectedRequest.ID
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
                Partner selPart = selectedRequest.Partner as Partner;
                // Заполняем поля формы данными выбранного партнера
                cbPartnerType.Text = selPart.PartnerType.title;
                tbTitle.Text = selPart.title;
                tbFIODir.Text = selPart.director;
                tbAddress.Text = selPart.address;
                tbRating.Text = selPart.rating.ToString();
                tbPhone.Text = selPart.phone;
                tbEmail.Text = selPart.email;

                List<Request_products> products = db.Request_products.Where(x => x.id_request == selectedRequest.ID).ToList();
                foreach (Request_products i in products)
                {
                    Product p = db.Product.FirstOrDefault(x => x.ID == i.id_product);
                    listProducts.Items.Add(p);
                }

            }
            catch { MessageBox.Show("Сшибка!"); }

        }

        private void cbPartner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Partner selPart = cbPartner.SelectedItem as Partner;
                // Обновляем данные формы при изменении выбора партнера
                cbPartnerType.Text = selPart.PartnerType.title;
                tbTitle.Text = selPart.title;
                tbFIODir.Text = selPart.director;
                tbAddress.Text = selPart.address;
                tbRating.Text = selPart.rating.ToString();
                tbPhone.Text = selPart.phone;
                tbEmail.Text = selPart.email;
            }
            catch { MessageBox.Show("Ошибка!"); }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            // Удаление продукта из заявки
            Product selProd = listProducts.SelectedItem as Product;
            listProducts.Items.Remove(selProd.title);


            Request_products product = db.Request_products.FirstOrDefault(x => x.id_request == selectedRequest.ID && x.id_product == selProd.ID);
            db.Request_products.Remove(product);
            db.SaveChanges();

            listProducts.Items.Clear();
            List<Request_products> products = db.Request_products.Where(x => x.id_request == selectedRequest.ID).ToList();
            foreach (Request_products i in products)
            {
                Product p = db.Product.FirstOrDefault(x => x.ID == i.id_product);
                listProducts.Items.Add(p);
            }
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
    }
    }
    

