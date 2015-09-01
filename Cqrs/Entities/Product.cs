using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate.Mapping;

namespace Cqrs.Entities
{
    public class Product
    {
        public virtual int id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal price { get; set; }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.id);
            Map(x => x.Name)
                .Not.Nullable();
            Map(x => x.price)
                .Not.Nullable();
        }
    }
}
