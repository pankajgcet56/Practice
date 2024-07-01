using System;

namespace DecoratorPattern.PizzaDecorator
{
    public class TommatoSos : ToppingDecorator
    {
        public TommatoSos(IPizza pizza) : base(pizza)
        {
            decoCost = 5.5f;
            Console.WriteLine("Adding Tommato SOS");
        }

        public override float getCost()
        {
            return base.getCost()+decoCost;
        }

        public override string getDiscription()
        {
            return base.getDiscription()+ ", Tomato SOS";
        }
    }
}