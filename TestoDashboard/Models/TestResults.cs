using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestoDashboard.Models
{
    public class TestResults
    {
        [Key]
        public int ResultId { get; set; }
        public int? TimeElapsed { get; set; }
        public float? AverageTimeOnTask { get; set; }
        public string? VisitedSites { get; set; }
        public int? Errors { get; set; }
        public int? UserClicks { get; set; }
        public float? TaskSucessRate { get; set; }
        public string? ScrollDepthRate { get; set; }
        [ForeignKey("CaseStudies")]
        public int CaseStudyId { get; set; }
        public CaseStudies? CaseStudy { get; set; }


    }
}
