using TestoAPI.Models;

namespace TestoAPI.Interfaces
{
    public interface ITestCasesRepository
    {
        Task<IEnumerable<TestCases>> GetAllAsync();

        Task<IEnumerable<string>> GetAllTestCasesOnly();
        Task<IEnumerable<string>> GetAllLinksOnly();
    }
}
