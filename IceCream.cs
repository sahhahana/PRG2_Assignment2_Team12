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

            return $"Option: {Option}\nNumber of Scoops: {Scoops}\nFlavours: {flavoursString}\nToppings: {toppingsString}" +
       $"\n\nCustomization:\n{customFlavourString}";
        }

        // Helper method to format flavours list into a string for the printing of flavours
        string FormatFlavour(List<Flavour> flavours)
        {
            if (flavours == null || flavours.Count == 0)
            {
                return string.Empty; // or any other default value
            }
            // Convert flavour list into string list
            List<string> flavourNames = flavours.Select(flavour => flavour.Type).ToList();

            return FormatFlavour(flavourNames.ToArray());
        }
        // Helper method to format toppings list into a string for the printing of toppings
        string FormatToppings(List<Topping> toppings)
        {
            if (toppings == null || toppings.Count == 0)
            {
                return string.Empty; // or any other default value
            }
            // Convert toppings list into string list
            List<string> toppingNames = toppings.Select(topping => topping.Type).ToList();

            return FormatToppings(toppingNames.ToArray());
        }
        // Helper method to format the string of flavours for printing
        string FormatFlavour(string[] flavours)
        {
            if (flavours == null || flavours.Length == 0)
            {
                return string.Empty; // or any other default value
            }
            // Use StringBuilder to help join strings together which skips over null flavours
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
        // Helper method to format the string of toppings for printing
        string FormatToppings(string[] toppings)
        {
            if (toppings == null || toppings.Length == 0)
            {
                return string.Empty; // or any other default value
            }
            // Use StringBuilder to help join strings together which skips over null toppings
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
