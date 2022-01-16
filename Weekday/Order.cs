using System;
using System.Collections.Generic;
using System.Text;

namespace Weekday
{
    public class Order
    {
        public int orderId { get; set; }
        public List<string> meals { get; set; }
        public double distance { get; set; }
    }
    
}
