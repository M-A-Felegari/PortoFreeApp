namespace PortoFree.Application.Utilities;

public static class FileSizeConvertor
{
    public static long BytesToMegabytes(this long bytes)
    {
        return bytes / 1024 / 1024; //todo: make it more performant by calculate 1024 * 1024 directly
    }
}