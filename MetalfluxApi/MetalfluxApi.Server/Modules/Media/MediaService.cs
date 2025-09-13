using Amazon.S3.Model;
using MetalfluxApi.Server.Core.Base;
using MetalfluxApi.Server.Core.Dto;
using MetalfluxApi.Server.Core.Exceptions;
using MetalfluxApi.Server.Core.Service;
using Microsoft.AspNetCore.StaticFiles;

namespace MetalfluxApi.Server.Modules.Media;

public interface IMediaService : IService<MediaDto, MediaModel>
{
    List<MediaDto> Search(CursorSearchRequestDto body, out int lastId, out bool lastItemReached);
    Task<MediaDto> UploadMedia(int id, IFormFile file);
    Task<(Stream file, string contentType, string fileName)> GetMediaStream(int id);
}

internal sealed class MediaService(
    IMediaRepository repository,
    S3Service s3Service,
    IConfiguration configuration
) : IMediaService
{
    public MediaDto Get(int id)
    {
        var item = repository.Get(id);
        if (item == null)
            throw new EntityNotFoundException("Media", id);

        return ToDto(item);
    }

    public async Task<(Stream file, string contentType, string fileName)> GetMediaStream(int id)
    {
        var item = repository.Get(id);
        if (item == null)
            throw new EntityNotFoundException("Media", id);

        var fileName = $"{item.Id}.{item.FileExtension}";

        var getRequest = new GetObjectRequest
        {
            BucketName = configuration["S3:BucketName"],
            Key = fileName,
        };

        var mediaFile = await s3Service.GetObjectAsync(getRequest);
        var success = new FileExtensionContentTypeProvider().TryGetContentType(
            fileName,
            out var contentType
        );
        if (!success || contentType == null)
            throw new Exception("Could not parse content type");

        return (mediaFile, contentType, fileName);
    }

    public MediaDto Add(MediaDto item)
    {
        var created = ToDto(repository.Add(ToModel(item)));
        return created;
    }

    public async Task<MediaDto> UploadMedia(int id, IFormFile file)
    {
        var item = repository.Get(id);
        if (item == null)
            throw new EntityNotFoundException("Media", id);

        var fileName = $"{item.Id}.{item.FileExtension}";

        await using var stream = file.OpenReadStream();
        var putRequest = new PutObjectRequest
        {
            BucketName = configuration["S3:BucketName"],
            Key = fileName,
            InputStream = stream,
            ContentType = file.ContentType,
        };

        await s3Service.PutObjectAsync(putRequest);

        item.UpdatedAt = DateTime.UtcNow;
        item.HasUploadedMedia = true;
        repository.Update(item);
        return ToDto(item);
    }

    public int Remove(int id)
    {
        if (!repository.Exists(id))
            throw new EntityNotFoundException("User", id);

        return repository.Remove(id);
    }

    public MediaDto Update(MediaDto item)
    {
        if (!repository.Exists(item.Id))
            throw new EntityNotFoundException("User", item.Id);

        return ToDto(repository.Update(ToModel(item)));
    }

    public List<MediaDto> Search(
        CursorSearchRequestDto body,
        out int lastId,
        out bool lastItemReached
    )
    {
        return ToDto(repository.Search(body, out lastId, out lastItemReached));
    }

    public MediaDto ToDto(MediaModel model)
    {
        return new MediaDto
        {
            Id = model.Id,
            Name = model.Name,
            FileExtension = model.FileExtension,
            HasUploadedMedia = model.HasUploadedMedia,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
        };
    }

    private List<MediaDto> ToDto(List<MediaModel> model)
    {
        return model.Select(ToDto).ToList();
    }

    public MediaModel ToModel(MediaDto dto)
    {
        var model = repository.Get(dto.Id);

        return model
            ?? new MediaModel
            {
                Name = dto.Name,
                FileExtension = dto.FileExtension,
                HasUploadedMedia = dto.HasUploadedMedia,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
            };
    }
}
