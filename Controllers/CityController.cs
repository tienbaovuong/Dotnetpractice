using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
  private readonly ICityService _cityService;
  public CityController(ICityService cityService)
  {
    _cityService = cityService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<City>>> GetAllCities()
  {
    var cities = await _cityService.GetAllCities();
    return Ok(cities);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<City>> GetCityById(Guid id)
  {
    var city = await _cityService.GetCityById(id);
    if (city == null)
    {
      return NotFound();
    }
    return Ok(city);
  }

  [HttpPost]
  public async Task<IActionResult> CreateCity([FromBody] City city)
  {
    if (city == null)
    {
      return BadRequest();
    }
    city.Id = Guid.NewGuid();
    await _cityService.CreateCity(city);
    return CreatedAtAction(nameof(GetCityById), new { id = city.Id }, city);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateCity(Guid id, [FromBody] City city)
  {
    if (city == null)
    {
      return BadRequest();
    }
    try
    {
      await _cityService.UpdateCity(id, city);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCity(Guid id)
  {
    try
    {
      await _cityService.DeleteCity(id);
    }
    catch (ArgumentException ex) when (ex.Message.Contains("not found"))
    {
      return NotFound();
    }
    return NoContent();
  }
}