using System;
using System.Runtime.Serialization;


namespace OrderSample.Contracts.Requests
{
    [DataContract]
    public class CreateOrderRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}
