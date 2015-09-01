using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;
using Cqrs.Entities;
using FluentNHibernate.Cfg.Db;
using NHibernate.Engine;
using NHibernate.Tool.hbm2ddl;

namespace Cqrs
{
    public static class SessionFactory
    {
        private static ISessionFactory factory=null;

        public static ISessionFactory GetFactory()
        {
            if (factory == null)
            {
                var conf = MySqlInitializer.GetConfiguration();
                var factory = (ISessionFactoryImplementor)conf.BuildSessionFactory();
                return factory;
            }
            return factory;
        }

    }

    class MySqlInitializer 
    {
        public static Configuration GetConfiguration()
        {
            var dbServer = "localhost";
            var dbUsername = "user";
            var dbName = "mydb";
            var dbPassword = "1234";

            var config = Fluently.Configure()
                .Database(MySQLConfiguration
                              .Standard
                              .ConnectionString(cs => cs
                                      .Server(dbServer)
                                      .Database(dbName)
                                      .Username(dbUsername)
                                      .Password(dbPassword))
                              .Driver<NHibernate.Driver.MySqlDataDriver>()
                              .Dialect<NHibernate.Dialect.MySQL5InnoDBDialect>())
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Order>())
                /*.ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(true, true))*/;

            return config.BuildConfiguration();
        }
    }
}
