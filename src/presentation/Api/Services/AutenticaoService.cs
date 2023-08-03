using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Consts;
using Dto.Autenticacao.Response;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class AutenticaoService
{
    public  async Task<string> GenerateToken(UsuarioLogadoResponse user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(LaunchSettings.JWT_SECRET);
        var claims = new List<Claim>();
        user.Regras.Select(regra => new Claim(ClaimTypes.Role, regra.ToString())).ToList();
        user.Telas.Select(tela => new Claim(ClaimTypes.Role, tela.ToString())).ToList();
        claims.AddRange(new []
        {
            new Claim(ClaimTypes.Name, user.Nome.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString()),
            new Claim(ClaimTypes.WindowsAccountName, user.UsuarioWindows.ToString()),
        });
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}