﻿//==========================================================
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
// COMMON METHODS
//=================
// method to add order to queue
void addingOrderToQueue(Order orderNew, string membershipStatus)
{
    if (membershipStatus == "Gold")
    {
        goldOrderQueue.Enqueue(orderNew);
    }
    else
    {
        regularOrderQueue.Enqueue(orderNew);
    }
}


// method to check ice cream option
string checkIceCreamOption()
{
    string option = null;
    while (true)
    {
        Console.Write("Option (Cup/ Cone/ Waffle): ");
        option = Console.ReadLine().ToLower();

        if (option == "cup" || option == "cone" || option == "waffle")
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid option! Please enter Cup, Cone, or Waffle.");
        }
    }
    return option;

}

// displays flavour menu
void flavourMenu()
{
    Console.WriteLine("\nFlavour Menu\n{0,-13}{1,-13}", "Regular", "Premium");
    Console.WriteLine("{0,-13}{1,-13}", "----------", "---------");
    Console.WriteLine("{0,-13}{1,-13}\n{2,-13}{3,-13}\n{4,-13}{5,-13}\n", "Vanilla", "Durian",
        "Chocolate", "Ube", "Strawberry", "Sea Salt");
}
Flavour checkFlavour(int scoops)
{
    List<string> availableFlavours = new List<string> { "vanilla", "durian", "chocolate", "ube", "strawberry", "sea salt" };
    string type;
    bool premium;

    while (true)
    {
        flavourMenu();
        Console.Write("Flavour: ");
        type = Console.ReadLine().ToLower();
        premium = IsPremiumFlavour(type);

        if (availableFlavours.Contains(type))
        {
            Flavour flavourObj = new Flavour(type, premium, scoops);
            return flavourObj; // Exit the loop if the input is a valid flavour
        }
        else
        {
            Console.WriteLine("Invalid flavour. Please choose from the available flavours.");
        }
    }
}

// Used when reading orders.csv file
// Helper method for dipped cone
bool AskForChocolateDippedCone()
{
    Console.Write("Do you want a chocolate-dipped cone? (Y/N): ");
    return Console.ReadLine().Trim().ToUpper() == "Y";
}

// Helper method for waffle type
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
            Console.Write("Invalid waffle flavour. Please choose among original, red velvet, charcoal or pandan waffle type.");
        }
    }
}

// Helper method for toppings
void AskForToppings(List<Topping> toppingList)
{
    while (true)
    {
        Console.Write("Do you want topping(s)? (Y/N): ");
        string yesOrNo = Console.ReadLine().ToUpper();

        if (yesOrNo == "N")
        {
            break;
        }
        else if (yesOrNo == "Y")
        {
            Console.WriteLine("\n{0}\n---------\n{1}\n{2}\n{3}\n{4}\n", "Toppings", "Sprinkles", "Mochi", "Sago", "Oreo");
            Console.Write("Enter topping(s) separated by a ',': ");
            string toppings = Console.ReadLine();
            Topping toppingsObj = new Topping(toppings);
            toppingList.Add(toppingsObj);
            return;
        }
        else
        {
            // input validation to check if they want toppings
            Console.WriteLine("Invalid Input. Please select Y/N.");
        }
    }

}
// methods to check if flavour is premium when reading orders.csv file
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
        order.TimeFulfilled = timeFulfilled;


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
    }
    return toppingsList;
}

// Display menu
void DisplayOptions()
{
    Console.WriteLine("\n========= Welcome to I.C.Treats! =========\n" +
        "\n1. List all customers\n" +
        "2. List all current orders\n" +
        "3. Register a new customer\n" +
        "4. Create a customer's order\n" +
        "5. Display order details of a customer\n" +
        "6. Modify order details\n" +
        "7. Process an order and checkout\n" +
        "8. Display monthly charged amounts breakdown & total charged amounts for the year\n" +
        "9. Create a customer's customized order\n" +
        "0. Exit\n");
    Console.Write("Enter your option: ");
}

