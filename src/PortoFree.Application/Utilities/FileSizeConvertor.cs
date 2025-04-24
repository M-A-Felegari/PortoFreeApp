namespace PortoFree.Application.Utilities;

public static class FileSizeConvertor
{
    public static double BytesToMegabytes(this long bytes)
    {
        return bytes / ( 1024 * 1024.0);
    }
}