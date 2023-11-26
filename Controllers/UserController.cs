using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace passwordulatorServer.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUser")]
    public string Get()
    {
        return "usage: /user/{userid}";
    }

    [HttpGet("{userId}", Name = "Id")]
    public async Task<ActionResult<UserData>> GetUser(string UserId)
    {
        UserData User = new();

        string filePath = Environment.CurrentDirectory + "/userdata/" + UserId;
        if (System.IO.File.Exists(filePath)) {
            string fileStr = await System.IO.File.ReadAllTextAsync(filePath);
            if (fileStr != null) {
                User.Name = UserId;
                User.Data = fileStr;
                return User;
            } else {
                System.IO.File.Delete(filePath);
                return Ok();
            }

        }
        return NotFound();

    }

    [HttpPost(Name = "User")]
    public async Task<ActionResult<UserData>> PostUser(UserData User)
    {
        if (User.Name != null) {

            if (User.Data != null){
                if (!Directory.Exists(Environment.CurrentDirectory + "/userdata/")) {Directory.CreateDirectory(Environment.CurrentDirectory + "/userdata/");}
                string filePath = Environment.CurrentDirectory + "/userdata/" + User.Name;
                using (StreamWriter outputFile = new(filePath))
                {
                   await outputFile.WriteAsync(User.Data);
                }
                return User;

            }

        }
        return ValidationProblem();
    }   
    

}
