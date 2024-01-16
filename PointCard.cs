//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment2_Team12
{
    internal class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;
        public int Points { get; set; }
        public int PunchCard { get; set;}
        public string Tier { get; set; }
        public PointCard() { }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            UpdateTier(); //use UpdateTier in the case where the no. of points is >0 50 or >= 100
            
        }
        public void AddPoints(int amountPaid)
        {
            int earnedPoints = (int)Math.Floor(amountPaid * 0.72); //formula based on rubrics
            Points += earnedPoints;
            UpdateTier();
        }
        public void RedeemPoints(int pointsToRedeem)
        {
            if (tier == "Silver" || tier == "Gold") // || should refer to 'or'
            {
                points -= pointsToRedeem;
                UpdateTier();
            }
            else
            {
                Console.WriteLine("Only Silver and Gold members can redeem points.");
            }
        }
        public void Punch()
        {
            punchCard++;
            if (punchCard == 10)
            {
                Console.WriteLine("Congratulations! Your 11th ice cream is free!");
                punchCard = 0; // Reset punch card
            }
        }
        private void UpdateTier() //for easier reference to update the tier regularly instead of constantly re-coding it out
        {
            if (points >= 100)
            {
                tier = "Gold";
            }
            else if (points >= 50)
            {
                tier = "Silver";
            }
            else
            {
                tier = "Ordinary";
            }
        }
    }
}
