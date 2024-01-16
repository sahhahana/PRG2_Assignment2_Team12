//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using PRG2_Assignment2_Team12;


//chloe - features 2,5 and 6
//sahana- features 1, 3 ad 4

// Feature 1
using (StreamReader sr = new StreamReader("customers.csv"))
{
    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        if (line != null)
        {
            string[] data = line.Split(",");
            Console.WriteLine(data[0]);
        }
    }
}


// Feature 2

// Feature 3

// Feature 4

// Feature 5

// Feature 6

// Advanced (a)

// Advanced (b)

// Advanced (c)

