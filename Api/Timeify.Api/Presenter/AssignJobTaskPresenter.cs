﻿using System.Net;
using Timeify.Api.Serialization;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Api.Presenter
{
    [Injectable]
    public class AssignJobTaskPresenter : IOutputPort<AssignJobTaskResponse>
    {
        public AssignJobTaskPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public JsonContentResult ContentResult { get; }

        public void Handle(AssignJobTaskResponse response)
        {
            ContentResult.StatusCode =
                (int) (response.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
            ContentResult.Content = response.Success
                ? JsonSerializer.SerializeObject(response)
                : JsonSerializer.SerializeObject(response.Errors);
        }
    }
}