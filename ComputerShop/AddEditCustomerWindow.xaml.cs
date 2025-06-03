using System.Windows;

namespace ComputerShop
{
    public partial class AddEditCustomerWindow : Window
    {
        private ComputerShopDBEntities _context;
        private Customer _customer;

        public AddEditCustomerWindow(ComputerShopDBEntities context, Customer customer = null)
        {
            InitializeComponent();
            _context = context;
            _customer = customer ?? new Customer();
            DataContext = new AddEditCustomerViewModel(_customer);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_customer.FirstName) || string.IsNullOrWhiteSpace(_customer.LastName))
            {
                MessageBox.Show("Имя и фамилия клиента обязательны для заполнения!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_customer.IDCustomer == 0)
            {
                _context.Customers.Add(_customer);
            }
            DialogResult = true;
        }
    }

    public class AddEditCustomerViewModel
    {
        public Customer CurrentCustomer { get; set; }
        public string WindowTitle { get; set; }

        public AddEditCustomerViewModel(Customer customer)
        {
            CurrentCustomer = customer;
            WindowTitle = customer.IDCustomer == 0 ? "Добавление клиента" : "Редактирование клиента";
        }
    }
}