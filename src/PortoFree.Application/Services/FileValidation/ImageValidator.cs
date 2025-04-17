using PortoFree.Application.Utilities;

namespace PortoFree.Application.Services.FileValidation;

public class ImageValidator : IFileValidator
{
    public static string[] ValidExtensions { get; } = ["jpg", "jpeg", "png"];
    
    private static readonly byte[] JpegHeader = [0xFF, 0xD8, 0xFF];
    private static readonly byte[] PngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
    
    public bool IsFileValid(Stream fileStream, string fileName)
    {
        if (fileStream is null || fileStream.Length < 3)
            return false;

        Span<byte> headerBytes = stackalloc byte[8];
        fileStream.Seek(0, SeekOrigin.Begin);
        fileStream.ReadExactly(headerBytes);
        
        var isJpeg = headerBytes[..3].SequenceEqual(JpegHeader);
        var isPng = headerBytes.SequenceEqual(PngHeader);
        
        var fileExtension = Path.GetExtension(fileName).Split('.')[1];
        
        var isExtensionValid = ValidExtensions.Contains(fileExtension);
        return (isJpeg || isPng) && isExtensionValid;
    }

    public bool IsSizeValid(long fileSizeInBytes, int maxValidSizeInMb)
    {
        return fileSizeInBytes >= 0 && fileSizeInBytes.BytesToMegabytes() <= maxValidSizeInMb;
    }
}