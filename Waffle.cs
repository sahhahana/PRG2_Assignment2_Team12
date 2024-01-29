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

            // Calculate base price based on scoops
            if (Scoops == 1)
            {
                price += 7.0;
            }
            else if (Scoops == 2)
            {
                price += 8.5;
            }
            else if (Scoops == 3)
            {
                price += 9.5;
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

            return price+=toppingPrice;
        }

        public override string ToString()
        {
            return $"{base.ToString()}\nWaffle Flavour: {WaffleFlavour}";
        }
    }
}
