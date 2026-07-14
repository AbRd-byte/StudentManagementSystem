using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("AuthController is working!");
        }
        [HttpPost]
        public IActionResult Post()
        {
            return Ok("AuthController POST is working!");
        }
    }
}
