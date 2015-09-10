using System;

using Nelibur.ServiceModel.Clients;

using OrderSample.Client.Properties;
using OrderSample.Contracts;
using OrderSample.Contracts.Commands;


namespace OrderSample.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var userId = Guid.NewGuid();

            var client = new JsonServiceClient(Settings.Default.EndpointAddress);

            Console.WriteLine("-> Waiting for create NewOrder. To exit press CTRL+C");
            Console.WriteLine("-> Enter new order text");

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                client.Post<Order>(new CreateOrderCommand { NewOrder = new Order(userId, line)});
            }
        }
    }
}
