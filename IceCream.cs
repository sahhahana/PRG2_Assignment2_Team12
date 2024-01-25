//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment2_Team12
{
    abstract class IceCream
    {
        public string Option { get; set; }
        public int Scoops { get; set; }
        public List<Flavour> Flavours { get; set; } = new List<Flavour>();
        public List<Topping> Toppings { get; set; } = new List<Topping>();
        public IceCream() { }
        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }
        public abstract double CalculatePrice();
        public virtual string ToString()
        {
            string flavoursString = FormatFlavour(Flavours);
            string toppingsString = FormatToppings(Toppings);

            return $"Option: {Option}\nNumber of Scoops: {Scoops}\nFlavours: {flavoursString}\nToppings: {toppingsString}";
        }

        string FormatFlavour(List<Flavour> flavours)
        {
            if (flavours == null || flavours.Count == 0)
            {
                return string.Empty; // or any other default value
            }

            List<string> flavourNames = flavours.Select(flavour => flavour.Type).ToList();

            return FormatFlavour(flavourNames.ToArray());
        }

        string FormatToppings(List<Topping> toppings)
        {
            if (toppings == null || toppings.Count == 0)
            {
                return string.Empty; // or any other default value
            }

            List<string> toppingNames = toppings.Select(topping => topping.Type).ToList();

            return FormatToppings(toppingNames.ToArray());
        }

        string FormatFlavour(string[] flavours)
        {
            if (flavours == null || flavours.Length == 0)
            {
                return string.Empty; // or any other default value
            }

            StringBuilder formattedFlavours = new StringBuilder(flavours[0]);

            for (int i = 1; i < flavours.Length && i < 3; i++)
            {
                if (!string.IsNullOrWhiteSpace(flavours[i]))
                {
                    formattedFlavours.Append($",{flavours[i]}");
                }
            }

            return formattedFlavours.ToString();
        }

        string FormatToppings(string[] toppings)
        {
            if (toppings == null || toppings.Length == 0)
            {
                return string.Empty; // or any other default value
            }

            StringBuilder formattedToppings = new StringBuilder(toppings[0]);

            for (int i = 1; i < toppings.Length && i < 3; i++)
            {
                if (!string.IsNullOrWhiteSpace(toppings[i]))
                {
                    formattedToppings.Append($",{toppings[i]}");
                }
            }

            return formattedToppings.ToString();
        }



    }

}
