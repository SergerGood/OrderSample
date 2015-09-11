using System;


namespace Service.InternalContract
{
    public sealed class MonitoringOrderQueueItem
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public Guid RequestId { get; set; }

        public override string ToString()
        {
            return $"UserId: {UserId} -- Text: {Text}";
        }
    }
}