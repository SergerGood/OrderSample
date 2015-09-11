using System;
using System.Text;

using Nelibur.Sword.Extensions;

using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Service.Contracts.Requests;


namespace Service.Processor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    var consumer = new QueueingBasicConsumer(model);
                    model.BasicConsume("request queue", false, consumer);

                    while (true)
                    {
                        ReceiveMessage(model, consumer);
                    }
                }
            }
        }


        private static void ReceiveMessage(IModel model, QueueingBasicConsumer consumer)
        {
            BasicDeliverEventArgs deliverEventArgs = consumer.Queue.Dequeue();

            var request = deliverEventArgs.Body
                .ToOption()
                .Map(Encoding.UTF8.GetString)
                .Map(JsonConvert.DeserializeObject<CreateOrderRequest>)
                .Value;

            model.BasicAck(deliverEventArgs.DeliveryTag, false);
        }
    }
}
