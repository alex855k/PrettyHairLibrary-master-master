using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrettyHairLibrary
{
   public class Order
    {
        // Unique key for the product, and then the amount of this product in the order
        Dictionary<Product, int> orderlines = new Dictionary<Product, int>();
        private string deliveryDate;
        private string orderDate;
        public int OrderId { get; private set; } 
        
        public Order(int orderid, string dd,string od, Dictionary<Product, int> ol)
        {
            orderlines = ol;
            deliveryDate = dd;
            orderDate = od;
            this.OrderId = orderid;
        }

        public bool CheckQuantity()
        {
            bool cond = true;

            foreach(KeyValuePair<Product,int> p in orderlines)
            {
                if (p.Key.Amount < p.Value) {
                    cond = false;
                }
            }
            return cond;
        }

        public Dictionary<Product, int> GetOrderLines()
        {
            return orderlines;
        }

        public Order()
        {

        }
        public override string ToString()
        {

            string orderString = "order [deliverydate="+ this.deliveryDate +", orderdate="+this.orderDate+"]";
            return orderString;
        }
    }
}
