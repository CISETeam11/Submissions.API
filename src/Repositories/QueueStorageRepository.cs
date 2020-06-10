using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using Submissions.API.Contracts;

namespace Submissions.API.Repositories
{
    public class QueueStorageRepository : IQueueStorageRepository
    {
        private readonly CloudQueueClient _queueClient;

        public QueueStorageRepository(IConfiguration configuration)
        {
            var connectionString = configuration["SeerAzureQueueStorageConnection"];

            var storageAccount = CloudStorageAccount.Parse(connectionString);

            _queueClient = storageAccount.CreateCloudQueueClient();
        }

        public Task<CloudQueueMessage> GetQueueMessageAsync(string queue)
        {
            return _queueClient.GetQueueReference(queue).GetMessageAsync(TimeSpan.FromMinutes(5), null, null);
        }

        public async Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queue)
        {
            return await _queueClient.GetQueueReference(queue).PeekMessagesAsync(32);
        }

        public async Task CreateMessageAsync(string queue, string message)
        {
            await _queueClient.GetQueueReference(queue).CreateIfNotExistsAsync();

            await _queueClient.GetQueueReference(queue).AddMessageAsync(new CloudQueueMessage(message));
        }

        public async Task DeleteQueueMessageAsync(string queue, string messageId, string popReceipt)
        {
            await _queueClient.GetQueueReference(queue).DeleteMessageAsync(messageId, popReceipt);
        }
    }
}
