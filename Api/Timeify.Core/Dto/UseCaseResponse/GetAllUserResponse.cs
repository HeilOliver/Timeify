using System.Collections.Generic;
using Timeify.Api.Shared.Models.Response;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class GetAllUserResponse : UseCaseResponseMessage
    {
        public GetAllUserResponse(bool success = false, string message = null) : base(success, message)
        {
        }

        public List<GetUserResponse> Users { get; set; }
    }
}