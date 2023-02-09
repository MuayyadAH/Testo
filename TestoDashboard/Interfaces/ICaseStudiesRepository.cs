using TestoAPI.Models;

namespace TestoAPI.Interfaces
{
    public interface ICaseStudiesRepository
    {
        ICollection<CaseStudies> GetAllCaseStudies();
        CaseStudies GetByCaseId(int caseId);
        bool Save();
        bool Add(CaseStudies caseStudy);

    }
}
