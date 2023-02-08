using System.ComponentModel.DataAnnotations;

namespace TestoDashboard.Models
{
    public class CaseStudies
    {
        [Key]
        public int CaseStudyId { get; set; }
        public string? CaseName { get; set; }
        public DateTime CaseCreationDate { get; set; }
        public string? Questionnaires { get; set; }
        public string? TestCases { get; set; } 
        public string? isTestCaseDone { get; set; }
    }
}
