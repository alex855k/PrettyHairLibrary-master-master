using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettyHairLibrary;

namespace ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Just testing the first event
            Product product1 = new Product(00, "product", 5.99, 10);
            Product product2 = new Product(00, "product", 5.99, 10);
            OrderRepository or = new OrderRepository();
            Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
            orderlines.Add(product1, 2);
            orderlines.Add(product2, 2);
            Order o = new Order(1, "2016-11-26", "2016-12-26", orderlines);
            MailServer sm = new MailServer();
            sm.Subscribe(or);
            or.Add(o);
            Console.ReadLine();
        }
    }
}
