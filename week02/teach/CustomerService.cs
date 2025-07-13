/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Add one customer and serve them to test basic functionality
        // Expected Result: Should display the customer that was added and served
        Console.WriteLine("Test 1");
        var cs = new CustomerService(3);
        cs.AddCustomerForTesting("Alice", "A001", "Password reset");
        Console.WriteLine("Queue after adding 1 customer:");
        Console.WriteLine(cs);
        
        Console.WriteLine("Serving customer:");
        cs.ServeCustomer(); // Should serve Alice
        Console.WriteLine("Queue after serving customer:");
        Console.WriteLine(cs);

        // Defect(s) Found: ServeCustomer method removes customer before displaying them

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add multiple customers and serve them to test FIFO order
        // Expected Result: Customers should be served in the order they were added (FIFO)
        Console.WriteLine("Test 2");
        cs = new CustomerService(5);
        cs.AddCustomerForTesting("Bob", "B002", "Account locked");
        cs.AddCustomerForTesting("Charlie", "C003", "Billing question");
        Console.WriteLine("Queue after adding 2 customers:");
        Console.WriteLine(cs);
        
        Console.WriteLine("Serving first customer (should be Bob):");
        cs.ServeCustomer();
        Console.WriteLine("Serving second customer (should be Charlie):");
        cs.ServeCustomer();

        // Defect(s) Found: Same as Test 1 - ServeCustomer removes before displaying

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
        
        // Test 3
        // Scenario: Test queue capacity limit
        // Expected Result: Should reject adding customer when queue is full
        Console.WriteLine("Test 3");
        cs = new CustomerService(2); // Small queue for testing
        cs.AddCustomerForTesting("Customer1", "C001", "Problem1");
        cs.AddCustomerForTesting("Customer2", "C002", "Problem2");
        Console.WriteLine("Queue with 2 customers (at capacity):");
        Console.WriteLine(cs);
        
        Console.WriteLine("Trying to add third customer to full queue:");
        cs.AddCustomerForTesting("Customer3", "C003", "Problem3"); // Should be rejected
        Console.WriteLine("Queue after attempting to add third customer:");
        Console.WriteLine(cs);

        // Defect(s) Found: AddNewCustomer used > instead of >= for capacity check
        
        Console.WriteLine("=================");
        
        // Test 4
        // Scenario: Test serving from empty queue
        // Expected Result: Should handle empty queue gracefully
        Console.WriteLine("Test 4");
        cs = new CustomerService(5);
        Console.WriteLine("Empty queue:");
        Console.WriteLine(cs);
        
        Console.WriteLine("Trying to serve from empty queue:");
        cs.ServeCustomer(); // Should display appropriate message

        // Defect(s) Found: All defects have been resolved
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in the queue.");
            return;
        }
        
        var customer = _queue[0];  // Get the first customer BEFORE removing
        _queue.RemoveAt(0);        // Then remove them from the queue
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }

    /// <summary>
    /// Helper method for testing - adds a customer directly without user input
    /// </summary>
    private void AddCustomerForTesting(string name, string accountId, string problem) {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }
}