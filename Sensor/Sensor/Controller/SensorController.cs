using Microsoft.AspNetCore.Mvc;

namespace Sensor.Sensor.Controller;

[ApiController]
[Route("[controller]")]
public class SensorController
{

    [HttpGet]
    public IActionResult Sensor()
    {

        return new OkObjectResult("Sensor");
    }
    
}