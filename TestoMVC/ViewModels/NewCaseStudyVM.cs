namespace TestoMVC.ViewModels
{
    public class NewCaseStudyVM
    {
        public string CaseStudyName { get; set; }
        public string Guidelines { get; set; }
        public string StartingPoint { get; set; }
        public IEnumerable<string>? LinksInScope { get; set; }
        public IEnumerable<string> TestCases { get; set; }

    }
}
