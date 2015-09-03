using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Entities;

namespace Cqrs
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var Customers = CustomerQuery.Get();
            
            Console.WriteLine("---Customers---\n");
            Parallel.ForEach(Customers, cus => Console.WriteLine(cus.ToString())) ;
            Console.WriteLine("---End---\n");

            Console.Write("Write Customer Id to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Parallel.For(1, 4, x => CustomerComamnds.Delete(id));
            Console.WriteLine();

            var newCustomers = new List<Customer>
            {new Customer("Vasia","Pupkin"),
            new Customer("Viktor", "Pupkin"),
            new Customer("Vasia","Pupkin"),
            new Customer("Viktor", "Pupkin")};

            Parallel.ForEach(newCustomers, cus => CustomerComamnds.Add(cus.FirstName,cus.LastName,cus.number) );
            
            Customers = CustomerQuery.Get();

            Console.WriteLine("---Customers---\n");
            Parallel.ForEach(Customers, cus => Console.WriteLine(cus.ToString()));
            Console.WriteLine("---End---\n");


            IList<Event> Events = EventQuery.Get();
            Console.WriteLine("---Events---\n");
            //Parallel.ForEach(Events, e => Console.WriteLine(e.ToString()));
            foreach (var e in Events)
                Console.WriteLine(e.ToString());
            Console.WriteLine("---End---\n");

            Console.ReadKey();
        }
    }
}
