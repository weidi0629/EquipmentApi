using Microsoft.AspNetCore.Mvc;
using EquipmentApi.Models;
using EquipmentApi.Services;

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
    public IEnumerable<Equipment> Get()
    {
        _logger.LogInformation("Get all equipment request received");

        return _service.GetAll();
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

        return Ok(equipment);
    }
}
