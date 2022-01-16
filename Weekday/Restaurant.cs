using System;
using System.Collections.Generic;
using System.Text;

namespace Weekday
{
    class Restaurant
    {
        private int cookingSlots = 7;
        private double SingleOrderTime(Order order)
        {
            try
            {
                double distance = order.distance;
                List<string> meals = order.meals;
                int slots = meals.Count;
                int timePerDish = 17;
                if (slots > 7)
                {
                    return -1;
                }
                for (int i = 0; i < slots; i++)
                {
                    if (meals[i] == "M")
                    {
                        timePerDish = 29;
                        break;
                    }
                }
                return 8 * distance + timePerDish;
            }
            catch (Exception e) {
                Console.WriteLine("Exception Cought : {0}", e);
                return 0;
            }
            
        }
        public List<string> CalculateTimeForEachOrder(Queue<Order> queue)
        {
            SortedDictionary<double, int> timeMapSlots = new SortedDictionary<double, int>();
            List<string> timePerOrder = new List<string>();
            double waitingTimeGlobal = 0;
            string message;
            while (queue.Count > 0)
            {
                try
                {
                    Order order = queue.Dequeue();
                    double singleOrderTime = SingleOrderTime(order);
                    int slotsRequired = order.meals.Count;
                    if (singleOrderTime == -1)
                    {
                        message = string.Format("Order {0} is denied because the restaurant cannot accommodate it.", order.orderId);
                        timePerOrder.Add(message);
                        continue;
                    }
                    if (slotsRequired <= cookingSlots)
                    {
                        timeMapSlots[singleOrderTime] = slotsRequired;
                        cookingSlots = cookingSlots - slotsRequired;
                        string mess = string.Format("Order {0} will get delivered in {1} minutes", order.orderId, singleOrderTime + waitingTimeGlobal);
                        timePerOrder.Add(mess);
                    }
                    else
                    {
                        int slotDifference = slotsRequired - cookingSlots;
                        double waitingTime = 0;
                        List<double> keysToBeDeleted = new List<double>();
                        foreach (var keyValuePair in timeMapSlots)
                        {
                            waitingTime = Math.Max(waitingTime, keyValuePair.Key);
                            slotDifference = slotDifference - keyValuePair.Value;
                            keysToBeDeleted.Add(keyValuePair.Key);
                            if (slotDifference <= 0)
                            {
                                break;
                            }
                        }
                        double totalTime = waitingTime + singleOrderTime + waitingTimeGlobal;
                        if (totalTime > 150)
                        {
                            string mes = string.Format("Order {0} is denied because the restaurant cannot accommodate it.", order.orderId);
                            timePerOrder.Add(mes);
                            continue;
                        }
                        else
                        {
                            waitingTimeGlobal += waitingTime;
                        }
                        foreach (var key in keysToBeDeleted)
                        {
                            cookingSlots += timeMapSlots[key];
                            timeMapSlots.Remove(key);
                        }
                        keysToBeDeleted.Clear();
                        Dictionary<double, int> newPairs = new Dictionary<double, int>();
                        foreach (var keyValuePair in timeMapSlots)
                        {
                            double newKey = keyValuePair.Key - waitingTime;
                            int newSlots = keyValuePair.Value;
                            keysToBeDeleted.Add(keyValuePair.Key);
                            newPairs[newKey] = newSlots;
                        }
                        foreach (var key in keysToBeDeleted)
                        {
                            timeMapSlots.Remove(key);
                        }
                        foreach (var keyValuePair in newPairs)
                        {
                            timeMapSlots[keyValuePair.Key] = keyValuePair.Value;
                        }
                        timeMapSlots[singleOrderTime] = slotsRequired;

                        cookingSlots -= slotsRequired;

                        string mess = string.Format("Order {0} will get delivered in {1} minutes", order.orderId, totalTime);
                        timePerOrder.Add(mess);

                    }
                }
                catch (Exception e){
                    Console.WriteLine("Exception Caught : {0}", e);
                }
            }
            return timePerOrder;
        }
    }
}
