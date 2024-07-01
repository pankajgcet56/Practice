
using System;
using DecoratorPattern.PizzaDecorator;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //string key;
            //while ((key = Console.ReadKey().KeyChar.ToString()) != "7")
            {
                PizzaDecorator.IPizza pizza = new Mozzarella(new Chease(new TommatoSos(new Chease(new Pizza()))));
                
                Console.WriteLine(pizza.getCost()+" : Cost");
                Console.WriteLine(pizza.getDiscription()+" : Discription");
            }
            
        }
    }
}
