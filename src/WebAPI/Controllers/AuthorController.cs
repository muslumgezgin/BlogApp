using Blog.Application.Common.Dtos.AuthorDtos;
using Blog.Application.Common.Wrappers;
using Blog.Application.Features.AuthorFeatures.Commands.Create;
using Blog.Application.Features.AuthorFeatures.Queries.Get;
using Blog.Application.Features.AuthorFeatures.Queries.GetAll;
using Blog.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : BaseController
{
    public AuthorController(ILogger<BaseController> logger) : base(logger)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
    public async Task<ActionResult<Response<AuthorModelDto>>> Get([FromQuery] GetAuthorsQuery request)
    {
        var response = await Mediator!.Send(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
    public async Task<ActionResult<Response<AuthorModelDto>>> Get(Guid id)
    {
        var response = await Mediator!.Send(new GetAuthorByIdQuery { Id = id });
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
    public async Task<ActionResult<Response<List<AuthorModelDto>>>> Post([FromBody] CreateAuthorCommand request)
    {

        var resp = await Mediator!.Send(request);
        return Created("", resp);
    }
}
