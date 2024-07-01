namespace SCSSolution;

public class MultithreadingProblem
{
    

}

// Following code implements a semaphore of a given capacity using C# class System.Threading.Monitor.
// Learn the basics of the System.Threading.Monitor class methods such as Wait()/Pulse()/Enter()/Exit()
// Identify weaknesses/bugs, if any, in the following code (before the interview) We will discuss during the interview. (10 min)
public class Semaphore 
{
    private object _mutex = new object();
    private int _currAvail;
    
    public Semaphore(int capacity) 
    {
        _currAvail = capacity;
    }

    public void DoIncrement()
    {
        lock (this)
        {
            _currAvail++;
        }
    }
    
    public void Wait() 
    {
        lock(_mutex) 
        {
            if (_currAvail == 0) 
                Monitor.Wait(_mutex);
            _currAvail--; 
        }
        /*
         
        lock(_mutex) 
           {
               while (_currAvail == 0) // If Spurious Wakeups (False Wakeups) is called 
               {
                   Monitor.Wait(_mutex);
               }
               _currAvail--; 
           }*/
        /*
           
        Why Do Spurious Wakeups Happen?
           
           Spurious wakeups can happen for several reasons, including but not limited to:
           
           	1.	Optimization by the OS or Runtime: The operating system or the runtime environment may cause threads to wake up for reasons related to internal optimizations or scheduling.
           	2.	Hardware Glitches: Certain rare hardware conditions may cause threads to wake up.
           	3.	Implementation Specifics: Some implementations of condition variables may allow for spurious wakeups as part of their design.
           	
           	// Monitor.PulseAll(semaphore); 
           
        */
        
    }
    public void Signal() 
    {
        lock(_mutex) 
        {
            _currAvail++;
            Monitor.Pulse(_mutex); 
        }
    } 
}
