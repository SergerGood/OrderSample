using System;
using System.IO;

using Nelibur.ServiceModel.Clients;

using OrderSample.FileWatcher.Properties;

using Service.Contracts.Requests;


namespace OrderSample.FileWatcher
{
    internal class Program
    {
        private static JsonServiceClient client;
        private static readonly Guid userId = Guid.NewGuid();


        private static void Main(string[] args)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            client = new JsonServiceClient(Settings.Default.EndpointAddress);

            Console.WriteLine("UserId: {0}", userId);
            Console.WriteLine("-> Waiting for create NewOrder. To exit press CTRL+C");
            Console.WriteLine("-> Put new file in folder {0}", directory);

            var watcher = new FileSystemWatcher
            {
                Path = directory,
                Filter = "*.txt"
            };

            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            while (Console.ReadLine() != null)
            {
            }
        }


        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string text = File.ReadAllText(e.FullPath);

            var request = new CreateOrderRequest { Text = text, UserId = userId, RequestId = Guid.NewGuid()};
            client.Post(request);
        }
    }
}
