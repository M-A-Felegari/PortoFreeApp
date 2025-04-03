using PortoFree.Application.Utilities;

namespace PortoFree.Application.Services.FileValidation;

public class ImageValidator : IFileValidator
{
    public static string[] ValidExtensions { get; } = ["jpg", "jpeg", "png"];
    
    private static readonly byte[] JpegHeader = [0xFF, 0xD8, 0xFF];
    private static readonly byte[] PngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
    
    public bool IsFileValid(byte[] fileBytes, string fileName)
    {
        if (fileBytes.Length == 0 )
        {
            return false;
        }
        
        var header = fileBytes.Take(8).ToArray();
        
        var isJpeg = header.Length >= 3 && header.SequenceEqual(JpegHeader);
        var isPng = header.Length == 8 && header[..8].SequenceEqual(PngHeader);
        
        var fileExtension = Path.GetExtension(fileName);
        
        return (isJpeg || isPng) && ValidExtensions.Contains(fileExtension);
    }

    public bool IsSizeValid(long fileSizeInBytes, int maxValidSizeInMb)
    {
        return fileSizeInBytes >= 0 && fileSizeInBytes.BytesToMegabytes() <= maxValidSizeInMb;
    }
}