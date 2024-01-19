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
            double toppingCount = 1;
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
            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Type == "Durian")
                {
                    price += 2;
                }
                else if (flavour.Type == "Ube")
                {
                    price += 2;
                }
                else if (flavour.Type == "Sea Salt")
                {
                    price += 2;
                }
            }
            foreach (Topping topping in Toppings)
            {
                toppingCount += 1;
            }
            price += (toppingCount * 1);
            if (WaffleFlavour == "Red Velvet")
            {
                price += 3;
            }
            else if (WaffleFlavour == "Charcoal")
            {
                price += 3;
            }
            else if (WaffleFlavour == "Pandan")
            {
                price += 3;
            }
            return price;
        }
        public string ToString()
        {
            return $"{base.ToString()}\nWaffle Flavour: {WaffleFlavour}";
        }
    }
}
