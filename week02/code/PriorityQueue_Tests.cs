using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests  // KEEPING SAME NAME - This is correct
{
    [TestMethod]
    // Scenario: Basic priority queue operations with different priorities
    // Expected Result: Items should be dequeued in order of highest priority first
    // Defect(s) Found: 
    // 1. Loop in Dequeue() was skipping first and last elements
    // 2. Priority comparison used >= instead of >, breaking FIFO for equal priorities
    // 3. Items were not actually removed from the queue after dequeue
    public void TestPriorityQueue_BasicPriority()
    {
        var priorityQueue = new PriorityQueue();

        // Enqueue items with different priorities
        priorityQueue.Enqueue("Task A", 2);  // Low priority
        priorityQueue.Enqueue("Task B", 5);  // High priority
        priorityQueue.Enqueue("Task C", 3);  // Medium priority
        priorityQueue.Enqueue("Task D", 5);  // Same high priority as Task B

        // Task B should come first (priority 5, added first)
        Assert.AreEqual("Task B", priorityQueue.Dequeue());

        // Task D should come next (priority 5, added after Task B)
        Assert.AreEqual("Task D", priorityQueue.Dequeue());

        // Task C should come next (priority 3)
        Assert.AreEqual("Task C", priorityQueue.Dequeue());

        // Task A should come last (priority 2)
        Assert.AreEqual("Task A", priorityQueue.Dequeue());

        // Queue should now be empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Multiple items with same priority - should maintain FIFO order
    // Expected Result: Items with same priority should be dequeued in the order they were added
    // Defect(s) Found: 
    // 1. Priority comparison used >= which caused later items to replace earlier ones
    // 2. This broke the FIFO requirement for equal priority items
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();

        // Add multiple items with same priority
        priorityQueue.Enqueue("First", 3);
        priorityQueue.Enqueue("Second", 3);
        priorityQueue.Enqueue("Third", 3);
        priorityQueue.Enqueue("Fourth", 3);

        // Should dequeue in FIFO order
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Fourth", priorityQueue.Dequeue());

        // Queue should be empty
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Complex mix of priorities
    // Expected Result: Highest priority items first, FIFO for same priority
    // Defect(s) Found: 
    // 1. Loop bounds were incorrect (index < _queue.Count - 1)
    // 2. This caused the last item to never be considered
    public void TestPriorityQueue_ComplexMix()
    {
        var priorityQueue = new PriorityQueue();

        // Mixed priorities
        priorityQueue.Enqueue("Low1", 1);
        priorityQueue.Enqueue("High1", 5);
        priorityQueue.Enqueue("Med1", 3);
        priorityQueue.Enqueue("High2", 5);  // Same as High1
        priorityQueue.Enqueue("Low2", 1);   // Same as Low1
        priorityQueue.Enqueue("Med2", 3);   // Same as Med1

        // Expected order: High1, High2, Med1, Med2, Low1, Low2
        Assert.AreEqual("High1", priorityQueue.Dequeue());
        Assert.AreEqual("High2", priorityQueue.Dequeue());
        Assert.AreEqual("Med1", priorityQueue.Dequeue());
        Assert.AreEqual("Med2", priorityQueue.Dequeue());
        Assert.AreEqual("Low1", priorityQueue.Dequeue());
        Assert.AreEqual("Low2", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Empty queue dequeue attempt
    // Expected Result: Should throw InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: 
    // 1. This test was actually passing - empty queue check was correct
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        // Should throw exception
        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Single item in queue
    // Expected Result: Should return the single item, then throw exception on next dequeue
    // Defect(s) Found: 
    // 1. Loop bounds issue affected single item case
    // 2. With index < Count - 1, when Count = 1, the loop doesn't run at all
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Single", 2);
        Assert.AreEqual("Single", priorityQueue.Dequeue());

        // Should now be empty
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Negative priorities
    // Expected Result: Should work with negative priorities (lower numbers = lower priority)
    // Defect(s) Found: 
    // 1. No specific defects for negative priorities
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Most Negative", -5);
        priorityQueue.Enqueue("Negative", -2);
        priorityQueue.Enqueue("Zero", 0);
        priorityQueue.Enqueue("Positive", 3);

        // Highest priority is 3 (Positive)
        Assert.AreEqual("Positive", priorityQueue.Dequeue());

        // Next is 0 (Zero)
        Assert.AreEqual("Zero", priorityQueue.Dequeue());

        // Next is -2 (Negative)
        Assert.AreEqual("Negative", priorityQueue.Dequeue());

        // Last is -5 (Most Negative)
        Assert.AreEqual("Most Negative", priorityQueue.Dequeue());
    }
}