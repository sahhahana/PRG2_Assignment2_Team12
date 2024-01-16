//==========================================================
// Student Number : S10256056
// Student Name : Chloe 
// Partner Name : Sahana
//==========================================================
using PRG2_Assignment2_Team12;
using System.Numerics;

Console.WriteLine("Hello World");

//chloe - features 2,5 and 6
//sahana- features 1, 3 ad 4

// Feature 1
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
// Feature 2
List<Order> orderList = new List<Order>();
using (StreamReader sr = new StreamReader("orders.csv"))
{
    string s = sr.ReadLine(); //read heading
    if (s != null)
    {
        string[] headings = s.Split(","); //headings
    }
    while ((s = sr.ReadLine()) != null)
    {
        string[] data = s.Split(",");
        // Order: id, timeReceived
        Order orderData = new Order(Convert.ToInt32(data[0]), Convert.ToDateTime(data[2]));
        orderList.Add(orderData);
    }
}
foreach (Order order in orderList)
{
    // Find the corresponding customer based on MemberId
    Customer customer = customerList.Find(c => c.Memberid == order.Id);

    Console.WriteLine("{0:-10}{1:-10}{2:-10}{3:-10}","Customer","MemberID","Status","Time Received");

    // Check if the customer is found and has a valid MembershipStatus
    if (customer != null && (customer.MembershipStatus == "Gold" || customer.MembershipStatus == "Ordinary"))
    {
        // Display information about the customer and the order
        Console.WriteLine("{0:-10}{1:-10}{2:-10}{3:-10}{4:-10}", customer.Name, customer.Memberid, customer.MembershipStatus, order.TimeReceived);
    }
}

// Feature 3

// Feature 4

// Feature 5

// Feature 6

// Advanced (a)

// Advanced (b)

// Advanced (c)

Console.WriteLine("hello world");