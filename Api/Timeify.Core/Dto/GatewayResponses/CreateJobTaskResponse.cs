using System.Collections.Generic;

namespace Timeify.Core.Dto.GatewayResponses
{
    public class CreateJobTaskResponse : BaseGatewayResponse
    {
        public CreateJobTaskResponse(int id, bool success = false, IEnumerable<ApplicationError> errors = null) : base(
            success, errors)
        {
            Id = id;
        }

        public int Id { get; }
    }
}