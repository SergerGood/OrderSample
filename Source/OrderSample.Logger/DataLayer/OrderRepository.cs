using System;

using MongoDB.Driver;

using OrderSample.Logger.DataLayer.Entities;


namespace OrderSample.Logger.DataLayer
{
    public class OrderRepository
    {
        private readonly IMongoDatabase database;


        public OrderRepository()
        {
            IMongoClient client = new MongoClient();
            database = client.GetDatabase("OrderDatabase");
        }


        public void Log(Order order)
        {
            var collection = GetOrdersCollection();
            collection.InsertOneAsync(order).Wait();
        }


        private IMongoCollection<Order> GetOrdersCollection()
        {
            return database.GetCollection<Order>("orders");
        }
    }
}
