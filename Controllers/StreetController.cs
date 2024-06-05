using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StreetController : ControllerBase
{
  private readonly IStreetService _streetService;
  public StreetController(IStreetService streetService)
  {
    _streetService = streetService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<StreetDTO>>> GetAllStreets([FromQuery] Guid? suburbId)
  {
    if (suburbId == null)
    {
      var Streets = await _streetService.GetAllStreets();
      return Ok(Streets);
    }
    else
    {
      var Streets = await _streetService.GetStreetsBySuburbId(suburbId);
      return Ok(Streets);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<StreetDetailDTO>> GetStreetById(Guid id)
  {
    var Street = await _streetService.GetStreetById(id);
    if (Street == null)
    {
      return NotFound();
    }
    return Ok(Street);
  }

  [Authorize(Roles = "admin")]
  [HttpPost]
  public async Task<IActionResult> CreateStreet([FromBody] StreetDTO street)
  {
    if (street == null)
    {
      return BadRequest();
    }
    street.Id = Guid.NewGuid();
    await _streetService.CreateStreet(street);
    return CreatedAtAction(nameof(GetStreetById), new { id = street.Id }, street);
  }

  [Authorize(Roles = "admin")]
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateStreet(Guid id, [FromBody] StreetDTO street)
  {
    if (street == null)
    {
      return BadRequest();
    }
    try
    {
      await _streetService.UpdateStreet(id, street);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }

  [Authorize(Roles = "admin")]
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteStreet(Guid id)
  {
    try
    {
      await _streetService.DeleteStreet(id);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }
}