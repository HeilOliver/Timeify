using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Timeify.Api.Shared.Models;
using Timeify.Common.DI;

namespace Timeify.Api.Controllers
{
    [Authorize(Roles = "api_access, user")]
    [Route("api/[controller]")]
    [ApiController]
    [Injectable(InjectableAttribute.LifeTimeType.Hierarchical)]
    public class ProtectedController : ControllerBase
    {
        //// GET api/protected/home
        //[HttpGet]
        //public IActionResult Home()
        //{
        //    return new OkObjectResult(new {result = true});
        //}

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}