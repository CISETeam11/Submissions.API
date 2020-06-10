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
    public class AnalysisController : ControllerBase
    {
        private readonly IQueueStorageRepository _queueStorageRepository;
        private const string ModerationQueue = "moderation";
        private const string AnalysisQueue = "analysis";

        public AnalysisController(IQueueStorageRepository queueStorageRepository)
        {
            _queueStorageRepository = queueStorageRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAnalysisQueueMessageAsync([FromBody] [BindRequired] ModerationQueueMessage moderationQueueMessage)
        {
            await _queueStorageRepository.DeleteQueueMessageAsync(ModerationQueue, moderationQueueMessage.Id, moderationQueueMessage.PopReceipt);

            await _queueStorageRepository.CreateMessageAsync(AnalysisQueue, JsonSerializer.Serialize(moderationQueueMessage.Article));

            return Ok(moderationQueueMessage);
        }

        [HttpGet("submissions")]
        [ProducesResponseType(typeof(IEnumerable<CloudQueueMessage>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAnalysisQueueMessagesAsync()
        {
            return Ok(await _queueStorageRepository.GetQueueMessagesAsync(AnalysisQueue));
        }
    }
}