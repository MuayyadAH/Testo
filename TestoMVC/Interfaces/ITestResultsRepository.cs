using TestoMVC.Models;

namespace TestoMVC.Interfaces
{
    public interface ITestResultsRepository
    {
        ICollection<TestResults> GetTestResults();
    }
}
