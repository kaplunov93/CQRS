using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Entities
{
    public class Order
    {
        public virtual int id { get; set; }
        public virtual DateTime date { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<Product> Products { get; set; }
    }

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.id);
            Map(x => x.date)
                .Not.Nullable();
            References(x => x.Customer);
            HasMany(x => x.Products);
        }
    }
}
