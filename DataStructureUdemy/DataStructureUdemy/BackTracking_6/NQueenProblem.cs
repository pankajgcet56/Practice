namespace DataStructureUdemy.BackTracking_6;

public class NQueenProblem : Problem
{
    public NQueenProblem()
    {
        this.RunIndex = 6.0f;
    }
    public override void Run()
    {
        Console.WriteLine("N Queen problem, Backtracking");
        Console.WriteLine("----------------------------------------");
        int boardSize = 4;
        int[,] board = new int[boardSize, boardSize];
        SolveNQueen(boardSize, board, 0);
        // PrintBoard(boardSize,board);
    }
    
    /// <param name="n : Board Size"></param>
    /// <param name="board"></param>
    /// <param name="i  : Current Row where Queen will be placed"></param>
    /// <returns></returns>
    private bool SolveNQueen(int n, int [,] board,int i)
    {
        if (i == n)
        {
            // Board is Solved
            PrintBoard(n,board);
            return true;
        }
        // Recursive Case
        // Try Placing a Q in every row
        for (int j = 0; j < n; j++)
        {
            PrintBoard(n,board);
            Console.WriteLine("Can Place Queen = "+CanPlace(board, n, i, j));
            Console.WriteLine();
            if (CanPlace(board, n, i, j))
            {
                board[i, j] = 1;
                bool success = SolveNQueen(n, board, i + 1);
                if (success)
                    return true;
            }
            // Backtrack
            board[i, j] = 0;
        }
        
        return false;
    }

    private bool CanPlace(int[,] board, int n, int x, int y)
    {
        // Row check is skipped as we are placing 1 Q in a Row 
        // Check Column 
        for (int k = 0; k < x; k++)
        {
            if (board[k, y] == 1)
            {
                return false;
            }
        }

        int i = x;
        int j = y;
        // Check Left Dig
        while (i>0 && j>0)
        {
            if (board[i, j] == 1)
                return false;
            i--;
            j--;
        }
        // Check Right Dig
        i = x;
        j = y;
        while (i > 0 && j> n)
        {
            if (board[i, j] == 1)
                return false;
            i--;
            j++;
        }
        
        return true;
    }

    private void PrintBoard(int n, int [,] board)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(board[i,j]+" ");
            }
            Console.WriteLine();
        }
    }
}