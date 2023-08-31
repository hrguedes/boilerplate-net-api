using Application.Autenticacao.Interfaces;
using AutoMapper;
using Base;
using Consts;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;
using Entities.Autenticacao;
using Entities.Autenticacao.Interfaces;
using Utils;

namespace Application.Autenticacao;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly IBaseService _baseService;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IBaseService baseService)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _baseService = baseService;
    }

    public async Task<ReturnOk<UsuarioResponse>> DetalhesUsuario(string id)
    {
        if (string.IsNullOrEmpty(id))
            return new ReturnOk<UsuarioResponse>(null, new[] { "Por favor, informe o usuário" }, false, 400);
        var usuario = await _usuarioRepository.LoadRecordByIdAsync(id);
        if(usuario == null)
            return new ReturnOk<UsuarioResponse>(null, new[] { $"O Usuario => {id} não foi encontrado" }, false, 400);
        return new ReturnOk<UsuarioResponse>(_mapper.Map<UsuarioResponse>(usuario), new[] { "Detalhes usuário" });
    }

    public async Task<ReturnOk<ListarUsuariosResponse>> ListarUsuarios(ListarUsuariosRequest request)
    {
        return new ReturnOk<ListarUsuariosResponse>(new ListarUsuariosResponse()
        {
            Data = _mapper.Map<List<UsuarioResponse>>(
                await _usuarioRepository.ListarUsuarios(request.Limit, request.Page)),
            Limit = request.Limit,
            Page = request.Page
        }, new[] { "Registros" });
    }

    public async Task<ReturnOk<UsuarioResponse>> CadastrarNovoUsuario(CadastrarUsuarioRequest request,
        string userId)
    {
        var isValid = await ValidarUsuarioParaCadastro(request);
        if (isValid.Count() > 0)
            return new ReturnOk<UsuarioResponse>(null, isValid.ToArray(), false, 400);
        var novoUsuario = new Usuario(userId)
        {
            Nome = request.Nome,
            Email = request.Email,
            Telefone = request.Telefone,
            UsuarioDominio = request.UsuarioDominio,
            Senha = EncryptionHelper.Encrypt(LaunchSettings.SENHA_PADRAO),
            Login = request.UsuarioDominio
        };
        if (request.Regras.Any())
        {
            var regrasValidadas = await _baseService.ValidarRegras(request.Regras.ToArray());
            foreach (var regra in regrasValidadas.Where(regra => !regra.RegistroRemovido))
            {
                novoUsuario.Regras.Add(regra);
            }
        }
        await _usuarioRepository.InsertRecordAsync(novoUsuario);
        return new ReturnOk<UsuarioResponse>(_mapper.Map<UsuarioResponse>(novoUsuario), new[] { "Registro Inserido" });
    }

    public async Task<ReturnOk<bool>> EditarUsuario(EditarUsuarioRequest request, string userId)
    {
        if (string.IsNullOrEmpty(request.Id))
            return new ReturnOk<bool>(false, new[] { "Informe o usuário a ser editado" }, false, 400);

        var usuario = await _usuarioRepository.LoadRecordByIdAsync(request.Id);
        if (usuario == null)
            return new ReturnOk<bool>(false, new[] { "Usuário não encontrado" }, false, 400);
        
        usuario.UsuarioAtualizacaoId = userId;
        usuario.Nome = request.Nome ?? usuario.Nome;
        usuario.Email = request.Email ?? usuario.Email;
        usuario.Telefone = request.Telefone ?? usuario.Telefone;
        usuario.UsuarioDominio = request.UsuarioDominio ?? usuario.UsuarioDominio;

        await _usuarioRepository.UpdateRecordAsync(usuario.Id, usuario);
        return new ReturnOk<bool>(true, new[] { "Cadastro Atualizado" });
    }

    public async Task<ReturnOk<bool>> RemoverUsuario(string usuarioRemoverId, string userId)
    {
        if (string.IsNullOrEmpty(usuarioRemoverId))
            return new ReturnOk<bool>(false, new[] { "Por favor, informe o Usuário a ser removido" }, false, 400);
        var usuario = await _usuarioRepository.LoadRecordByIdAsync(usuarioRemoverId);
        if(usuario == null)
            return new ReturnOk<bool>(false, new[] { $"O usuário => {usuarioRemoverId} não foi encontrado" }, false, 400);
        
        usuario.RegistroRemovido = true;
        usuario.UsuarioAtualizacaoId = userId;
        await _usuarioRepository.UpdateRecordAsync(usuarioRemoverId, usuario);
        
        return new ReturnOk<bool>(true, new[] { "Cadastro Removido" });
    }


    #region Validacões
    private async Task<List<string>> ValidarUsuarioParaCadastro(CadastrarUsuarioRequest request)
    {
        var erros = new List<string>();
        
        if (string.IsNullOrEmpty(request.Nome) ||
            string.IsNullOrEmpty(request.Email) ||
            string.IsNullOrEmpty(request.UsuarioDominio)
           )
        {
            erros.Add("Os campo Nome, Email e UsuarioDominio são obrigatórios");
        }
        var usuarioJaExiste = await _usuarioRepository.BuscarUsuarioFiltro(request.Nome, request.Email, request.UsuarioDominio);
        if (usuarioJaExiste != null)
            erros.Add($@"Usuario já cadastrado => {usuarioJaExiste.Id}");
        return erros;
    }
    #endregion
}