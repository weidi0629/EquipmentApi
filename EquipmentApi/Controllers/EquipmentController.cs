using Microsoft.AspNetCore.Mvc;
using EquipmentApi.Models;
using EquipmentApi.Services;
using EquipmentApi.Dtos;

namespace EquipmentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    // excercise 2
    private readonly IEquipmentService _service;
    private readonly ILogger<EquipmentController> _logger;

    public EquipmentController(IEquipmentService service,
    ILogger<EquipmentController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    //public IEnumerable<Equipment> Get() => _service.GetAll(); //ex2
    public IEnumerable<EquipmentDto> Get()
    {
        _logger.LogInformation("Get all equipment request received");

        //return _service.GetAll();
        //ex5
        return _service.GetAll().Select(e => new EquipmentDto(e.Id, e.Name, e.Status));

    }
    //ex 3
    [HttpGet("/equipment/page")]
    public IActionResult Page()
        => Content("<h1>设备列表</h1><p>CNC-01</p >", "text/html");
   
    /*  excercise 1
    public IEnumerable<Equipment> Get() => new[]
    {
        new Equipment(1, "Line1-Press", "Normal"),
        new Equipment(2, "Line2-Motor", "Fault"),
    };*/

    // ex 4
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        _logger.LogInformation(
        "Get equipment request received. Id: {Id}", id);

        var equipment = _service.GetById(id);

        if (equipment == null)
        {
            _logger.LogWarning(
            "Equipment not found. Id: {Id}", id);

            return NotFound(new
            {
                error = "equipment not exist"
            });
        }

        var dto = new EquipmentDto(equipment.Id, equipment.Name, equipment.Status);

        //return Ok(equipment);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateEquipmentDto dto)
    {
        _logger.LogInformation("Create equipment request received. Name: {Name}", dto.Name);

        var created = _service.Create(dto);
        var result = new EquipmentDto(created.Id, created.Name, created.Status);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] CreateEquipmentDto dto)
    {
        _logger.LogInformation("Update equipment request received. Id: {Id}", id);

        var updated = _service.Update(id, dto);

        if (updated == null)
        {
            _logger.LogWarning("Equipment not found. Id: {Id}", id);

            return NotFound(new
            {
                error = "equipment not exist"
            });
        }

        return Ok(new EquipmentDto(updated.Id, updated.Name, updated.Status));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Delete equipment request received. Id: {Id}", id);

        if (!_service.Delete(id))
        {
            _logger.LogWarning("Equipment not found. Id: {Id}", id);

            return NotFound(new
            {
                error = "equipment not exist"
            });
        }

        return NoContent();
    }

}
