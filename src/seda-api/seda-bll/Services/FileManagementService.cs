using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using seda_bll.Contracts;

namespace seda_bll.Services;

public class FileManagementService: IFileManagementService
{
    private readonly IMinioClient _minioClient;
    private readonly IConfiguration _configuration;

    public FileManagementService(IMinioClient minioClient, IConfiguration configuration)
    {
        _minioClient = minioClient;
        _configuration = configuration;
    }

    public void CheckFileType()
    {
        throw new NotImplementedException();
    }

    public async Task UploadFile(string fileName, string contentType, Stream documentStream)
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_configuration["MinIO:BucketName"])
            .WithObject(fileName)
            .WithStreamData(documentStream)
            .WithObjectSize(documentStream.Length)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(putObjectArgs);
    }

    public Task DownloadFile()
    {
        throw new NotImplementedException();
    }
}