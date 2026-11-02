using Microsoft.AspNetCore.Mvc;
using EquipmentApi.Models;

namespace EquipmentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Equipment> Get() => new[]
    {
        new Equipment(1, "Line1-Press", "Normal"),
        new Equipment(2, "Line2-Motor", "Fault"),
    };
}
