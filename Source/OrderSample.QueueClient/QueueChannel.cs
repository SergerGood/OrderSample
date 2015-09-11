using System;

using RabbitMQ.Client;


namespace OrderSample.QueueClient
{
    public class QueueChannel : IDisposable
    {
        private readonly string queueName;
        private IConnection connection;
        private IModel model;


        public QueueChannel(string queueName)
        {
            ConfigureChannel();
            this.queueName = queueName;
        }


        public void Dispose()
        {
            model?.Close();
            connection?.Close();
        }


        public IModel GetModel()
        {
            return model;
        }


        public void Publish(byte[] message)
        {
            model.QueueDeclare(queueName, true, false, false, null);

            IBasicProperties properties = model.CreateBasicProperties();
            properties.SetPersistent(true);

            model.BasicPublish("", queueName, properties, message);
        }


        public QueueingBasicConsumer CreateConsumer()
        {
            var consumer = new QueueingBasicConsumer(model);
            model.BasicConsume(queueName, false, consumer);

            return consumer;
        }


        public void Ack(ulong deliveryTag)
        {
            model.BasicAck(deliveryTag, false);
        }


        private void ConfigureChannel()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            connection = factory.CreateConnection();
            model = connection.CreateModel();
        }
    }
}
