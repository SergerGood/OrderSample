using System;
using System.Runtime.Serialization;


namespace Service.Contracts.Requests
{
    [DataContract]
    public class CreateOrderRequest
    {
        [DataMember]
        public Guid RequestId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
