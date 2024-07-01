using System;
namespace StateDesignPattern.GateExample
{

    public interface IGateState
    {
        void pay();
        void payOk();
        void payFailed();
        void enter();
    }

    public abstract class GateState : IGateState
    {
        public Gate gate;

        public virtual void enter()
        {
            Console.WriteLine("Rejecting enter from : " + this.GetType().ToString());
        }

        public virtual void pay()
        {
            Console.WriteLine("Rejecting Pay from : " + this.GetType().ToString());
        }

        public virtual void payFailed()
        {
            Console.WriteLine("Rejecting payFailed from : " + this.GetType().ToString());
        }

        public virtual void payOk()
        {
            Console.WriteLine("Rejecting payOk from : " + this.GetType().ToString());
        }

        public GateState(Gate gate)
        {
            Console.WriteLine("Creating New State : " + this.GetType().ToString());
            this.gate = gate;
        }
    }

    public class OpenGateState : GateState
    {
        public override void enter()
        {
            // On message Enter Let the user Enter to gate

            Console.WriteLine("In OpenGateState : Enter");
            // Change Gate State to closed state
            this.gate.changeState(new ClosedGateState(this.gate));
        }

        public OpenGateState(Gate gate) : base(gate)
        {
            this.gate = gate;
        }
    }

    public class ClosedGateState : GateState
    {
        public override void pay()
        {
            Console.WriteLine("Found Pay in : " + this.GetType().Name);
            this.gate.changeState(new ProcessingGateState(this.gate));
        }


        public ClosedGateState(Gate gate) : base(gate)
        {
            this.gate = gate;
        }
    }

    public class ProcessingGateState : GateState
    {


        public override void payFailed()
        {
            Console.WriteLine("Found payFailed in : " + this.GetType().Name);
            this.gate.changeState(new ClosedGateState(this.gate));
        }

        public override void payOk()
        {

            Console.WriteLine("Found payOk in : " + this.GetType().Name);
            this.gate.changeState(new OpenGateState(this.gate));
        }

        public ProcessingGateState(Gate gate) : base(gate)
        {
            this.gate = gate;
        }
    }
}
