using System.Threading;

namespace O9Test
{
    
    /// <summary>
    /// Enter : Acquires an exclusive lock on a specified object.
    /// Exit  : Releases an exclusive lock on the specified object.
    /// Pulse : Notifies a thread in the waiting queue of a change in the locked object's state.
    /// Wait    : Releases the lock on an object and blocks the current thread until it reacquires the lock.
    /// PulseAll : Notifies all waiting threads of a change in the object's state.
    /// </summary>
    
    public class SemaphoreO9 
    {
        private object _mutex = new object();
        private int _currAvail;
        
        // private System.Threading.SemaphoreO9 _semaphore = new System.Threading.SemaphoreO9(0, 5);
        
        public SemaphoreO9(int capacity) 
        {
            _currAvail = capacity; 
        }
        
        public void Wait() 
        {
            lock(_mutex) 
            {
                if (_currAvail == 0) 
                    Monitor.Wait(_mutex);
                _currAvail--; 
            }
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
}