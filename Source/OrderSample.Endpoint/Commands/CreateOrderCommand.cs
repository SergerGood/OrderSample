using System;
using System.Text;

using Nelibur.ServiceModel.Services.Operations;
using Nelibur.Sword.Extensions;

using Newtonsoft.Json;

using RabbitMQ.Client;

using Service.Contracts.Requests;


namespace Service.Endpoint.Commands
{
    public class CreateOrderCommand : IPostOneWay<CreateOrderRequest>
    {
        public void PostOneWay(CreateOrderRequest request)
        {
            request.ToOption()
                .Map(JsonConvert.SerializeObject)
                .Map(Encoding.UTF8.GetBytes)
                .Do(PushRequest);
        }


        private void PushRequest(byte[] message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    Publish(message, model);
                }
            }
        }


        private static void Publish(byte[] message, IModel model)
        {
            var requestName = "request queue";
            model.QueueDeclare(requestName, true, false, false, null);

            IBasicProperties properties = model.CreateBasicProperties();
            properties.SetPersistent(true);

            model.BasicPublish("", requestName, properties, message);
        }
    }
}
