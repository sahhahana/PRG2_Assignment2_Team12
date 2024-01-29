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
        public PointCard(int punchCard, int points, string tier)
        {
            Points = points;
            PunchCard = punchCard;
            Tier = tier;
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
            if (Tier == "Silver" || Tier == "Gold") // || should refer to 'or'
            {
                Points -= pointsToRedeem;
                UpdateTier();
            }
            else
            {
                Console.WriteLine("Only Silver and Gold members can redeem points.");
            }
        }
        public void Punch()
        {
            PunchCard++;
            if (PunchCard % 10 == 0)
            {
                if (PunchCard > 10)
                {
                    PunchCard = 10; // Reset punch card to 10
                }
                else
                {
                    PunchCard = 0; // Reset punch card
                }
                    
            }
        }
        public void UpdateTier() //for easier reference to update the tier regularly instead of constantly re-coding it out
        {
            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50)
            {
                Tier = "Silver";
            }
            else
            {
                Tier = "Ordinary";
            }
        }
    }
}
