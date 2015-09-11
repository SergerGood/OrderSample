using System;
using System.Text;

using Nelibur.Sword.Extensions;

using Newtonsoft.Json;

using OrderSample.QueueClient;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Service.DataLayer;
using Service.DataLayer.Entities;
using Service.InternalContract;


namespace Service.Processor
{
    internal class Program
    {
        private static IOrderRepository orderRepository;

        private static void Main(string[] args)
        {
            SessionFactory.Create();
            orderRepository = new OrderRepository();

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

            deliverEventArgs.Body
                .ToOption()
                .Map(Encoding.UTF8.GetString)
                .Map(JsonConvert.DeserializeObject<OrderQueueItem>)
                .Map(ToEntity)
                .Do(SaveItem);

            channel.Ack(deliverEventArgs.DeliveryTag);
        }

        private static Order ToEntity(OrderQueueItem queueItem)
        {
            return new Order
            {
                Text = queueItem.Text,
                UserId = queueItem.UserId
            };
        }


        private static void SaveItem(Order order)
        {
            orderRepository.Save(order);
        }
    }
}
