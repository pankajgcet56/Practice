namespace SCSSolution.Algorithum;

public class FenwickTree
{
    private float[] _tree;
    
    public FenwickTree(int size)
    {
        _tree = new float[size + 1]; // Tree is One Based, that's why Length is 1
    }
    // Complexity = O(N)
    public FenwickTree(float[] values)
    {
        this._tree = values ?? throw new ArgumentNullException("Values  can't be null");
        for (int i = 0; i < _tree.Length; i++)
        {
            int j = i + LeastSignificantBit(i);
            if (j < _tree.Length) _tree[j] += _tree[i];
        }
    }
    // Lowest bit, which is Set
    /*
        000001100 | +12 ~
        ----------|-------
        111110011 | ~12 +
                1 |
        ----------|-------
        111110100 | -12 

        111110100 | -12 & 
        000001100 | +12
        ----------|-------
        000000100 |   8`
     */
    public int LeastSignificantBit(int value) => value & -value;

    // Compute the Prefix Sum from [1, i], one based array, 1 and i are inclusive
    //Complexity = O(Log(N))
    public float PrefixSum(int i)
    {
        float sum = 0;
        while (i != 0)
        {
            sum += _tree[i];
            i -= LeastSignificantBit(i);
        }
        return sum;
    }

    // Sum from i to j
    public float Sum(int i, int j)
    {
        if (j < i) throw new ArgumentException("Make sure j >= i ");
        return PrefixSum(j) - PrefixSum(i);
    }

    // Add k to index i, Complexity = O(Log(N))
    public void Add(int i,float k)
    {
        while (i<_tree.Length)//
        {
            _tree[i] += k;
            i += LeastSignificantBit(i); // 9, 10, 12, 16
        }
    }
    
    public override string ToString() => string.Join(", ", _tree);
    
}

// Inventory Index Start from 1
public class SupplyChainSolution
{
    private int _inventoryLength = 10;
    private FenwickTree _fenwickTree;

    public SupplyChainSolution(int inventoryLength)
    {
        _inventoryLength = inventoryLength;
        _fenwickTree = new FenwickTree(new float[_inventoryLength]);
    }

    public void PrintInventory()
    {
        for (int i = 1; i < _inventoryLength; i++)
        {
            Console.Write(GetInventory(i)+", ");
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    public void AddSupply(int bucket, float delta)
    {
        Console.WriteLine("Supply:: "+bucket+" --> "+delta);
        _fenwickTree.Add(bucket,delta);
        PrintInventory();
    }

    public void AddDemand(int bucket, float delta)
    {
        Console.WriteLine("Demand:: "+bucket+" --> "+delta);
        _fenwickTree.Add(bucket,-delta);
        PrintInventory();
    }

    public float GetInventory(int bucket) // O(Log(N))
    {
        return _fenwickTree.Sum(0, bucket);
    }
}