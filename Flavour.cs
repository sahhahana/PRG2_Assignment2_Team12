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

        public Flavour() { }
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }
        public override string ToString()
        {
            return $"Type: {Type}\nPremium: {Premium}\nQuantity: {Quantity}\n";
        }
    }
}
