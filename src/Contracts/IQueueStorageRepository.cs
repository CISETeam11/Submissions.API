using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Queue;

namespace Submissions.API.Contracts
{
    public interface IQueueStorageRepository
    {
        Task<CloudQueueMessage> GetQueueMessageAsync(string queue);
        Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queue);
        Task CreateMessageAsync(string queue, string message);
        Task DeleteQueueMessageAsync(string queue, string messageId, string popReceipt);
    }
}