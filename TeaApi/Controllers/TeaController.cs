using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeaApi.Controllers
{
    [ApiController]
    [Route("api/teas")]
    public class TeaController : ControllerBase
    {
        private static readonly string[] Teas = new[]
        {
            "Camomila", "Frutas Vermelhas", "Ervas", "Cogumelo"
        };

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(_ => Teas[rng.Next(Teas.Length)]);
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
