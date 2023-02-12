using TestoMVC.Models;

namespace TestoMVC.Interfaces
{
    public interface ITestResultsRepository
    {
        Task<ICollection<TestResults>> GetAllAsync();
        IEnumerable<TestResults> GetAll();
        bool Save();
        bool Add(TestResults testResult);
        bool Update(TestResults testResult);
        bool Delete(TestResults testResult);
        Task<TestResults> GetByIdAsync(int id);
        bool DeleteAll();
    }
}
