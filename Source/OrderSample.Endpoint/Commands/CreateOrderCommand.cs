using Nelibur.ServiceModel.Services.Operations;

using OrderSample.Contracts.Requests;


namespace OrderSample.Endpoint.Commands
{
    public class CreateOrderCommand : IPostOneWay<CreateOrderRequest>
    {
        public void PostOneWay(CreateOrderRequest request)
        {
            
        }
    }
}