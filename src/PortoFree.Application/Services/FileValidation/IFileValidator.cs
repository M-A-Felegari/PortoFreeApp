namespace PortoFree.Application.Services.FileValidation;

internal interface IFileValidator
{
    public bool IsFileValid(Stream fileStream, string fileName);
    public bool IsSizeValid(long sizeInBytes, int sizeInMb);
}