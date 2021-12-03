using DinningHall.Domain.Repository;
using DinningHall.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DinningHall.Service
{
    public class RequestService : IRequestService
    {
        private static string sendUrl = "http://localhost:8000/";

        public void SendOrder(Waiter waiter, Order order, Table table)
        {
            using (var client = new HttpClient())
            {
                var message = JsonSerializer.Serialize(order);
                var response = client.PostAsync(sendUrl + "order", new StringContent(message, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine($"Order {order.Id} was sent.");
                    waiter.State = WaiterState.Available;
                }
            }
        }
    }
}
