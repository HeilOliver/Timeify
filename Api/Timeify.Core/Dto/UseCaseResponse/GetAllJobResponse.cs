using System.Collections.Generic;
using Timeify.Api.Shared.Models.Response;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class GetAllJobResponse : UseCaseResponseMessage
    {
        public GetAllJobResponse(bool success = false, string message = null) : base(
            success, message)
        {
        }

        public GetAllJobResponse(IEnumerable<Job> jobs, bool success = true, string message = null) : base(
            success, message)
        {
            Jobs = jobs;
        }

        public IEnumerable<Job> Jobs { get; }
    }
}