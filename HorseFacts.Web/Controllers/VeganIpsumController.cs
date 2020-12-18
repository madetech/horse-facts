using HorseFacts.Boundary.Responses;
using HorseFacts.Boundary.UseCaseInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HorseFacts.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeganIpsumController : Controller
    {
        private IUseCase<GetRandomVeganIpsumParagraphResponse> _getRandomVeganIpsumParagraph;

        public VeganIpsumController(IUseCase<GetRandomVeganIpsumParagraphResponse> getRandomVeganIpsumParagraph)
        {
            _getRandomVeganIpsumParagraph = getRandomVeganIpsumParagraph;
        }

        // GET
        [HttpGet("random")]
        public async Task<IActionResult> Random()
        {
            var veganIpsum = await _getRandomVeganIpsumParagraph.Execute();
            return Ok(veganIpsum);
        }
    }
}
