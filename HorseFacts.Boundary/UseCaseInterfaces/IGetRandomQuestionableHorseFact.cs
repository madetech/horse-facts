using HorseFacts.Boundary.Responses;

namespace HorseFacts.Boundary.UseCaseInterfaces
{
    public interface IGetRandomQuestionableHorseFact
    {
        GetRandomQuestionableHorseFactResponse Execute();
    }
}
