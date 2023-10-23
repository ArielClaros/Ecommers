using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Ecommers.API.Services
{
    public class MessageProducer : IMessagesProducer
    {
        public void SendingMessages<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "fly.rmq.cloudamqp.com",
                UserName = "kijmtwky",
                Password = "CbEnuZwX_ZWxR1JnhERxQxiTOZop1nt4",
                VirtualHost = "kijmtwky"
            };

            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();

            channel.QueueDeclare("bookings", durable: true, exclusive: false);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "bookings", body: body);
        }
    }
}