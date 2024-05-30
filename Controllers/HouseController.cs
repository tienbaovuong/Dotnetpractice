using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class HouseController : ControllerBase
{
  private readonly IHouseService _houseService;
  public HouseController(IHouseService houseService)
  {
    _houseService = houseService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<House>>> GetAllHouses([FromQuery] Guid? cityId)
  {
    if (cityId == null)
    {
      var Houses = await _houseService.GetAllHouses();
      return Ok(Houses);
    }
    else
    {
      var Houses = await _houseService.GetHousesByStreetId(cityId);
      return Ok(Houses);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<House>> GetHouseById(Guid id)
  {
    var House = await _houseService.GetHouseById(id);
    if (House == null)
    {
      return NotFound();
    }
    return Ok(House);
  }

  [HttpPost]
  public async Task<IActionResult> CreateHouse([FromBody] House house)
  {
    if (house == null)
    {
      return BadRequest();
    }
    house.Id = Guid.NewGuid();
    await _houseService.CreateHouse(house);
    return CreatedAtAction(nameof(GetHouseById), new { id = house.Id }, house);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateHouse(Guid id, [FromBody] House house)
  {
    if (house == null)
    {
      return BadRequest();
    }
    try
    {
      await _houseService.UpdateHouse(id, house);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteHouse(Guid id)
  {
    try
    {
      await _houseService.DeleteHouse(id);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }
}