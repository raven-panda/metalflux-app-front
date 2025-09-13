using Amazon.S3;
using Amazon.S3.Model;

namespace MetalfluxApi.Server.Core.Service;

internal sealed class S3Service(IConfiguration configuration)
{
    private readonly IAmazonS3 _s3Client = new AmazonS3Client(
        configuration["S3:Username"],
        configuration["S3:Password"],
        new AmazonS3Config { ServiceURL = configuration["S3:Url"], ForcePathStyle = true }
    );
    private const string BucketName = "medias";

    public string GetPresignedUploadUrl(string fileName)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = BucketName,
            Key = fileName,
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddMinutes(10),
        };

        return _s3Client.GetPreSignedURL(request);
    }

    public string GetPresignedDownloadUrl(string fileName)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = BucketName,
            Key = fileName,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddMinutes(10), // durée valable
        };

        return _s3Client.GetPreSignedURL(request);
    }

    public async Task PutObjectAsync(PutObjectRequest request)
    {
        await _s3Client.PutObjectAsync(request);
    }

    public async Task<Stream> GetObjectAsync(GetObjectRequest request)
    {
        var response = await _s3Client.GetObjectAsync(request);
        var stream = response.ResponseStream;
        return stream;
    }
}
