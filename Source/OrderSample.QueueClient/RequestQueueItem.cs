using System;


namespace OrderSample.QueueClient
{
    public sealed class RequestQueueItem
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}
