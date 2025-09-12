using MetalfluxApi.Server.Core.Base;
using MetalfluxApi.Server.Core.Dto;
using MetalfluxApi.Server.Core.Exceptions;

namespace MetalfluxApi.Server.Modules.Media;

public interface IMediaService : IService<MediaDto, MediaModel>
{
    List<MediaDto> Search(CursorSearchRequestDto body, out int lastId, out bool lastItemReached);
}

internal sealed class MediaService(IMediaRepository repository) : IMediaService
{
    public MediaDto Get(int id)
    {
        var item = repository.Get(id);
        if (item == null)
            throw new EntityNotFoundException("Media", id);

        return ToDto(item);
    }

    public MediaDto Add(MediaDto item)
    {
        return ToDto(repository.Add(ToModel(item)));
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
        return new MediaDto()
        {
            Id = model.Id,
            Name = model.Name,
            Url = model.Url,
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
                Url = dto.Url,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
            };
    }
}
