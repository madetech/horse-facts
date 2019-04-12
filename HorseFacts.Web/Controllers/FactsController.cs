using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HorseFacts.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactsController : Controller
    {
        private IGetRandomQuestionableHorseFact _getRandomQuestionableHorseFact;

        public FactsController(IGetRandomQuestionableHorseFact getRandomQuestionableHorseFact)
        {
            _getRandomQuestionableHorseFact = getRandomQuestionableHorseFact;
        }

        // GET
        [HttpGet("random")]
        public IActionResult Random()
        {
            return Ok(_getRandomQuestionableHorseFact.Execute());
        }
    }
}
