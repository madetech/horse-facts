using HorseFacts.Boundary.Responses;
using System.Threading.Tasks;

namespace HorseFacts.Boundary.UseCaseInterfaces
{
    public interface IGetRandomQuestionableHorseFact
    {
        Task<GetRandomQuestionableHorseFactResponse> Execute();
    }
}
