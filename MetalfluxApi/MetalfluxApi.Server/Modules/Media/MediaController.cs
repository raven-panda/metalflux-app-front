using MetalfluxApi.Server.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MetalfluxApi.Server.Modules.Media;

[Route("media")]
[ApiController]
public class MediaController(IMediaService service) : ControllerBase
{
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

    [HttpPost("post")]
    public IActionResult PostMedia(MediaDto body)
    {
        var media = service.Add(body);
        return Ok(media);
    }
}
