namespace PortoFree.Domain.Entities;

public class User
{
    public IEnumerable<WorkExample>? WorkExamples { get; set; }
    public int UsedSpace { get; set; }
    public int TotalSpace { get; set; }
    public bool IsBanned { get; set; }
}
