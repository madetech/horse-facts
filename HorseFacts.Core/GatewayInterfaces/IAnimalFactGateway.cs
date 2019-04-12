using HorseFacts.Core.Domain;

namespace HorseFacts.Core.GatewayInterfaces
{
    public interface IAnimalFactGateway
    {
        AnimalFact GetAnimalFact();
    }
}