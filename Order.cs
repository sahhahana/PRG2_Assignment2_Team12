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
            TimeFulfilled = DateTime.MinValue;
        }
        public void ModifyIceCream(int iceCreamIndex)
        {
            foreach(IceCream iceCream in iceCreamList)
            {
                // Prompt user for modifications
                Console.WriteLine("Enter modified information for the ice cream.");
                Console.Write("Enter the new option (cup, cone, waffle): ");
                string newOption = Console.ReadLine();

                Console.Write("Enter the new numebr of scoops: ");
                int newScoopsInput = Convert.ToInt32(Console.ReadLine());

                int flavourQuantity =0;
                List<Flavour> newFlavourList = new List<Flavour>();
                while (flavourQuantity<=newScoopsInput)
                {
                    Console.WriteLine("Enter the type of flavour you want: ");
                    string flavour = Console.ReadLine();
                    Console.WriteLine("Is this flavour a premium flavour? Yes/No? \nPremium flavours: Durian, Ube, Sea Salt");
                    bool premiumFlavour = false;
                    if (Console.ReadLine() == "Yes")
                    {
                        premiumFlavour = true;
                    }
                    else if (Console.ReadLine() == "No")
                    {
                        premiumFlavour = false;
                    }
                    Console.WriteLine("How much of this flavour do you want?\nEnter the quantity of scoops of this flavour: ");
                    int scoopQuantity = Convert.ToInt32(Console.ReadLine());
                    flavourQuantity += scoopQuantity;
                    Flavour newFlavour = new Flavour(flavour, premiumFlavour, scoopQuantity);
                    newFlavourList.Add(newFlavour);
                }

                }
            }
        }
        public AddIceCream(IceCream iceCream)
        {

        }
        public DeleteIceCream(int iceCreamId)
        {

        }
        public double CalculateTotal()
        {

        public void DeleteIceCream(int iceCreamIndex)
        {
            IceCreamList.Remove(IceCreamList[iceCreamIndex]);
            Console.WriteLine("Order {0} has been removed.", iceCreamIndex);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach(IceCream iceCream in IceCreamList)
            {
                double price = iceCream.CalculatePrice();
                total += price;
            }
            Console.WriteLine("Total amount to pay: {0}", total);
            return total;
        }
    }
}
