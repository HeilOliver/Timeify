using Timeify.Core.Dto;

namespace Timeify.Core.Services
{
    public interface IApplicationErrorFactory
    {
        ApplicationError ResourceNotFound { get; }

        ApplicationError ResourceNotOwned { get; }

        ApplicationError InvalidUser { get; }

        ApplicationError LoginFailed { get; }

        ApplicationError ChangeNotAllowed { get; }
    }
}