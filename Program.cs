//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using PRG2_Assignment2_Team12;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;


//chloe - features 2,5 and 6
//sahana- features 1, 3 ad 4

Queue<Order> regularOrderQueue = new Queue<Order>();
Queue<Order> goldOrderQueue = new Queue<Order>();
int orderIdCounter = 1;

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


// Read orders.csv separately beacuse it is used throughout the whole program
List<string[]> orderDetailsList = new List<string[]>();
List<Order> orderList = new List<Order>();
using (StreamReader sr=new StreamReader("orders.csv"))
{
    string s = sr.ReadLine(); //read heading
    if (s != null)
    {
        string[] headings = s.Split(",");//headings
    }
    while ((s = sr.ReadLine()) != null)
    {
        string[] data = s.Split(",");
        // Order: id, time received
        Order orderData = new Order(Convert.ToInt32(data[0]), Convert.ToDateTime(data[2]));
        orderList.Add(orderData);
        orderDetailsList.Add(data);
    }
}

//display menu
static void DisplayMenu()
{
    Console.WriteLine("========= Welcome to I.C.Treats! =========\nChoose your option:" +
        "\n1. List all customers\n2. List all current orders\n3. Register a new customer" +
        "\n4. Create a customer's order\n5. Display order details of a customer" +
        "\n6. Modify order details\n7. Process an order and checkout" +
        "\n8. Display monthly charged amounts breakdown & total charged amounts for the year" +
        "\n0. Exit");
    Console.Write("Enter your option: ");
}
    // Run program
    while (true)
{
    DisplayMenu();
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
        OptionFive(customerDictionary,orderDetailsList,orderList);
    }
    else if (option == "6")
    {
        OptionSix(customerDictionary,orderDetailsList,orderList);
    }
    else if (option == "7")
    {
        OptionSeven();
    }
    else if (option == "8")
    {
        //OptionEight();
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
static void OptionTwo(Dictionary<int, Customer> customerDictionary)
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
    Console.WriteLine("\nNew Customer\n\r" +
                      "------------");
    Console.Write("Enter your name: ");
    string name = Console.ReadLine();

    Console.Write("Enter 6-digit member ID: ");
    int memberId = Convert.ToInt32(Console.ReadLine());

    Console.Write("Enter your date of birth (dd/mm/yyyy): ");
    DateTime dob = Convert.ToDateTime(Console.ReadLine());

    string status = "Ordinary";

    Customer newCustomer = new Customer(name, memberId, dob);

    PointCard newPointCard = new PointCard(0, 0, status);
    newCustomer.Rewards = newPointCard;
    customerDictionary.Add(memberId, newCustomer);

    using (StreamWriter sw = new StreamWriter("Customers.csv", true))
    {
        sw.WriteLine($"\n{newCustomer.Name},{newCustomer.Memberid},{newCustomer.Dob.ToString("dd/MM/yyyy")},{newCustomer.Rewards.Tier},{newCustomer.Rewards.Points},{newCustomer.Rewards.PunchCard}");

    }
    Console.WriteLine("\nCustomer is officially a member of I.C.Treats! Welcome:D\n");

}

// Feature 4
void OptionFour()
{
    Console.WriteLine($"{"Name",-8} {"MemberID",-12} {"DOB",-14} {"Status",-14} {"Points",-10} {"Punch Card",-10}");
    Console.WriteLine($"{"----",-8} {"--------",-12} {"---",-14} {"------",-14} {"------",-10} {"----------",-10}");

    foreach (Customer existingCustomer in customerDictionary.Values)
    {
        Console.WriteLine($"{existingCustomer.Name,-8} {existingCustomer.Memberid,-12} {existingCustomer.Dob.ToString("dd/MM/yyyy"),-14} {existingCustomer.Rewards.Tier,-14} {existingCustomer.Rewards.Points,-10} {existingCustomer.Rewards.PunchCard,-10}");
    }

    Console.WriteLine("Enter customer's member ID to select: ");
    int memberid = Convert.ToInt32(Console.ReadLine());

    if (customerDictionary.ContainsKey(memberid))
    {
        Customer customer = customerDictionary[memberid];

        Console.WriteLine($"{customer.Name}'s Order");
        Console.WriteLine("------------------------------\n");

        Console.WriteLine("Option (Cup/ Cone/ Waffle): ");
        string option = Console.ReadLine().ToLower();

        Console.WriteLine("Scoop(s): ");
        int scoop = Convert.ToInt32(Console.ReadLine().ToLower());

        Console.WriteLine("Flavour(s): ");
        string type = Console.ReadLine().ToLower();
        bool premium = IsPremiumFlavour(type);

        List<Flavour> flavoursList = new List<Flavour>();
        Flavour flavour = new Flavour(type, premium, scoop);
        flavoursList.Add(flavour);

        Console.WriteLine("Topping(s): ");
        string toppings = Console.ReadLine().ToLower();

        List<Topping> toppingList = new List<Topping>();
        Topping toppingsObj = new Topping(toppings);
        toppingList.Add(toppingsObj);

        Order orderNew = customer.MakeOrder();

        IceCream newOrder;

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

        orderNew.AddIceCream(newOrder);
        
        customer.CurrentOrder = orderNew;

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
    }
    else
    {
        Console.WriteLine("Customer is not a member at I.C.Treats. Please ensure the correct Member ID was selected or register this customer by choosing option 3.");
    }
}

