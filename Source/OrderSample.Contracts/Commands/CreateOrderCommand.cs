using System;


namespace OrderSample.Contracts.Commands
{
    public class CreateOrderCommand
    {
        public Order NewOrder { get; set; }
    }
}
