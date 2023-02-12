using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TestoAPI.Interfaces;
using TestoAPI.Models;
using TestoAPI.ParaModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultsRepository _testResultsRepository;

        public TestResultsController(ITestResultsRepository testResultsRepository)
        {
            _testResultsRepository = testResultsRepository;
        }
        // GET: api/<TestResultsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestResultsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestResultsController>
        [HttpPost("Send")]
        /*
        public void Post([FromBody] int timeElapsed, [FromBody] int averageTimeOnTask, [FromBody] string[] visitedSites,
                         [FromBody] int errors, [FromBody] int userClicks, [FromBody] float taskSuccess, [FromBody] float scrollDepth) */
        public IActionResult Post([FromBody] AcceptTestResult recievedTestResult)
        {
            TestResults testResult = new TestResults()
            {
                TimeElapsed = recievedTestResult.TimeElapsed,
                AverageTimeOnTask = recievedTestResult.AverageTimeOnTask,
                VisitedSites = string.Join(",", recievedTestResult.VisitedSites),
                Errors = recievedTestResult.Errors,
                UserClicks = recievedTestResult.UserClicks,
                TaskSucessRate = recievedTestResult.TaskSucessRate,
                ScrollDepthRate = recievedTestResult.ScrollDepthRate
            };
            _testResultsRepository.Add(testResult);
            return Ok();
        }

        // PUT api/<TestResultsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestResultsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
