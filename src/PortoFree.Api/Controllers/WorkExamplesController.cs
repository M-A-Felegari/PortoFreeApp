using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortoFree.Api.Dtos;
using PortoFree.Application.Features.WorkExamples.Commands.AddWorkExample;
using PortoFree.Application.Features.WorkExamples.Commands.DeleteWorkExample;
using PortoFree.Application.Features.WorkExamples.Commands.UpdateWorkExample;
using PortoFree.Application.Features.WorkExamples.Queries.GetWorkExamples;
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

    [HttpGet]
    public async Task<IActionResult> GetWorkExamples(int ownerId, int nextCursor, int limit)
    {
        var result = await _mediator.Send(new GetWorkExamplesQuery(ownerId, nextCursor, limit));
        
        return Ok(result);
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

    [HttpPut("{id:int}")]
    [Authorize(UserRoles.User)]
    public async Task<IActionResult> Update(int id, UpdateWorkExampleDto request)
    {
        var command = new UpdateWorkExampleCommand()
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            FinishDate = request.FinishDate,
        };
        
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(UserRoles.User)]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteWorkExampleCommand(id));
        
        return NoContent();
    }
}