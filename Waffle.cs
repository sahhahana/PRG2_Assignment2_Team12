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
    class Waffle : IceCream
    {
        public string WaffleFlavour { get; set; }
        public Waffle(): base() { }
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) : base("Waffle", scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
        }
        public override double CalculatePrice()
        {
            double price = 0;
            if (CustomFlavour != null)
            {
                price = 0;
                // Calculate base price based on scoops
                if (Scoops == 1)
                {
                    price = 7.0;
                }
                else if (Scoops == 2)
                {
                    price = 8.5;
                }
                else if (Scoops == 3)
                {
                    price = 9.5;
                }

                // Add extra price for specific waffle flavors
                if (WaffleFlavour == "Red Velvet" || WaffleFlavour == "Charcoal" || WaffleFlavour == "Pandan")
                {
                    price += 3.0;
                }

                // Add extra price for specific ice cream flavors
                foreach (Flavour flavour in Flavours)
                {
                    if (flavour.Type == "Durian" || flavour.Type == "Ube" || flavour.Type == "Sea Salt")
                    {
                        price += 2.0;
                    }
                }

                // Add the fixed price for each topping multiplied by the count of toppings
                double toppingPrice = 0;
                foreach (Topping topping in Toppings)
                {
                    if (topping != null)
                    {
                        toppingPrice += 1.0;
                    }
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

                // Add extra price for specific waffle flavors
                if (WaffleFlavour == "Red Velvet" || WaffleFlavour == "Charcoal" || WaffleFlavour == "Pandan")
                {
                    price += 3.0;
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
                price += toppingPrice;
            }

            // return a default value 0
            return price;
        }
        // Helper method to get the price for min-in flavours
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
            return $"{base.ToString()}\nWaffle Flavour: {WaffleFlavour}";
        }
    }
}
