using System;

using Service.DataLayer.Entities;


namespace Service.DataLayer
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
