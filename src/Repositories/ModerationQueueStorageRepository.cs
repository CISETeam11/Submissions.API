using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using Submissions.API.Contracts;

namespace Submissions.API.Repositories
{
    public class ModerationQueueStorageRepository : IModerationQueueStorageRepository
    {
        private readonly CloudQueue _queue;

        public ModerationQueueStorageRepository(IConfiguration configuration)
        {
            var connectionString = configuration["SeerAzureQueueStorageConnection"];

            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var queueClient = storageAccount.CreateCloudQueueClient();

            _queue = queueClient.GetQueueReference("moderation");
        }

        public Task<CloudQueueMessage> GetQueueMessageAsync()
        {
            return _queue.GetMessageAsync(TimeSpan.FromMinutes(5), null, null);
        }

        public async Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync()
        {
            return await _queue.PeekMessagesAsync(32);
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