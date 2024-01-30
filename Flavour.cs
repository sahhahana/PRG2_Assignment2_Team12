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
    internal class Flavour
    {
        private string type;
        private bool premium;
        private int quantity;
        public string Type { get; set; }    
        public bool Premium { get; set; }
        public int Quantity { get; set; }

        // New properties for customization
        public bool IsBaseFlavor { get; set; }
        public double AdditionalCost { get; set; }

        // Mix-in flavors property
        public List<Flavour> MixInFlavours { get; set; }

        public Flavour() { }
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;

            IsBaseFlavor = true; // Mark as a base flavor
            MixInFlavours = new List<Flavour>();
        }
        // Constructor for mix-in flavors
        public Flavour(string type, double additionalCost)
        {
            Type = type;
            AdditionalCost = additionalCost;
            IsBaseFlavor = false; // Mark as a mix-in flavor
        }
        public override string ToString()
        {
            return $"Type: {Type}\nPremium: {Premium}\nQuantity: {Quantity}\n" +
                   $"IsBaseFlavor: {IsBaseFlavor}\nAdditionalCost: {AdditionalCost}\n";
        }
    }
}
