using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TestoMVC.Interfaces;
using TestoMVC.Models;
using TestoMVC.ViewModels;

namespace TestoMVC.Controllers
{
    public class TestResultsController : Controller
    {
        private readonly ITestResultsRepository _testResultsRepository;

        public TestResultsController(ITestResultsRepository testResultsRepository)
        {
            _testResultsRepository = testResultsRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<TestResults> alltestResults = _testResultsRepository.GetAll();
            return View(alltestResults);
        }

        [HttpGet]
        public IActionResult DeleteAll()
        {
            _testResultsRepository.DeleteAll();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Export(int id)
        {
            TestResults getResult = await _testResultsRepository.GetByIdAsync(id);
            var csv = new StringBuilder();

            // Write the header row
            csv.AppendLine("Time Elapsed,Average Time Elapsed,Errors,User Clicks,Task Success Rate,Scroll Depth Rate");

            csv.AppendLine($"{getResult.TimeElapsed},{getResult.AverageTimeOnTask}," +
                           $"{getResult.Errors},{getResult.UserClicks},{getResult.TaskSucessRate},{getResult.ScrollDepthRate}");

            for(int i=0; i<5; i++) { csv.AppendLine(); }

            csv.AppendLine("Site #,Full URL,Base URL,Time on Website");

            string? json = getResult.VisitedSites;
            List<JsonVisitedSites> savedSites = JsonConvert.DeserializeObject<List<JsonVisitedSites>>(json);
            for (int i = 1; i < savedSites.Count() + 1; i++)
            {
                csv.AppendLine($"{i},{savedSites[i - 1].fullUrl},{savedSites[i - 1].baseUrl},{savedSites[i - 1].timePerTask}");
            }

            // Return the CSV content as a response
            var content = csv.ToString();
            var bytes = Encoding.UTF8.GetBytes(content);
            return File(bytes, "text/csv", "result.csv");
        }

        public async Task<IActionResult> ExportAll()
        {
            IEnumerable<TestResults> allTestResults = await _testResultsRepository.GetAllAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Time Elapsed,Average Time Elapsed,Errors,User Clicks,Task Success Rate,Scroll Depth Rate");

            foreach (TestResults testResult in allTestResults)
            {
                csv.AppendLine($"{testResult.TimeElapsed},{testResult.AverageTimeOnTask}," +
                               $"{testResult.Errors},{testResult.UserClicks},{testResult.TaskSucessRate},{testResult.ScrollDepthRate}");
            }

            // Visited Sites
            for (int i = 0; i < 5; i++) { csv.AppendLine(); }

            csv.AppendLine("Site #,Full URL,Base URL,Time on Website");
            int index = 1;
            foreach (TestResults testResult in allTestResults)
            {
                var json = testResult.VisitedSites;
                List<JsonVisitedSites> savedSites = JsonConvert.DeserializeObject<List<JsonVisitedSites>>(json);
                foreach (JsonVisitedSites jsonVisited in savedSites)
                {
                    csv.AppendLine($"{index},{jsonVisited.fullUrl},{jsonVisited.baseUrl},{jsonVisited.timePerTask}");
                    index++;
                }
                csv.AppendLine();
            }


            // Return the CSV content as a response
            var content = csv.ToString();
            var bytes = Encoding.UTF8.GetBytes(content);
            return File(bytes, "text/csv", "AllResult.csv");
        }
    }
}
