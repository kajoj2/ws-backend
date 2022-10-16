using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sensor.Sensor.Services;

namespace Sensor.Sensor.Controller;

[ApiController]
[Route("[controller]")]
public class DataController
{
    readonly QuestDbService _service = new QuestDbService();
    [HttpGet]
    public async Task<IActionResult> Sensor()
    {
        var data = await _service.GetLast24hChartData("temperature");

        var json =  JsonConvert.SerializeObject(data);
        
         return new OkObjectResult(json);
    }

    [HttpGet("{type}/24h/ChartData")]
    public async Task<IActionResult> GetTemperature(string type)
    {
        var data = await _service.GetLast24hChartData(type);
        var json =  JsonConvert.SerializeObject(data);
        
        return new OkObjectResult(json);
    }

}