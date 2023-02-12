using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TestoAPI.Interfaces;
using TestoAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TestCasesController : ControllerBase
    {
        private readonly ITestCasesRepository _testCasesRepository;

        public TestCasesController(ITestCasesRepository testCasesRepository)
        {
            _testCasesRepository = testCasesRepository;
        }
        // GET: api/<TestCasesController>
        [HttpGet("Tasks")]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var tasks = await _testCasesRepository.GetAllTestCasesOnly();
            return Ok(tasks);
        }

        [HttpGet("Links")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllLinks()
        {
            var links = await _testCasesRepository.GetAllLinksOnly();
            return Ok(links);
        }

        // GET api/<TestCasesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestCasesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestCasesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestCasesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
