
using System;

namespace StateDesignPattern
{
    class Program
    {
        private static GateExample.Gate gate;
        static void Main(string[] args)
        {
            Program program = new Program();
            gate = new GateExample.Gate();


            Console.WriteLine(@"Select one of the following option:                                
                            1-Enter
                            2-Pay
                            3-PayOk
                            4-PayFailed
                            5-Get Current State
                            6-Help
                            7-exit");

            string key;
            while ((key = Console.ReadKey().KeyChar.ToString()) != "7")
            {
                Console.WriteLine();
                int keyValue;
                int.TryParse(key, out keyValue);

                ProcessInput(keyValue);
            }

        }

        private static void ProcessInput(int keyValue)
        {
            switch (keyValue)
            {
                case 1:
                    gate.enter();
                    break;
                case 2:
                    gate.pay();
                    break;
                case 3:
                    gate.payOk();
                    break;
                case 4:
                    gate.payFailed();
                    break;
                case 5:
                    Console.WriteLine("Current State : " + gate.currentGateState.GetType().Name);
                    break;
                case 6:
                    Console.WriteLine(@"Select one of the following option:                                
                            1-Enter
                            2-Pay
                            3-PayOk
                            4-PayFailed
                            5-Get Current State
                            6-Help
                            7-exit");

                    break;
            }


        }
    }
}
