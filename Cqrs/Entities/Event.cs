using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Entities
{
    public class Event
    {
        public virtual int id { get; set; }
        public virtual string operation { get; set; }
        public virtual string data { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}\n",id,operation,data);
        }
    }

    public class EventMap:ClassMap<Event>
    {
        public EventMap()
        {
            Id(x=>x.id);
            Map(x=>x.operation)
                .Not.Nullable();
            Map(x => x.data)
               .Not.Nullable();
        }
    }
}
