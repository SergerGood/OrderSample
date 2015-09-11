using System;


namespace Service.InternalContract
{
    public sealed class MonitoringOrderQueueItem
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}