﻿//==========================================================
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
    class Cup : IceCream
    {
        public Cup() { }
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base("Cup", scoops, flavours, toppings)
        {

        }
        public override double CalculatePrice()
        {
            double price = 0;
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
                if (flavour.Type == "durian" || flavour.Type == "ube" || flavour.Type == "sea salt")
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
            return price += toppingPrice;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    } 
}
