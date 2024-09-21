using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaMenu
{
    internal class Customer
    {
        //for the customer ID
        public string customerID { get; set; }

        //customers address
        public string customerAddress { get; set; }

        //customer method
        public Customer(string CustomerID, string CustomerAddress)
        {
            customerID = CustomerID;
            customerAddress = CustomerAddress;
        }

    }
}
