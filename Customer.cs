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
        public DateTime Dob { get; set; }
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
            Order order = new Order();
            return order;
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
