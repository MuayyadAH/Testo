using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestoMVC.Models
{
    public class TestResults
    {
        [Key]
        public int ResultId { get; set; }
        [Display(Name = "Test Time Elapsed")]
        public int? TimeElapsed { get; set; }
        [Display(Name = "Average time")]
        public float? AverageTimeOnTask { get; set; }
        [Display(Name = "Visited websites")]
        public string? VisitedSites { get; set; }
        [Display(Name = "Errors")]
        public int? Errors { get; set; }
        [Display(Name = "User Clicks")]
        public int? UserClicks { get; set; }
        [Display(Name = "Task Success Rate (%)")]
        public float? TaskSucessRate { get; set; }
        [Display(Name = "Average Scroll Depth Rate (pixels)")]
        public float? ScrollDepthRate { get; set; }
    }
}
