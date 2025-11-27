using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для metod.xaml
    /// </summary>
    public partial class metod : Window
    {
        
        public metod()
        {
            InitializeComponent();
        }
        //Для того чтобы много раз не прописывать на каждый текст бокс методы на "фильтрацию" вызываем всех их к одному методу TextBox
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextAllowed(e.Text);
        }
        public static bool TextAllowed(string text)
        {
            return text.All(char.IsDigit);
        }
    
    private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            if (!Regex.IsMatch(newText, @"^\d*[,]?\d{0,2}$"))
            {
                e.Handled = true;
            }
        }
        private void TextBox_PreviewTextInput_2(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            if (!Regex.IsMatch(newText, @"^\d*[,]?\d{0,2}$"))
            {
                e.Handled = true;
            }
        }
        private void TextBox_PreviewTextInput_3(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextAllowed(e.Text);
        }

        private void TextBox_PreviewTextInput_4(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextAllowed(e.Text);
        }

        private void raschetbt_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //экземпляр класса
                Class1 class1 = new Class1();

                int prType = int.Parse(tbprType.Text);
                int matType = int.Parse(tbmatType.Text);
                int requiredProd = int.Parse(tbRequiredAmount.Text);
                int warehouseProd = int.Parse(tbWarehouseAmount.Text);
                float width = float.Parse(tbWidth.Text);
                float height = float.Parse(tbHeight.Text);

                //пишем название переменной класса и ее данные
                int result = class1.MaterialAm(prType, matType, requiredProd, warehouseProd, width, height);

                //вывод в лейбл
                labelOutput.Content = result.ToString();
            }
            catch
            {
                MessageBox.Show("Error!!");
            }
        }
    }
}
    
