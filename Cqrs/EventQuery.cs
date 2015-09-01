using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Entities;
using NHibernate;

namespace Cqrs
{
    public static class EventQuery
    {
        public static IList<Event> Get()
        {
            using (ISession session = SessionFactory.GetFactory().OpenSession())
            {
                return session.CreateQuery("from Event").List<Event>();
            }
        }
    }
}
