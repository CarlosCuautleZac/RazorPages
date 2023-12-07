using APIProductos.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                //checar si existe en la bd
                if (loginDTO.Usuario == "memo" && loginDTO.Contraseña == "nuevonacho")
                {
                    List<Claim> cliams = new()
                {
                    new Claim("Id","100"),
                    new Claim(ClaimTypes.Name,loginDTO.Usuario),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim("Carrera", "Sistemas")
                };

                    SecurityTokenDescriptor tokenDescriptor = new()
                    {
                        Issuer = "https://itesrc.net",
                        Audience = "panaderia.itesrc.com",
                        IssuedAt = DateTime.UtcNow,
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("panaderiaLave9.1G"))
                                , SecurityAlgorithms.HmacSha256),
                        Subject = new ClaimsIdentity(cliams, JwtBearerDefaults.AuthenticationScheme)
                    };

                    JwtSecurityTokenHandler handler = new();
                    var token = handler.CreateToken(tokenDescriptor);

                    return Ok(handler.WriteToken(token));
                }
                else
                    return Unauthorized("Nombre de usuario o contraseña incorrectas.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
