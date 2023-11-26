using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace passwordulatorServer.Controllers;

[ApiController]
[Route("[controller]")]
// [Route("[controller]/{id}")]
public class ShameListController : ControllerBase
{
    private readonly ILogger<ShameListController> _logger;

    public ShameListController(ILogger<ShameListController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetShameList")]
    public string Get()
    {
        return "usage: get /shamelist/{servicename}";
    }

    [HttpGet("{service}", Name = "GetShame")]
    public async Task<ActionResult<ShameList>>  GetService(string service)
    {
        ShameList sl;

        string shame = Environment.CurrentDirectory + "/shamelist/" + service;
        if (System.IO.File.Exists(shame)) {
            
            string shamestr = await System.IO.File.ReadAllTextAsync(shame);
            try {
                sl = JsonSerializer.Deserialize<ShameList>(shamestr);
                if (sl != null) {
                    return sl;
                } else {
                    return new ShameList();
                }

            } catch {
                return NotFound();

            }

        }
        return NotFound();

    }


    
}
