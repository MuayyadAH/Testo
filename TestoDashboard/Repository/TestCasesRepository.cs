using Microsoft.EntityFrameworkCore;
using TestoAPI.Data;
using TestoAPI.Interfaces;
using TestoAPI.Models;

namespace TestoAPI.Repository
{
    public class TestCasesRepository : ITestCasesRepository
    {
        private readonly DataContext _context;

        public TestCasesRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TestCases>> GetAllAsync()
        {
            return await _context.TestsCases.OrderBy(i => i.TestCaseNumber).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllTestCasesOnly()
        {
            return await _context.TestsCases.Select(x => x.TestCaseName).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllLinksOnly()
        {
            return await _context.TestsCases.OrderBy(i => i.TestCaseNumber).Select(x => x.TestCaseLink).ToListAsync();
        }
    }
}
