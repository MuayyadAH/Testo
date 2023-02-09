using TestoMVC.Models;

namespace TestoMVC.Interfaces
{
    public interface ITestCasesRepository
    {
        Task<ICollection<TestCases>> GetAllTestCasesAsync();
        IEnumerable<TestCases> GetAllTestCases();
        bool Save();
        bool Add(TestCases testCase);
        bool Update(TestCases testCase);
        bool Delete(TestCases testCase);
        Task<TestCases> GetByIdAsync(int id);
    }
}
