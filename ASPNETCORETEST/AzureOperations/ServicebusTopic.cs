using Azure.Messaging.ServiceBus;

namespace AzureOperationsLib
{
    public interface IServicebusTopic
    {
        void SendMessage(string data);
    }
    public class ServicebusTopic: IServicebusTopic
    {
        private string _connectionString;
        private string _queueName;
        public ServicebusTopic(AzureConfigData azureConfigData)
        {
            _connectionString = azureConfigData.ServiceBusConnectionString;
            _queueName = azureConfigData.ServiceBusQueue;
        }
        public async void SendMessage(string data)
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
    }
}