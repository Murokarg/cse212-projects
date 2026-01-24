using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue them.
    // Expected Result: Items are dequeued in order of highest priority first.
    // Defect(s) Found:
    // - The Dequeue method does not remove the item from the queue, so the same item is returned repeatedly.
    // - The loop in Dequeue stops at _queue.Count - 1, so the last item is never considered for highest priority.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);
        
        Assert.AreEqual("B", priorityQueue.Dequeue()); // Highest priority (3)
        Assert.AreEqual("C", priorityQueue.Dequeue()); // Next highest (2)
        Assert.AreEqual("A", priorityQueue.Dequeue()); // Last (1)

        //Assert.Fail("Implement the test case and then remove this.");
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority.
    // Expected Result: The item closest to the front (FIFO) is dequeued first.
    // Defect(s) Found:
    // - The Dequeue method uses ">=" instead of ">" when comparing priorities, causing it to select the LAST item with the highest priority instead of the FIRST (violates FIFO for ties).
    // - The item is not removed from the queue, so subsequent Dequeue calls return the same value.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 3); // Lower priority

        Assert.AreEqual("First", priorityQueue.Dequeue()); // Same priority, but first in
        Assert.AreEqual("Second", priorityQueue.Dequeue()); // Next in line
        Assert.AreEqual("Third", priorityQueue.Dequeue()); // Only one left

        //Assert.Fail("Implement the test case and then remove this.");
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."

    public void TestPriorityQueue_Empty()
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
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception of type {e.GetType()}: {e.Message}");
        }
    }

    [TestMethod]
    // Scenario: Enqueue items in arbitrary order, with mixed priorities.
    // Expected Result: Dequeue returns items by highest priority first, then FIFO for ties.
    // Defect(s) Found: 
    // - The last item in the queue is ignored due to loop condition "index < _queue.Count - 1".
    // - Items are not removed after dequeue, causing repeated returns of the same value.
    // - For equal priorities, the wrong item is selected due to use of ">=" instead of ">".
    public void TestPriorityQueue_MixedOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("AlsoHigh", 10); // Same as High

        Assert.AreEqual("High", priorityQueue.Dequeue());     // First high priority item
        Assert.AreEqual("AlsoHigh", priorityQueue.Dequeue()); // Second high priority, FIFO
        Assert.AreEqual("Medium", priorityQueue.Dequeue());   // Next highest
        Assert.AreEqual("Low", priorityQueue.Dequeue());      // Last
    }
}
