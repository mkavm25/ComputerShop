using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class AddEditSupplierViewModel
    {
        public Supplier CurrentSupplier { get; }
        public string WindowTitle { get; }

        public AddEditSupplierViewModel(Supplier supplier)
        {
            CurrentSupplier = supplier;
            WindowTitle = supplier.SupplierID == 0 ? "Добавление поставщика" : "Редактирование поставщика";
        }
    }
}
