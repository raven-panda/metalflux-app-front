using MetalfluxApi.Server.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MetalfluxApi.Server.Modules.Media;

[Route("media")]
[ApiController]
public class MediaController(IMediaService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var media = service.Get(id);
        return Ok(media);
    }

    [HttpPost("browse")]
    public IActionResult Search(CursorSearchRequestDto body)
    {
        var medias = service.Search(body, out var lastId, out var lastItemReached);
        return Ok(
            new CursorResponse<MediaDto>()
            {
                Data = medias,
                NextCursor = lastId,
                LastItemReached = lastItemReached,
            }
        );
    }

    [HttpPost("create")]
    public IActionResult CreateMedia(MediaDto body)
    {
        var media = service.Add(body);
        return Ok(media);
    }

    [HttpPost("{id:int}/upload-media")]
    public async Task<IActionResult> UploadMedia(int id, [FromForm] IFormFile file)
    {
        var media = await service.UploadMedia(id, file);
        return Ok(media);
    }

    [HttpGet("{id:int}/stream")]
    public async Task<IActionResult> GetMediaStream(int id)
    {
        var (mediaStream, contentType, fileName) = await service.GetMediaStream(id);
        return File(mediaStream, contentType, fileName);
    }
}
