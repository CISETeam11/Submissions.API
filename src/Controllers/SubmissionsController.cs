using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Submissions.API.Contracts;

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
    }
}