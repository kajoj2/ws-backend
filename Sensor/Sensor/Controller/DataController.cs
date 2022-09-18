using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sensor.Sensor.Services;

namespace Sensor.Sensor.Controller;

[ApiController]
[Route("[controller]")]
public class DataController
{
    [HttpGet]
    public async Task<IActionResult> Sensor()
    {
        QuestDbService service = new QuestDbService();


        var data = await service.GetTemperatureData("temperature");
        var json =  JsonConvert.SerializeObject(data);
        
         return new OkObjectResult(json);
    }
}