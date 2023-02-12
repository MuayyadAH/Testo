namespace TestoAPI.ParaModel
{
    public class AcceptTestResult
    {
        public int? TimeElapsed { get; set; }
        public float? AverageTimeOnTask { get; set; }
        public string? VisitedSites { get; set; }
        public int? Errors { get; set; }
        public int? UserClicks { get; set; }
        public float? TaskSucessRate { get; set; }
        public float? ScrollDepthRate { get; set; }
    }
}
