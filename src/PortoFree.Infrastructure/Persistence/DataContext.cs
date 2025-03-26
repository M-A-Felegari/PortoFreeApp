using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortoFree.Domain.Entities;

namespace PortoFree.Infrastructure.Persistence;

internal class DataContext(DbContextOptions options) :
    IdentityDbContext<User,IdentityRole<int>,int>(options)
{
    public DbSet<WorkExample> WorkExamples { get; set; }
    public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
}
