using Microsoft.AspNetCore.Mvc;

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
  public async Task<ActionResult<IEnumerable<Suburb>>> GetAllSuburbs([FromQuery] Guid? cityId)
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
  public async Task<ActionResult<Suburb>> GetSuburbById(Guid id)
  {
    var suburb = await _suburbService.GetSuburbById(id);
    if (suburb == null)
    {
      return NotFound();
    }
    return Ok(suburb);
  }

  [HttpPost]
  public async Task<IActionResult> CreateSuburb([FromBody] Suburb suburb)
  {
    if (suburb == null)
    {
      return BadRequest();
    }
    suburb.Id = Guid.NewGuid();
    await _suburbService.CreateSuburb(suburb);
    return CreatedAtAction(nameof(GetSuburbById), new { id = suburb.Id }, suburb);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateSuburb(Guid id, [FromBody] Suburb suburb)
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