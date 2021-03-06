﻿using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HorseFacts.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactsController : Controller
    {
        private IUseCase<GetRandomQuestionableHorseFactResponse> _getRandomQuestionableHorseFact;

        public FactsController(IUseCase<GetRandomQuestionableHorseFactResponse> getRandomQuestionableHorseFact)
        {
            _getRandomQuestionableHorseFact = getRandomQuestionableHorseFact;
        }

        // GET
        [HttpGet("random")]
        public async Task<IActionResult> Random()
        {
            var horseFact = await _getRandomQuestionableHorseFact.Execute();
            return Ok(horseFact);
        }
    }
}
