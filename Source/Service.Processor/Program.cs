using System;

using OrderSample.QueueClient;

using Service.DataLayer;


namespace Service.Processor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SessionFactory.Create();
            IOrderRepository orderRepository = new OrderRepository();

            using (var channel = new QueueChannel("request queue"))
            {
                var consumer = channel.CreateConsumer();

                var processor = new OrderProcessor(channel, consumer, orderRepository);
                while (true)
                {
                    processor.Process();
                }
            }
        }
    }
}
