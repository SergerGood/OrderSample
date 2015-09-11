using System;
using System.Text;

using Nelibur.Sword.Extensions;

using Newtonsoft.Json;

using OrderSample.Logger.DataLayer;
using OrderSample.Logger.DataLayer.Entities;
using OrderSample.QueueClient;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Service.InternalContract;


namespace OrderSample.Logger
{
    internal class Program
    {
        private static readonly OrderRepository orderRepository = new OrderRepository();


        private static void Main(string[] args)
        {
            using (var channel = new QueueChannel("log queue"))
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

            LogOrderQueueItem queueItem = GetQueueItem(deliverEventArgs);

            queueItem.ToOption()
                .Map(CreateOrderEntitiy)
                .Do(LogOrder);

            channel.Ack(deliverEventArgs.DeliveryTag);
        }


        private static void LogOrder(Order order)
        {
            orderRepository.Log(order);
        }


        private static Order CreateOrderEntitiy(LogOrderQueueItem queueItem)
        {
            return new Order
            {
                Text = queueItem.Text,
                UserId = queueItem.UserId,
                Id = Guid.NewGuid()
            };
        }


        private static LogOrderQueueItem GetQueueItem(BasicDeliverEventArgs deliverEventArgs)
        {
            return deliverEventArgs.Body
                .ToOption()
                .Map(Encoding.UTF8.GetString)
                .Map(JsonConvert.DeserializeObject<LogOrderQueueItem>)
                .Value;
        }
    }
}
