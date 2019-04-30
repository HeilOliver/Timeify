using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class GetAllJobRequest : IUseCaseRequest<GetAllJobResponse>
    {
        public GetAllJobRequest(int page, int pageSize, string callerId)
        {
            Page = page;
            PageSize = pageSize;
            CallerId = callerId;
        }

        public int Page { get; }

        public int PageSize { get; }

        public string CallerId { get; }
    }
}