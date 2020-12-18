using HorseFacts.Core.Domain;
using System.Threading.Tasks;

namespace HorseFacts.Core.GatewayInterfaces
{
    public interface IFoodIpsumGateway
    {
        Task<FoodIpsum> GetFoodIpsum();
    }
}