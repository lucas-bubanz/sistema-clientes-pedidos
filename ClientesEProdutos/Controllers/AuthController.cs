using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientesEProdutos.Models.DTOs;
using ClientesEProdutos.Models.Entities;
using ClientesEProdutos.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ApplicationDBContext.Data;

namespace ClientesEProdutos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(AppDbContext context, TokenService tokenService, JwtSettings jwtSettings)
        {
            _context = context;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioTokenDto>> Registro(RegistroDto registroDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == registroDto.Email))
            {
                return BadRequest("Email já está em uso");
            }

            using var hmac = new HMACSHA512();
            var usuario = new Usuario
            {
                Nome = registroDto.Nome,
                Email = registroDto.Email,
                Senha = ComputeHash(registroDto.Senha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var token = _tokenService.GerarToken(usuario);

            return new UsuarioTokenDto
            {
                Token = token,
                ExpiracaoEmHoras = _jwtSettings.ExpiracaoHoras,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioTokenDto>> Login(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            if (usuario == null || !VerificarSenha(loginDto.Senha, usuario.Senha))
            {
                return Unauthorized("Email ou senha inválidos");
            }

            var token = _tokenService.GerarToken(usuario);

            return new UsuarioTokenDto
            {
                Token = token,
                ExpiracaoEmHoras = _jwtSettings.ExpiracaoHoras,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        private string ComputeHash(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerificarSenha(string senhaDigitada, string senhaHash)
        {
            var senhaDigitadaHash = ComputeHash(senhaDigitada);
            return senhaDigitadaHash == senhaHash;
        }
    }
}