using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SuburbController : ControllerBase
{
  private readonly ISuburbService _suburbService;
  public SuburbController(ISuburbService suburbService)
  {
    _suburbService = suburbService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<SuburbDTO>>> GetAllSuburbs([FromQuery] Guid? cityId)
  {
    if (cityId == null)
    {
      var suburbs = await _suburbService.GetAllSuburbs();
      return Ok(suburbs);
    }
    else
    {
      var suburbs = await _suburbService.GetSuburbsByCityId(cityId);
      return Ok(suburbs);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<SuburbDetailDTO>> GetSuburbById(Guid id)
  {
    var suburb = await _suburbService.GetSuburbById(id);
    if (suburb == null)
    {
      return NotFound();
    }
    return Ok(suburb);
  }

  [Authorize(Roles = "admin")]
  [HttpPost]
  public async Task<IActionResult> CreateSuburb([FromBody] SuburbDTO suburb)
  {
    if (suburb == null)
    {
      return BadRequest();
    }
    suburb.Id = Guid.NewGuid();
    await _suburbService.CreateSuburb(suburb);
    return CreatedAtAction(nameof(GetSuburbById), new { id = suburb.Id }, suburb);
  }

  [Authorize(Roles = "admin")]
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateSuburb(Guid id, [FromBody] SuburbDTO suburb)
  {
    if (suburb == null)
    {
      return BadRequest();
    }
    try
    {
      await _suburbService.UpdateSuburb(id, suburb);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }

  [Authorize(Roles = "admin")]
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteSuburb(Guid id)
  {
    try
    {
      await _suburbService.DeleteSuburb(id);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }
}