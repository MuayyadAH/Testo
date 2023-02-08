using Microsoft.EntityFrameworkCore;
using TestoDashboard.Models;

namespace TestoAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CaseStudies> CaseStudies { get; set; }
        public DbSet<TestResults> Results { get; set; }


    }
}
