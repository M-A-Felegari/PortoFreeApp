using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Domain.Constants;

namespace PortoFree.Api.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController : ControllerBase
{
    [Authorize(UserRoles.Owner)]
    [HttpGet]
    public IActionResult GetAllSkills()
    {
        return Ok();
    }
}