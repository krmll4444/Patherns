using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class FastFood
    {
        public static void TakeOrder(int[,] order)
        {
            for (int i = 0; i < order.GetLength(0); i++)
            {
                Console.WriteLine($"Meal {order[i, 0]} - {order[i, 1]} times;");
            }
            Console.WriteLine();
        }
    }
    class Sushi
    {
        public static void TakeOrder(int[,] order)
        {
            for (int i = 0; i < order.GetLength(0); i++)
            {
                Console.WriteLine($"Meal {order[0, i]} - {order[1, i]} times;");
            }
            Console.WriteLine();
        }
    }
    class TraditionalUkrainian
    {
        public static void TakeOrder(int[] order)
        {
            foreach (int i in order)
            {
                Console.WriteLine($"Meal {i};");
            }
        }
    }
    class Facade
    {
        public static void TakeOrder(string food, Dictionary<int, int> order)
        {   
            switch (food)
            {
                case "Fastfood":
                    int[,] processedOrder1 = new int[order.Count,2];
                    int counter1 = 0;
                    foreach (var item in order)
                    {
                        processedOrder1[counter1, 0] = item.Key;
                        processedOrder1[counter1, 1] = item.Value;
                        counter1++;
                    }
                    FastFood.TakeOrder(processedOrder1);
                    break;

                case "Sushi":
                    int[,] processedOrder2 = new int[2, order.Count];
                    int counter2 = 0;
                    foreach (var item in order)
                    {
                        processedOrder2[0, counter2] = item.Key;
                        processedOrder2[1, counter2] = item.Value;
                        counter2++;
                    }
                    Sushi.TakeOrder(processedOrder2);
                    break;

                case "UkrTraditional":
                    List<int> processedOrder3 = new List<int>();
                    foreach (var item in order)
                    {
                        for (int i = 0; i < item.Value; i++)
                        {
                            processedOrder3.Add(item.Key);
                        }
                    }
                    TraditionalUkrainian.TakeOrder(processedOrder3.ToArray());
                    break;
                default:
                    throw new Exception("Invalid food");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            FastFood.TakeOrder(new int[,] { { 123, 2 }, { 42, 4 } });
            Sushi.TakeOrder(new int[,] { { 123, 42 }, { 2, 4 } });
            TraditionalUkrainian.TakeOrder(new int[] { 123, 123, 42, 42, 42, 42 });
            */
            Dictionary<int, int> order = new Dictionary<int, int>() {
                { 123, 2 }, 
                { 42, 4 }
            };
            Facade.TakeOrder("Fastfood", order);
            Facade.TakeOrder("Sushi", order);
            Facade.TakeOrder("UkrTraditional", order);
        }
    }
}