using Azure.Messaging.ServiceBus;
using AzureOperationsMessages;
using System.Text.Json;

namespace AzureOperationsLib
{
    public interface IServicebusQueue
    {
        Task SendMessage(string data);
        Task SendMessage(QueueMessage data);
    }
    public class ServicebusQueue:IServicebusQueue
    {
        private string _connectionString;
        private string _queueName;
        public ServicebusQueue(AzureConfigData azureConfigData)
        {
            _connectionString = azureConfigData.ServiceBusConnectionString;
            _queueName = azureConfigData.ServiceBusQueue;
        }
        public async Task SendMessage(string data)
        {
            await using (var client = new ServiceBusClient(_connectionString))
            {
                await using (var sender = client.CreateSender(_queueName))
                {
                    var msg = new ServiceBusMessage(data);
                    await sender.SendMessageAsync(msg);
                }
            }
        }

        public async Task SendMessage(QueueMessage data)
        {
            await using (var client = new ServiceBusClient(_connectionString))
            {
                await using (var sender = client.CreateSender(_queueName))
                {
                    var jsonData = JsonSerializer.Serialize(data);
                    var msg = new ServiceBusMessage(jsonData);
                    await sender.SendMessageAsync(msg);
                }
            }
        }
    }
}