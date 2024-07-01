using System;
namespace DecoratorPattern.PizzaDecorator
{
    public abstract class ToppingDecorator : Pizza
    {
        public IPizza pizza;
        protected float decoCost;

        protected ToppingDecorator(IPizza pizza)
        {
            Console.WriteLine("Creating Pizza");
            this.pizza = pizza;
        }
        
        public virtual string getDiscription()
        {
            return "Topping Decorator, " + pizza.getDiscription();
        }

        public virtual float getCost()
        {
            return 0 + pizza.getCost();
        }
    }
}