using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment2_Team12
{
    class Cup:IceCream
    {
        public Cup() { }
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings)
        {

        }
        public override double CalculatePrice()
        {
            double price = 0;
            double toppingCount = 1;
            if (Scoops == 1)
            {
                price = 4.0;
            }
            else if (Scoops = 2)
            {
                price = 5.5;
            }
            else if (Scoops = 3)
            {
                price = 6.5;
            }
            foreach (Flavour flavour in Flavours)
            {
                if (flavour = 'Durian')
                {
                    price += 2;
                }
                else if (flavour = 'Ube')
                {
                    price += 2;
                }
                else if (flavour = 'Sea Salt')
                {
                    price += 2;
                }
            }
            foreach (Topping topping in Toppings)
            {
                toppingCount += 1
            }
            return price+(toppingCount*1);
        }
        public override string ToString()
        {
            return $"{base.ToString()}";
        }
}
