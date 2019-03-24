using Microsoft.AspNetCore.Mvc;
using Timeify.Common.DI;

namespace Timeify.Api.Presenter
{
    [Injectable]
    public sealed class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}