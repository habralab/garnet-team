namespace Garnet.Common.Application.S3;

public interface IRemoteFileStorage
{
    /// <param name="filePath">Путь к файлу, по которому он будет положен в хранилище, например: 'assets/image.jpg'</param>
    /// <param name="contentType">Http Content-Type (application/octet-stream, image/png, ...)</param>
    /// <returns>Абсолютная публичная ссылка на файл</returns>
    Task<string> UploadFile(
        string filePath, 
        string? contentType, 
        Stream stream
    );
}