using System;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using Submissions.API.Contracts;

namespace Submissions.API.Repositories
{
    public class QueueStorageRepository : IQueueStorageRepository
    {
        private readonly CloudQueue _queue;

        public QueueStorageRepository(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:SeerAzureQueueStorageConnection"];

            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var queueClient = storageAccount.CreateCloudQueueClient();

            _queue = queueClient.GetQueueReference("moderation");
        }

        public Task<CloudQueueMessage> GetQueueMessageAsync()
        {
            return _queue.GetMessageAsync(TimeSpan.FromMinutes(5), null, null);
        }

        public async Task CreateMessageAsync(string message)
        {
            await _queue.CreateIfNotExistsAsync();

            await _queue.AddMessageAsync(new CloudQueueMessage(message));
        }

        public async Task DeleteQueueMessageAsync(string messageId, string popReceipt)
        {
            await _queue.DeleteMessageAsync(messageId, popReceipt);
        }
    }
}