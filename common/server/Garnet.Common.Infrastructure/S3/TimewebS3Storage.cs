using Garnet.Common.Application.S3;
using Minio;
using Minio.DataModel.Args;

namespace Garnet.Common.Infrastructure.S3;

public class TimewebS3Storage : IRemoteFileStorage
{
    private readonly string _bucket;
    private readonly IMinioClient _minioClient;

    public TimewebS3Storage(string bucket, IMinioClient minioClient)
    {
        _bucket = bucket;
        _minioClient = minioClient;
    }

    public async Task<string> UploadFile(
        string filePath,
        string? contentType,
        Stream stream
    )
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucket)
            .WithObject(filePath)
            .WithObjectSize(stream.Length)
            .WithStreamData(stream)
            .WithContentType(contentType);

        var response = await _minioClient.PutObjectAsync(putObjectArgs);
        return ConstructUrl(_bucket, response.ObjectName);
    }

    private string ConstructUrl(string bucket, string objectName)
    {
        Uri.TryCreate(
            new Uri(_minioClient.Config.Endpoint),
            $"{bucket}/{objectName}",
            out var uri);
        return uri!.ToString();
    }
}