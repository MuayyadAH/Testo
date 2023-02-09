using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestoMVC.Models
{
    public class TestCases
    {
        [Key]
        public int TestCaseNumber { get; set; }
        [Display(Name = "Test case Content")]
        public string? TestCaseName { get; set; }
        [Display(Name = "Test case scoped website link")]
        public string? TestCaseLink { get; set; }
        public bool? isTestCaseDone { get; set; }
    }
}
