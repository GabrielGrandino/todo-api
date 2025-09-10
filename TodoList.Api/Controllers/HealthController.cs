using Microsoft.AspNetCore.Mvc;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Endpoint de teste
        /// </summary>
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "TodoList API está rodando", timestamp = DateTime.UtcNow });
    }
}
