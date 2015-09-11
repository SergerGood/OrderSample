using System;


namespace Service.InternalContract
{
    public sealed class OrderQueueItem
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}
