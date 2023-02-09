using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestoAPI.Interfaces;
using TestoAPI.Models;
using System.Text.Json;
using NuGet.Protocol;

namespace TestoDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CaseStudiesController : ControllerBase
    {
        private readonly ICaseStudiesRepository _caseStudiesRepository;

        public CaseStudiesController(ICaseStudiesRepository caseStudiesRepository)
        {
            _caseStudiesRepository = caseStudiesRepository;
        }

        [HttpGet(Name = "GetApi")]
        public CaseStudies Get()
        {
            return _caseStudiesRepository.GetByCaseId(6);
            /*IEnumerable<CaseStudies> allCases =  _caseStudiesRepository.GetAllCaseStudies();
            return allCases;
             IEnumerable<int> result = from value in Enumerable.Range(0, 5)
                                      select value;
            /* return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray(); */
        }

        [HttpPost]
        public string Post([FromQuery] int timeElapsed, [FromQuery] float AverageTimeOnTask)
        {
            return "Data Recieved: " + timeElapsed + ", " + AverageTimeOnTask;
        }
        /* public string Post([FromQuery]string value)
        {

            return "Data Recieved: " + value;
        } */

    }
}
