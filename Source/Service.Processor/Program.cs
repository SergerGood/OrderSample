using System;
using System.Text;

using Nelibur.Sword.Extensions;

using Newtonsoft.Json;

using OrderSample.QueueClient;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Service.Processor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var channel = new QueueChannel("request queue"))
            {
                var consumer = channel.CreateConsumer();
                while (true)
                {
                    ReceiveMessage(channel, consumer);
                }
            }
        }


        private static void ReceiveMessage(QueueChannel channel, QueueingBasicConsumer consumer)
        {
            BasicDeliverEventArgs deliverEventArgs = consumer.Queue.Dequeue();

            var request = deliverEventArgs.Body
                .ToOption()
                .Map(Encoding.UTF8.GetString)
                .Map(JsonConvert.DeserializeObject<RequestQueueItem>)
                .Value;

            channel.Ack(deliverEventArgs.DeliveryTag);
        }
    }
}
