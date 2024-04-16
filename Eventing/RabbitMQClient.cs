using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using NeoEditAPI.Models;

namespace NeoEdit.Api.Eventing
{
    public class RabbitMQClient
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQClient(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "documents", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void PublishDocumentCreated(Document document)
        {
            var message = JsonSerializer.Serialize(document);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: "documents", basicProperties: null, body: body);
        }

        public void Close()
        {
            _connection.Close();
        }
    }

}
