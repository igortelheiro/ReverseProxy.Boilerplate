using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeApi.Controllers
{
    [ApiController]
    [Route("api/coffees")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Cappuccino", "Forte", "Gelado"
        };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(_ => Summaries[rng.Next(Summaries.Length)]);
        }
    }
}
