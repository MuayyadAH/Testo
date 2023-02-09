using Microsoft.AspNetCore.Mvc;
using TestoMVC.Models;
using TestoMVC.Interfaces;

namespace TestoMVC.Controllers
{
    public class CaseStudyController : Controller
    {
        private readonly ICaseStudiesRepository _caseStudiesRepository;

        public CaseStudyController(ICaseStudiesRepository caseStudiesRepository)
        {
            _caseStudiesRepository = caseStudiesRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<CaseStudies> allCaseStudies = _caseStudiesRepository.GetAllCaseStudies();
            return View(allCaseStudies);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CaseStudies caseStudy = await _caseStudiesRepository.GetByCaseIdAsync(id);
            return View(caseStudy); 
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(CaseStudies caseStudy)
        {
            CaseStudies editedCaseStudy = await _caseStudiesRepository.GetByCaseIdAsync(caseStudy.CaseStudyId);
            editedCaseStudy.CaseName = caseStudy.CaseName;
            editedCaseStudy.Questionnaires = caseStudy.Questionnaires;
            editedCaseStudy.StartingPoint = caseStudy.StartingPoint;
            editedCaseStudy.GuideLines = caseStudy.GuideLines;

            _caseStudiesRepository.Update(editedCaseStudy);
            return RedirectToAction("Index", "CaseStudy");
        }
    }
}
