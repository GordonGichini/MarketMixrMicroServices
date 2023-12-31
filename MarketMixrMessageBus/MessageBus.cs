using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMixrMessageBus
{
    public class MessageBus : IMessageBus
    {

            private readonly string connectionString = "Endpoint=sb://marketmixrbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5T3OdXP3IEoNeV4nTZK8uyyd8sD5eq4HP+ASbF+HGlI=";
            public async Task PublishMessage(object message, string Topic_Queue_Name)
            {
                //create a client 
                var client = new ServiceBusClient(connectionString);

                ServiceBusSender sender = client.CreateSender(Topic_Queue_Name);

                //convert to Json
                var body = JsonConvert.SerializeObject(message);

                ServiceBusMessage theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(body))
                {
                    CorrelationId = Guid.NewGuid().ToString(),
                };

                //send the message 
                await sender.SendMessageAsync(theMessage);

                //free the Resources/Clean uP
                await sender.DisposeAsync();
            }
    }

}
