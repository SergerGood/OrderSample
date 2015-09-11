using System;

using Nelibur.ServiceModel.Clients;
using Nelibur.Sword.Extensions;

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

            Console.WriteLine("UserId: {0}", userId);
            Console.WriteLine("-> Waiting for create NewOrder. To exit press CTRL+C");
            Console.WriteLine("-> Enter text for create new order");

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                100.Times(x =>
                {
                    var request = new CreateOrderRequest { Text = line + x, UserId = userId , RequestId =  Guid.NewGuid()};
                    client.Post(request);
                });
            }
        }
    }
}
