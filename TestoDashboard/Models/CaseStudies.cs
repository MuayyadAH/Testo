using System.ComponentModel.DataAnnotations;

namespace TestoAPI.Models
{
    public class CaseStudies
    {
        [Key]
        public int CaseStudyId { get; set; }
        public string? CaseName { get; set; }
        public string? GuideLines { get; set; }
        public DateTime CaseCreationDate { get; set; }
        public string? StartingPoint { get; set; }
        public string? Questionnaires { get; set; }
        /*
        public string? TestCases { get; set; } 
        public string? isTestCaseDone { get; set; }
        */
    }
}
