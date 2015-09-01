using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Entities;
using NHibernate;

namespace Cqrs
{
    
    public class AddNewCustomer
    {

        public AddNewCustomer(string FirstName,string LastName,string number="")
        {
            Customer newCustomer = new Customer(FirstName, LastName,number);
            while (!CustomerEvents.onStartEvent(newCustomer)) { }
            if (!CustomerQuery.Have(newCustomer))
            {
                using (ISession session = SessionFactory.GetFactory().OpenSession())
                {
                    session.Save(newCustomer);
                }
                CustomerEvents.onEndEvent("Add Customer",newCustomer);
                return;
            }
            CustomerEvents.onEndEvent(newCustomer);
        }
    }

    /// <summary>
    ///!!! Don't Work !!!
    /// </summary>
    public class DeleteCustomer
    {
        public DeleteCustomer(int id)
        {
            Customer customer = CustomerQuery.FindById(id);
            
            if (customer != null)
            {
                while (!CustomerEvents.onStartEvent(customer)) { }
                Customer customer1 = CustomerQuery.FindById(id);
                if (customer1 != null)
                {
                    using (ISession session = SessionFactory.GetFactory().OpenSession())
                    {
                        using (var transaction = session.BeginTransaction())
                        {
                            session.Delete(customer);
                            transaction.Commit();
                        }
                    }
                    CustomerEvents.onEndEvent("Delete Customer", customer);
                }
                CustomerEvents.onEndEvent(customer);
            }
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
