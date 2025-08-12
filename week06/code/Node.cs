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
        // TODO Start Problem 1
        
        // Don't insert if the value already exists (unique values only)
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
        // TODO Start Problem 2
        
        // Base case: found the value
        if (value == Data)
            return true;
        
        // Recursive cases: search left or right subtree
        if (value < Data)
        {
            // Search in left subtree
            if (Left is null)
                return false;
            else
                return Left.Contains(value);
        }
        else
        {
            // Search in right subtree
            if (Right is null)
                return false;
            else
                return Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        
        // Height is 1 plus the maximum height of left or right subtree
        int leftHeight = 0;
        int rightHeight = 0;
        
        // Get height of left subtree
        if (Left is not null)
            leftHeight = Left.GetHeight();
        
        // Get height of right subtree
        if (Right is not null)
            rightHeight = Right.GetHeight();
        
        // Return 1 plus the maximum of the two subtree heights
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}