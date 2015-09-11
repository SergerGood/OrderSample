using System;
using System.ServiceModel.Web;

using Nelibur.ServiceModel.Services;
using Nelibur.ServiceModel.Services.Default;

using Service.Contracts.Requests;
using Service.Endpoint.Commands;


namespace Service.Endpoint
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Configure();

            var service = new WebServiceHost(typeof(JsonServicePerCall));
            service.Open();

            Console.ReadLine();
        }


        private static void Configure()
        {
            NeliburRestService.Configure(x => x.Bind<CreateOrderRequest, CreateOrderCommand>());
        }
    }
}
