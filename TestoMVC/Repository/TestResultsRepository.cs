using Microsoft.EntityFrameworkCore;
using TestoMVC.Data;
using TestoMVC.Interfaces;
using TestoMVC.Models;

namespace TestoMVC.Repository
{
    public class TestResultsRepository : ITestResultsRepository
    {
        private readonly dbContext _context;

        public TestResultsRepository(dbContext context)
        {
            _context = context;
        }
        public bool Add(TestResults testResult)
        {
            _context.Add(testResult);
            return Save();
        }

        public bool Delete(TestResults testResult)
        {
            _context.Remove(testResult);
            return Save();
        }

        public bool DeleteAll()
        {
            _context.RemoveRange(_context.TestResults.ToList());
            return Save();
        }

        public IEnumerable<TestResults> GetAll()
        {
            return _context.TestResults.OrderBy(p => p.ResultId).ToList();
        }

        public async Task<ICollection<TestResults>> GetAllAsync()
        {
            return await _context.TestResults.OrderBy(p => p.ResultId).ToListAsync();
        }

        public async Task<TestResults> GetByIdAsync(int id)
        {
            return await _context.TestResults.FirstOrDefaultAsync(i => i.ResultId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(TestResults testResult)
        {
            _context.Update(testResult);
            return Save();
        }
    }
}
