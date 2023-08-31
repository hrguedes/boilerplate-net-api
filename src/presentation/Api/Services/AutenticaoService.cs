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
        user.Regras.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Chave)));
        claims.AddRange(new []
        {
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.WindowsAccountName, user.UsuarioWindows),
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