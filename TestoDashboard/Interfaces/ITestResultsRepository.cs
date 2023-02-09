using TestoAPI.Models;

namespace TestoAPI.Interfaces
{
    public interface ITestResultsRepository
    {
        ICollection<TestResults> GetTestResults();
        bool Save();
        bool Add(TestResults testResult);
    }
}
