using System;
using System.Reflection;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

using Environment = NHibernate.Cfg.Environment;


namespace Service.DataLayer
{
    public class SessionFactory
    {
        private static ISessionFactory factory;


        public static void Create()
        {
            var configuration = CreatConfiguration();

            factory = configuration.BuildSessionFactory();
        }


        private static Configuration CreatConfiguration()
        {
            ModelMapper mapper = GetMapper();
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.SetProperty(Environment.Dialect, "NHibernate.Dialect.MsSql2012Dialect");
            configuration.SetProperty(Environment.ConnectionString, @"Data Source=.\sqlexpress;Initial Catalog=OrdersDatabase;Integrated Security=True");
            configuration.AddMapping(mapping);

            return configuration;
        }


        private static ModelMapper GetMapper()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            return mapper;
        }


        public static ISession OpenSession()
        {
            return factory.OpenSession();
        }
    }
}