// Run program
while (true)
{
    DisplayOptions();
    string option = Console.ReadLine();
    Console.WriteLine(" ");

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
    else if (option == "9")
    {
        //OptionNine(customerDictionary);
    }
    else if (option == "0")
    {
        Console.WriteLine("Enjoy your ice cream!");
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
    Console.WriteLine($"{"Name",-8} {"MemberID",-12} {"DOB",-14} {"Status",-14} {"Points",-10} {"Punch Card",-10}");
    Console.WriteLine($"{"----",-8} {"--------",-12} {"---",-14} {"------",-14} {"------",-10} {"----------",-10}");

    //List<int> membersToRemove = new List<int>();

    foreach (var entry in customerDictionary)
    {
        Customer customer = entry.Value;
       
        Console.WriteLine($"{customer.Name,-8} {customer.Memberid,-12} {customer.Dob.ToString("dd/MM/yyyy"),-14} {customer.Rewards.Tier,-14} {customer.Rewards.Points,-10} {customer.Rewards.PunchCard,-10}");
        
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
    // Constants for memberID
    const int minMemberId = 100000;
    const int maxMemberId = 999999;

    while (true)
    {
        try
        {
            Console.WriteLine("\nNew Customer\n\r" +
                              "------------");
            string name;
            while (true)
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine();

                // input validation to check a name is entered and is in correct format
                if (!string.IsNullOrWhiteSpace(name) && name.All(c => char.IsLetter(c) || c == ' '))
                {
                    break;
                }

                Console.WriteLine("Invalid name format. Please input only alphabets with a single space between first and last name.");
            }

            Random random = new Random();

            // Generate a random six-digit ID
            int memberId;
            do
            {
                // Generate a random member ID until it is unique
                memberId = random.Next(minMemberId, maxMemberId + 1);
            } while (customerDictionary.ContainsKey(memberId));

            Console.WriteLine($"Your 6-digit member ID is {memberId} ");

            Console.Write("Enter your date of birth (dd/mm/yyyy): ");
            string dobInput = Console.ReadLine();
            DateTime dob;

            // Try to parse the date of birth, and validate it
            if (!DateTime.TryParseExact(dobInput, "dd/MM/yyyy", null, DateTimeStyles.None, out dob) || (dob > DateTime.Today && dob.Year > 1900))
            {
                // Throw a FormatException for invalid date
                throw new FormatException("Invalid date of birth. Please enter a valid date.");
            }

            string status = "Ordinary";

            // Create a new customer object
            Customer newCustomer = new Customer(name, memberId, dob);

            // Create a new PointCard for the customer
            PointCard newPointCard = new PointCard(0, 0, status);
            newCustomer.Rewards = newPointCard;

            // Add the new customer to the dictionary
            customerDictionary.Add(memberId, newCustomer);

            using (var sw = new StreamWriter("Customers.csv", true))
            {
                // Write customer details to the CSV file
                // Ensure consistent date formatting in the CSV file
                sw.WriteLine($"{newCustomer.Name},{newCustomer.Memberid},{newCustomer.Dob.ToString("dd/MM/yyyy")},{newCustomer.Rewards.Tier},{newCustomer.Rewards.Points},{newCustomer.Rewards.PunchCard}");
            }

            Console.WriteLine($"\n{newCustomer.Name} is officially a member of I.C.Treats! Welcome:D\n");
            break;  // Exit the loop after successfully creating a new customer
        }
        catch (FormatException ex)
        {
            // Handle FormatException for invalid input
            Console.WriteLine($"Invalid input: {ex.Message}");
            // Optionally log the exception details
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
            // Optionally log the exception details
        }
    }


}


// Feature 4
void OptionFour()
{
    try
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
        Console.Write("\nEnter customer's member ID to select: ");
        int memberid = Convert.ToInt32(Console.ReadLine());
        // check if customer is a member
        if (customerDictionary.ContainsKey(memberid))
        {
            customer = customerDictionary[memberid];
        }
        else
        {
            Console.WriteLine("Customer is not a member at I.C.Treats.\nPlease ensure the correct Member ID was selected or register this customer by choosing option 3.");
            return;
        }

        //creating order object to make an order
        Order orderNew = customer.MakeOrder(customer);

        // assigning order as currentOrder
        orderNew = customer.CurrentOrder;

        while (anotherIceCream == "Y")
        {
            // prompt user to enter their ice cream order
            Console.WriteLine($"{customer.Name}'s Order");
            Console.WriteLine("------------------------------");

            string option = checkIceCreamOption();


            int scoop = 0;

            List<Flavour> flavoursList = new List<Flavour>();

            while (true)
            {
                Console.Write("Scoop(s): ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out scoop))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for scoops.");
                    continue;
                }

                if (scoop < 1 || scoop > 3)
                {
                    Console.WriteLine("Invalid number of scoops. Please enter a value between 1 and 3.");
                    continue;
                }

                for (int i = 0; i < scoop; i++)
                {
                    flavoursList.Add(checkFlavour(scoop));
                }
                break;
            }
            List<Topping> toppingList = new List<Topping>();
            AskForToppings(toppingList);

            List<IceCream> IceCreamList = orderNew.IceCreamList;
            IceCream newIceCream = orderNew.CreateIceCream(option, scoop, flavoursList, toppingList);

            // ensures ice cream order was placed
            if (newIceCream != null)
            {
                orderNew.AddIceCream(newIceCream);

            }
            else
            {
                Console.WriteLine("Order cancelled due to invalid option selected.");
                return;
            }

            Console.Write("Would you like to add another ice cream to the order? (Y/N): ");
            anotherIceCream = Console.ReadLine().ToUpper();

            if (anotherIceCream == "N")
            {
                string membershipStatus = customer.Rewards.Tier;
                addingOrderToQueue(orderNew, membershipStatus);
                break; // Exit the loop if the user enters "N"
            }
            else
            {
                continue;
            }
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

}

// Feature 5
static void OptionFive(Dictionary<int, Customer> customerDictionary)
{
    // Display all customers
    DisplayCustomerDictionary(customerDictionary);
    // User input for the member ID of customer to view orders for
    Console.Write("Enter the Member ID of the customer you want to view orders for:");
    string inputId = Console.ReadLine();
    int selectedMemberId;

    try
    {
        selectedMemberId = Convert.ToInt32(inputId);
        if (customerDictionary.ContainsKey(selectedMemberId))
        {
            // Retrieve the selected customer from the customer dictionary
            Customer selectedCustomer = customerDictionary[selectedMemberId];
            Console.WriteLine($"\nOrders for {selectedCustomer.Name} (MemberID: {selectedCustomer.Memberid}):\n");
            // Retrieve the order history of the selected customer
            List<Order> orderHistory = selectedCustomer.OrderHistory;
            // Display all orders (past and present) from the customer
            foreach (Order order in orderHistory)
            {
                DisplayOrderDetails(order);
                Console.WriteLine();
            }
        }
        else
        {
            // Check if the customer exists in the customer dictionary
            Console.WriteLine("Customer not found or invalid input. Please enter a valid MemberID.");
        }
    }
    catch (FormatException)
    {
        // Catch other error types and classify them as invalid input format
        Console.WriteLine("Invalid input format. Please enter a valid integer for MemberID.");
    }
}

// Helper method to display order details for each order in the selected customer's order history
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
            Console.WriteLine($"ID: {order.Id}\nTime Received: {order.TimeReceived}\nTime Fulfilled: {order.TimeFulfilled?.ToString("dd/MM/yyyy HH:mm") ?? "Not Fulfilled"}");
            Console.WriteLine(iceCream.ToString());
        }
    }
}

