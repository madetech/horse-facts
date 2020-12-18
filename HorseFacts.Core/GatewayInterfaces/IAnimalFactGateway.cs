using HorseFacts.Core.Domain;
using System.Threading.Tasks;

namespace HorseFacts.Core.GatewayInterfaces
{
    public interface IAnimalFactGateway
    {
        Task<AnimalFact> GetAnimalFact();
    }
}