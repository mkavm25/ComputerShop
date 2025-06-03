using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    public partial class MainWindow : Window
    {
        private readonly ComputerShopDBEntities _context = new ComputerShopDBEntities();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _context.Products.Load();
            ProductsGrid.ItemsSource = _context.Products.Local.ToBindingList();

            _context.Categories.Load();
            CategoriesGrid.ItemsSource = _context.Categories.Local.ToBindingList();

            _context.Customers.Load();
            CustomersGrid.ItemsSource = _context.Customers.Local.ToBindingList();

            _context.Orders.Include(o => o.Customer).Load();
            OrdersGrid.ItemsSource = _context.Orders.Local.ToBindingList();

            _context.Suppliers.Load();
            SuppliersGrid.ItemsSource = _context.Suppliers.Local.ToBindingList();
        }

        #region Product Methods

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditProductWindow(_context);
            if (window.ShowDialog() == true)
            {
                _context.SaveChanges();
                ProductsGrid.Items.Refresh();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int productId = (int)button.Tag;
                var product = _context.Products.Find(productId);
                if (product != null)
                {
                    var window = new AddEditProductWindow(_context, product);
                    if (window.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        ProductsGrid.Items.Refresh();
                    }
                }
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int productId = (int)button.Tag;
                var product = _context.Products.Find(productId);
                if (product != null)
                {
                    if (MessageBox.Show($"Удалить товар '{product.Name}'?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _context.Products.Remove(product);
                        _context.SaveChanges();
                        ProductsGrid.Items.Refresh();
                    }
                }
            }
        }

        #endregion

        #region Category Methods

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditCategoryWindow(_context);
            if (window.ShowDialog() == true)
            {
                _context.SaveChanges();
                CategoriesGrid.Items.Refresh();
            }
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int categoryId = (int)button.Tag;
                var category = _context.Categories.Find(categoryId);
                if (category != null)
                {
                    var window = new AddEditCategoryWindow(_context, category);
                    if (window.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        CategoriesGrid.Items.Refresh();
                    }
                }
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int categoryId = (int)button.Tag;
                var category = _context.Categories.Find(categoryId);
                if (category != null)
                {
                    if (MessageBox.Show($"Удалить категорию '{category.Name}'?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _context.Categories.Remove(category);
                        _context.SaveChanges();
                        CategoriesGrid.Items.Refresh();
                    }
                }
            }
        }

        #endregion

        #region Customer Methods

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditCustomerWindow(_context);
            if (window.ShowDialog() == true)
            {
                _context.SaveChanges();
                CustomersGrid.Items.Refresh();
            }
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int customerId = (int)button.Tag;
                var customer = _context.Customers.Find(customerId);
                if (customer != null)
                {
                    var window = new AddEditCustomerWindow(_context, customer);
                    if (window.ShowDialog() == true)
                    {
                        _context.SaveChanges();
                        CustomersGrid.Items.Refresh();
                    }
                }
            }
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int customerId = (int)button.Tag;
                var customer = _context.Customers.Find(customerId);
                if (customer != null)
                {
                    if (MessageBox.Show($"Удалить клиента '{customer.LastName} {customer.FirstName}'?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _context.Customers.Remove(customer);
                        _context.SaveChanges();
                        CustomersGrid.Items.Refresh();
                    }
                }
            }
        }

        #endregion

        #region Order Methods

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateOrderWindow(_context);
            if (window.ShowDialog() == true)
            {
                _context.SaveChanges();
                OrdersGrid.Items.Refresh();
            }
        }

        private void ShowOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int orderId = (int)button.Tag;
                var order = _context.Orders.Find(orderId);
                if (order != null)
                {
                    var window = new OrderDetailsWindow(_context, order);
                    window.ShowDialog();
                }
            }
        }

        #endregion

        #region Supplier Methods

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditSupplierWindow(_context);
            if (window.ShowDialog() == true && window.CurrentSupplier != null)
            {
                try
                {
                    _context.Suppliers.Add(window.CurrentSupplier);
                    _context.SaveChanges();
                    _context.Suppliers.Load();
                    SuppliersGrid.ItemsSource = _context.Suppliers.Local.ToBindingList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении поставщика: {ex.Message}",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersGrid.SelectedItem is Supplier selectedSupplier)
            {
                var window = new AddEditSupplierWindow(_context, selectedSupplier);
                if (window.ShowDialog() == true)
                {
                    try
                    {
                        _context.Entry(selectedSupplier).State = EntityState.Modified;
                        _context.SaveChanges();

                        _context.Suppliers.Load();
                        SuppliersGrid.ItemsSource = _context.Suppliers.Local.ToBindingList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}",
                                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int supplierId = (int)button.Tag;
                var supplier = _context.Suppliers.Find(supplierId);
                if (supplier != null)
                {
                    if (MessageBox.Show($"Удалить поставщика '{supplier.SupplierName}'?", "Подтверждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _context.Suppliers.Remove(supplier);
                        _context.SaveChanges();
                        SuppliersGrid.Items.Refresh();
                    }
                }
            }
        }

        #endregion

        private string GetDbErrorDetails(DbUpdateException dbEx)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var entry in dbEx.Entries)
            {
                sb.AppendLine($"Сущность: {entry.Entity.GetType().Name}");
            }
            if (dbEx.InnerException != null)
            {
                sb.AppendLine($"Внутренняя ошибка: {dbEx.InnerException.Message}");
                if (dbEx.InnerException.InnerException != null)
                {
                    sb.AppendLine($"Детали: {dbEx.InnerException.InnerException.Message}");
                }
            }
            return sb.ToString();
        }
    }
}