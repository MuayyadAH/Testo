using Microsoft.EntityFrameworkCore;
using TestoAPI.Models;

namespace TestoAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CaseStudies> CaseStudies { get; set; }
        public DbSet<TestResults> TestResults { get; set; }
        public DbSet<TestCases> TestsCases { get; set; }

    }
}
