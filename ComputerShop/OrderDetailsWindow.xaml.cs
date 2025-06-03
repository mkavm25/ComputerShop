using System.Linq;
using System.Windows;

namespace ComputerShop
{
    public partial class OrderDetailsWindow : Window
    {
        private ComputerShopDBEntities _context;

        public OrderDetailsWindow(ComputerShopDBEntities context, Order order)
        {
            InitializeComponent();
            _context = context;

            _context.Entry(order).Collection(o => o.OrderDetails).Load();

            foreach (var detail in order.OrderDetails)
            {
                _context.Entry(detail).Reference(d => d.Product).Load();
            }

            DataContext = order;
        }
    }
}