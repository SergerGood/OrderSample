using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

using Service.DataLayer.Entities;


namespace Service.DataLayer.Maps
{
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Table("[dbo].[Orders]");

            Id(x => x.Id, mapper => mapper.Generator(Generators.Identity));

            Property(x => x.Text);
            Property(x=> x.UserId);
        }
    }
}