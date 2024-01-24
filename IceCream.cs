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
            // Extract the flavour names from the list
            List<string> flavourNames = flavours.Select(flavour => flavour.Type).ToList();

            // Use the FormatFlavour method to concatenate the flavour names
            return FormatFlavour(flavourNames.ToArray());
        }

        string FormatToppings(List<Topping> toppings)
        {
            // Extract the topping names from the list
            List<string> toppingNames = toppings.Select(topping => topping.Type).ToList();

            // Use the FormatToppings method to concatenate the topping names
            return FormatToppings(toppingNames.ToArray());
        }

        string FormatFlavour(string[] flavours)
        {
            return $"{flavours[0]}{(string.IsNullOrWhiteSpace(flavours[1]) ? "" : $",{flavours[1]}")}{(string.IsNullOrWhiteSpace(flavours[2]) ? "" : $",{flavours[2]}")}";
        }

        string FormatToppings(string[] toppings)
        {
            return $"{toppings[0]}{(string.IsNullOrWhiteSpace(toppings[1]) ? "" : $",{toppings[1]}")}{(string.IsNullOrWhiteSpace(toppings[2]) ? "" : $",{toppings[2]}")}";
        }

    }

}
