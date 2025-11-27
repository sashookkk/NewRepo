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

namespace mdk0202
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { //подключаем бд
        Entities db = new Entities();
        public MainWindow()
        {
            InitializeComponent();
            RequestList.ItemsSource = db.Request.ToList();
        }


        private void RequestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //выбранный пользователем материал
            Request selreq = RequestList.SelectedItem as Request;
            //если выбранный материал не пустой, то происходит переход
            if (selreq != null)
            {
                RequestEdit perehod = new RequestEdit(selreq);
                perehod.Show();
                this.Close();
            }
            else { MessageBox.Show("Пожалуйста, выберите материал для редактирования"); }
        }


        private void Productsbt_Click(object sender, RoutedEventArgs e)
        {
            //переход на форму со списком продуктов!
            Producti perehod = new Producti();
            perehod.Show();
            this.Close();
        }

        private void AddRequestbt_Click(object sender, RoutedEventArgs e)
        {
            //переход на форму добавления заявки!
            Requstadd perehod = new Requstadd();
            perehod.Show();
            this.Close();
        }

        private void btnMethod_Click(object sender, RoutedEventArgs e)
        {
            //переход на форму метода!
            metod perehod = new metod();
            perehod.Show();
            this.Close();
        }
    }
}
