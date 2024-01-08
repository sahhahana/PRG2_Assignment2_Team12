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
        private List<Order> orderHistory;
        private PointCard rewards;
        public string Name { get; set; }
        public int Memberid { get; set; }
        public DateTime Dob { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public PointCard Rewards { get; set; }

    }
}
