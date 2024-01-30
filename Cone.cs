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
    class Cone : IceCream
    {
        public bool Dipped { get; set; }
        public Cone() { }
        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base("cone", scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double price = 0;
            if (CustomFlavour != null)
            {
                price = 0;
                if (Scoops == 1)
                {
                    price = 4.0;
                }
                else if (Scoops == 2)
                {
                    price = 5.5;
                }
                else if (Scoops == 3)
                {
                    price = 6.5;
                }
                foreach (Flavour flavour in Flavours)
                {
                    if (flavour.Type == "Durian" || flavour.Type == "Ube" || flavour.Type == "Sea Salt")
                    {
                        price += 2.0;
                    }
                }
                double toppingPrice = 0;
                foreach (Topping topping in Toppings)
                {
                    if (topping != null)
                    {
                        toppingPrice += 1.0;
                    }
                }
                if (Dipped == true)
                {
                    price += 2.0;
                }
                price += toppingPrice;
            }

            else if (CustomFlavour != null)
            {
                price = 0;
                if (Scoops == 1)
                {
                    price = 4.0;
                }
                else if (Scoops == 2)
                {
                    // Check if it's a swirl ice cream
                    if (CustomFlavour.MixInFlavours.Count == 2)
                    {
                        price = 7.0; // Set a price for swirl ice cream (adjust as needed)
                    }
                    else
                    {
                        price = 6.0; // Default price for two scoops without swirl
                    }
                }
                else if (Scoops == 3)
                {
                    price = 7.0;
                }

                // Calculate price for mix-in flavors
                foreach (Flavour mixInFlavour in CustomFlavour.MixInFlavours)
                {
                    price += GetMixInFlavorPrice(mixInFlavour);
                }


                // Calculate price for base flavors
                foreach (Flavour baseFlavour in Flavours)
                {
                    if (baseFlavour.Type == "durian" || baseFlavour.Type == "ube" || baseFlavour.Type == "sea salt")
                    {
                        price += 2.0;
                    }
                }

                double toppingPrice = 0;
                foreach (Topping topping in Toppings)
                {
                    if (topping != null)
                    {
                        toppingPrice += 2.0;
                    }
                }
                if (Dipped == true)
                {
                    price += 2.0;
                }
                price += toppingPrice;
            }

            // return a default value 0
            return price;
        }
        // Helper method to get the price for mix-in flavours
        double GetMixInFlavorPrice(Flavour mixInFlavour)
        {
            // Assuming each mix-in flavor has an additional cost
            // Adjust the logic based on your pricing strategy

            // For example, you might have a property in the Flavour class
            // that indicates the additional cost for mix-in flavors.
            double additionalCost = mixInFlavour.AdditionalCost;

            // You can also include any other logic specific to mix-in flavors
            // based on their properties.

            return additionalCost;
        }
        public override string ToString()
        {
            return $"{base.ToString()}\nDipped: {Dipped}";
        }
    } 
}
