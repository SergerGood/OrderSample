using System;


namespace Service.DataLayer.Entities
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string Text { get; set; }
    }
}