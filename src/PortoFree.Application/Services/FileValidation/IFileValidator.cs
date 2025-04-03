namespace PortoFree.Application.Services.FileValidation;

internal interface IFileValidator
{
    public bool IsFileValid(byte[] fileBytes, string fileName);
    public bool IsSizeValid(long sizeInBytes, int sizeInMb);
}