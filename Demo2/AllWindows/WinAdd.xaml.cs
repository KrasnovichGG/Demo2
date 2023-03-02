using Demo2.DdTyalet;
using Microsoft.Win32;
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

namespace Demo2.AllWindows
{
    /// <summary>
    /// Логика взаимодействия для WinAdd.xaml
    /// </summary>
    public partial class WinAdd : Window
    {
        public WinAdd()
        {
            InitializeComponent();
            cmbTypeProduct.ItemsSource = App.demo2.ProductType.ToList();
        }
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            openFileDialog.FilterIndex = 1;
                if(openFileDialog.ShowDialog() == true) ;
            {
                File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFileDialog.FileName);
                image.EndInit();
                imageBym.Source = image;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            {
                product.Image = openFileDialog.FileName;
                product.Title = TbName.Text;
                product.ArticleNumber = TbArt.Text;
                product.MinCostForAgent = Convert.ToDecimal(TbMinCost.Text);
                product.ProductionPersonCount = Convert.ToInt32(TbCountPeople.Text);
                product.ProductionWorkshopNumber = Convert.ToInt32(TbNumerCex.Text);
                var idType = cmbTypeProduct.SelectedItem;
                product.ProductTypeID = ((ProductType)idType).ID;
            }
            App.demo2.Product.Add(product);
            MessageBox.Show("Добавлено");
            App.demo2.SaveChanges();
            this.Close();
        }
    }
}
