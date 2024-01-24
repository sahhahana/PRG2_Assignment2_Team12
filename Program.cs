//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using Microsoft.VisualBasic.FileIO;
using PRG2_Assignment2_Team12;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;


//chloe - features 2,5 and 6
//sahana- features 1, 3 ad 4

// todo:
//current order
//order history
//icecream list
//how does the queue system work and how it links to order id

Queue<Order> regularOrderQueue = new Queue<Order>();
Queue<Order> goldOrderQueue = new Queue<Order>();


// Read customers.csv and create a dictionary to store customer data
Dictionary<int, Customer> customerDictionary = new Dictionary<int, Customer>();


using (StreamReader sr = new StreamReader("customers.csv")) //to read file 'customers.csv'
{
    string s = sr.ReadLine(); //read heading
    if (s != null)
    {
        string[] headings = s.Split(","); //headings
    }
    while ((s = sr.ReadLine()) != null)
    {
        string[] data = s.Split(",");
        if (data.Length == 6)
        {
            string name = data[0];
            int memberId = (int)Convert.ToInt64(data[1]);
            DateTime dob = Convert.ToDateTime(data[2]);
            int points = (int)Convert.ToInt32(data[4]);
            int punchCard = Convert.ToInt32(data[5]);
            string status = data[3];

            // Create Customer and PointCard objects
            Customer customerData = new Customer(name, memberId, dob);
            PointCard pointCard = new PointCard(punchCard, points, status);

            // Link the PointCard to the Customer
            customerData.Rewards = pointCard;

            // Add Customer to the dictionary
            customerDictionary.Add(memberId, customerData);
        }
    }
}

bool IsPremiumFlavour(string flavour)
{
    List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
    return premiumFlavours.Contains(flavour);
}

// Read orders.csv separately beacuse it is used throughout the whole program

Customer customer = null;

using (StreamReader sr = new StreamReader("orders.csv"))
{
    string s = sr.ReadLine(); // Read heading
    if (s != null)
    {
        string[] headings = s.Split(","); // Headings
    }

    while ((s = sr.ReadLine()) != null)
    {
        string[] data = s.Split(",");

        int orderId = Convert.ToInt32(data[0]);
        int memberId = Convert.ToInt32(data[1]);

        // Find the customer based on memberId
        if (customerDictionary.TryGetValue(memberId, out Customer foundCustomer))
        {
            customer = foundCustomer;
        }

        DateTime timeReceived = Convert.ToDateTime(data[2]);
        DateTime timeFulfilled = Convert.ToDateTime(data[3]);

        // Create a new order and add it to the customer's order history
        Order order = new Order(orderId, timeReceived);
        customer?.OrderHistory.Add(order);

        // Simplify the creation of Flavour and Topping instances
        List<Flavour> flavoursList = CreateFlavoursList(data, 8, 10);
        List<Topping> toppingsList = CreateToppingsList(data, 11, 14);

        // Do something with the lists (e.g., add them to the order)

        // Now, create an IceCream object and add it to the order's IceCreamList
        string option = data[4];
        int scoops = Convert.ToInt32(data[5]);

        if (option == "Cup")
        {
            IceCream iceCream = new Cup(option, scoops, flavoursList, toppingsList);
            order.IceCreamList.Add(iceCream);
        }
        else if (option == "Cone")
        {
            bool dipped = Convert.ToBoolean(data[6]);
            IceCream iceCream = new Cone(option, scoops, flavoursList, toppingsList, dipped);
            order.IceCreamList.Add(iceCream);
        }
        else if (option == "Waffle")
        {
            string waffleFlavour = data[7];
            IceCream iceCream = new Waffle(option, scoops, flavoursList, toppingsList, waffleFlavour);
            order.IceCreamList.Add(iceCream);
        }
    }
}

// Helper method to create a list of Flavour instances
List<Flavour> CreateFlavoursList(string[] data, int startIndex, int endIndex)
{
    List<Flavour> flavoursList = new List<Flavour>();

    for (int i = startIndex; i <= endIndex; i++)
    {
        Flavour flavour = new Flavour(data[i], IsPremiumFlavour(data[i]), 1);
        flavoursList.Add(flavour);
    }
    return flavoursList;
}

// Helper method to create a list of Topping instances
List<Topping> CreateToppingsList(string[] data, int startIndex, int endIndex)
{
    List<Topping> toppingsList = new List<Topping>();

    for (int i = startIndex; i <= endIndex; i++)
    {
        Topping topping = new Topping(data[i]);
        toppingsList.Add(topping);
    }

    return toppingsList;
}




