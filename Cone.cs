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
        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base("Cone", scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double price = 0;
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
            return price+=toppingPrice;
        }
        public override string ToString()
        {
            return $"{base.ToString()}\nDipped: {Dipped}";
        }
    } 
}
