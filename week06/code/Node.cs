using System.Diagnostics.CodeAnalysis;

public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // If the value is already in the tree, we do not insert it again
        if (value == Data)
            return; 

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // If the value is in the current node, we return true
        if (value == Data)
            return true;

        // If the value is smaller, we look in the left subtree
        if (value < Data)
        {
            // If the left subtree is null, then the value is not in the tree
            if (Left is null)
                return false;

            else
                return Left.Contains(value); // recursive call to the left subtree
        }
        // If the value is larger, we look in the right subtree
        else
        {
            // if the right subtree is null, then the value is not in the tree
            if (Right is null)
                return false;
            else
                return Right.Contains(value); // recursive call to the right subtree
        }
    }

    public int GetHeight()
    {
        //If the left subtree is null,
        // we consider its height as -1 (so that the height of a leaf node is 0)
        int leftHeight = -1;
        if (Left != null)
            leftHeight = Left.GetHeight();

        // If the right subtree is null,
        // we consider its height as -1 (so that the height of a leaf node is 0)
        int rightHeight = -1;
        if (Right != null)
            rightHeight = Right.GetHeight();

        // The height of the current node is 1 + the height of the tallest subtree
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}