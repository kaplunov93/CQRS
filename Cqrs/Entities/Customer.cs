using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Cqrs.Entities
{
    public class Customer:IEquatable<Customer>
    {
        public virtual int id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string number { get; set; }
        public virtual IList<Order> orders { get; set; }

        public Customer(string FirstName,string LastName,string number="")
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.number = number;
        }

        public Customer() { }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}\n",id,FirstName,LastName,number);
        }

        public virtual bool Equals(Customer other)
        {
            if (other == null) return false;
            if (this.FirstName == other.FirstName && this.LastName == other.LastName && this.number == other.number)
                return true;
            return false;
        }
    }
    

    public class CustomerMap:ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.id);
            Map(x => x.FirstName)
                .Length(20)
                .Not.Nullable();
            Map(x => x.LastName)
                .Length(20)
                .Not.Nullable();
            Map(x => x.number);
            HasMany(x=>x.orders);
        }
        

    }
    
}