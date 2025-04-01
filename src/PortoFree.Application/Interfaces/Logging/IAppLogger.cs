namespace PortoFree.Application.Interfaces.Logging;

public interface IAppLogger<T>
{
    public void LogInformation(string? message, params object[] args);
    public void LogWarning(string? message, params object[] args);
    public void LogError(string? message, params object[] args);
}