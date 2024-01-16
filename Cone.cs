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
        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base(option, scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double price = 0;
            double toppingCount = 1;
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
            if (Dipped = true)
            {
                price += 2;
            }
            return price;
        }
        public string ToString()
        {
            return $"{base.ToString()}\nDipped: {Dipped}";
        }
    } 
}
