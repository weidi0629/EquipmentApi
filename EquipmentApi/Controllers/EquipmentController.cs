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

    public EquipmentController(IEquipmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Equipment> Get() => _service.GetAll(); //ex2

    /*  excercise 1
    public IEnumerable<Equipment> Get() => new[]
    {
        new Equipment(1, "Line1-Press", "Normal"),
        new Equipment(2, "Line2-Motor", "Fault"),
    };*/
}
