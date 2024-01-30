//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment2_Team12
{
    internal class Customer
    {
        private string name;
        private int memberid;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory;
        private PointCard rewards;
        public string Name { get; set; }
        public int Memberid { get; set; }
        public DateTime Dob{ get; set; }
        public Order CurrentOrder { get; set; } // Customer's latest order before being fulfilled
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public PointCard Rewards { get; set; } = new PointCard();
        public Customer() { }
        public Customer(string name, int memberid, DateTime dob)
        {
            Name = name;
            Memberid = memberid;
            Dob = dob;
        }
        // Method to make a new order for the customer
        public Order MakeOrder( Customer customer)
        {
            int ID = OrderHistory.Count + 1;
            Order newOrder = new Order(ID, DateTime.Now, customer);
            CurrentOrder = newOrder;  // Link the new order to the customer's current order
            //OrderHistory.Add(newOrder);  // Add the new order to the customer's order history
            return newOrder;
        }
        // Method to check if it is the customer's birthday today
        public bool IsBirthday()
        {
            if (Dob.Month == DateTime.Today.Month && Dob.Day == DateTime.Today.Day) 
            { 
                return true;
            }
            return false;
        }
        // Method to make a custom order for the customer
        public Order MakeCustomOrder(Customer customer)
        {
            int orderId = OrderHistory.Count + 1;
            Order customOrder = new Order(orderId, DateTime.Now, customer);
            CurrentOrder = customOrder; // Link the new order to the customer's current order
            OrderHistory.Add(customOrder); // Add the new order to the customer's order history

            //customOrder.MakeCustomOrder(this);  // Call the new method in the Order class

            return customOrder;
        }

        public override string ToString()
        {
            return $"{base.ToString()} Name: {Name}, Member ID: {Memberid}, DOB: {Dob:dd/MM/yyyy}";
        }
    }
}
