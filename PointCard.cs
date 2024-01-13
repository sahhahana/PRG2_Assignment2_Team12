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
            Tier = "Ordinary";
            
        }
        public void AddPoints(int points)
        {
            PunchCard += points;

        }
        public void RedeemPoints(int points)
        {
            if (Tier != "Ordinary")
            {
                Console.WriteLine("Only Silver and Gold member can reedem their points");
            }
            else
            {
                if (Points != 0)
                {
                    PunchCard -= points;
                }
                else
                {
                    Console.WriteLine("Insufficient points to redeem.");
                }
            }
            
        }
        public void Punch()
        {
            PunchCard = 0;
        }

    }
}
