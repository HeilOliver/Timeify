using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Timeify.Common.DI;
using Timeify.Core.Dto;
using Timeify.Core.Dto.GatewayResponses;
using Timeify.Core.Entities;
using Timeify.Core.Entities.Specifications;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Infrastructure.Context.Identity;

namespace Timeify.Infrastructure.Context.App
{
    [Injectable(typeof(IUserRepository), InjectableAttribute.LifeTimeType.Hierarchical)]
    internal sealed class UserRepository : CrudRepository<UserEntity>, IUserRepository
    {
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;


        public UserRepository(UserManager<AppUser> userManager, IMapper mapper, AppDbContext context) : base(context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName,
            string password)
        {
            var appUser = new AppUser {Email = email, UserName = userName};
            var identityResult = await userManager.CreateAsync(appUser, password);
            var roleResult = await userManager.AddToRoleAsync(appUser, "user");

            if (!identityResult.Succeeded || !roleResult.Succeeded)
                return new CreateUserResponse(appUser.Id, false,
                    identityResult.Errors.Select(e => new ApplicationError(e.Code, e.Description)));

            var user = new UserEntity(firstName, lastName, appUser.Id, appUser.UserName);

            Context.Users.Add(user);
            await Context.SaveChangesAsync();

            return new CreateUserResponse(appUser.Id, identityResult.Succeeded,
                identityResult.Succeeded
                    ? null
                    : identityResult.Errors.Select(e => new ApplicationError(e.Code, e.Description)));
        }

        public async Task<UserEntity> FindByName(string userName)
        {
            var appUser = await userManager.FindByNameAsync(userName);
            return appUser == null
                ? null
                : mapper.Map(appUser, await GetSingleBySpec(new UserSpecification(appUser.Id)));
        }

        public async Task<bool> CheckPassword(UserEntity user, string password)
        {
            return await userManager.CheckPasswordAsync(mapper.Map<AppUser>(user), password);
        }
    }
}