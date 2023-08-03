using Api.Services;
using Application.Autenticacao.Interfaces;
using Base;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Autenticacao.v1;


[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AutenticacaoController : ControllerBase
{
    /// <summary>
    /// Autenticar Usuário e Gerar Token
    /// </summary>
    /// <param name="usuarioService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/Autenticacao/login
    /// </remarks>
    /// <returns> Usuário Logado com Sucesso </returns>
    /// <response code="200">Usuário Logado com Sucesso</response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [HttpPost("login")]
    [ApiExplorerSettings(GroupName = "Autenticacao")]
    [ProducesResponseType(typeof(ReturnOk<List<UsuarioLogadoResponse>>), 200)]
    [ProducesResponseType(typeof(ReturnOk<List<UsuarioLogadoResponse>>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> AutenticarUsuario(
        [FromServices] IAutenticarUsuarioService usuarioService,
        [FromBody] AutenticarUsuarioRequest request
        )
    {
        try
        {
            var resp = await usuarioService.ObterUsuarioPorLogin(request);
            if (resp.Ok)
            {
                var jwtToken = await new AutenticaoService().GenerateToken(resp.Data);
                resp.Data.Token = jwtToken;
                await usuarioService.SalvarTokenNaSessao(resp.Data.SessaoId, jwtToken);
            }
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    /// <summary>
    /// Validar se o Usuário está Logado
    /// </summary>
    /// <remarks>
    /// POST /api/v1/Autenticacao/autenticado
    /// </remarks>
    /// <returns> Nome do Usuário </returns>
    /// <response code="200">Nome do Usuário</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [HttpGet("autenticado")]
    [ApiExplorerSettings(GroupName = "Autenticacao")]
    [ProducesResponseType(typeof(ReturnOk<string>), 200)]
    [ProducesResponseType(typeof(string), 500)]
    [Authorize]
    public IActionResult Authenticated()
    {
        try
        {
            return StatusCode(200, User.Identity.Name);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }    
    }
}