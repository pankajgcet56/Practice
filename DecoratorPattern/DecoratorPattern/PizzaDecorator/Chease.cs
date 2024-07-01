using System;

namespace DecoratorPattern.PizzaDecorator
{
    public class Chease : ToppingDecorator
    {
        public Chease(IPizza pizza) : base(pizza)
        {
            decoCost = 25.8f;
            Console.WriteLine("Adding Chease");
        }

        public override float getCost()
        {
            return base.getCost()+decoCost;
        }

        public override string getDiscription()
        {
            return base.getDiscription()+", Chease";
        }
    }
}