//display menu
void DisplayOptions()
{
    Console.WriteLine("========= Welcome to I.C.Treats! =========\n" +
        "\n1. List all customers\n" +
        "2. List all current orders\n" +
        "3. Register a new customer\n" +
        "4. Create a customer's order\n" +
        "5. Display order details of a customer\n" +
        "6. Modify order details\n" +
        "7. Process an order and checkout\n" +
        "8. Display monthly charged amounts breakdown & total charged amounts for the year\n" +
        "0. Exit\n");
    Console.Write("Enter your option: ");
}




// Run program

while (true)
{
    DisplayOptions();
    string option = Console.ReadLine();

    if (option == "1")
    {
        OptionOne();
    }
    else if (option == "2")
    {
        OptionTwo(customerDictionary);
    }
    else if (option == "3")
    {
        OptionThree();
    }
    else if (option == "4")
    {

        OptionFour();

    }
    else if (option == "5")
    {
        OptionFive(customerDictionary);
    }
    else if (option == "6")
    {
        OptionSix(customerDictionary);
    }
    else if (option == "7")
    {
        //OptionSeven();
    }
    else if (option == "8")
    {
        // OptionEight();
    }
    else if (option == "0")
    {
        Console.WriteLine("Thank you for shopping with I.C.Treats!");
        break;
    }
    else
    {
        Console.WriteLine("Invalid option! Please try agian.");
    }
}

// Feature 1
void OptionOne()
{
    //In this case, i (chloe) used "Celeste" as a test to test if the program worked
    //So here I have to remove all "Celeste" Values to show only the values taken from the .csv file
    //Just know that this part works :>

    Console.WriteLine($"{"Name",-8} {"MemberID",-12} {"DOB",-14} {"Status",-14} {"Points",-10} {"Punch Card",-10}");
    Console.WriteLine($"{"----",-8} {"--------",-12} {"---",-14} {"------",-14} {"------",-10} {"----------",-10}");

    List<int> membersToRemove = new List<int>();

    foreach (var entry in customerDictionary)
    {
        Customer customer = entry.Value;
        // Since I used "Celeste" as my test run experiments, I will remove them from the dictionary
        // This is because "Celeste" is (unfortunately) now part of the dict permanantly
        // If this code is removed "Celeste" values will re-appear

        if (customer.Name != "Celeste")
        {
            Console.WriteLine($"{customer.Name,-8} {customer.Memberid,-12} {customer.Dob.ToString("dd/MM/yyyy"),-14} {customer.Rewards.Tier,-14} {customer.Rewards.Points,-10} {customer.Rewards.PunchCard,-10}");
        }
    }
    Console.WriteLine("");


}

// Feature 2
void OptionTwo(Dictionary<int, Customer> customerDictionary)
{
    List<Customer> goldList = new List<Customer>();
    List<Customer> nonGoldList = new List<Customer>();

    foreach (Customer customer in customerDictionary.Values)
    {
        if (customer.Rewards.Tier == "Gold")
        {
            goldList.Add(customer);
        }
        else if (customer.Rewards.Tier == "Ordinary")
        {
            nonGoldList.Add(customer);
        }
    }

    using (StreamReader sr = new StreamReader("orders.csv"))
    {
        string s = sr.ReadLine();
        if (s != null)
        {
            string[] headings = s.Split(",");
            Console.WriteLine("{0,-5}  {1,-10}  {2,-20} {3,-20} {4,-10}" +
                "{5,-10}  {6,-10} {7,-15} {8,-30} {9,-10}",
                headings[0], headings[1], headings[2], headings[3],
                headings[4], headings[5], headings[6], headings[7], "Flavour", "Toppings");
            Console.WriteLine("{0,-5}  {1,-10}  {2,-20} {3,-20} {4,-10}" +
                "{5,-10}  {6,-10} {7,-15} {8,-30} {9,-10}",
                "--", "--------", "------------", "------------",
                "------", "------", "------", "-------------", "-------", "--------");
        }

        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");

            int custId = Convert.ToInt32(data[1]);

            // Check if the Customer with the specified MemberId is in the goldList
            if (goldList.Any(c => c.Memberid == custId))
            {
                Console.WriteLine("{0,-5}  {1,-10}  {2,-20} {3,-20} {4,-10}" +
                  "{5,-10}  {6,-10} {7,-15} {8,-30} {9,-10}", data[0], data[1], data[2], data[3], data[4],
                  data[5], data[6], data[7], FormatFlavour(data[8], data[9], data[10]), FormatToppings(data[11], data[12], data[13]));
            }

            // Check if Customer with the specified MemberID is in the nonGoldList
            else if (nonGoldList.Any(c => c.Memberid == custId))
            {
                Console.WriteLine("{0,-5}  {1,-10}  {2,-20} {3,-20} {4,-10}" +
                  "{5,-10}  {6,-10} {7,-15} {8,-30} {9,-10}", data[0], data[1], data[2], data[3], data[4],
                  data[5], data[6], data[7], FormatFlavour(data[8], data[9], data[10]), FormatToppings(data[11], data[12], data[13]));
            }
        }
        Console.WriteLine("");
    }
    string FormatFlavour(string f1, string f2, string f3)
    {
        return $"{f1}{(string.IsNullOrWhiteSpace(f2) ? "" : $",{f2}")}{(string.IsNullOrWhiteSpace(f3) ? "" : $",{f3}")}";
    }
    string FormatToppings(string t1, string t2, string t3)
    {
        return $"{t1}{(string.IsNullOrWhiteSpace(t2) ? "" : $",{t2}")}{(string.IsNullOrWhiteSpace(t3) ? "" : $",{t3}")}";
    }
}


