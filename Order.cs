using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment2_Team12
{
    internal class Order
    {
        private int id;
        private DateTime timeReceived;
        private DateTime? timeFulfilled;
       
        public int Id { get;set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set;}
        public List<IceCream> IceCreamList { get; set; } = new List<IceCream>();
        public Order() { }
        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;
        }
        public ModifyIceCream(int id)
        {
            foreach(Custome)
            {
                if (iceCream == id)
                {

                }
            }
        }
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);

        }
        public DeleteIceCream(int iceCreamId)
        {
            IceCreamList.Remove(IceCream)
        }
        public double CalculateTotal()
        {

        }
        public override string ToString()
        {
            return $"{base.ToString()} ID: {Id} Order Received Time: {TimeReceived} Order Fulfilled Time: {TimeFulfilled}";
        }
    }
}
