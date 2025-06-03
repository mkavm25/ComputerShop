using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    public partial class CreateOrderWindow : Window
    {
        private readonly ComputerShopDBEntities _context;
        private readonly Order _order;
        private readonly List<OrderDetail> _orderItems = new List<OrderDetail>();

        public CreateOrderWindow(ComputerShopDBEntities context)
        {
            InitializeComponent();
            _context = context;
            _order = new Order { OrderDate = DateTime.Now };
            DataContext = new CreateOrderViewModel(_context, _order, _orderItems);
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CreateOrderViewModel;
            if (viewModel == null || viewModel.SelectedCustomerId == 0)
            {
                MessageBox.Show("Выберите клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (viewModel.OrderItems == null || !viewModel.OrderItems.Any())
            {
                MessageBox.Show("Добавьте товары в заказ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _order.CustomerID = viewModel.SelectedCustomerId;
                _order.TotalAmount = viewModel.TotalAmount ?? 0m; 

                _context.Orders.Add(_order);
                _context.SaveChanges();

                foreach (var item in viewModel.OrderItems)
                {
                    item.OrderID = _order.IDOrder;
                    _context.OrderDetails.Add(item);

                    var product = _context.Products.Find(item.ProductID);
                    if (product != null)
                    {
                        if (product.Quantity < item.Quantity)
                        {
                            MessageBox.Show($"Недостаточно товара {product.Name} на складе!", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        product.Quantity -= item.Quantity;
                    }
                }

                _context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении заказа: {ex.InnerException?.Message ?? ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is OrderDetail item)
            {
                (DataContext as CreateOrderViewModel)?.OrderItems.Remove(item);
            }
        }
    }
}