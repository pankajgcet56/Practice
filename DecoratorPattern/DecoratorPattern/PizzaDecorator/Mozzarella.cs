using System;

namespace DecoratorPattern.PizzaDecorator
{
    public class Mozzarella : ToppingDecorator
    {
        public override float getCost()
        {
            return base.getCost() + decoCost;
        }

        public override string getDiscription()
        {
            return base.getDiscription()+", Mozzarella";
        }

        public Mozzarella(IPizza pizza) : base(pizza)
        {
            this.pizza = pizza;
            Console.WriteLine("Adding Mozzarella");
            decoCost  = 10;
        }
    }
}