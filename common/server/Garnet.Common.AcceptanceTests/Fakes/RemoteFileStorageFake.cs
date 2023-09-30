using Garnet.Common.Application.S3;

namespace Garnet.Common.AcceptanceTests.Fakes;

public class RemoteFileStorageFake : IRemoteFileStorage
{
    public Dictionary<string, byte[]> FilesInStorage { get; } = new();
    
    public async Task<string> UploadFile(string filePath, string contentType, Stream stream)
    {
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        FilesInStorage[filePath] = memoryStream.ToArray();
        return filePath;
    }
}