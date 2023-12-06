using APIProductos.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            //checar si existe en la bd
            if (loginDTO.Usuario == "memo" && loginDTO.Usuario == "nuevonacho")
            {
                List<Claim> cliams = new()
                {
                    new Claim("Id","100"),
                    new Claim(ClaimTypes.Name,loginDTO.Usuario),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim("Carrera", "Sistemas")
                };
            }
        }
    }
}
