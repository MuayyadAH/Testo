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
