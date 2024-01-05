using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace NPaperless.Services.MinIO;

public class FileUpload
{
    private readonly IMinioClient _minioClient = new MinioClient()
        .WithEndpoint("minio:9000")
        .WithCredentials("npaperless", "npaperless")
        .Build();

    public async Task UploadFileAsync(Stream fileStream, string fileName)
    {
        var bucketName = "npaperless";

        try
        {
            var beArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await _minioClient.BucketExistsAsync(beArgs);
            if (!found)
            {
                var mbArgs = new MakeBucketArgs().WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(mbArgs);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length);
            await _minioClient.PutObjectAsync(putObjectArgs);
        }
        catch (MinioException e)
        {
            Console.WriteLine("File Upload Error: {0}", e.Message);
        }
    }

    public async Task<Stream> DownloadFileAsync(string fileName)
    {
        var bucketName = "npaperless";
    
        // Bestätigen, dass das Objekt existiert, bevor ein Versuch unternommen wird, es abzurufen
        StatObjectArgs statObjectArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName);
        await _minioClient.StatObjectAsync(statObjectArgs);

        MemoryStream memoryStream = new MemoryStream();
        
        // InputStream erhalten
        GetObjectArgs getObjectArgs = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream((stream) =>
            {
                stream.CopyTo(memoryStream);
            });
        await _minioClient.GetObjectAsync(getObjectArgs);

        // Stellen Sie sicher, dass der MemoryStream zurückgespult wird, bevor er zurückgegeben wird
        memoryStream.Position = 0;

        return memoryStream;
    }
    
    
}