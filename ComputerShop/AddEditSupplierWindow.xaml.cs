using System.Data.Entity;
using System;
using System.Windows;

namespace ComputerShop
{
    public partial class AddEditSupplierWindow : Window
    {
        private readonly ComputerShopDBEntities _context;
        private Supplier _supplier;
        public Supplier CurrentSupplier => _supplier;

        public AddEditSupplierWindow(ComputerShopDBEntities context, Supplier supplier = null)
        {
            InitializeComponent();
            _context = context;
            _supplier = supplier ?? new Supplier();

            DataContext = new AddEditSupplierViewModel(_supplier);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_supplier.SupplierName))
            {
                MessageBox.Show("Название поставщика не может быть пустым!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_supplier.SupplierID == 0)
                {
                    _context.Suppliers.Add(_supplier);
                }
                else
                {
                    _context.Entry(_supplier).State = EntityState.Modified;
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}