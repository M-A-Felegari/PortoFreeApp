using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace PortoFree.Api.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllSkills()
    {
        return Ok();
    }
}