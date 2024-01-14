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
            if (iceCreamIndex >= 0 && iceCreamIndex < IceCreamList.Count)
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

                List<Topping> newToppingList = new List<Topping>();
                Console.WriteLine("Do you want any toppings? Yes/No: ");
                if (Console.ReadLine() == "Yes")
                {
                    while (true) 
                    {
                        Console.WriteLine("Enter the type of topping (enter 'Quit' to exit): ");
                        string toppingType = Console.ReadLine();
                        Topping newTopping = new Topping(toppingType);
                        newToppingList.Add(newTopping);
                        if (Console.ReadLine() == "Quit")
                        {
                            break;
                        }
                    }
                }
                else if (Console.ReadLine() == "No")
                {
                    Topping newTopping = new Topping(null);
                    newToppingList.Add(newTopping);
                }
                // Update the ice cream with the modified information
                if (newOption == "Cup") 
                {
                    IceCream modifiedIceCream = new Cup(newOption, newScoopsInput, newFlavourList, newToppingList);
                    IceCreamList[iceCreamIndex] = modifiedIceCream;
                }
                else if (newOption == "Cone")
                {
                    Console.WriteLine("Do you want your cone dipped? Yes/No: ");
                    bool dipped = false;
                    if (Console.ReadLine() == "Yes")
                    {
                        dipped = true;
                    }
                    else if (Console.ReadLine() == "No")
                    {
                        dipped = false;
                    }
                    IceCream modifiedIceCream = new Cone(newOption, newScoopsInput, newFlavourList, newToppingList,dipped);
                    IceCreamList[iceCreamIndex] = modifiedIceCream;
                }
                else if (newOption == "Waffle")
                {
                    Console.WriteLine("Enter your waffle flavour: ");
                    string waffleFlavour = Console.ReadLine();
                    List<string> waffleFlavourList = new List<string> { "Red Velvet", "Charcoal", "Pandan" };
                    if (waffleFlavourList.Contains(waffleFlavour))
                    {
                        IceCream modifiedIceCream = new Waffle(newOption, newScoopsInput, newFlavourList, newToppingList, waffleFlavour);
                        IceCreamList[iceCreamIndex] = modifiedIceCream;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid ice cream index.");
            }
        }

    }
}
