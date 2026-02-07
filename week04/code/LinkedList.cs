using System.Collections;
using System.Globalization;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {   // Create a new node
        Node newNode = new(value);
        // Verify if the list is empty
        if (_head is null)
        {   // Case 1: Empty list -> the new node is head and tail
            _head = newNode;
            _tail = newNode;
        }
        else
        {   // Case 2: List is NOT empty
            _tail!.Next = newNode; // Connect the actual tail with the new node
            newNode.Prev = _tail;  // Connect the new node with the actual tail
            _tail = newNode;       // Update tail to the new node
        }
    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {    // Case 1: Empty list -> logic error
        if (_head is null)
        {
            throw new InvalidOperationException("Cannot remove from an empty list.");
        }
        // Case 2: List with just one element
        // (when _head == _tail, it means there is exactly 1 node)
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // Case 3: List with multiple elements
        else
        {   // Step A: Access the second to last node (the one that's going to be the new tail)
            Node newTail = _tail!.Prev!;
            // Step B: Disconnect the last node
            newTail.Next = null; // Break the bound towards the node that is being eliminated
            // Step C: Update _tail to the second to last node
            _tail = newTail;
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {   // Step 1: Search for the node that contains 'value'
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {   // Found, now remove this node according to its position
                // Case A: Its the ONLY node in the list
                if(_head == _tail)
                {
                    _head = null;
                    _tail = null;
                }
                // Case B: Its the HEAD (but there are mode nodes)
                else if (curr == _head)
                {
                    _head = _head.Next; // Move head to the next node
                    _head!.Prev = null; // Breaks the link pointing to the removed node
                }
                // Case C: Its the TAIL (but there are more nodes)
                else if (curr == _tail)
                {
                    _tail = _tail.Prev; // Move tail to the previous node
                    _tail!.Next = null; // Break the link pointing to the removed node 
                }
                // Case D: Its in the MIDDLE of the list
                else
                {   // Reconnect the previous node with the next one
                    curr.Prev!.Next = curr.Next; // [previous].Next -> [next]
                    curr.Next!.Prev = curr.Prev; // [next].Prev -> [previous]
                }
                // Exits after removing the FIRST occurrence
                return;
            }
            // Continues search
            curr = curr.Next;
        }

    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {   // Step 1: Start from the HEAD (fist node in the list)
        Node? curr = _head; // Temporary pointer that'll us to iterate the list
                           // curr = "current node"

        // Step 2: Iterate the list without getting to the end
        while (curr is not null) // Halt condition: when curr is null, it means we have reach the end of the list
        {
            // Step 3: Check if the actual node contains the value to replace
            if (curr.Data == oldValue) // Access the 'Data' property of the actual node and compares it to 'oldValue'
            {
                // Step 4: It matches = replace the value
                curr.Data = newValue; // Modifies ONLY the stored data
            }
            //Step 5: Move to the next node (avoids getting stuck in an infinite loop)
            curr = curr.Next;
        }

    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        // TODO Problem 5
        yield return 0; // replace this line with the correct yield return statement(s)
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}