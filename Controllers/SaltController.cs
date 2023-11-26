using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

// using System;
// using System.Text.Json;
// using System.Text.Json.Serialization;
using System.Text;

namespace passwordulatorServer.Controllers;

[ApiController]
[Route("[controller]")]
// [Route("[controller]/{id}")]
public class SaltController : ControllerBase
{
    private readonly ILogger<SaltController> _logger;

    public SaltController(ILogger<SaltController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetSalt")]
    public async Task<ActionResult<SaltData>> Get()
    {
        string salt = "";

        string saltFile = Environment.CurrentDirectory + "/salt";
        if (System.IO.File.Exists(saltFile)) {
            salt = await System.IO.File.ReadAllTextAsync(saltFile);
        }

        if (salt == "") {
            StringBuilder builder = new();
            Random random = new();
            char ch;
            for (int i = 0; i < 32; i++) {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(94 * random.NextDouble() + 32)));                 
                builder.Append(ch);
            }

            salt = builder.ToString();

            string filePath = Environment.CurrentDirectory + "/salt";
            using StreamWriter outputFile = new(filePath);
            {
                outputFile.Write(salt);
            }

        }

        SaltData sd = new() { Salt = salt };

        return sd;
    }

    
}


public struct SaltData
{
    public string Salt {get; set;}
}