using Demo2.AllWindows;
using Demo2.DdTyalet;
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

namespace Demo2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WinOsnov : Window
    {
        public WinOsnov()
        {
            InitializeComponent();
            ListB.ItemsSource = App.demo2.Product.ToList();
            FellCombBox();
            CombBoxFilter.ItemsSource = App.demo2.ProductType.ToList();
            CombBoxFilter.DisplayMemberPath = "Title";
           
        }

        private void TbPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            var res = App.demo2.Product.ToList();
            res = res.Where(x => x.Title.ToLower().Contains(TbPoisk.Text.ToLower())).ToList();
            ListB.ItemsSource = res.ToList();
        }
        private void FellCombBox()
        {
            cmbBoxSort.Items.Add("Наименование от А до Я");
            cmbBoxSort.Items.Add("Наименование от Я до А");
        }
        private void SortInfo()
        {
            switch (cmbBoxSort.SelectedItem)
            {
                case "Наименование от А до Я":
                    ListB.ItemsSource = null;
                    ListB.ItemsSource = App.demo2.Product.OrderBy(x => x.Title).ToList();
                    break;
                case "Наименование от Я до А":
                    ListB.ItemsSource = null;
                    ListB.ItemsSource = App.demo2.Product.OrderByDescending(x => x.Title).ToList();
                    break;
                    default:
                    ListB.ItemsSource= null;
                    ListB.ItemsSource = App.demo2.Product.ToList();
                    break;
            }
        }

        private void cmbBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortInfo();
        }

        private void CombBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedType = CombBoxFilter.SelectedItem;
            var type = ((ProductType)selectedType).ID;
            ListB.ItemsSource = App.demo2.Product.Where(x => x.ProductTypeID == type).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                   WinAdd winAdd = new WinAdd();
            winAdd.Show();
        }

        private void ListB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = MessageBox.Show("Вы точно хотите удалить элемент?", "Удаление элемента", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (MessageBoxResult.Yes == a)
            {
                try
                {
                    var item = ListB.SelectedItem as Product;
                    App.demo2.ProductCostHistory.RemoveRange(App.demo2.ProductCostHistory.Where(x => x.ProductID == item.ID).ToList());
                    App.demo2.ProductMaterial.RemoveRange(App.demo2.ProductMaterial.Where(x => x.ProductID == item.ID).ToList());
                    App.demo2.ProductSale.RemoveRange(App.demo2.ProductSale.Where(x => x.ProductID == item.ID).ToList());
                    App.demo2.Product.Remove(item);
                    App.demo2.SaveChanges();
                    MessageBox.Show("Успешное удаление!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
        }
    }
}
