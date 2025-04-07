using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortoFree.Api.Dtos;
using PortoFree.Application.Features.WorkExamples.Commands.AddWorkExample;
using PortoFree.Domain.Constants;

namespace PortoFree.Api.Controllers;

[ApiController]
[Route("api/work-examples")]
public class WorkExamplesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkExamplesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(UserRoles.User)]
    public async Task<IActionResult> Add([FromForm] AddWorkExampleRequestDto request)
    {
        await using var stream = request.Image?.OpenReadStream();
        var command = new AddWorkExampleCommand()
        {
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            FinishDate = request.FinishDate,
            ImageFileStream = stream,
            ImageFileName = request.Image?.FileName
        };
        
        var createdId = await _mediator.Send(command);
        
        return Ok(createdId);
    }
}