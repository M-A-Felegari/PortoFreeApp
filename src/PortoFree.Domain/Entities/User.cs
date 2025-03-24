namespace PortoFree.Domain.Entities;

public class User
{
    public int Id { get; set; } //temp id because we want to use identityUser
    public int UsedSpace { get; set; }
    public int TotalSpace { get; set; }
    public bool IsBanned { get; set; }
    public IEnumerable<WorkExample>? WorkExamples { get; set; }
    public IEnumerable<UserSkill>? UserSkills { get; set; }
    public IEnumerable<EmploymentHistory>? EmploymentsHistory { get; set; }
    public IEnumerable<Comment>? Comments { get; set; }
}
