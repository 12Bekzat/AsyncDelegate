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

namespace AsyncDelegate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Category CurrentCategory = new Category();
        private Product CurrentProduct = new Product();
        Func<List<Category>, List<Category>> actionCategory;
        Func<List<Product>, List<Product>> actionProduct;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCategoryClick(object sender, RoutedEventArgs e)
        {
            products.Visibility = Visibility.Collapsed;
            somethings.Visibility = Visibility.Visible;
            actionCategory = new Func<List<Category>, List<Category>>(FillCategory);
            var categories = new List<Category>();
            actionCategory.BeginInvoke(categories, AddListCategory, null);
            productBtn.Visibility = Visibility.Visible;
            addCategory.Visibility = Visibility.Visible;
        }

        private void AddListCategory(IAsyncResult ar)
        {
            var categories = actionCategory.EndInvoke(ar);
            foreach (var category in categories)
            {
                somethings.Items.Add(category.Name);
            }
        }

        private List<Category> FillCategory(List<Category> categories)
        {
            using (var context = new Context())
            {
                foreach (var category in context.Categories)
                {
                    categories.Add(category);
                }
                return categories;
            }
        }

        private void SelectSomethings(object sender, SelectionChangedEventArgs e)
        {
            var item = somethings.SelectedItem as Category;
            CurrentCategory = item;
        }

        private void SelectProducts(object sender, SelectionChangedEventArgs e)
        {
            var item = products.SelectedItem as Product;
            CurrentProduct = item;
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            AddElement addElement = new AddElement(true);
            addElement.Show();
        }
        
        private void ButtonProductClick(object sender, RoutedEventArgs e)
        {
            somethings.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Visible;
            actionProduct = new Func<List<Product>, List<Product>>(FillProducts);
            var productsList = new List<Product>();
            actionProduct.BeginInvoke(productsList, AddListProduct, null);
            addProduct.Visibility = Visibility.Visible;
        }

        private void AddListProduct(IAsyncResult ar)
        {
            var product = actionCategory.EndInvoke(ar);
            foreach (var item in product)
            {
                products.Items.Add(item.Name);
            }
        }

        private List<Product> FillProducts(List<Product> items)
        {
            using(var context = new Context())
            {
                foreach(var product in context.Products)
                {
                    items.Add(product);
                }
                return items;
            }
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            AddElement addElement = new AddElement(false);
            addElement.Show();
        }
    }
}
