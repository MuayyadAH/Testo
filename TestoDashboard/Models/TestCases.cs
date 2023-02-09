
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestoAPI.Models
{
    public class TestCases
    {
        [Key]
        public int TestCaseNumber { get; set; }
        public string? TestCaseName { get; set; }
        public string? TestCaseLink { get; set; }
        public bool? isTestCaseDone { get; set; }
    }
}
