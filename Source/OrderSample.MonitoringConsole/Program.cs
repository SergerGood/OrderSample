using System;
using System.Text;

using Nelibur.Sword.Extensions;

using Newtonsoft.Json;

using OrderSample.QueueClient;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Service.InternalContract;


namespace OrderSample.MonitoringConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var channel = new QueueChannel("monitoring queue"))
            {
                var consumer = channel.CreateConsumer();

                while (true)
                {
                    RecieveOrderInfo(consumer, channel);
                }
            }
        }


        private static void RecieveOrderInfo(QueueingBasicConsumer consumer, QueueChannel channel)
        {
            BasicDeliverEventArgs deliverEventArgs = consumer.Queue.Dequeue();

            var queueItem = GetQueueItem(deliverEventArgs);
            Console.WriteLine(queueItem);

            channel.Ack(deliverEventArgs.DeliveryTag);
        }


        private static MonitoringOrderQueueItem GetQueueItem(BasicDeliverEventArgs args)
        {
            return args.Body
                .ToOption()
                .Map(Encoding.UTF8.GetString)
                .Map(JsonConvert.DeserializeObject<MonitoringOrderQueueItem>)
                .Value;
        }
    }
}
