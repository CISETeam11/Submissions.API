namespace Submissions.API.Contracts
{
    public interface IQueueMessage
    {
        string Id { get; set; }

        string PopReceipt { get; set; }
    }
}