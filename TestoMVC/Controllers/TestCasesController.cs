using Microsoft.AspNetCore.Mvc;
using TestoMVC.Data;
using TestoMVC.Interfaces;
using TestoMVC.Models;

namespace TestoMVC.Controllers
{
    public class TestCasesController : Controller
    {
        private readonly ITestCasesRepository _testCasesRepository;

        public TestCasesController(ITestCasesRepository testCasesRepository)
        {
            _testCasesRepository = testCasesRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<TestCases> allTestCases = _testCasesRepository.GetAllTestCases();
            return View(allTestCases);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TestCases testCase)
        {
            if (ModelState.IsValid)
            {
                var newTestCase = new TestCases()
                {
                    TestCaseName = testCase.TestCaseName,
                    TestCaseLink = testCase.TestCaseLink,
                    isTestCaseDone = false
                };
                _testCasesRepository.Add(newTestCase);
            }
            return RedirectToAction("Index", "TestCases");
        }
    }
}
