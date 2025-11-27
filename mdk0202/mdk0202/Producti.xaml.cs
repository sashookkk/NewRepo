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
    /// Логика взаимодействия для Producti.xaml
    /// </summary>
    public partial class Producti : Window
    {
        Entities db = new Entities();
        public Producti()
        {
            InitializeComponent();
            ProductsList.ItemsSource = db.Product.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //переход назад
            MainWindow perehod = new MainWindow();
            perehod.Show();
            this.Close();
        }
    }
}
