using System;

using NHibernate;

using Service.Processor.DataLayer.Entities;


namespace Service.Processor.DataLayer
{
    public class OrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(order);
                    transaction.Commit();
                }
            }
        }
    }
}
