using System;
using System.Globalization;
using System.Collections.Generic;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Кофе - 2,30 - 1шт 01.01.2000\n" +
                "Молоко - 2,60 - 10шт 02.01.2005\n" +
                "Капуста - 50,30 - 100шт 10.01.2000";
            string[] ordersStr = text.Split("\n");
            List<Order> orders = new();

            foreach (var orderStr in ordersStr)
            {
                Order order = null;
                try
                {
                    order = new(orderStr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (order == null) continue;

                order.Print();
                order.Print(new CultureInfo("ru-RU"));
            }

        }
    }
}
