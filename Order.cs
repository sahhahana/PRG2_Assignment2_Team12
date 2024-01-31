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

        // Reference to the associated customer
        public Customer AssociatedCustomer { get; set; }
        public Order() { }
        public Order(int id, DateTime timeReceived, Customer associatedCustomer)
        {
            Id = id;
            TimeReceived = timeReceived;
            AssociatedCustomer = associatedCustomer; // Link the order to the customer
        }

        // Method for modify ice cream
        public void ModifyIceCream(int iceCreamIndex)
        {
            if (iceCreamIndex >= 0 && iceCreamIndex < IceCreamList.Count)
            {
                Console.WriteLine("Enter modified information for the ice cream.");

                // Prompt user for modifications
                Console.Write("Option (Cup/ Cone/ Waffle): ");
                string newOption = Console.ReadLine().ToLower();

                Console.Write("Scoop(s): ");
                int newScoopsInput = Convert.ToInt32(Console.ReadLine().ToLower());

                List<Flavour> newFlavourList = GetFlavours(newScoopsInput);

                List<Topping> newToppingList = new List<Topping>();
                AskForToppings(newToppingList);

                // Update the ice cream with the modified information
                if (newOption == "cup" || newOption == "cone" || newOption == "waffle")
                {
                    IceCream modifiedIceCream = CreateIceCream(newOption, newScoopsInput, newFlavourList, newToppingList);
                    IceCreamList[iceCreamIndex] = modifiedIceCream;

                    // Printing out modified order
                    DisplayModifiedOrder(modifiedIceCream);
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter Cup, Cone, or Waffle.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ice cream index.");
            }
        }

        // Helper method for dipped cone
        bool AskForChocolateDippedCone()
        {
            while (true)
            {
                Console.Write("Do you want a chocolate-dipped cone? (Y/N): ");
                string userInput = Console.ReadLine().Trim().ToUpper();

                if (userInput == "Y")
                {
                    return true;
                }
                else if (userInput == "N")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No.");
                }
            }
        }

        // Method for waffle type
        string AskForWaffleType()
        {
            List<string> waffleFlavour = new List<string> { "orignal", "red velvet", "charcoal", "pandan" };
            while (true)
            {
                Console.Write("Select Waffle Type (original, red velvet, charcoal, or pandan): ");
                string wafflesType = Console.ReadLine().ToLower();

                if (waffleFlavour.Contains(wafflesType))
                {
                    return wafflesType;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter your waffle type.");
                    return "";
                }
            }
        }

        // Method for toppings
        void AskForToppings(List<Topping> toppingList)
        {
            Console.WriteLine("Do you want topping(s)? (Y/N): ");
            string yesOrNo = Console.ReadLine().ToUpper();
            if (yesOrNo == "N")
            {
                return;

            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Toppings: Sprinkles, Mochi, Sago, Oreos\n");
                    Console.Write("Enter topping(s): ");
                    string toppings = Console.ReadLine();
                    if (toppings.ToUpper() == "SPRINKLES" || toppings.ToUpper() == "MOCHI" || toppings.ToUpper() == "SAGO" || toppings.ToUpper() == "OREOS")
                    {
                        Topping toppingsObj = new Topping(toppings);
                        toppingList.Add(toppingsObj);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid topping. Re-enter the topping.");
                    }
                }
            }
        }

        // Check if flavour is a premium flavour
        bool IsPremiumFlavour(string flavour)
        {
            List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
            return premiumFlavours.Contains(flavour);
        }

        // Retrieve flavours from user input
        private List<Flavour> GetFlavours(int newScoopsInput)
        {
            List<string> availableFlavours = new List<string> { "vanilla", "durian", "chocolate", "ube", "strawberry", "sea salt" };
            List<Flavour> flavoursList = new List<Flavour>();

            for (int i = 1; i <= newScoopsInput; i++)
            {
                string type;
                bool premium = false;

                while (true)
                {
                    Console.WriteLine("Flavours:\n{0,-13}{1,-13}", "Regular", "Premium");
                    Console.WriteLine("{0,-13}{1,-13}\n{2,-13}{3,-13}\n{4,-13}{5,-13}\n", "Vanilla", "Durian",
                        "Chocolate", "Ube", "Strawberry", "Sea Salt");
                    Console.Write($"Flavour for scoop {i}: ");
                    type = Console.ReadLine().ToLower();

                    if (availableFlavours.Contains(type))
                    {
                        premium = IsPremiumFlavour(type);
                        break; // exit the loop if the input is a valid flavour
                    }
                    else
                    {
                        Console.WriteLine("Invalid flavour. Please choose from the available flavours.");
                    }
                }

                Flavour flavour = new Flavour(type, premium, newScoopsInput);
                flavoursList.Add(flavour);
            }

            return flavoursList;
        }

        // Method to create ice cream 
        public IceCream CreateIceCream(string newOption, int newScoopsInput, List<Flavour> newFlavourList, List<Topping> newToppingList)
        {
            IceCream newIceCream = null;

            if (newOption.ToLower() == "cup")
            {
                newIceCream = new Cup(newOption, newScoopsInput, newFlavourList, newToppingList);

            }
            else if (newOption.ToLower() == "cone")
            {
                bool dipped = AskForChocolateDippedCone();
                newIceCream = new Cone(newOption, newScoopsInput, newFlavourList, newToppingList, dipped);
            }
            else if (newOption.ToLower() == "waffle")
            {
                string waffleType = AskForWaffleType();
                newIceCream = new Waffle(newOption, newScoopsInput, newFlavourList, newToppingList, waffleType);
            }

            return newIceCream;
        }

        // Helper method to display modified order
        private void DisplayModifiedOrder(IceCream modifiedIceCream)
        {
            Console.WriteLine("Order modified and added. This is your changed order:");
            Console.WriteLine($"Type: {modifiedIceCream.Option}");
            Console.WriteLine($"Number of Scoops: {modifiedIceCream.Scoops}");

            Console.WriteLine("Flavours:");
            foreach (Flavour flavour in modifiedIceCream.Flavours)
            {
                Console.WriteLine(flavour.Type);
            }

            if (modifiedIceCream.Toppings.Count > 0)
            {
                Console.WriteLine("Toppings:");
                foreach (Topping topping in modifiedIceCream.Toppings)
                {
                    Console.WriteLine(topping.Type);
                }
            }
            else
            {
                Console.WriteLine("No toppings requested.");
            }
        }

        // Method to add ice cream
        public void AddIceCream(IceCream iceCream)
        {
            // Update the ice cream with the modified information
            IceCreamList.Add(iceCream);
            Console.WriteLine("\nOrder has been made successfully!");
        }

        // Method to delete ice cream
        public void DeleteIceCream(int iceCreamIndex)
        {
            if (iceCreamIndex >= 0 && iceCreamIndex < IceCreamList.Count)
            {
                IceCream iceCreamToRemove = IceCreamList[iceCreamIndex];
                Console.WriteLine("Removing ice cream:");
                Console.WriteLine(iceCreamToRemove); // Display the ice cream being removed
                IceCreamList.Remove(iceCreamToRemove);
                Console.WriteLine("Ice cream removed.");
            }
            else
            {
                Console.WriteLine("Invalid ice cream index.");
            }
        }


        // Method to calculate total cost of all ice creams in customer's current order
        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream iceCream in IceCreamList)
            {
                double price = iceCream.CalculatePrice();
                total += price;
            }
            return total;
        }
    }
}