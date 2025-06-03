using ComputerShop;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

public class CreateOrderViewModel : INotifyPropertyChanged
{
    private readonly ComputerShopDBEntities _context;

    public ObservableCollection<Customer> Customers { get; set; }
    public ObservableCollection<Product> Products { get; set; }

    private int? _selectedCustomerId;
    public int? SelectedCustomerId
    {
        get => _selectedCustomerId;
        set
        {
            _selectedCustomerId = value;
            OnPropertyChanged(nameof(SelectedCustomerId));
        }
    }

    private decimal? _totalAmount;
    public decimal? TotalAmount
    {
        get => _totalAmount;
        set
        {
            _totalAmount = value;
            OnPropertyChanged(nameof(TotalAmount));
        }
    }

    public ObservableCollection<OrderDetail> OrderItems { get; set; }

    public CreateOrderViewModel(ComputerShopDBEntities context, Order order, List<OrderDetail> orderItems)
    {
        _context = context;

        Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
        Products = new ObservableCollection<Product>(_context.Products.ToList());

        OrderItems = new ObservableCollection<OrderDetail>(orderItems);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}