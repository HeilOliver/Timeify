using System.Collections.Generic;

namespace Timeify.Core.Dto.GatewayResponses
{
    public sealed class CreateUserResponse : BaseGatewayResponse
    {
        public CreateUserResponse(string id, bool success = false, IEnumerable<ApplicationError> errors = null) : base(
            success, errors)
        {
            Id = id;
        }

        public string Id { get; }
    }
}