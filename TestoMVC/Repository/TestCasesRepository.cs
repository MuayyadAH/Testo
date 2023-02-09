using Microsoft.EntityFrameworkCore;
using TestoMVC.Data;
using TestoMVC.Interfaces;
using TestoMVC.Models;

namespace TestoMVC.Repository
{
    public class TestCasesRepository : ITestCasesRepository
    {
        private readonly dbContext _context;

        public TestCasesRepository(dbContext context)
        {
            _context = context;
        }
        public bool Add(TestCases testCase)
        {   
            _context.Add(testCase);
            return Save();
        }

        public bool Delete(TestCases testCase)
        {
            _context.Remove(testCase);
            return Save();
        }

        public IEnumerable<TestCases> GetAllTestCases()
        {
            return _context.TestsCases.OrderBy(p => p.TestCaseNumber).ToList();
        }

        public async Task<ICollection<TestCases>> GetAllTestCasesAsync()
        {
            return await _context.TestsCases.OrderBy(p => p.TestCaseNumber).ToListAsync();
        }

        public Task<TestCases> GetByIdAsync(int id)
        {
            return _context.TestsCases.FirstOrDefaultAsync(x => x.TestCaseNumber == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(TestCases testCase)
        {
            _context.Update(testCase);
            return Save();
        }
    }
}
