using TestoDashboard.Models;

namespace TestoAPI.Interfaces
{
    public interface ITestResultsRepository
    {
        ICollection<TestResults> GetTestResults();
    }
}
