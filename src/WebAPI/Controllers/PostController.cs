using Blog.Application.Common.Dtos.PostDtos;
using Blog.Application.Common.Wrappers;
using Blog.Application.Features.PostFeatures.Commands.Create;
using Blog.Application.Features.PostFeatures.Queries.Get;
using Microsoft.AspNetCore.Mvc;
using Blog.WebAPI.Controllers.Base;

namespace Blog.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : BaseController
{
    public PostController(ILogger<PostController> logger) : base(logger)
    {
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
    public async Task<ActionResult<Response<PostModelDto>>> Get(Guid id, [FromQuery] bool includeAuthor = false)
    {
        var response = await Mediator!.Send(new GetPostByIdQuery { Id = id, IncludeAuthor = includeAuthor });
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
    public async Task<ActionResult<Response<PostModelWithAuthorDto>>> Post([FromBody] CreatePostCommand request)
    {

        var resp = await Mediator!.Send(request);
        return Created("", resp);
    }

}