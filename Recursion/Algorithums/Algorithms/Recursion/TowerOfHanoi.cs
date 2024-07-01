using System;

namespace Algorithms.Recursion
{
    public class TowerOfHanoi
    {
        public static void TowerOfHanoiExecute(int n,char fromPage, char toPage, char auxPage)
        {
            if (n == 1)
            {
                Console.WriteLine("Moving Disk {0}, from {1} to {2}",n,fromPage,toPage);
                return;
            }
            /* Move N-1 Discs from A to B Using C as Aux*/
            
            TowerOfHanoiExecute(n-1,fromPage,auxPage,toPage);
            Console.WriteLine("Moving Disk {0}, from {1} to {2}",n,fromPage,toPage);
            TowerOfHanoiExecute(n-1,auxPage,toPage,fromPage); 
        }
    }
}