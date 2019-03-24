using System.Threading.Tasks;
using Timeify.Core.Dto.GatewayResponses;
using Timeify.Core.Entities;

namespace Timeify.Core.Interfaces.Gateways
{
    public interface IUserRepository : ICrudRepository<UserEntity>
    {
        Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName,
            string password);

        Task<UserEntity> FindByName(string userName);

        Task<bool> CheckPassword(UserEntity user, string password);
    }
}