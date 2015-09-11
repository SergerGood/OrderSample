using System;

using Service.Processor.DataLayer.Entities;


namespace Service.Processor.DataLayer
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