bool IsPremiumFlavour(string flavour)
{
    List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
    return premiumFlavours.Contains(flavour);
}

bool AskForChocolateDippedCone()
{
    Console.WriteLine("Do you want a chocolate-dipped cone? (Y/N): ");
    return Console.ReadLine().Trim().ToUpper() == "Y";
}

string AskForWaffleType()
{
    Console.WriteLine("Select Waffle Type (red velvet, charcoal, or pandan): ");
    string wafflesType = Console.ReadLine().ToLower();
    if(wafflesType)
    return Console.ReadLine();
}








// Feature 5
static void OptionFive(Dictionary<int, Customer> customerDictionary, List<string[]> orderDetailsList, List<Order> orderList)
{
    DisplayCustomerDictionary(customerDictionary);

    Console.WriteLine("Enter the Member ID of the customer you want to view orders for:");
    string inputId = Console.ReadLine();
    int selectedMemberId;

    if (int.TryParse(inputId, out selectedMemberId) && customerDictionary.ContainsKey(selectedMemberId))
    {
        Customer selectedCustomer = customerDictionary[selectedMemberId];

        Console.WriteLine($"\nOrders for {selectedCustomer.Name} (MemberID: {selectedCustomer.Memberid}):\n");

        foreach (string[] orderDetails in orderDetailsList)
        {
            int orderMemberId = Convert.ToInt32(orderDetails[1]);
            if (orderMemberId == selectedMemberId)
            {
                Console.WriteLine($"Order ID: {orderDetails[1]}, Time Received: {orderDetails[2]}");
                DisplayOrderDetails(orderDetails);
                Console.WriteLine("\n");
            }
        }
    }
    else
    {
        Console.WriteLine("Customer not found or invalid input. Please enter a valid MemberID.");
    }
}

static void DisplayOrderDetails(string[] orderDetails)
{
    Console.WriteLine($"ID: {orderDetails[0]}\nMember ID: {orderDetails[1]}\nTime Received: {orderDetails[2]}\nTime Fulfilled: {orderDetails[3]}" +
        $"\nOption: {orderDetails[4]}\nScoops: {orderDetails[5]}");

    if (orderDetails[4] == "Cone")
    {
        Console.WriteLine("Dipped: {0}", orderDetails[6]);
    }
    if (orderDetails[4] == "Waffle")
    {
        Console.WriteLine("Waffle Flavour: {0}", orderDetails[7]);
    }
    if (orderDetails[4] == "Cup" || orderDetails[4] == "Cone" || orderDetails[4] == "Waffle")
    {
        string flavours = string.Join(", ", GetNonNullOrWhiteSpaceValues(orderDetails, 8, 10));
        Console.WriteLine("Flavours: {0}", flavours);

        string toppings = string.Join(", ", GetNonNullOrWhiteSpaceValues(orderDetails, 11, 13));
        Console.WriteLine("Toppings: {0}", toppings);
    }
}
static void DisplayCustomerDictionary(Dictionary<int, Customer> customers)
{
    Console.WriteLine("Customer List:");
    foreach (var entry in customers)
    {
        Customer customer = entry.Value;

        // Since I used "Celeste" as my test run experiments, I will remove them from the dictionary
        // This is because "Celeste" is (unfortunately) now part of the dict permanantly
        // If this code is removed "Celeste" values will re-appear
        if (customer.Name != "Celeste")
        {
            Console.WriteLine($"MemberID: {customer.Memberid}, Name: {customer.Name}");
        }
    }
    Console.WriteLine();
}
static IEnumerable<string> GetNonNullOrWhiteSpaceValues(string[] array, int startIndex, int endIndex)
{
    for (int i = startIndex; i <= endIndex; i++)
    {
        if (!string.IsNullOrWhiteSpace(array[i]))
        {
            yield return array[i];
        }
    }
}
// Feature 6
static void OptionSix(Dictionary<int, Customer> customerDictionary, List<string[]> orderDetailsList, List<Order> orderList)
{
    /* - list customers
     * - user chooses a customer > get that customers order
     * - list all iceCream objects contained in the order
     * - choose: [1] modify iceCream, [2] add new iceCream, [3] delete iceCream
     * [1]: choose which iceCream to modify > prompt for all the information > update accordingly
     * [2]: prompt for all the information > update accordingly (add to order)
     * [3]: choose which iceCream to remove > remove iceCream from order 
     *      (if iceCream count == 1: "You cannot have 0 iceCreams in an order."
     * - display the new updated order      */

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