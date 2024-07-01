using System;
namespace StateDesignPattern.GateExample
{
    public class Gate
    {
        public GateState currentGateState;

        public void enter()
        {
            currentGateState.enter();
        }

        public void pay()
        {
            currentGateState.pay();
        }

        public void payOk()
        {
            currentGateState.payOk();
        }

        public void payFailed()
        {
            currentGateState.payFailed();
        }

        public void changeState(GateState state)
        {
            if(state == null)
            {
                // Can't Put FSM to null state
                return;
            }

            Console.WriteLine("Changing State From : " + currentGateState.GetType().Name + " to : " + state.GetType().Name);

            this.currentGateState = state;
        }

        public Gate(GateState state)
        {
            if(state == null)
            {
                // Start From Closed State

                this.currentGateState = new ClosedGateState(this);
            }
            this.currentGateState = state;
        }

        public Gate()
        {
            // Start FSM with Closed gate State
            this.currentGateState = new ClosedGateState(this);
        }
    }
}
