using System;
namespace DecoratorPattern.PizzaDecorator
{
    public interface IPizza
    {
        string getDiscription();
        float getCost();
    }
    
    public class Pizza : IPizza
    {
        public Pizza()
        {

        }

        public virtual string getDiscription()
        {
            return "Pizza";
        }

        public virtual float getCost()
        {
            return 100.5f;
        }
    }
}
