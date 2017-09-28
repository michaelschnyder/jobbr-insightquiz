using System.Collections.Generic;

namespace InsightQuizzer.Core.Model
{
    public class OrganisationUnitDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int Type { get; set; }
        public List<object> Children { get; set; }
        public int Participants { get; set; }
        public string TypeText { get; set; }
        public ParentDto Parent { get; set; }
    }
}