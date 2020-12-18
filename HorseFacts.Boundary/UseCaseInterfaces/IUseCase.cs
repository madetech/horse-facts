using System.Threading.Tasks;

namespace HorseFacts.Boundary.UseCaseInterfaces
{
    public interface IUseCase<TResponse>
    {
        Task<TResponse> Execute();
    }
}
