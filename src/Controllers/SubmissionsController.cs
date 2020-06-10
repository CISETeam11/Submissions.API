using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Submissions.API.Contracts;
using Submissions.API.Models;

namespace Submissions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SubmissionsController : ControllerBase
    {
        private readonly IQueueStorageRepository _queueStorageRepository;

        public SubmissionsController(IQueueStorageRepository queueStorageRepository)
        {
            _queueStorageRepository = queueStorageRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ModerationQueueMessage), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModerationQueueMessageAsync()
        {
            var queueMessage = await _queueStorageRepository.GetQueueMessageAsync();

            var moderationQueueMessage = new ModerationQueueMessage
            {
                Id = queueMessage.Id,
                PopReceipt = queueMessage.PopReceipt,
                Article = JsonSerializer.Deserialize<Article>(queueMessage.AsString)
            };

            return Ok(moderationQueueMessage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateModerationQueueMessageAsync([FromBody] [BindRequired] Article article)
        {
            await _queueStorageRepository.CreateMessageAsync(JsonSerializer.Serialize(article));

            return Ok(article);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteModerationQueueMessageAsync([FromBody] [BindRequired] QueueMessage queueMessage)
        {
            await _queueStorageRepository.DeleteQueueMessageAsync(queueMessage.Id, queueMessage.PopReceipt);

            return NoContent();
        }
    }
}