using TestoAPI.Data;
using TestoAPI.Interfaces;
using TestoAPI.Models;

namespace TestoAPI.Repository
{
    public class TestResultsRepository : ITestResultsRepository
    {
        private readonly DataContext _context;

        public TestResultsRepository(DataContext context)
        {
            _context = context;
        }
        public bool Add(TestResults testResult)
        {
            _context.Add(testResult);
            return Save();        }

        public ICollection<TestResults> GetTestResults()
        {
            return _context.TestResults.OrderBy(p => p.ResultId).ToArray();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
