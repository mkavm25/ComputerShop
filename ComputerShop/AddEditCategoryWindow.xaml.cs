using System.Windows;

namespace ComputerShop
{
    public partial class AddEditCategoryWindow : Window
    {
        private ComputerShopDBEntities _context;
        private Category _category;

        public AddEditCategoryWindow(ComputerShopDBEntities context, Category category = null)
        {
            InitializeComponent();
            _context = context;
            _category = category ?? new Category();
            DataContext = new AddEditCategoryViewModel(_category);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_category.Name))
            {
                MessageBox.Show("Название категории не может быть пустым!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_category.IDCategory == 0)
            {
                _context.Categories.Add(_category);
            }
            DialogResult = true;
        }
    }

    public class AddEditCategoryViewModel
    {
        public Category CurrentCategory { get; set; }
        public string WindowTitle { get; set; }

        public AddEditCategoryViewModel(Category category)
        {
            CurrentCategory = category;
            WindowTitle = category.IDCategory == 0 ? "Добавление категории" : "Редактирование категории";
        }
    }
}