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
        public int PunchCard { get; set; }
        public string Tier { get; set; }
        public PointCard() { }
        public PointCard(int punchCard, int points, string tier)
        {
            Points = points;
            PunchCard = punchCard;
            Tier = tier;
            UpdateTier(); // Use UpdateTier in the case where the number of points is >0, 50 or >= 100

        }
        // Method to add points to the pointcard
        public void AddPoints(int amountPaid)
        {
            int earnedPoints = (int)Math.Floor(amountPaid * 0.72); //formula based on rubrics
            Points += earnedPoints;
            UpdateTier();
        }
        // Method to redeem the points from the pointcard
        public void RedeemPoints(int pointsToRedeem)
        {
            if (Points >= pointsToRedeem) // Check if the user's tier is silver or gold
            {
                Points -= pointsToRedeem;
                // Call to update the tier in case the number of points drops the tier status
                UpdateTier();
            }
            else
            {
                Console.WriteLine("Insufficient number of points.");
                return;
            }
        }
        public void Punch()
        {

            if (PunchCard > 11)
            {
                PunchCard = 10; // Reset punch card to 10
            }
            else if (PunchCard == 11)
            {
                PunchCard = 0; // Reset punch card
            }


        }
        // Method to update tier
        // For easier reference to update the tier regularly instead of constantly re-coding it out
        public void UpdateTier()
        {
            if (Tier == "Silver")
            {
                if (Points >= 100)
                {
                    Tier = "Gold";
                }
                else
                {
                    Tier = "Silver";
                }
            }
            else if (Tier == "Ordinary")
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
}
