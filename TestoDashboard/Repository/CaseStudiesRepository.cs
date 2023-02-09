using Microsoft.EntityFrameworkCore;
using TestoAPI.Data;
using TestoAPI.Interfaces;
using TestoAPI.Models;

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
            //return _context.CaseStudies.FromSql($"SELECT * from dbo.CaseStudies WHERE ISJSON(TestCases) = 1").ToList();
            return _context.CaseStudies.OrderBy(p => p.CaseStudyId).ToArray();
        }

        public CaseStudies GetByCaseId(int caseId)
        {
            return _context.CaseStudies.FirstOrDefault(i => i.CaseStudyId == caseId);
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
