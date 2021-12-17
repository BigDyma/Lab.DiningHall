using DinningHall.Domain.Repository;
using DinningHall.Models;
using Newtonsoft.Json;
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
        private static string sendUrl = @"http://localhost:5000/";

        public  async Task SendOrder(Waiter waiter, Order order, Table table)
        {
            using (var client = new HttpClient())
            {
               var res = await PostSendOrder(order, client);

               if (res.StatusCode == HttpStatusCode.OK)
               {
                   Console.WriteLine($"Order {order.Id} was sent.");
                   waiter.State = WaiterState.Available;
               }
               else throw  new Exception("Request was not sent properly");
            }
        }
        private async Task<HttpResponseMessage> PostSendOrder(Order filter, HttpClient httpClient)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, $"{sendUrl}api/Order");

            var convertedJson = JsonConvert.SerializeObject(filter);
            msg.Content = new StringContent(convertedJson, Encoding.UTF8, "Application/*+json");

            var response = await httpClient.SendAsync(msg);

            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
