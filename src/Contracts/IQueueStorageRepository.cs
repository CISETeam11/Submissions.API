using System.Threading.Tasks;
using Microsoft.Azure.Storage.Queue;

namespace Submissions.API.Contracts
{
    public interface IQueueStorageRepository
    {
        Task<CloudQueueMessage> GetQueueMessageAsync();
        Task CreateMessageAsync(string message);
        Task DeleteQueueMessageAsync(string messageId, string popReceipt);
    }
}