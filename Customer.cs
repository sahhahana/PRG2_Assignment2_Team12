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
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public PointCard Rewards { get; set; } = new PointCard();
        public Customer() { }
        public Customer(string name, int memberid, DateTime dob)
        {
            Name = name;
            Memberid = memberid;
            Dob = dob;
        }
        public Order MakeOrder()
        {
            int ID = OrderHistory.Count + 1;
            Order newOrder = new Order(ID, DateTime.Now);
            CurrentOrder = newOrder;  // Link the new order to the customer's current order
            OrderHistory.Add(newOrder);  // Add the new order to the customer's order history
            return newOrder;
        }

        public bool IsBirthday()
        {
            if (Dob == DateTime.Today) 
            { 
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{base.ToString()} Name: {Name} Member ID: {Memberid} DOB: {Dob}";
        }
    }
}
