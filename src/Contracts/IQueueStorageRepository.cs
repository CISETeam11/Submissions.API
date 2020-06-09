using System.Threading.Tasks;

namespace Submissions.API.Contracts
{
    public interface IQueueStorageRepository
    {
        Task CreateMessageAsync(string message);
    }
}
