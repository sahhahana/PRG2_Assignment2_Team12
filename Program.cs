//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using PRG2_Assignment2_Team12;
using System;
using System.Linq;
using System.Numerics;


//chloe - features 2,5 and 6
//sahana- features 1, 3 ad 4

// Read customers.csv separately because it is used throughout the whole program

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
            Customer customerData = new Customer(name, memberId, dob, status);
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
        if (customer.MembershipStatus == "Gold")
        {
            goldList.Add(customer);
        }
        else if (customer.MembershipStatus == "Ordinary")
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

    Customer newCustomer = new Customer(name, memberId, dob, status);

    PointCard newPointCard = new PointCard(0, 0, status);
    newCustomer.Rewards = newPointCard;
    customerDictionary.Add(memberId, newCustomer);

    using (StreamWriter sw = new StreamWriter("Customers.csv", true))
    {
        sw.WriteLine($"\n{newCustomer.Name},{newCustomer.Memberid},{newCustomer.Dob.ToString("dd/MM/yyyy")},{newCustomer.MembershipStatus},{newCustomer.Rewards.Points},{newCustomer.Rewards.PunchCard}");

    }
    Console.WriteLine("\nCustomer is officially a member of I.C.Treats! Welcome:D\n");

}

// Feature 4
static void OptionFour()
{
    Console.WriteLine("Hello World");
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
    DisplayCustomerDictionary(customerDictionary);

    Console.WriteLine("Enter the Member ID of the customer whose order you want to modify:");
    string inputId = Console.ReadLine();
    int selectedMemberId;

    if (int.TryParse(inputId, out selectedMemberId) && customerDictionary.ContainsKey(selectedMemberId))
    {
        Customer selectedCustomer = customerDictionary[selectedMemberId];

        Console.WriteLine($"\nOrders for {selectedCustomer.Name} (MemberID: {selectedCustomer.Memberid}):\n");

        // Display existing orders for the selected customer
        foreach (string[] orderDetails in orderDetailsList)
        {
            if (int.TryParse(orderDetails[1], out int orderMemberId) && orderMemberId == selectedMemberId)
            {
                Console.WriteLine($"Order ID: {orderDetails[0]}, Time Received: {orderDetails[2]}");
                DisplayOrderDetails(orderDetails);
                Console.WriteLine("\n");
            }
        }

        Console.WriteLine("Enter the Order ID you want to modify:");
        string orderIdInput = Console.ReadLine();
        int orderId;

        if (int.TryParse(orderIdInput, out orderId))
        {
            Order selectedOrder = orderList.Find(o => o.Id == orderId);

            if (selectedOrder != null)
            {
                Console.WriteLine("Choose an action:\n[1] Modify an existing ice cream\n[2] Add a new ice cream\n[3] Delete an existing ice cream");
                string actionInput = Console.ReadLine();
                try
                {
                    if (actionInput == "1")
                    {
                        Console.WriteLine("Enter the index of the ice cream you want to modify:");
                        int iceCreamIndex = Convert.ToInt32(Console.ReadLine())-1;
                        selectedOrder.ModifyIceCream(iceCreamIndex);
                    }
                    else if (actionInput == "2")
                    {
                        Console.WriteLine("Enter details for the new ice cream:");
                        IceCream newIceCream = CreateIceCream();
                        selectedOrder.AddIceCream(newIceCream);
                    }
                    else if (actionInput == "3")
                    {
                        Console.WriteLine("Enter the index of the ice cream you want to delete:");
                        int iceCreamIndex;
                        if (int.TryParse(Console.ReadLine(), out iceCreamIndex))
                        {
                            iceCreamIndex--; // Subtract 1 to make it 0-based
                            if (iceCreamIndex >= 0 && iceCreamIndex < selectedOrder.IceCreamList.Count)
                            {
                                IceCream iceCreamToDelete = selectedOrder.IceCreamList[iceCreamIndex];
                                selectedOrder.DeleteIceCream(iceCreamToDelete);
                            }
                            else
                            {
                                Console.WriteLine("Invalid ice cream index.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid numeric index.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid action. Please enter a valid option.");
                    }
                }
                catch(Exception x)
                {
                    Console.WriteLine("Error occured. Please try again.");
                }

            }

            // Display the updated order details
            Console.WriteLine("Updated Order Details:");
            Console.WriteLine($"Order ID: {selectedOrder.Id}, Time Received: {selectedOrder.TimeReceived}");
            foreach (IceCream iceCream in selectedOrder.IceCreamList)
            {
                Console.WriteLine($"Ice Cream: {iceCream.Option}, Scoops: {iceCream.Scoops}");
                // Display additional details as needed
            }
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }
}
static IceCream CreateIceCream()
{
    Console.Write("Enter the option (cup, cone, waffle): ");
    string option = Console.ReadLine();

    Console.Write("Enter the number of scoops: ");
    int scoops = Convert.ToInt32(Console.ReadLine());

    List<Flavour> flavours = new List<Flavour>();
    int flavourQuantity = 0;

    while (flavourQuantity != scoops)
    {
        Console.Write("Enter the type of flavour: ");
        string flavourType = Console.ReadLine();

        Console.Write("Is this a premium flavour? (Yes/No): ");
        bool isPremium = Console.ReadLine().Equals("Yes", StringComparison.OrdinalIgnoreCase);

        Console.Write("Enter the quantity of scoops for this flavour: ");
        int flavourScoops = Convert.ToInt32(Console.ReadLine());

        flavours.Add(new Flavour(flavourType, isPremium, flavourScoops));
        flavourQuantity += flavourScoops;
    }

    List<Topping> toppings = new List<Topping>();
    Console.Write("Do you want any toppings? (Yes/No): ");
    if (Console.ReadLine()=="Yes")
    {
        while (toppings.Count<=4)
        {
            Console.WriteLine("You can add a maximum of 4 toppings.");
            Console.Write("Enter the type of topping (enter 'Quit' to exit): ");
            string toppingType = Console.ReadLine();
            if (toppingType=="Quit")
            {
                break;
            }
            toppings.Add(new Topping(toppingType));
        }
    }

    // Create and return the IceCream object
    IceCream iceCream = null;
    if (option == "Cup")
    {
        iceCream = new Cup(option, scoops, flavours, toppings);
    }
    else if (option == "Cone")
    {
        Console.WriteLine("Do you want your cone dipped? Yes/No: ");
        bool dipped = Convert.ToBoolean(Console.ReadLine());
        iceCream = new Cone(option, scoops, flavours, toppings, dipped);
    }
    else if (option == "Waffle")
    {
        Console.WriteLine("Enter your waffle flavour: ");
        string waffleFlavour = Console.ReadLine();
        iceCream = new Waffle(option, scoops, flavours, toppings, waffleFlavour);
    }
    return iceCream;
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