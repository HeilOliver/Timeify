using System.Collections.Generic;

namespace Timeify.Core.Dto.GatewayResponses
{
    public abstract class BaseGatewayResponse
    {
        protected BaseGatewayResponse(bool success = false, IEnumerable<ApplicationError> errors = null)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; }
        public IEnumerable<ApplicationError> Errors { get; }
    }
}