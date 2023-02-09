using Microsoft.EntityFrameworkCore;
using TestoMVC.Data;
using TestoMVC.Interfaces;
using TestoMVC.Models;

namespace TestoMVC.Repository
{
    public class CaseStudiesRepository : ICaseStudiesRepository
    {
        private readonly dbContext _context;

        public CaseStudiesRepository(dbContext context)
        {
            _context = context;
        }

        public bool Add(CaseStudies caseStudy)
        {
            _context.Add(caseStudy);
            return Save();
        }

        public IEnumerable<CaseStudies> GetAllCaseStudies()
        {
            return _context.CaseStudies.OrderBy(p => p.CaseStudyId).ToList();
        }
        public async Task<ICollection<CaseStudies>> GetAllCaseStudiesAsync()
        {
            return await _context.CaseStudies.OrderBy(p => p.CaseStudyId).ToListAsync();
        }

        public async Task<CaseStudies> GetByCaseId(int caseId)
        {
            return await _context.CaseStudies.FirstOrDefaultAsync(i => i.CaseStudyId == caseId);
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

        public bool Update(CaseStudies caseStudy)
        {
            _context.Update(caseStudy);
            return Save();
        }
    }
}
