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

namespace AsyncDelegate
{
    /// <summary>
    /// Interaction logic for AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        private bool IsCategory;
        public AddElement(bool isCategory)
        {
            InitializeComponent();
            IsCategory = isCategory;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            nameElement.Text = String.Empty;
        }

        private void SaveElement(object sender, RoutedEventArgs e)
        {
            using(var context = new Context())
            {
                if (IsCategory)
                {
                    var setCategory = context.Set<Category>();
                    setCategory.Add(new Category
                    {
                        Name = nameElement.Text
                    });
                    context.SaveChanges();
                }
                else
                {
                    var setProduct = context.Set<Product>();
                    setProduct.Add(new Product
                    {
                        Name = nameElement.Text,
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
