using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Weekday
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order1 = new Order();
            order1.orderId = 11;
            var l = new List<string> { "A", "A" };
            order1.meals = l ;
            order1.distance = 5;

            Order order2 = new Order();
            order2.orderId = 12;
            var l2 = new List<string> { "A", "M" };
            order2.meals = l2;
            order2.distance = 1;

            Order order3 = new Order();
            order3.orderId = 13;
            var l3 = new List<string> { "M","A", "A", "M" };
            order3.meals = l3;
            order3.distance = 1;

            Order order4 = new Order();
            order4.orderId = 14;
            var l4 = new List<string> { "M" };
            order4.meals = l4;
            order4.distance = 0.1;

            Order order5 = new Order();
            order5.orderId = 15;
            var l5 = new List<string> { "A" };
            order5.meals = l5;
            order5.distance = 3;

            Queue<Order> queue = new Queue<Order>();

            queue.Enqueue(order1);
            queue.Enqueue(order2);
            queue.Enqueue(order3);
            queue.Enqueue(order4);
            queue.Enqueue(order5);
            Restaurant R = new Restaurant();
            

            string fileName = "C:/Users/320105541/Documents/devloper/weekday/Weekday/Weekday/input.json";
            string jsonString = File.ReadAllText(fileName);
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonString);
            Queue<Order> Q = new Queue<Order>();
            foreach (var order in orders) {
                Q.Enqueue(order);
            }
            List<string> ans = R.CalculateTimeForEachOrder(Q);
            foreach (var s in ans)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}
