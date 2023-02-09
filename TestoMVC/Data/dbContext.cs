using Microsoft.EntityFrameworkCore;
using TestoMVC.Models;

namespace TestoMVC.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        public DbSet<CaseStudies> CaseStudies { get; set; }
        public DbSet<TestResults> TestResults { get; set; }
        public DbSet<TestCases> TestsCases { get; set; }
    }
}
