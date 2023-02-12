using Microsoft.AspNetCore.Mvc;
using TestoMVC.Interfaces;
using TestoMVC.Models;
using TestoMVC.Repository;

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

        
        public async Task<IActionResult> DeleteTask(int id)
        {
            TestCases testCase = await _testCasesRepository.GetByIdAsync(id);
            return View(testCase);
        }

        [HttpPost, ActionName("DeleteTask")]
        public async Task<IActionResult> confirmDeleteTask(int id)
        {
            TestCases testCase = await _testCasesRepository.GetByIdAsync(id);
            if (testCase != null)
            {
                _testCasesRepository.Delete(testCase);
            }
            return RedirectToAction("Index","TestCases");
        }

        public async Task<IActionResult> Edit(int id)
        {
            TestCases testCase = await _testCasesRepository.GetByIdAsync(id);
            return View(testCase);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(TestCases testCase)
        {
            TestCases editedTestCase = await _testCasesRepository.GetByIdAsync(testCase.TestCaseNumber);
            editedTestCase.TestCaseName = testCase.TestCaseName;
            editedTestCase.TestCaseLink = testCase.TestCaseLink;
            editedTestCase.isTestCaseDone = testCase.isTestCaseDone;

            _testCasesRepository.Update(editedTestCase);
            return RedirectToAction("Index", "TestCases");
        }
    }
}
