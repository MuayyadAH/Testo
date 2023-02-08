using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestoDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class ApiController : ControllerBase
    {

        [HttpGet(Name = "GetDashboard")]
        public IEnumerable<int> Get()
        {
            IEnumerable<int> result = from value in Enumerable.Range(0, 5)
                                      select value;
            return result.ToArray();
            /* return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray(); */
        }

        [HttpPost]
        public string Post([FromBody]string value)
        {
            return "Data Recieved: " + value;
        }


    }
}