// Feature 3
void OptionThree()
{
    while (true)
    {
        try
        {
            Console.WriteLine("\nNew Customer\n\r" +
                          "------------");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Random random = new Random();

            // Generate a random six-digit ID
            int memberId = random.Next(100000, 1000000);

            Console.WriteLine($"Your 6-digit member ID is {memberId} ");

            Console.Write("Enter your date of birth (dd/mm/yyyy): ");
            DateTime dob = Convert.ToDateTime(Console.ReadLine());
            if (dob >= DateTime.Today)
            {
                throw new Exception("Date of Birth cannot later than today!");
            }

            string status = "Ordinary";

            Customer newCustomer = new Customer(name, memberId, dob);

            PointCard newPointCard = new PointCard(0, 0, status);
            newCustomer.Rewards = newPointCard;
            customerDictionary.Add(memberId, newCustomer);

            using (StreamWriter sw = new StreamWriter("Customers.csv", true))
            {
                sw.WriteLine($"{newCustomer.Name},{newCustomer.Memberid},{newCustomer.Dob.ToString("dd/MM/yyyy")},{newCustomer.Rewards.Tier},{newCustomer.Rewards.Points},{newCustomer.Rewards.PunchCard}");

            }
            Console.WriteLine("\nCustomer is officially a member of I.C.Treats! Welcome:D\n");
            break;

        }
        catch (FormatException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

}

// Feature 4
void OptionFour()
{
    // list customer details
    Console.WriteLine($"{"Name",-8} {"MemberID",-12} {"DOB",-14} {"Status",-14} {"Points",-10} {"Punch Card",-10}");
    Console.WriteLine($"{"----",-8} {"--------",-12} {"---",-14} {"------",-14} {"------",-10} {"----------",-10}");

    foreach (Customer existingCustomer in customerDictionary.Values)
    {
        Console.WriteLine($"{existingCustomer.Name,-8} {existingCustomer.Memberid,-12} {existingCustomer.Dob.ToString("dd/MM/yyyy"),-14} {existingCustomer.Rewards.Tier,-14} {existingCustomer.Rewards.Points,-10} {existingCustomer.Rewards.PunchCard,-10}");
    }

    Customer customer;
    string anotherIceCream = "Y";

    // prompt user to select a customer
    Console.Write("Enter customer's member ID to select: ");
    int memberid = Convert.ToInt32(Console.ReadLine());
    while (anotherIceCream == "Y")
    {

        // retreive selected customer
        if (customerDictionary.ContainsKey(memberid))
        {
            customer = customerDictionary[memberid];

            // prompt user to enter their ice cream order
            Console.WriteLine($"{customer.Name}'s Order");
            Console.WriteLine("------------------------------\n");

            Console.Write("Option (Cup/ Cone/ Waffle): ");
            string option = Console.ReadLine().ToLower();

            Console.Write("Scoop(s): ");
            int scoop = Convert.ToInt32(Console.ReadLine().ToLower());
            int scoopNo = 1;
            List<Flavour> flavoursList = new List<Flavour>();
            if (scoop > 1)
            {
                for (int i = 1; i < scoop + 1; i++)
                {
                    Console.Write($"Flavour for scoop {i}: ");
                    string type = Console.ReadLine().ToLower();
                    bool premium = IsPremiumFlavour(type);


                    Flavour flavour = new Flavour(type, premium, scoop);
                    flavoursList.Add(flavour);

                }

            }
            else
            {
                Console.Write("Flavour: ");
                string type = Console.ReadLine().ToLower();
                bool premium = IsPremiumFlavour(type);


                Flavour flavour = new Flavour(type, premium, scoop);
                flavoursList.Add(flavour);

            }
            List<Topping> toppingList = new List<Topping>();
            AskForToppings(toppingList);

            Order orderNew = customer.MakeOrder();
            List<IceCream> IceCreamList = orderNew.IceCreamList;
            IceCream newOrder;

            // create ice cream object 
            if (option == "cup")
            {
                newOrder = new Cup(option, scoop, flavoursList, toppingList);
            }
            else if (option == "cone")
            {
                bool dipped = AskForChocolateDippedCone();
                newOrder = new Cone(option, scoop, flavoursList, toppingList, dipped);
            }
            else if (option == "waffle")
            {
                string waffleType = AskForWaffleType();
                newOrder = new Waffle(option, scoop, flavoursList, toppingList, waffleType);
            }
            else
            {
                Console.WriteLine("Invalid option! Please enter Cup, Cone, or Waffle.");
                return;
            }

            // add ice cream object to order
            orderNew.AddIceCream(newOrder);

            customer.CurrentOrder = orderNew;

            Console.Write("Would you like to add another ice cream to the order? (Y/N): ");
            anotherIceCream = Console.ReadLine().ToUpper();
            if (anotherIceCream == "N")
            {
                break;
            }
            else
            {
                continue;
            }

            //Append the order to the appropriate queue based on the customer's Pointcard tier
            if (customer.Rewards.Tier == "Gold")
            {
                goldOrderQueue.Enqueue(orderNew);
            }
            else
            {
                regularOrderQueue.Enqueue(orderNew);
            }

            Console.WriteLine("\nOrder has been made successfully!");
            DateTime timeFulfilled = (DateTime)customer.CurrentOrder.TimeFulfilled;
            DateTime timeReceived = (DateTime)customer.CurrentOrder.TimeReceived;

            int maxFlavors = 3;
            int maxToppings = 4;

            string flavorFormat = string.Join(",", Enumerable.Range(0, maxFlavors).Select(i => $"{{newOrder.Flavours[{i}]}}"));
            string toppingFormat = string.Join(",", Enumerable.Range(0, maxToppings).Select(i => $"{{newOrder.Toppings[{i}]}}"));
            customer.CurrentOrder.Id += 1;

            foreach (IceCream iceCream in customer.CurrentOrder.IceCreamList)
            {
                for (int i = 0; i < iceCream.Flavours.Count; i++)
                {
                    for (int j = 0; j < iceCream.Toppings.Count; j++)
                    {
                        Console.WriteLine($"{customer.CurrentOrder.Id},{customer.Memberid},{timeReceived.ToString("dd/MM/yyyy")},{timeFulfilled.ToString("dd/MM/yyyy")},{iceCream.Option},{iceCream.Scoops},{iceCream.Flavours[i].Type},{iceCream.Toppings[j].Type}");

                    }

                }

            }
        }
        else
        {
            Console.WriteLine("Customer is not a member at I.C.Treats. Please ensure the correct Member ID was selected or register this customer by choosing option 3.");
        }
        // todo: add points and modify code -- add a orderid couonter, add topping methods, i need to add this order plus info from order in th eorder.csv file, add 1 flavour at a time
        //Id,MemberId,TimeReceived,TimeFulfilled,Option,Scoops,Dipped,WaffleFlavour,Flavour1,Flavour2,Flavour3,Topping1,Topping2,Topping3,Topping4
    }

    /*
    void WriteIntoCSV()
    {
        using (StreamWriter sw = new StreamWriter) {};
    }
    */

    bool AskForChocolateDippedCone()
    {
        Console.Write("Do you want a chocolate-dipped cone? (Y/N): ");
        return Console.ReadLine().Trim().ToUpper() == "Y";
    }

    string AskForWaffleType()
    {
        List<string> waffleFlavour = new List<string> { "red velvet", "charcoal", "pandan" };
        while (true)
        {
            Console.Write("Select Waffle Type (red velvet, charcoal, or pandan): ");
            string wafflesType = Console.ReadLine().ToLower();

            if (waffleFlavour.Contains(wafflesType))
            {
                return wafflesType;
            }
            else
            {
                Console.Write("Please choose among red velvet, charcoal or pandan waffle type.");
                return "";
            }
        }
    }
    void AskForToppings(List<Topping> toppingList)
    {
        Console.Write("Do you want topping(s)? (Y/N): ");
        string yesOrNo = Console.ReadLine().ToUpper();


        if (yesOrNo == "N")
        {
            return;

        }
        else
        {
            Console.Write("Enter topping(s): ");
            string toppings = Console.ReadLine();
            Topping toppingsObj = new Topping(toppings);
            toppingList.Add(toppingsObj);
        }
    }
}


// Feature 5
static void OptionFive(Dictionary<int, Customer> customerDictionary)
{
    /* list the customers
 prompt ottoo the robot to select a customer and retrieve the selected customer
 retrieve all the order objects of the customer, past and current -- using order history
 for each order, display all the details of the order including datetime received, datetime
fulfilled(if applicable) and all ice cream details associated with the order*/
    DisplayCustomerDictionary(customerDictionary);

    Console.Write("Enter the Member ID of the customer you want to view orders for:");
    string inputId = Console.ReadLine();
    int selectedMemberId;

    try
    {
        selectedMemberId = Convert.ToInt32(inputId);
        if (customerDictionary.ContainsKey(selectedMemberId))
        {
            Customer selectedCustomer = customerDictionary[selectedMemberId];
            Console.WriteLine($"\nOrders for {selectedCustomer.Name} (MemberID: {selectedCustomer.Memberid}):\n");
            List<Order> orderHistory = selectedCustomer.OrderHistory;
            foreach (Order order in orderHistory)
            {
                DisplayOrderDetails(order);
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Customer not found or invalid input. Please enter a valid MemberID.");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input format. Please enter a valid integer for MemberID.");
    }
}

static void DisplayOrderDetails(Order order)
{
    Console.WriteLine($"ID: {order.Id}\nTime Received: {order.TimeReceived}\nTime Fulfilled: {order.TimeFulfilled?.ToString("dd/MM/yyyy HH:mm") ?? "Not fulfilled"}");

    foreach (var iceCream in order.IceCreamList)
    {
        Console.WriteLine(iceCream.ToString());
    }
}
static void DisplayCustomerDictionary(Dictionary<int, Customer> customers)
{
    Console.WriteLine("Customer List:");
    foreach (var entry in customers)
    {
        Customer customer = entry.Value;
        Console.WriteLine($"MemberID: {customer.Memberid}, Name: {customer.Name}");
    }
    Console.WriteLine();
}


// Feature 6
static void OptionSix(Dictionary<int, Customer> customerDictionary)
{
    /* - list customers
     * - user chooses a customer > get that customers order
     * - list all iceCream objects contained in the order
     * need to use orderid to refernece all the ice cream orders referencing ice cream list 

     * - choose: [1] modify iceCream, [2] add new iceCream, [3] delete iceCream
     * [1]: choose which iceCream to modify > prompt for all the information > update accordingly
     * [2]: prompt for all the information > update accordingly (add to order)
     * [3]: choose which iceCream to remove > remove iceCream from order 
     *      (if iceCream count == 1: "You cannot have 0 iceCreams in an order."
     * - display the new updated order      */
    DisplayCustomerDictionary(customerDictionary);
    Console.Write("Enter the Member ID of the customer you want to view orders for:");
    string inputId = Console.ReadLine();
    int selectedMemberId;

    try
    {
        selectedMemberId = Convert.ToInt32(inputId);
        if (customerDictionary.ContainsKey(selectedMemberId))
        {
            Customer selectedCustomer = customerDictionary[selectedMemberId];
            Console.WriteLine($"\nOrders for {selectedCustomer.Name} (MemberID: {selectedCustomer.Memberid}):\n");
            List<Order> orderHistory = selectedCustomer.OrderHistory;
            foreach (Order order in orderHistory)
            {
                DisplayOrderDetails(order);
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Customer not found or invalid input. Please enter a valid MemberID.");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input format. Please enter a valid integer for MemberID.");
    }
}


// Advanced (a)- Feature 7
static void OptionSeven()
{
    Console.WriteLine("Hello World");
}

// Advanced (b)- Feature 8
static void OptionEight()
{
    Console.WriteLine("Hello World");
}


// Advanced (c)- GUI
// Will be done most likely on a separate document