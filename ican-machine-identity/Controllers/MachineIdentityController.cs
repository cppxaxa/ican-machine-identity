using System;
using Microsoft.AspNetCore.Mvc;

namespace ican_machine_identity.Controllers
{
    [ApiController]
    [Route("")]
    public class MachineIdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "ok" });
        }

        [HttpPost("env/{varName}")]
        public IActionResult Post(string varName)
        {
            if (varName != "KUBECONFIG")
            {
                return BadRequest("Only KUBECONFIG is supported");
            }

            var filePath = Environment.GetEnvironmentVariable(varName);
            if (filePath == null)
            {
                return NotFound($"Environment variable {varName} not found");
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File {filePath} not found");
            }

            var content = System.IO.File.ReadAllText(filePath);
            return Ok(content);
        }
    }
}
