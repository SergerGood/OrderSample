using System;
using System.ServiceModel.Web;

using Nelibur.ServiceModel.Services;
using Nelibur.ServiceModel.Services.Default;

using OrderSample.Contracts.Requests;
using OrderSample.Endpoint.Commands;


namespace OrderSample.Endpoint
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NeliburRestService.Configure(x => x.Bind<CreateOrderRequest, CreateOrderCommand>());

            var service = new WebServiceHost(typeof(JsonServicePerCall));
            service.Open();

            Console.ReadLine();
        }
    }
}
