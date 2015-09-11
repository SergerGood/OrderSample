using System;

using NHibernate;

using Service.DataLayer.Entities;


namespace Service.DataLayer
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
