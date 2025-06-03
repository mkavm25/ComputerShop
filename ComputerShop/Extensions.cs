namespace ComputerShop
{
    public static class Extensions
    {
        public static string FullName(this Customer customer)
        {
            return $"{customer.LastName} {customer.FirstName}";
        }

        public static string FullName(this Product product)
        {
            return $"{product.Brand} {product.Name} ({product.Model})";
        }
    }
}