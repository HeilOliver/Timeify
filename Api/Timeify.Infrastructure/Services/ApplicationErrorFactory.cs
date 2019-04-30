using Timeify.Common.DI;
using Timeify.Core.Dto;
using Timeify.Core.Services;

namespace Timeify.Infrastructure.Services
{
    [Injectable(typeof(IApplicationErrorFactory), InjectableAttribute.LifeTimeType.Container)]
    public class ApplicationErrorFactory : IApplicationErrorFactory
    {
        public ApplicationError ResourceNotFound =>
            new ApplicationError("resource_not_found", "No Resource with the given id is found");

        public ApplicationError ResourceNotOwned =>
            new ApplicationError("not_resource_owner", "You are not the owner of this resource");

        public ApplicationError InvalidUser =>
            new ApplicationError("user_invalid", "Invalid User");

        public ApplicationError LoginFailed =>
            new ApplicationError("login_failure", "Invalid username or password.");

        public ApplicationError ChangeNotAllowed =>
            new ApplicationError("resource_change_not_allowed", "You are not allowed to change this resource");
    }
}