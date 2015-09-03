using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Entities;
using NHibernate;

namespace Cqrs
{
    public static class ICommand
    {
        private static Queue<Tuple<string,object>> commands=new Queue<Tuple<string, object>>();
        private static bool Working = false;

        public static void Add(Tuple<string, object> command)
        {
            commands.Enqueue(command);
            if(!Working)
            {
                Thread work = new Thread(Work);
                work.Start();
            }
        }

        private static void Work()
        {
            while (commands.Count>0)
            {
                Tuple<string, object> command = commands.Dequeue();
                if (command.Item2 != null)
                {
                    Console.WriteLine(
                          string.Format("Peek From Queue To {0} : {1}"
                              , command.Item1
                              , command.Item2.ToString()));
                    if (command.Item1 == "AddCustomer")
                        if (AddNewCustomer(command.Item2 as Customer))
                            new AddnewEvent(command.Item1, command.Item2.ToString());
                    if (command.Item1 == "DeleteCustomer")
                        if (DeleteCustomer(command.Item2 as Customer))
                            new AddnewEvent(command.Item1, command.Item2.ToString());
                }
            }
            Working = false;
        }

        private static bool AddNewCustomer(Customer newCustomer)
        {
            if (!CustomerQuery.Have(newCustomer))
            {
                using (ISession session = SessionFactory.GetFactory().OpenSession())
                {
                    session.Save(newCustomer);
                }
                return true;
            }
            return false;
        }

        private static bool DeleteCustomer(Customer customer)
        {
            bool flag = false;
            if(CustomerQuery.FindById(customer.id)!=null)
            using (ISession session = SessionFactory.GetFactory().OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(customer);
                        transaction.Commit();
                        flag = true;
                    }
                }
            return flag;
        }
    }

    public static class CustomerComamnds
    {
        public static void Add(string FirstName, string LastName, string number = "")
        {
            Customer newCustomer = new Customer(FirstName, LastName, number);
            ICommand.Add(new Tuple<string, object>("AddCustomer",newCustomer));
        }

        public static void Delete(int id)
        {
            Customer customer = CustomerQuery.FindById(id);
            ICommand.Add(new Tuple<string, object>("DeleteCustomer", customer));
        }
    }

    public class AddnewEvent
    {
        public AddnewEvent(string operation,object customer)
        {
            var newEvent = new Event();
            newEvent.operation = operation;
            newEvent.data = customer.ToString();
            using (ISession session = SessionFactory.GetFactory().OpenSession())
            {
                session.Save(newEvent);
            }
        }
    }
}
