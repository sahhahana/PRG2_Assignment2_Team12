//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using PRG2_Assignment2_Team12;
using System.Linq;
using System.Numerics;


//chloe - features 2,5 and 6
//sahana- features 1, 3 ad 4

// Read customers.csv separately because it is used throughout the whole program

List<Customer> customerList = new List<Customer>();
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
        // Customer: name, memberid, dob
        Customer customerData = new Customer(data[0], Convert.ToInt32(data[1]), Convert.ToDateTime(data[2]), data[3]);
        customerList.Add(customerData);
    }
}

// Read customers.csv and create a dictionary to store customer data
Dictionary<int, Customer> customerDictionary = new Dictionary<int, Customer>();

// Read orders.csv separately beacuse it is used throughout the whole program
List<Order> orderList = new List<Order>();

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
        OptionTwo(customerList);
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
        OptionFive(customerList,orderList);
    }
    else if (option == "6")
    {
        OptionSix();
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


    using (StreamReader sr = new StreamReader("customers.csv"))
    {
        string s = sr.ReadLine(); // Read heading

        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");
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


    Console.WriteLine($"{"Name",-8} {"MemberID",-12} {"DOB",-14} {"Status",-14} {"Points",-10} {"Punch Card",-10}");
    Console.WriteLine($"{"----",-8} {"--------",-12} {"---",-14} {"------",-14} {"------",-10} {"----------",-10}");

    foreach (Customer customer in customerDictionary.Values)
    {
        Console.WriteLine($"{customer.Name,-8} {customer.Memberid,-12} {customer.Dob.ToString("dd/MM/yyyy"),-14} {customer.Rewards.Tier,-14} {customer.Rewards.Points,-10} {customer.Rewards.PunchCard,-10}");
    }
    Console.WriteLine("");

}

// Feature 2
static void OptionTwo(List<Customer> customerList)
{
    List<Customer> goldList = new List<Customer>();
    List<Customer> nonGoldList = new List<Customer>();
    foreach (Customer customer in customerList)
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
        }
        while ((s = sr.ReadLine()) != null)
        {
            string[] data = s.Split(",");

            int custId = Convert.ToInt32(data[1]);
            // Check if the Customer with the specified MemberId is in the goldList
            Customer customer = goldList.Find(c => c.Memberid == custId);

            if (customer != null)
            {
                Console.WriteLine("{0,-5}  {1,-10}  {2,-20} {3,-20} {4,-10}" +
                  "{5,-10}  {6,-10} {7,-15} {8,-30} {9,-10}", data[0], data[1], data[2], data[3], data[4],
                  data[5], data[6], data[7], FormatFlavour(data[8], data[9], data[10]), FormatToppings(data[11], data[12], data[13]));
            }

            // Check if Customer with the specified MemberID is in the nonGoldList
            Customer customer2 = nonGoldList.Find(c => c.Memberid == custId);
            if (customer2 != null)
            {
                Console.WriteLine("{0,-5}  {1,-10}  {2,-20} {3,-20} {4,-10}" +
                  "{5,-10}  {6,-10} {7,-15} {8,-30} {9,-10}", data[0], data[1], data[2], data[3], data[4],
                  data[5], data[6], data[7], FormatFlavour(data[8], data[9], data[10]), FormatToppings(data[11], data[12], data[13]));
            }

        }
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
    customerList.Add(newCustomer);

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
static void OptionFive(List<Customer> customerList, List<Order> orderList)
{
    DisplayCustomerList(customerList);

    Console.WriteLine("Enter the MemberID of the customer you want to view orders for:");
    int selectedMemberId;
    if (int.TryParse(Console.ReadLine(), out selectedMemberId))
    {
        Customer selectedCustomer = customerList.Find(c => c.Memberid == selectedMemberId);

        if (selectedCustomer != null)
        {
            Console.WriteLine($"Orders for {selectedCustomer.Name} (MemberID: {selectedCustomer.Memberid}):");

            List<Order> customerOrders = GetCustomerOrders(orderList, selectedMemberId);

            foreach (Order order in customerOrders)
            {
                DisplayOrderDetails(order);
            }
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid MemberID.");
    }
}

static void DisplayCustomerList(List<Customer> customers)
{
    Console.WriteLine("Customer List:");
    foreach (Customer customer in customers)
    {
        Console.WriteLine($"MemberID: {customer.Memberid}, Name: {customer.Name}");
    }
    Console.WriteLine();
}

static List<Order> GetCustomerOrders(List<Order> orders, int memberId)
{
    return orders.FindAll(o => o.Id == memberId);
}

static void DisplayOrderDetails(Order order)
{
    Console.WriteLine($"Order ID: {order.Id}, Time Received: {order.TimeReceived}");

    // Assuming Order class has a property for TimeFulfilled and a list of IceCream objects
    Console.WriteLine($"Time Fulfilled: {order.TimeFulfilled}");

    Console.WriteLine("Ice Cream Details:");
    foreach (IceCream iceCream in order.IceCreamList)
    {
        Console.WriteLine($"Flavour: {iceCream.Flavours}, Toppings: {iceCream.Toppings}");
    }

    Console.WriteLine();
}

// Feature 6
static void OptionSix()
{
    Console.WriteLine("Hello World");
}

// Advanced (a)
static void OptionSeven()
{
    Console.WriteLine("Hello World");
}
// Advanced (b)

// Advanced (c)

Console.WriteLine("hello world");