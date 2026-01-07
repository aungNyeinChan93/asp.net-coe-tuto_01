namespace asp_tuto_01.Classes.Products
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public Product()
        {

        }

        public Product(int id, string name, int price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }
    }

    static class ProductRepository
    {
        private static List<Product> _products =
            [
                new Product(1,"I phone 17",1000),
                new Product(2,"Mac Book ",3000),
            ];

        public static List<Product> GetProducts() => ProductRepository._products;

        public static void AddProduct(Product product) => ProductRepository._products.Add(product);

        public static bool UpdateProduct(Product product)
        {
            if(product is not null)
            {
                var findProduct = _products.FirstOrDefault(p => p.Id == product.Id);
                if(findProduct is not null)
                {
                    findProduct.Id = product.Id;
                    findProduct.Name = product.Name;
                    findProduct.Price = product.Price;
                    return true;
                }
            }
            return false;
        }

        public static bool DeleteProduct(int? id)
        {
            if(id is not null)
            {
                var product = _products.FirstOrDefault(p => p.Id == id);
                if (product is not null) 
                {
                    _products.Remove(product);
                    return true;
                }
            }
            return false;
        }
    }
}
