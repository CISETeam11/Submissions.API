using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Azure.Storage.Queue;
using Submissions.API.Contracts;
using Submissions.API.Models;

namespace Submissions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ModerationController : ControllerBase
    {
        private readonly IQueueStorageRepository _queueStorageRepository;
        private const string ModerationQueue = "moderation";

        public ModerationController(IQueueStorageRepository queueStorageRepository)
        {
            _queueStorageRepository = queueStorageRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ModerationQueueMessage), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModerationQueueMessageAsync()
        {
            var queueMessage = await _queueStorageRepository.GetQueueMessageAsync(ModerationQueue);

            var moderationQueueMessage = new ModerationQueueMessage
            {
                Id = queueMessage.Id,
                PopReceipt = queueMessage.PopReceipt,
                Article = JsonSerializer.Deserialize<Article>(queueMessage.AsString)
            };

            return Ok(moderationQueueMessage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateModerationQueueMessageAsync([FromBody] [BindRequired] Article article)
        {
            await _queueStorageRepository.CreateMessageAsync(ModerationQueue, JsonSerializer.Serialize(article));

            return Ok(article);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteModerationQueueMessageAsync([FromBody] [BindRequired] QueueMessage queueMessage)
        {
            await _queueStorageRepository.DeleteQueueMessageAsync(ModerationQueue, queueMessage.Id, queueMessage.PopReceipt);

            return NoContent();
        }

        [HttpGet("submissions")]
        [ProducesResponseType(typeof(IEnumerable<CloudQueueMessage>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetModerationQueueMessagesAsync()
        {
            return Ok(await _queueStorageRepository.GetQueueMessagesAsync(ModerationQueue));
        }
    }
}