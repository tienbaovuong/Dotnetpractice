using Microsoft.AspNetCore.Mvc;

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
  public async Task<ActionResult<IEnumerable<Street>>> GetAllStreets([FromQuery] Guid? cityId)
  {
    if (cityId == null)
    {
      var Streets = await _streetService.GetAllStreets();
      return Ok(Streets);
    }
    else
    {
      var Streets = await _streetService.GetStreetsBySuburbId(cityId);
      return Ok(Streets);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Street>> GetStreetById(Guid id)
  {
    var Street = await _streetService.GetStreetById(id);
    if (Street == null)
    {
      return NotFound();
    }
    return Ok(Street);
  }

  [HttpPost]
  public async Task<IActionResult> CreateStreet([FromBody] Street street)
  {
    if (street == null)
    {
      return BadRequest();
    }
    street.Id = Guid.NewGuid();
    await _streetService.CreateStreet(street);
    return CreatedAtAction(nameof(GetStreetById), new { id = street.Id }, street);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateStreet(Guid id, [FromBody] Street street)
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