// Helper method to display all customers from the customer dictionary
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
    // Display all customers
    DisplayCustomerDictionary(customerDictionary);
    // User input for the member ID of customer to view orders for
    Console.Write("Enter the Member ID of the customer you want to view orders for:");
    string inputId = Console.ReadLine();
    int selectedMemberId;

    try
    {
        selectedMemberId = Convert.ToInt32(inputId);
        // Check if the customer dictionary contains the selected customer/member
        if (customerDictionary.ContainsKey(selectedMemberId))
        {
            // Retrieve the selected customer from the customer dictionary
            Customer selectedCustomer = customerDictionary[selectedMemberId];
            Console.WriteLine($"\nOrders for {selectedCustomer.Name} (MemberID: {selectedCustomer.Memberid}):\n");
            // Retrieve the order history of the selected customer
            List<Order> orderHistory = selectedCustomer.OrderHistory;
            // Display all orders (past and present) from the customer
            foreach (Order order in orderHistory)
            {
                DisplayOrderDetails(order);
                Console.WriteLine();
            }
            // Get user to choose from options 1 to 3 
            string option = ChooseOption();
            if (option == "1" || option == "3")
            {
                // Retrieve the order ID to be modified
                Console.Write("Enter the Order ID you want to modify: ");
                int orderIdToModify = Convert.ToInt32(Console.ReadLine());

                // Retrieve the selected order from the customer's order history
                Order selectedOrder = selectedCustomer.OrderHistory.FirstOrDefault(order => order.Id == orderIdToModify);

                if (selectedOrder != null)
                {
                    if (option == "1")
                    {
                        // Modify ice cream
                        ModifyIceCream(selectedCustomer, orderIdToModify);
                        // Display updated order details for the modified order only
                        Console.WriteLine("\nUpdated Order:");
                        DisplayOrderDetails(selectedOrder);
                    }
                    else
                    {
                        // Delete ice cream
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
                // Add new ice cream
                AddNewIceCream(selectedCustomer);
                // Display updated order details
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
                // Check if the customer exists or if there is an input error
                Console.WriteLine("Customer not found or invalid input. Please enter a valid MemberID.");
            }
        }
    }
    catch (FormatException)
    {
        // Check for input format error
        Console.WriteLine("Invalid input format. Please enter a valid integer for MemberID.");
    }

    // Method to delete ice cream
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
                    // Check for valid ice cream index
                    Console.WriteLine("Invalid ice cream index.");
                }
            }
            else
            {
                // Check whether the ice cream count > 1
                Console.Write("There is only one ice cream in this order. You cannot remove this order.\n");
            }
        }
        else
        {
            //  Check for other errors that result in ice cream being unable to be deleted
            Console.WriteLine($"Order with ID {orderIdToRemoveIceCream} cannot be deleted.\n");
        }
    }


    // Method to add new ice cream
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

        // Display updated orders
        Console.WriteLine("\nUpdated Orders:");
        foreach (Order order in customer.OrderHistory)
        {
            DisplayOrderDetails(order);
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    // Method to modify ice cream
    static void ModifyIceCream(Customer customer, int orderIdToModify)
    {
        // Identify the selected order to be modified by the customer
        Order selectedOrder = customer.OrderHistory.FirstOrDefault(order => order.Id == orderIdToModify);

        // Set the default ice cream index to 0
        int iceCreamIndexToModify = 0;
        if (selectedOrder != null && selectedOrder.IceCreamList.Count > 1)
        {
            // Get input for ice cream index
            Console.Write("Enter the index of the ice cream you want to modify: ");
            iceCreamIndexToModify = Convert.ToInt32(Console.ReadLine()) - 1;
        }
        selectedOrder.ModifyIceCream(iceCreamIndexToModify);
    }

    // Method to choose the desired option and return the option number to call for the option methods
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

    // Method to retrieve ice cream details when adding new ice cream
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

    // Helper method for dipped cone
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

    // Helper method for waffle type
    string AskForWaffleType()
    {
        List<string> waffleFlavour = new List<string> { "original", "red velvet", "charcoal", "pandan" };
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

    // Helper method for toppings
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

    // Method to indicate if the flavour is a premium flavour
    bool IsPremiumFlavour(string flavour)
    {
        List<string> premiumFlavours = new List<string> { "durian", "ube", "sea salt" };
        return premiumFlavours.Contains(flavour);
    }
}



// Advanced (a)- Feature 7
void OptionSeven(Queue<Order> RegularOrderQueue, Queue<Order> GoldOrderQueue)
{
    try
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
                Console.WriteLine($"{iceCream.ToString()}");
                Console.WriteLine($"Total: ${iceCream.CalculatePrice():0.00}\n");
            }
        }

        // Dequeue the first order
        currentOrder = currentQueue.Dequeue();

        ProcessOrderQueue(currentOrder);

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
            // write order into orders.csv
            using (StreamWriter sw = new StreamWriter("orders.csv", true))
            {
                sw.WriteLine(information);
            }

        }
        // Method to process order queue
        void ProcessOrderQueue(Order currentOrder)
        {
            string name = currentOrder.AssociatedCustomer.Name;
            // Display the total bill amount
            double totalBill = currentOrder.CalculateTotal();
            Console.WriteLine($"\n{name}'s total is ${totalBill:0.00}");
            Console.WriteLine($"{name}'s Membership Status: {currentOrder.AssociatedCustomer.Rewards.Tier}\n{name}'s Membership Points: {currentOrder.AssociatedCustomer.Rewards.Points}\n");


            List<double> prices = new List<double>();
            double firstOrder_forPunch = 0;
            if (currentOrder.IceCreamList.Count > 1)
            {
                firstOrder_forPunch += currentOrder.IceCreamList[0].CalculatePrice();

                foreach (IceCream ic in currentOrder.IceCreamList)
                {
                    double pricePerIceCream = ic.CalculatePrice();
                    prices.Add(pricePerIceCream);
                }
            }


            // Check if it's the customer's birthday
            if (currentOrder.AssociatedCustomer.IsBirthday())
            {
                bool hasAlreadyComeToday = currentOrder.AssociatedCustomer.OrderHistory.Any(order => order.TimeReceived.Date == DateTime.Now.Date);
                if (hasAlreadyComeToday)
                {
                    Console.WriteLine("The customer has already redeemed their free birthday ice cream.");
                    return;
                }
                else
                {
                    // Calculate the final bill with the most expensive ice cream costing $0.00
                    if (currentOrder.IceCreamList.Count > 1)
                    {
                        totalBill = totalBill - (prices.Max());
                    }
                    else
                    {
                        totalBill = 0;
                    }
                    Console.WriteLine($"FREE Birthday Ice Cream Redeemed!\n{name}'s total is ${totalBill:0.00}");

                }

            }

            // Check if the customer has completed their punch card
            if (currentOrder.AssociatedCustomer.Rewards.PunchCard == 10)
            {
                if (currentOrder.IceCreamList.Count > 1)
                {
                    totalBill -= firstOrder_forPunch;
                }
                else
                {
                    totalBill = 0;
                }


                Console.WriteLine($"Yay! 11th Ice Cream is FREE!\n");
                Console.WriteLine($"{name}'s total is ${totalBill:0.00}");
            }

            // Check Pointcard status to determine if the customer can redeem points
            if (currentOrder.AssociatedCustomer.Rewards.Tier == "Silver" || currentOrder.AssociatedCustomer.Rewards.Tier == "Gold" && totalBill != 0) // Check if the user's tier is silver or gold
            {
                int pointsToOffset = 0;
                while (true)
                {
                    Console.Write("How many points would you like to use to offset the final bill? ");
                    string inputStr = Console.ReadLine();

                    if (int.TryParse(inputStr, out pointsToOffset))
                    {
                        // Redeem points
                        currentOrder.AssociatedCustomer.Rewards.RedeemPoints(pointsToOffset);
                        totalBill -= (pointsToOffset * 0.02);
                        // Input is a valid integer, exit the loop
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number of points.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"{name} is unable redeem points.");
            }


            // Display the final total bill amount
            Console.WriteLine($"\nFinal Total Bill Amount: {totalBill:C}");

            // Prompt user to press any key to make payment
            Console.WriteLine("\nPress any key to make payment...");
            Console.ReadKey();

            // Increment the punch card for every ice cream in the order (if it goes above 10, set it back down to 10)

            currentOrder.AssociatedCustomer.Rewards.PunchCard += currentOrder.IceCreamList.Count;

            currentOrder.AssociatedCustomer.Rewards.Punch();

            // Earn points
            currentOrder.AssociatedCustomer.Rewards.AddPoints(Convert.ToInt32(totalBill));

            // Upgrade member status accordingly
            currentOrder.AssociatedCustomer.Rewards.UpdateTier();

            // Add this fulfilled order object to the customer’s order history
            currentOrder.AssociatedCustomer.OrderHistory.Add(currentOrder);
        }





    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }



}


// Advanced (b)- Feature 8
void OptionEight(Dictionary<int, Customer> customerDictionary, List<object[]> timeFulfilledList)
{
    int year;
    while (true)
    {
        try
        {
            // Get the year to retrieve sales from user input
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
                // indicates a valid year input
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