using Microsoft.AspNetCore.Identity;

namespace PortoFree.Domain.Entities;

public class User : IdentityUser<int>
{
    public long UsedSpace { get; set; }
    public long TotalSpace { get; set; }
    public bool IsBanned { get; set; }
    public IEnumerable<WorkExample>? WorkExamples { get; set; }
    public IEnumerable<UserSkill>? UserSkills { get; set; }
    public IEnumerable<EmploymentHistory>? EmploymentsHistory { get; set; }
    public IEnumerable<Comment>? Comments { get; set; }
    
    public long FreeSpace => TotalSpace - UsedSpace;
}
