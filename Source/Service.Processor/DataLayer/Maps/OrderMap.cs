using System;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

using Service.Processor.DataLayer.Entities;


namespace Service.Processor.DataLayer.Maps
{
    public class OrderMap : ClassMapping<Order>
    {
        public OrderMap()
        {
            Table("[dbo].[Orders]");

            Id(x => x.Id, mapper => mapper.Generator(Generators.Identity));

            Property(x => x.Text);
            Property(x => x.UserId);
            Property(x => x.RequestId);
        }
    }
}
