using System.ComponentModel.DataAnnotations;

namespace TestoMVC.Models
{
    public class CaseStudies
    {
        [Key]
        public int CaseStudyId { get; set; }
        [Display(Name="Case Study name")]
        public string? CaseName { get; set; }
        [Display(Name = "Provide guidelines for the study")]
        public string? GuideLines { get; set; }
        public DateTime CaseCreationDate { get; set; }
        [Display(Name = "Website Starting point")]
        public string? StartingPoint { get; set; }
        [Display(Name = "Provide questionnaire/survery (if applicable)")]
        public string? Questionnaires { get; set; }
        /*
        public string? TestCases { get; set; } 
        public string? isTestCaseDone { get; set; }
        */
    }
}
