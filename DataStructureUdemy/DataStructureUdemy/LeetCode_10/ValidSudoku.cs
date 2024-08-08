namespace DataStructureUdemy.LeetCode;

public class ValidSudoku : Problem
{
    public ValidSudoku()
    {
        RunIndex = 10.1f;
    }
    
    public override void Run()
    {
        char[][] dataSet = new char[9][];
        
        dataSet[0] = new char[9] {'.','.','4','.','.','.','6','3','.'};
        dataSet[1] = new char[9] {'.','.','.','.','.','.','.','.','.'};
        dataSet[2] = new char[9] {'5','.','.','.','.','.','.','9','.'};
        dataSet[3] = new char[9] {'.','.','.','5','6','.','.','.','.'};
        dataSet[4] = new char[9] {'4','.','3','.','.','.','.','.','1'};
        dataSet[5] = new char[9] {'.','.','.','7','.','.','.','.','.'};
        dataSet[6] = new char[9] {'.','.','.','5','.','.','.','.','.'};
        dataSet[7] = new char[9] {'.','.','.','.','.','.','.','.','.'};
        dataSet[8] = new char[9] {'.','.','.','.','.','.','.','.','.'};
        
        
        Console.WriteLine(IsValidSudoku(dataSet));
    }
    
    private int CharToInt(char ch) => ch - '0';

    private bool IsValidList(char[] dataSet)
    {
        if (dataSet == null || dataSet.Length != 9) return false;
        // If Any Repeat Char return False
        bool[] validationSet = new bool[9];
        foreach (var charVal in dataSet)
        {
            if(charVal == '.') continue;
            if (validationSet[CharToInt(charVal) - 1])
                return false;
            validationSet[CharToInt(charVal) - 1] = true;
        }
        return true;
    }
    
    public bool IsValidSudoku(char[][] board) 
    {
        // 9*9 grid
        // Horizontal Row
        bool valid = true;
        for (int i = 0; i < 9; i++)
        {
            valid = valid && IsValidList(board[i]);
            if (!valid)
                return false;
        }
        // vartical Row
        for (int i = 0; i < 9; i++)
        {
            List<char> newSet = new List<char>();
            for (int j = 0; j < 9; j++)
            {
                newSet.Add(board[j][i]);
            }
            valid = valid && IsValidList(newSet.ToArray());
            if (!valid)
                return false;
        }
        // All 9, 3by3 layout
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                List<char> tmp = new List<char>();
                //Create List in Block 
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        tmp.Add(board[k+i*3][l+j*3]);
                    }
                }

                valid = valid && IsValidList(tmp.ToArray());
                if (!valid)
                    return false;
            }
        }
        return true;
    }
}