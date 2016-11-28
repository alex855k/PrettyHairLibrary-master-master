using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrettyHairLibrary
{
    public class ProductRepository
    {
        Dictionary<int, Product> productList = new Dictionary<int, Product>();
        public void Delete(int key)
        {
            productList.Remove(key);
        }

        public void Add(Product product)
        {
            productList.Add(product.ID, product);
        }
        public Product GetProduct(int key)
        {
            return productList[key];
        }

        public void AdjustPrice(int key, double newPrice)
        {
            productList[key].Price = newPrice;
        }

        public void AdjustAmount(int key, int newAmount)
        {
            productList[key].Amount = newAmount;
        }

        public void AdjustDescription(int key, string newDescription)
        {
            productList[key].Description = newDescription;
        }

    }
}
