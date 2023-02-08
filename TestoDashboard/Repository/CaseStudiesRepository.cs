using Microsoft.EntityFrameworkCore;
using TestoAPI.Data;
using TestoAPI.Interfaces;
using TestoDashboard.Models;

namespace TestoAPI.Repository
{
    public class CaseStudiesRepository : ICaseStudiesRepository
    {
        private readonly DataContext _context;

        public CaseStudiesRepository(DataContext context)
        {
            _context = context;
        }

        public bool Add(CaseStudies caseStudy)
        {
            _context.Add(caseStudy);
            return Save();
        }

        public ICollection<CaseStudies> GetAllCaseStudies()
        {
            return _context.CaseStudies.OrderBy(p => p.CaseStudyId).ToList();
        }

        public CaseStudies GetByCaseId(int caseId)
        {
            throw new NotImplementedException();
        }

        public async Task<CaseStudies> GetByCaseIdAsync(int caseId)
        {
            return await _context.CaseStudies.FirstOrDefaultAsync(i => i.CaseStudyId == caseId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
