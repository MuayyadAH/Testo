using TestoMVC.Models;

namespace TestoMVC.Interfaces
{
    public interface ICaseStudiesRepository
    {
        Task<ICollection<CaseStudies>> GetAllCaseStudiesAsync();
        IEnumerable<CaseStudies> GetAllCaseStudies();
        Task<CaseStudies> GetByCaseIdAsync(int caseId);
        bool Save();
        bool Add(CaseStudies caseStudy);
        bool Update(CaseStudies caseStudy);


    }
}
