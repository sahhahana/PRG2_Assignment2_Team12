//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using Microsoft.VisualBasic.FileIO;
using PRG2_Assignment2_Team12;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;


//chloe - features 2,5 and 6
//sahana- features 1, 3 ad 4


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

// Create a list to store orders for feature 8
List<object[]> timeFulfilledList = new List<object[]>();

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
        Order order = new Order(orderId, timeReceived, foundCustomer);
        customer?.OrderHistory.Add(order);

       

        // Simplify the creation of Flavour and Topping instances
        List<Flavour> flavoursList = CreateFlavoursList(data, 8, 10);
        List<Topping> toppingsList = CreateToppingsList(data, 11, 14);

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


    for (int i = 11; i <= 14; i++)
    {
        string toppingInfo = data[i].Trim(); // Trim to remove leading and trailing spaces

        if (!string.IsNullOrWhiteSpace(toppingInfo))
        {
            Topping topping = new Topping(toppingInfo);
            toppingsList.Add(topping);
        }
        else
        {
            // Handle the case where the topping is just whitespace (or empty)
            // You can skip adding it to the list or take other appropriate actions.
        }
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
        OptionSeven(regularOrderQueue, goldOrderQueue);
    }
    else if (option == "8")
    {
        OptionEight(customerDictionary, timeFulfilledList);
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
            if (dob > DateTime.Today)
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
    if (customerDictionary.ContainsKey(memberid))
    {
        customer = customerDictionary[memberid];
    }
    else
    {
        Console.WriteLine("Customer is not a member at I.C.Treats.\nPlease ensure the correct Member ID was selected or register this customer by choosing option 3.");
        return;
    }

    Order orderNew = customer.MakeOrder(customer);
    while (anotherIceCream == "Y")
    {
        // prompt user to enter their ice cream order
        Console.WriteLine($"{customer.Name}'s Order");
        Console.WriteLine("------------------------------\n");

        Console.Write("Option (Cup/ Cone/ Waffle): ");
        string option = Console.ReadLine().ToLower();

        Console.Write("Scoop(s): ");
        int scoop = Convert.ToInt32(Console.ReadLine());
        
        List<string> availableFlavours = new List<string> { "vanilla", "durian", "chocolate", "ube", "strawberry", "sea salt" };
        List<Flavour> flavoursList = new List<Flavour>();
        if (scoop < 4)
        {
            for (int i = 1; i < scoop + 1; i++)
            {
                string type;
                bool premium = false;

                while (true)
                {
                    Console.WriteLine("Flavours:\n{0,-13}{1,-13}", "Regular", "Premium");
                    Console.WriteLine("{0,-13}{1,-13}\n{2,-13}{3,-13}\n{4,-13}{5,-13}\n", "Vanilla", "Durian",
                        "Chocolate", "Ube", "Strawberry", "Sea Salt");
                    Console.Write($"Flavour for scoop {i}: ");
                    type = Console.ReadLine().ToLower();

                    if (availableFlavours.Contains(type))
                    {
                        premium = IsPremiumFlavour(type);
                        Flavour flavour = new Flavour(type, premium, scoop);
                        flavoursList.Add(flavour);
                        break; // exit the loop if the input is a valid flavour
                    }
                    else
                    {
                        Console.WriteLine("Invalid flavour. Please choose from the available flavours.");
                    }
                }
            }
        }
        else if (scoop == 1)
        {
            while (true)
            {
                Console.WriteLine("Flavours:\n{0,-13}{1,-13}", "Regular", "Premium");

                Console.WriteLine("{0,-13}{1,-13}\n{2,-13}{3,-13}\n{4,-13}{5,-13}\n", "Vanilla", "Durian",
                "Chocolate", "Ube", "Strawberry", "Sea Salt");
                Console.Write("Flavour: ");
                string type = Console.ReadLine().ToLower();
                bool premium = IsPremiumFlavour(type);

                if (availableFlavours.Contains(type))
                {
                    premium = IsPremiumFlavour(type);
                    Flavour flavour = new Flavour(type, premium, scoop);
                    flavoursList.Add(flavour);
                    break; // exit the loop if the input is a valid flavour
                }
                else
                {
                    Console.WriteLine("Invalid flavour. Please choose from the available flavours.");
                }
            }
        }
        else
        {
            Console.WriteLine("3 scoops maximum.");
            break;
        }
        List<Topping> toppingList = new List<Topping>();
        AskForToppings(toppingList);

            
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
            if (customer.Rewards.Tier == "Gold")
            {
                goldOrderQueue.Enqueue(orderNew);
            }
            else
            {
                regularOrderQueue.Enqueue(orderNew);
            }
            break; // Exit the loop if the user enters "N"
        }
        else
        {
            continue;
        }
        
        
    }
}
    // todo: add points and modify code -- add a orderid couonter, add topping methods, i need to add this order plus info from order in th eorder.csv file, add 1 flavour at a time
    //Id,MemberId,TimeReceived,TimeFulfilled,Option,Scoops,Dipped,WaffleFlavour,Flavour1,Flavour2,Flavour3,Topping1,Topping2,Topping3,Topping4


    bool AskForChocolateDippedCone()
    {
        Console.Write("Do you want a chocolate-dipped cone? (Y/N): ");
        return Console.ReadLine().Trim().ToUpper() == "Y";
    }

    string AskForWaffleType()
    {
        List<string> waffleFlavour = new List<string> { "red velvet", "charcoal", "pandan", "original" };
        while (true)
        {
            Console.Write("Select Waffle Type (original, red velvet, charcoal, or pandan): ");
            string wafflesType = Console.ReadLine().ToLower();

            if (waffleFlavour.Contains(wafflesType))
            {
                return wafflesType;
            }
            else
            {
                Console.Write("Please choose among original, red velvet, charcoal or pandan waffle type.");
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
            Console.WriteLine("Toppings: Sprinkles, Mochi, Sago, Oreos\n");
            Console.Write("Enter topping(s): ");
            string toppings = Console.ReadLine();
            Topping toppingsObj = new Topping(toppings);
            toppingList.Add(toppingsObj);
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
    foreach (var iceCream in order.IceCreamList)
    {
        if (order.TimeFulfilled.HasValue)
        {
            Console.WriteLine($"ID: {order.Id}\nTime Received: {order.TimeReceived}\nTime Fulfilled: {order.TimeFulfilled?.ToString("dd/MM/yyyy HH:mm")}");
            Console.WriteLine(iceCream.ToString());
        }
        else
        {
            Console.WriteLine($"ID: {order.Id}\nTime Received: {order.TimeReceived}\nTime Fulfilled: {order.TimeFulfilled?.ToString("dd/MM/yyyy HH:mm")??"Not Fulfilled"}");
            Console.WriteLine(iceCream.ToString());
        }
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
            string option = ChooseOption();
            if (option == "1" || option == "3")
            {
                Console.Write("Enter the Order ID you want to modify: ");
                int orderIdToModify = Convert.ToInt32(Console.ReadLine());

                Order selectedOrder = selectedCustomer.OrderHistory.FirstOrDefault(order => order.Id == orderIdToModify);

                if (selectedOrder != null)
                {
                    if (option == "1")
                    {
                        ModifyIceCream(selectedCustomer, orderIdToModify);
                        // Display the updated order
                        Console.WriteLine("\nUpdated Order:");
                        DisplayOrderDetails(selectedOrder);
                    }
                    else
                    {
                        DeleteIceCream(selectedCustomer, orderIdToModify);
                        // Display updated order details
                        Console.WriteLine("\nUpdated Orders: ");
                        foreach (Order order in orderHistory)
                        {
                            DisplayOrderDetails(order);
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"Order with ID {orderIdToModify} not found.");
                }

            }
            else if (option == "2")
            {
                AddNewIceCream(selectedCustomer);
                Console.WriteLine("\nUpdated Orders: ");
                foreach (Order order in orderHistory)
                {
                    DisplayOrderDetails(order);
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Customer not found or invalid input. Please enter a valid MemberID.");
            }
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input format. Please enter a valid integer for MemberID.");
    }

    void DeleteIceCream(Customer customer, int orderIdToRemoveIceCream)
    {
        Order selectedOrder = customer.OrderHistory.FirstOrDefault(order => order.Id == orderIdToRemoveIceCream);

        if (selectedOrder != null && selectedOrder.IceCreamList.Count > 0)
        {
            if (selectedOrder.IceCreamList.Count > 0)
            {
                Console.Write("Enter the Ice Cream index you want to delete: ");
                int iceCreamIndexToDelete = Convert.ToInt32(Console.ReadLine()) - 1;

                if (iceCreamIndexToDelete >= 0 && iceCreamIndexToDelete < selectedOrder.IceCreamList.Count)
                {
                    // Delete the specified ice cream from the selected order
                    selectedOrder.DeleteIceCream(iceCreamIndexToDelete);
                    Console.WriteLine("Ice cream removed.");
                }
                else
                {
                    Console.WriteLine("Invalid ice cream index.");
                }
            }
            else
            {
                Console.Write("There is only one ice cream in this order. You cannot remove this order.\n");
            }
        }
        else
        {
            Console.WriteLine($"Order with ID {orderIdToRemoveIceCream} cannot be deleted.\n");
        }
    }



    void AddNewIceCream(Customer customer)
    {
        Customer selectedCustomer = customer;

        // Find the maximum order ID and add 1 to create a new unique order ID
        int orderIdToAddIceCream = customer.OrderHistory.Max(order => order.Id) + 1;

        // Create a new order if it doesn't exist
        Order selectedOrder = customer.OrderHistory.FirstOrDefault(order => order.Id == orderIdToAddIceCream);

        if (selectedOrder == null)
        {
            selectedOrder = new Order(orderIdToAddIceCream, DateTime.Now, selectedCustomer);
            customer.OrderHistory.Add(selectedOrder);
        }

        // Create a new ice cream based on user input
        IceCream newIceCream = GetIceCreamDetails(selectedCustomer);

        // Add the new ice cream to the order
        selectedOrder.AddIceCream(newIceCream);

        Console.WriteLine("\nUpdated Orders:");
        foreach (Order order in customer.OrderHistory)
        {
            DisplayOrderDetails(order);
            Console.WriteLine();
        }
        Console.WriteLine();
    }


    static void ModifyIceCream(Customer customer, int orderIdToModify)
    {
        Order selectedOrder = customer.OrderHistory.FirstOrDefault(order => order.Id == orderIdToModify);

        int iceCreamIndexToModify = 0;
        if (selectedOrder != null && selectedOrder.IceCreamList.Count>1)
        {
            Console.Write("Enter the index of the ice cream you want to modify: ");
            iceCreamIndexToModify = Convert.ToInt32(Console.ReadLine())-1;
        }
        selectedOrder.ModifyIceCream(iceCreamIndexToModify);
    }
        string ChooseOption()
    {
        while (true)
        {
            Console.WriteLine("Menu:\n1. Modify Ice Cream\n2. Add New Ice Cream\n3. Delete Ice Cream\n");

            Console.Write("Enter your option: ");
            string option = Console.ReadLine();
            if (option == "1" || option == "2" || option == "3")
            {
                return option;
                break;
            }
            else
            {
                Console.WriteLine("Please enter a valid option.");
            }
        }
    }

    IceCream GetIceCreamDetails(Customer customer)
    {
        Console.Write("Option (Cup/ Cone/ Waffle): ");
        string option = Console.ReadLine().ToLower();

        Console.Write("Scoop(s): ");
        int scoop = Convert.ToInt32(Console.ReadLine().ToLower());
        int scoopNo = 1;
        List<Flavour> flavoursList = new List<Flavour>();
        List<string> availableFlavours = new List<string> { "vanilla", "durian", "chocolate", "ube", "strawberry", "sea salt" };
        if (scoop > 1)
        {
            for (int i = 1; i < scoop + 1; i++)
            {
                string type;
                bool premium = false;

                while (true)
                {
                    Console.WriteLine("Flavours:\n{0,-13}{1,-13}", "Regular", "Premium");
                    Console.WriteLine("{0,-13}{1,-13}\n{2,-13}{3,-13}\n{4,-13}{5,-13}\n", "Vanilla", "Durian",
                        "Chocolate", "Ube", "Strawberry", "Sea Salt");
                    Console.Write($"Flavour for scoop {i}: ");
                    type = Console.ReadLine().ToLower();

                    if (availableFlavours.Contains(type))
                    {
                        premium = IsPremiumFlavour(type);
                        break; // exit the loop if the input is a valid flavour
                    }
                    else
                    {
                        Console.WriteLine("Invalid flavour. Please choose from the available flavours.");
                    }
                }


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

        Order orderNew = customer.MakeOrder(customer);
        List<IceCream> IceCreamList = orderNew.IceCreamList;
        IceCream newOrder;

        // create ice cream object 
        if (option == "cup")
        {
            newOrder = new Cup(option, scoop, flavoursList, toppingList);
            return newOrder;
        }
        else if (option == "cone")
        {
            bool dipped = AskForChocolateDippedCone();
            newOrder = new Cone(option, scoop, flavoursList, toppingList, dipped);
            return newOrder;
        }
        else if (option == "waffle")
        {
            string waffleType = AskForWaffleType();
            newOrder = new Waffle(option, scoop, flavoursList, toppingList, waffleType);
            return newOrder;
        }
        else
        {
            Console.WriteLine("Invalid option! Please enter Cup, Cone, or Waffle.");
            return null;
        }
    }

    bool AskForChocolateDippedCone()
    {
        while (true)
        {
            Console.Write("Do you want a chocolate-dipped cone? (Y/N): ");
            string userInput = Console.ReadLine().Trim().ToUpper();

            if (userInput == "Y")
            {
                return true;
            }
            else if (userInput == "N")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N'.");
            }
        }
    }

    string AskForWaffleType()
    {
        List<string> waffleFlavour = new List<string> { "oroginal","red velvet", "charcoal", "pandan" };
        while (true)
        {
            Console.Write("Select Waffle Type (original, red velvet, charcoal, or pandan): ");
            string wafflesType = Console.ReadLine().ToLower();

            if (waffleFlavour.Contains(wafflesType))
            {
                return wafflesType;
                break;
            }
            else
            {
                Console.WriteLine("Please enter your waffle type.");
                return "";
            }
        }
    }
    void AskForToppings(List<Topping> toppingList)
    {
        Console.WriteLine("Do you want topping(s)? (Y/N): ");
        string yesOrNo = Console.ReadLine().ToUpper();


        if (yesOrNo == "N")
        {
            return;

        }
        else
        {
            while (true)
            {
                Console.WriteLine("Toppings: Sprinkles, Mochi, Sago, Oreos\n");
                Console.Write("Enter topping(s): ");
                string toppings = Console.ReadLine();
                if (toppings.ToUpper() == "SPRINKLES" || toppings.ToUpper() == "MOCHI" || toppings.ToUpper() == "SAGO" || toppings.ToUpper() == "OREOS")
                {
                    Topping toppingsObj = new Topping(toppings);
                    toppingList.Add(toppingsObj);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid topping. Re-enter the topping.");
                }
            }
        }
    }
    bool IsPremiumFlavour(string flavour)
    {
        List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
        return premiumFlavours.Contains(flavour);
    }
}



// Advanced (a)- Feature 7
void OptionSeven(Queue<Order> RegularOrderQueue, Queue<Order> GoldOrderQueue)
{
    if (RegularOrderQueue.Count == 0 && GoldOrderQueue.Count == 0)
    {
        Console.WriteLine("No pending orders to process.");
        return;
    }

    Queue<Order> currentQueue;
    Order currentOrder;

    if (GoldOrderQueue.Count > 0)
    {
        currentQueue = GoldOrderQueue;
    }
    else
    {
        currentQueue = RegularOrderQueue;
    }

    // Display all ice creams in the order
    foreach (Order order in currentQueue)
    {
        foreach (IceCream iceCream in order.IceCreamList)
        {
            Console.WriteLine(iceCream.ToString());
        }
    }

    // Dequeue the first order
    currentOrder = currentQueue.Dequeue();


    DateTime timeFulfilled = DateTime.Now;
    DateTime timeReceived = currentOrder.TimeReceived;

    int maxToppings = 4;
    int maxFlavours = 3;


    string commonInformation = $"{currentOrder.Id},{currentOrder.AssociatedCustomer.Memberid},{timeReceived.ToString("dd/MM/yyyy HH:mm")},{timeFulfilled.ToString("dd/MM/yyyy HH:mm")},";

    foreach (IceCream iceCream in currentOrder.IceCreamList)
    {
        string type = iceCream.Option;

        string information = $"{commonInformation}";

        // Add common information for each ice cream
        information += $"{type},{iceCream.Scoops},";

        // For Cone option, check if it's a dipped cone
        if (type == "Cone")
        {
            Cone cone = (Cone)iceCream; // Assuming IceCream has a property named "Dipped"
            information += $"{(cone.Dipped ? "True" : "False")},,";
        }
        else if (type == "Cup")
        {
            information += ",,,";
        }
        // For Waffle option, add waffle flavour
        else if (type == "Waffle")
        {
            Waffle waffle = (Waffle)iceCream; // Assuming IceCream has a property named "WaffleFlavour"
            information += $",{waffle.WaffleFlavour},";
        }
        else
        {
            information += ",,,";
        }

        // Add flavours
        for (int i = 0; i < maxFlavours; i++)
        {
            if (i < iceCream.Flavours.Count)
            {
                information += $"{iceCream.Flavours[i].Type},";
            }
            else
            {
                information += ",";

            }
        }

        // Add toppings
        for (int i = 0; i < maxToppings; i++)
        {
            if (i < iceCream.Toppings.Count)
            {
                information += $"{iceCream.Toppings[i].Type},";
            }
            else
            {
                information += ",";
            }
        }
        using (StreamWriter sw = new StreamWriter("orders.csv", true))
        {
            sw.WriteLine(information);
        }

    }

    ProcessOrderQueue(currentOrder);
}

void ProcessOrderQueue(Order currentOrder)
{
    
    

    // Display the total bill amount
    double totalBill = currentOrder.CalculateTotal();

    // Check if it's the customer's birthday
    if (currentOrder.AssociatedCustomer.IsBirthday())
    {
        // Calculate the final bill with the most expensive ice cream costing $0.00
        totalBill = 0;
    }

    // Check if the customer has completed their punch card
    if (currentOrder.AssociatedCustomer.Rewards.PunchCard == 10)
    {
        // Calculate the final bill with the first ice cream costing $0.00
        totalBill = 0;

        // Reset punch card back to 0
        currentOrder.AssociatedCustomer.Rewards.PunchCard = 0;
    }

    // Check Pointcard status to determine if the customer can redeem points
    if (currentOrder.AssociatedCustomer.Rewards.Points > 0)
    {
        Console.WriteLine($"{currentOrder.AssociatedCustomer.Name}'s Membership Points: {currentOrder.AssociatedCustomer.Rewards.Points}\n");

        Console.Write("How many points would you like to use to offset the final bill? ");
        int pointsToOffset = Convert.ToInt32(Console.ReadLine());

        // Redeem points
        currentOrder.AssociatedCustomer.Rewards.RedeemPoints(pointsToOffset);
        totalBill -= (pointsToOffset * 0.02);
    }

    // Display the final total bill amount
    Console.WriteLine($"Final Total Bill Amount: {totalBill:C}");

    // Prompt user to press any key to make payment
    Console.WriteLine("Press any key to make payment...");
    Console.ReadKey();

    // Increment the punch card for every ice cream in the order (if it goes above 10, set it back down to 10)
    foreach (var iceCream in currentOrder.IceCreamList)
    {
        currentOrder.AssociatedCustomer.Rewards.Punch();
    }

    // Earn points
    currentOrder.AssociatedCustomer.Rewards.AddPoints(Convert.ToInt32(totalBill));

    // Upgrade member status accordingly
    currentOrder.AssociatedCustomer.Rewards.UpdateTier();

    // Mark the order as fulfilled with the current datetime
    currentOrder.TimeFulfilled = DateTime.Now;

    // Add this fulfilled order object to the customer’s order history
    currentOrder.AssociatedCustomer.OrderHistory.Add(currentOrder);
}




// Advanced (b)- Feature 8
void OptionEight(Dictionary<int, Customer> customerDictionary, List<object[]> timeFulfilledList)
{
    /*
     *  prompt the user for the year
 retrieve all order objects that were successfully fulfilled within the inputted year
 compute and display the monthly charged amounts breakdown & the total charged
amounts for the input year
    */

    int year;
    while (true)
    {
        try
        {
            Console.Write("Enter the year: ");
            year = Convert.ToInt32(Console.ReadLine());
            if (year.ToString().Length != 4)
            {
                Console.WriteLine("Invalid year. Year must have 4 digits.");
            }
            else if (year < 2023 || year > 2024)
            {
                Console.WriteLine("Invalid year. Please enter a year between 2023 and 2024.");
            }
            else
            {
                // Valid year input
                break; // Exit the loop if the year is valid
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Year must be an integer.");
        }
    }

    Dictionary<string, double> monthlyPrices = new Dictionary<string, double>();
    double yearlyTotal = 0;

    for (int i = 1; i <= 12; i++)
    {
        double monthlyTotal = 0;

        foreach (Customer customer in customerDictionary.Values)
        {
            foreach (Order order in customer.OrderHistory)
            {
                DateTime timeFulfilled = (DateTime)order.TimeFulfilled;
                if (timeFulfilled.Year == year && timeFulfilled.Month == i)
                {
                    double monthlyPrice = order.CalculateTotal();
                    Console.WriteLine(order.CalculateTotal());
                    yearlyTotal += monthlyPrice;
                    monthlyTotal += monthlyPrice;
                }
            }
        }

        // Update monthly total in the dictionary
        monthlyPrices[$"{GetMonthName(i).Substring(0, 3)} {year}"] = monthlyTotal;
    }

    // Print monthly totals
    foreach (var entry in monthlyPrices)
    {
        Console.WriteLine($"{entry.Key}:   ${entry.Value:F2}");
    }

    Console.WriteLine("\nTotal: ${0:F2}", yearlyTotal);

    string GetMonthName(int monthIndex)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthIndex);
    }

}

// Advanced (c)- GUI
// Will be done most likely on a separate document