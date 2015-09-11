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
    public class OrderProcessor
    {
        private readonly QueueChannel channel;
        private readonly QueueingBasicConsumer consumer;
        private readonly IOrderRepository orderRepository;


        public OrderProcessor(QueueChannel channel, QueueingBasicConsumer consumer, IOrderRepository orderRepository)
        {
            this.channel = channel;
            this.consumer = consumer;
            this.orderRepository = orderRepository;
        }


        public void Process()
        {
            BasicDeliverEventArgs deliverEventArgs = consumer.Queue.Dequeue();


            deliverEventArgs.Body
                .ToOption()
                .Map(Encoding.UTF8.GetString)
                .Map(JsonConvert.DeserializeObject<OrderQueueItem>)
                .Map(CreateOrderEntity)
                .Do(SaveItem)
                .Do(NotifyMonitoringConsole)
                .Do(NotifyLogger);


            channel.Ack(deliverEventArgs.DeliveryTag);
        }

        private void NotifyLogger(Order order)
        {
            order.ToOption()
                .Map(CreateLogQueueItem)
                .Map(JsonConvert.SerializeObject)
                .Map(Encoding.UTF8.GetBytes)
                .Do(PublishToLogger);
        }


        private void NotifyMonitoringConsole(Order order)
        {
            order.ToOption()
                .Map(CreateMonitoringQueueItem)
                .Map(JsonConvert.SerializeObject)
                .Map(Encoding.UTF8.GetBytes)
                .Do(PublishToMonitoring);
        }


        private Order CreateOrderEntity(OrderQueueItem queueItem)
        {
            return new Order
            {
                Text = queueItem.Text,
                UserId = queueItem.UserId
            };
        }


        private void SaveItem(Order order)
        {
            orderRepository.Save(order);
        }


        private void PublishToLogger(byte[] message)
        {
            using (var logChannel = new QueueChannel("log queue"))
            {
                logChannel.Publish(message);
            }
        }


        private void PublishToMonitoring(byte[] message)
        {
            using (var monitoringChannel = new QueueChannel("monitoring queue"))
            {
                monitoringChannel.Publish(message);
            }
        }


        private LogOrderQueueItem CreateLogQueueItem(Order order)
        {
            return new LogOrderQueueItem
            {
                Text = order.Text,
                UserId = order.UserId
            };
        }


        private MonitoringOrderQueueItem CreateMonitoringQueueItem(Order order)
        {
            return new MonitoringOrderQueueItem
            {
                Text = order.Text,
                UserId = order.UserId
            };
        }
    }
}
