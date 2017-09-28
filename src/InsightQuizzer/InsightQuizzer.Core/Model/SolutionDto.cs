using System.Collections.Generic;

namespace InsightQuizzer.Core.Model
{
    public class SolutionDto
    {
        public int VertecId { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool IsOwner { get; set; }
        public OfficeDto Office { get; set; }
        public string WorkingPlace { get; set; }
        public string LastUpdated { get; set; }
        public string EntryDate { get; set; }
        public string StartsIn { get; set; }
        public string Duration { get; set; }
        public string MobilePhone { get; set; }
        public string BusinessPhone { get; set; }
        public string Mail { get; set; }
        public string Location { get; set; }
        public QualificationDto Qualification { get; set; }
        public string Superior { get; set; }
        public string SuperiorCode { get; set; }
        public string JobProfile { get; set; }
        public string Title { get; set; }
        public string Graduation { get; set; }
        public CompanyDto Company { get; set; }
        public double ExternalRate { get; set; }
        public string Currency { get; set; }
        public TeamDto Team { get; set; }
        public List<OrganisationUnitDto> OrganisationUnits { get; set; }
        public bool Freelancer { get; set; }
        public bool TeamLeader { get; set; }
        public bool DdSystems { get; set; }
        public double WorkHoursPerDay { get; set; }
        public string ProfileLink { get; set; }
        public List<object> Notifications { get; set; }
        public List<ProfileDto> Profiles { get; set; }
        public int Id { get; set; }
    }
}