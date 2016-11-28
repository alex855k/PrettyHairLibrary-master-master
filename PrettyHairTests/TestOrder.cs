using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrettyHairLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrettyHairTests
{
    [TestClass]
    public class TestOrder
    {
        [TestMethod]
        public void CanCreateOrder()
        {
            Product product1 = new Product(00, "product", 5.99, 10);
            Product product2 = new Product(00, "product", 5.99, 10);
            Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
            orderlines.Add(product1, 2);
            orderlines.Add(product2, 2);
            Order o = new Order(1, "2016-11-26", "2016-12-26", orderlines);
            Assert.AreEqual("order [deliverydate=2016-11-26, orderdate=2016-12-26]", o.ToString());
        }

        [TestMethod]
        public void CanAddOrderToRepository()
        {
            Product product1 = new Product(00, "product", 5.99, 10);
            Product product2 = new Product(00, "product", 5.99, 10);
            OrderRepository or = new OrderRepository();
            Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
            orderlines.Add(product1, 2);
            orderlines.Add(product2, 2);
            Order o = new Order(1, "2016-11-26", "2016-12-26", orderlines);
            int orderid = 1;
            or.Add(o);
            Assert.AreEqual(or.GetOrder(orderid).ToString(), "order [deliverydate=2016-11-26, orderdate=2016-12-26]");

        }
        
        [TestMethod]
        public void CanRemoveOrderFromRepository()
        {
            Product product1 = new Product(00, "product", 5.99, 10);
            Product product2 = new Product(01, "product2", 5.99, 10);
            OrderRepository or = new OrderRepository();
            Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
            orderlines.Add(product1, 2);
            orderlines.Add(product2, 2);
            Order o = new Order(1, "2016-11-26", "2016-12-26", orderlines);
           
            or.Add(o);
            or.Remove(o);
            
            Assert.AreEqual(null, or.GetOrder(o.OrderId));
        }



        [TestMethod]
        public void CanNotifyWarehouseManager()
        {
            Product product1 = new Product(00, "product", 5.99, 10);
            OrderRepository or = new OrderRepository();
            Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
            orderlines.Add(product1, 2);
            Order o = new Order(1, "2016-11-26", "2016-12-26", orderlines);
            
            MailServer sm = new MailServer();
            sm.Subscribe(or);
            // When adding an order to the repository, the MailServer which is subscribed to OrderRepository executes the event handle
            // Method does nothing, just there to test if events work, test it by checking if an integer is being incremented
            or.Add(o);
                    
            Assert.AreEqual(1, sm.EmailsSent);

        }

        [TestMethod]
        public void CanCheckAmountInOrderlinesAgainstStock()
        {
            //products for the first order
            Product product1 = new Product(00, "product", 5.99, 4);
            Product product2 = new Product(01, "product2", 5.99, 10);
            
            //products for the orderlines in the 2nd order has a lower amount in stock than in the order and should return false in the 2nd assert equal
            Product product3 = new Product(02, "product3", 5.99, 0);
            Product product4 = new Product(03, "product4", 5.99, 2);

            OrderRepository or = new OrderRepository();

            Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
            orderlines.Add(product1, 2);
            orderlines.Add(product2, 2);

            Dictionary<Product, int> orderlines2 = new Dictionary<Product, int>();
            orderlines2.Add(product3, 2);
            orderlines2.Add(product4, 2);

            Order o = new Order(1, "2016-11-26", "2016-12-26", orderlines);
            Order o2 = new Order(2, "2016-11-22", "2016-12-23", orderlines2);

            Assert.AreEqual(true, o.CheckQuantity());
            Assert.AreEqual(false, o2.CheckQuantity());

        }

        [TestMethod]
        public void CanTriggerEventWhenInadaquiteAmount() {

            //products for an order inadaquite amount
            Product product1 = new Product(00, "product", 5.99, 0);
            Product product2 = new Product(01, "product2", 5.99, 1);

            OrderRepository or = new OrderRepository();

            Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
            orderlines.Add(product1, 2);
            orderlines.Add(product2, 3);

            Order o = new Order(1, "2016-11-26", "2016-12-26", orderlines);

            MailServer sm = new MailServer();
            sm.Subscribe(or);
            or.Add(o);
            // One email sent when adding and then another when the event fires from having an inadaquate amount
            Assert.AreEqual(sm.EmailsSent, 2);
        }
    }
}
