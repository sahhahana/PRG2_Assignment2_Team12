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
    internal class Order
    {
        private int id;
        private DateTime timeReceived;
        private DateTime? timeFulfilled;

        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
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
                // how to identify which order from the multi ice cream order do they want to modify
                Console.WriteLine("Enter modified information for the ice cream.");
                Console.Write("Enter the new option (cup, cone, waffle): ");
                string newOption = Console.ReadLine();

                Console.Write("Enter the new numebr of scoops: ");
                int newScoopsInput = Convert.ToInt32(Console.ReadLine());

                int flavourQuantity = 0;
                List<Flavour> newFlavourList = new List<Flavour>();
                while (flavourQuantity <= newScoopsInput)
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
                    IceCream modifiedIceCream = new Cone(newOption, newScoopsInput, newFlavourList, newToppingList, dipped);
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

                //printing out modified order
                Console.WriteLine("Order modified and added. This is your changed order: \nType: {0}\nNumber of Scoops: {1}\nFlavours:", newOption, newScoopsInput);
                foreach (Flavour flavour in newFlavourList)
                {
                    Console.WriteLine(flavour.Type.ToString());
                }
                if (newToppingList.Count > 0) //if toppings is requested
                {
                    Console.WriteLine("Toppings: ");
                    foreach (Topping topping in newToppingList)
                    {
                        Console.WriteLine(topping.Type.ToString());
                    }
                }
                else if (newToppingList.Count == 0) //if no toppings requested
                {
                    Console.WriteLine("No toppings requested.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ice cream index.");
            }
        }

        public void AddIceCream(IceCream iceCream)
        {
            // Update the ice cream with the modified information
            IceCreamList.Add(iceCream);
            /*printing out added order
            Console.WriteLine("Order added. This is your order: \nType: {0}\\nNumber of Scoops: {1}\\nFlavours:", iceCream.Option, iceCream.Scoops);
            foreach (Flavour flavour in iceCream.Flavours)
            {
                Console.WriteLine(flavour.ToString());
            }
            if (iceCream.Toppings.Count > 0) //if toppings is requested
            {
                Console.WriteLine("Toppings: ");
                foreach (Topping topping in iceCream.Toppings)
                {
                    Console.WriteLine(topping.Type.ToString());
                }
            }
            else if (iceCream.Toppings.Count == 0) //if no toppings requested
            {
                Console.WriteLine("No toppings requested.");
            }*/
            
        }

        public void DeleteIceCream(int iceCreamIndex)
        {
            if (iceCreamIndex >= 0 && iceCreamIndex < IceCreamList.Count)
            {
                IceCream iceCreamToRemove = IceCreamList[iceCreamIndex];
                IceCreamList.Remove(iceCreamToRemove);
                Console.WriteLine("Ice cream removed.");
            }
            else
            {
                Console.WriteLine("Invalid ice cream index.");
            }
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream iceCream in IceCreamList)
            {
                double price = iceCream.CalculatePrice();
                total += price;
            }
            Console.WriteLine("Total amount to pay: {0}", total);
            return total;
        }
    }
}