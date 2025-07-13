using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items and dequeue by priority - basic functionality
    // Expected Result: Should return items in order of highest priority first
    // Defect(s) Found: 1) Loop condition was wrong (_queue.Count - 1), 2) Items not actually removed from queue, 3) >= condition broke FIFO for same priority
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);
        
        // Should dequeue in priority order: High(5), Medium(3), Low(1)
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Multiple items with same priority - should follow FIFO for same priority
    // Expected Result: When priorities are equal, first enqueued should be dequeued first
    // Defect(s) Found: Same as Test 1 - >= condition selected later items instead of maintaining FIFO order
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 3);
        priorityQueue.Enqueue("Second", 3);
        priorityQueue.Enqueue("Third", 3);
        
        // Should dequeue in FIFO order for same priority
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
    
    [TestMethod]
    // Scenario: Mixed priorities with FIFO for same priority
    // Expected Result: Higher priority first, but FIFO within same priority
    // Defect(s) Found: Same as Test 1 - all three defects (loop condition, not removing items, FIFO order)
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low1", 1);
        priorityQueue.Enqueue("High1", 5);
        priorityQueue.Enqueue("Low2", 1);
        priorityQueue.Enqueue("High2", 5);
        priorityQueue.Enqueue("Medium", 3);
        
        // Should dequeue: High1(5), High2(5), Medium(3), Low1(1), Low2(1)
        Assert.AreEqual("High1", priorityQueue.Dequeue());
        Assert.AreEqual("High2", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low1", priorityQueue.Dequeue());
        Assert.AreEqual("Low2", priorityQueue.Dequeue());
    }
    
    [TestMethod]
    // Scenario: Empty queue dequeue
    // Expected Result: Should throw InvalidOperationException
    // Defect(s) Found: None - empty queue exception handling was already working correctly
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}