using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ComputerShop
{
    public partial class AddEditProductWindow : Window
    {
        private ComputerShopDBEntities _context;
        private Product _product;

        public AddEditProductWindow(ComputerShopDBEntities context, Product product = null)
        {
            InitializeComponent();
            _context = context;
            _product = product ?? new Product();
            DataContext = new AddEditProductViewModel(_context, _product);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void IntegerValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_product.IDProduct == 0)
            {
                _context.Products.Add(_product);
            }
            DialogResult = true;
        }
    }

    public class AddEditProductViewModel
    {
        public Product CurrentProduct { get; set; }
        public string WindowTitle { get; set; }
        public System.Collections.Generic.List<Category> Categories { get; set; }

        public AddEditProductViewModel(ComputerShopDBEntities context, Product product)
        {
            CurrentProduct = product;
            WindowTitle = product.IDProduct == 0 ? "Добавление товара" : "Редактирование товара";
            Categories = context.Categories.ToList();
        }
    }
}