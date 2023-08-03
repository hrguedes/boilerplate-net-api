using Application.Autenticacao.Interfaces;
using Base;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base.v1;


[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
    #region Regras (CRUD)

    /// <summary>
    ///  Cadastrar nova Regra
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/regras/cadastro
    /// </remarks>
    /// <returns> Regra Cadastrada </returns>
    /// <response code="200">  Regra Cadastrada </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Regras")]
    [HttpPost("regras/cadastro")]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> CadastrarNovaRegra(
        [FromServices] IBaseService baseService,
        [FromBody] CadastrarOuEditarRegraRequest request
    )
    {
        try
        {
            var resp = await baseService.CadastrarRegra(request, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Editar Regra
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/regras/editar
    /// </remarks>
    /// <returns> Regra Atualizada </returns>
    /// <response code="200">  Regra Atualizada </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Regras")]
    [HttpPut("regras/editar")]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> EditarRegra(
        [FromServices] IBaseService baseService,
        [FromBody] CadastrarOuEditarRegraRequest request
    )
    {
        try
        {
            var resp = await baseService.EditarRegra(request, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Remover Regra
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="id"></param>
    /// /// <remarks>
    /// POST /api/v1/base/regras/remover/{id}
    /// </remarks>
    /// <returns> Regra Removida </returns>
    /// <response code="200">  Regra Removida </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Regras")]
    [HttpDelete("regras/remover/{id}")]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> RemoverRegra(
        [FromServices] IBaseService baseService,
        [FromRoute] string id
    )
    {
        try
        {
            var resp = await baseService.RemoverRegra(id, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Listar Regras
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/regras/listar
    /// </remarks>
    /// <returns> Lista de regras </returns>
    /// <response code="200">Lista de regras </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Regras")]
    [HttpPost("regras/listar")]
    [ProducesResponseType(typeof(ReturnOk<ListarRegrasResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<ListarRegrasResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> RemoverRegra(
        [FromServices] IBaseService baseService,
        [FromBody] ListarRegrasRequest request
    )
    {
        try
        {
            var resp = await baseService.ListarRegras(request);
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Detalhar Regras
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="id"></param>
    /// /// <remarks>
    /// POST /api/v1/base/regras/detalhes
    /// </remarks>
    /// <returns> Detalhes Regra </returns>
    /// <response code="200"> Detalhes Regra </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Regras")]
    [HttpGet("regras/detalhes/{id}")]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> DetalhesRegra(
        [FromServices] IBaseService baseService,
        [FromRoute] string id
    )
    {
        try
        {
            var resp = await baseService.DetalhesRegra(id);
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    #endregion

    #region Telas (CRUD)

    /// <summary>
    ///  Cadastrar nova Tela
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/telas/cadastro
    /// </remarks>
    /// <returns> Tela Cadastrada </returns>
    /// <response code="200">  Tela Cadastrada </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Telas")]
    [HttpPost("telas/cadastro")]
    [ProducesResponseType(typeof(ReturnOk<TelaResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<TelaResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> CadastrarNovaTela(
        [FromServices] IBaseService baseService,
        [FromBody] CadastrarOuEditarTelaRequest request
    )
    {
        try
        {
            var resp = await baseService.CadastrarTela(request, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Editar Tela
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/telas/editar
    /// </remarks>
    /// <returns> Tela Atualizada </returns>
    /// <response code="200">  Tela Atualizada </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Telas")]
    [HttpPut("telas/editar")]
    [ProducesResponseType(typeof(ReturnOk<TelaResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<TelaResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> EditarTela(
        [FromServices] IBaseService baseService,
        [FromBody] CadastrarOuEditarTelaRequest request
    )
    {
        try
        {
            var resp = await baseService.EditarTela(request, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Remover Tela
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="id"></param>
    /// /// <remarks>
    /// POST /api/v1/base/telas/remover/{id}
    /// </remarks>
    /// <returns> Tela Removida </returns>
    /// <response code="200">  Tela Removida </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Telas")]
    [HttpDelete("telas/remover/{id}")]
    [ProducesResponseType(typeof(ReturnOk<TelaResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<TelaResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> RemoverTela(
        [FromServices] IBaseService baseService,
        [FromRoute] string id
    )
    {
        try
        {
            var resp = await baseService.RemoverTela(id, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Listar Telas
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/telas/listar
    /// </remarks>
    /// <returns> Listar telas </returns>
    /// <response code="200"> Listar telas </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Telas")]
    [HttpPost("telas/listar")]
    [ProducesResponseType(typeof(ReturnOk<ListarRegrasResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<ListarRegrasResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> RemoverTela(
        [FromServices] IBaseService baseService,
        [FromBody] ListarTelasRequest request
    )
    {
        try
        {
            var resp = await baseService.ListarTelas(request);
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Detalhar Tela
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="id"></param>
    /// /// <remarks>
    /// POST /api/v1/base/telas/detalhes
    /// </remarks>
    /// <returns> Detalhes Tela </returns>
    /// <response code="200"> Detalhes Tela </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Telas")]
    [HttpGet("telas/detalhes/{id}")]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> DetalhesTela(
        [FromServices] IBaseService baseService,
        [FromRoute] string id
    )
    {
        try
        {
            var resp = await baseService.DetalhesTela(id);
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    #endregion

    #region Usuarios (CRUD)

    /// <summary>
    ///  Cadastrar novo usuário
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/usuario/cadastro
    /// </remarks>
    /// <returns> Usuário cadastrado </returns>
    /// <response code="200"> Usuário cadastrado </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Usuarios")]
    [HttpPost("usuario/cadastro")]
    [ProducesResponseType(typeof(ReturnOk<UsuarioResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<UsuarioResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> CadastrarNovoUsuario(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] CadastrarUsuarioRequest request
    )
    {
        try
        {
            var resp = await usuarioService.CadastrarNovoUsuario(request, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Editar usuário
    /// </summary>
    /// <param name="baseService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/usuario/editar
    /// </remarks>
    /// <returns> Usuário atualizado </returns>
    /// <response code="200"> Usuário atualizado </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Usuarios")]
    [HttpPut("usuario/editar")]
    [ProducesResponseType(typeof(ReturnOk<UsuarioResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<UsuarioResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> EditarUsuario(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] EditarUsuarioRequest request
    )
    {
        try
        {
            var resp = await usuarioService.EditarUsuario(request, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Remover usuário
    /// </summary>
    /// <param name="usuarioService"></param>
    /// <param name="id"></param>
    /// /// <remarks>
    /// POST /api/v1/base/usuario/remover/{id}
    /// </remarks>
    /// <returns> Usuário removido </returns>
    /// <response code="200"> Usuário removido </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Usuarios")]
    [HttpDelete("usuario/remover/{id}")]
    [ProducesResponseType(typeof(ReturnOk<UsuarioResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<UsuarioResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> RemoverUsuario(
        [FromServices] IUsuarioService usuarioService,
        [FromRoute] string id
    )
    {
        try
        {
            var resp = await usuarioService.RemoverUsuario(id, new Guid().ToString());
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Listar usuários
    /// </summary>
    /// <param name="usuarioService"></param>
    /// <param name="request"></param>
    /// /// <remarks>
    /// POST /api/v1/base/usuario/listar
    /// </remarks>
    /// <returns> Lista de Usuários </returns>
    /// <response code="200"> Lista de Usuários</response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Usuarios")]
    [HttpPost("usuario/listar")]
    [ProducesResponseType(typeof(ReturnOk<ListarRegrasResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<ListarRegrasResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> ListarUsuarios(
        [FromServices] IUsuarioService usuarioService,
        [FromBody] ListarUsuariosRequest request
    )
    {
        try
        {
            var resp = await usuarioService.ListarUsuarios(request);
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    /// <summary>
    ///  Detalhar Usuário
    /// </summary>
    /// <param name="usuarioService"></param>
    /// <param name="id"></param>
    /// /// <remarks>
    /// POST /api/v1/base/usuario/detalhes
    /// </remarks>
    /// <returns> Detalhes Regra </returns>
    /// <response code="200"> Detalhes Regra </response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [Authorize("SUPORTE")]
    [ApiExplorerSettings(GroupName = "Usuarios")]
    [HttpGet("usuario/detalhes/{id}")]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 200)]
    [ProducesResponseType(typeof(ReturnOk<RegraResponse>), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> DetalhesUsuario(
        [FromServices] IUsuarioService usuarioService,
        [FromRoute] string id
    )
    {
        try
        {
            var resp = await usuarioService.DetalhesUsuario(id);
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    #endregion
}