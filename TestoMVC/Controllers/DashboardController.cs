using Microsoft.AspNetCore.Mvc;
using TestoMVC.Models;
using TestoMVC.Interfaces;

namespace TestoMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICaseStudiesRepository _caseStudiesRepository;

        public DashboardController(ICaseStudiesRepository caseStudiesRepository)
        {
            _caseStudiesRepository = caseStudiesRepository;
        }


        public async Task<IActionResult> AllTestCases()
        {
            IEnumerable<CaseStudies> caseStudies = await _caseStudiesRepository.GetAllCaseStudiesAsync();
            return View(caseStudies);
        }

        [HttpGet]
        public IActionResult NewCaseStudy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewCaseStudy(CaseStudies caseStudy)
        {
            var newCaseStudy = new CaseStudies()
            {   
                CaseName = caseStudy.CaseName,
                CaseCreationDate = DateTime.Now,
                // isTestCaseDone = null,
                Questionnaires = caseStudy.Questionnaires,
                // TestCases = caseStudy.TestCases,
                StartingPoint = caseStudy.StartingPoint,
                GuideLines = caseStudy.GuideLines,
            };
            if (ModelState.IsValid)
            {
                _caseStudiesRepository.Add(newCaseStudy);
            }
            return View(newCaseStudy);
        }
    }

}
