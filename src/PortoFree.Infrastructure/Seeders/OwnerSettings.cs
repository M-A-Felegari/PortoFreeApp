namespace PortoFree.Infrastructure.Seeders;

internal class OwnerSettings
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required long TotalSpaceBytes { get; set; }
}