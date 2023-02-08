using Microsoft.AspNetCore.Mvc;
using TestoAPI.Interfaces;
using TestoDashboard.Models;

namespace TestoAPI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ICaseStudiesRepository _caseStudiesRepository;

        public DashboardController(ICaseStudiesRepository caseStudiesRepository)
        {
            _caseStudiesRepository = caseStudiesRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCaseStudy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCaseStudy(CaseStudies caseStudy)
        {
            if (ModelState.IsValid)
            {
                var newCaseStudy = new CaseStudies()
                {
                    CaseCreationDate = new DateTime(),
                    CaseName = caseStudy.CaseName,
                    isTestCaseDone = caseStudy.isTestCaseDone,
                    Questionnaires = caseStudy.Questionnaires
                };
                _caseStudiesRepository.Add(newCaseStudy);
            }
            return View(caseStudy);
        }
    }
}
