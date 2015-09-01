using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Entities;

namespace Cqrs
{
    public static class CustomerEvents
    {
        private static IList<Customer> customers =new List<Customer>();

        public static bool onStartEvent(Customer customer)
        {
            if (customers.Contains(customer))
                return false;
            customers.Add(customer);
            return true;
        }
        public static void onEndEvent(Customer customer)
        {
            customers.Remove(customer);
        }

        public static void onEndEvent(string operation, Customer customer)
        {
            new AddnewEvent(operation,customer.ToString());
            customers.Remove(customer);
        }
    }
}
