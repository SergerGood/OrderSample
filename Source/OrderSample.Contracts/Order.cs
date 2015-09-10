using System;


namespace OrderSample.Contracts
{
    public sealed class Order
    {
        public Order(Guid userId, string text)
        {
            UserId = userId;
            Text = text;
        }


        public Guid UserId { get; private set; }

        public string Text { get; private set; }
    }
}
