using System;


namespace Service.InternalContract
{
    public sealed class RequestQueueItem
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}
