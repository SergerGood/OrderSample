using System;
using System.Text;

using Nelibur.ServiceModel.Services.Operations;
using Nelibur.Sword.Extensions;

using Newtonsoft.Json;

using OrderSample.QueueClient;

using Service.Contracts.Requests;
using Service.InternalContract;


namespace Service.Endpoint.Commands
{
    public class CreateOrderCommand : IPostOneWay<CreateOrderRequest>
    {
        public void PostOneWay(CreateOrderRequest request)
        {
            request.ToOption()
                .Map(ToQueueItem)
                .Map(JsonConvert.SerializeObject)
                .Map(Encoding.UTF8.GetBytes)
                .Do(PushRequest);
        }


        private void PushRequest(byte[] message)
        {
            using (var channel = new QueueChannel("request queue"))
            {
                channel.Publish(message);
            }
        }


        private OrderQueueItem ToQueueItem(CreateOrderRequest request)
        {
            return new OrderQueueItem
            {
                Text = request.Text,
                UserId = request.UserId,
                RequestId = request.RequestId
            };
        }
    }
}
