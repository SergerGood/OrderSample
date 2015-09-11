using System;

using Nelibur.ServiceModel.Clients;

using OrderSample.Client.Properties;

using Service.Contracts.Requests;


namespace OrderSample.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var userId = Guid.NewGuid();

            var client = new JsonServiceClient(Settings.Default.EndpointAddress);

            Console.WriteLine("-> Waiting for create NewOrder. To exit press CTRL+C");
            Console.WriteLine("-> Enter text for create new order");

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                var request = new CreateOrderRequest { Text = line, UserId = userId };
                client.Post(request);
            }
        }
    }
}
