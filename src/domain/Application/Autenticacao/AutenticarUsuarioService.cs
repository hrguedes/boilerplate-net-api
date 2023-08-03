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

public class AutenticarUsuarioService : IAutenticarUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISessaoRepository _sessaoRepository;
    private readonly IMapper _mapper;

    public AutenticarUsuarioService(IUsuarioRepository usuarioRepository, ISessaoRepository sessaoRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _sessaoRepository = sessaoRepository;
        _mapper = mapper;
    }

    public async Task SalvarTokenNaSessao(string id, string token)
    {
        var sessao = await _sessaoRepository.LoadRecordByIdAsync(id);
        if (sessao != null)
        {
            sessao.Token = token;
            await _sessaoRepository.UpdateRecordAsync(id, sessao);
        }
    }

    public async Task<ReturnOk<UsuarioLogadoResponse>> ObterUsuarioPorLogin(AutenticarUsuarioRequest request)
    {
        if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Senha))
            return new ReturnOk<UsuarioLogadoResponse>(null, new[] { "Por favor, informar Login e Senha" }, false, 401);
        
        var usuario = await _usuarioRepository.BuscarUsuarioPorLogin(request.Login);
        if (usuario == null)
            return new ReturnOk<UsuarioLogadoResponse>(null, new[] { "Usuário não encontrado" }, false, 401);

        var senhaRequisicao = EncryptionHelper.Encrypt(request.Senha);
        if (senhaRequisicao != usuario.Senha)
            return new ReturnOk<UsuarioLogadoResponse>(null, new[] { "Usuario/Senha Incorretos" }, false, 401);

        var dadosSessao = new Sessao(usuario.Id)
        {
            Inicio = DateTime.Now,
            Termino = DateTime.Now.AddHours(Convert.ToDouble(LaunchSettings.TEMPO_SESSAO_HORAS))
        };
        await _sessaoRepository.InsertRecordAsync(dadosSessao);
        
        return new ReturnOk<UsuarioLogadoResponse>(new UsuarioLogadoResponse()
        {
            Id = usuario.Id,
            Email = usuario.Email,
            Nome = usuario.Nome,
            Token = null,
            SessaoId = dadosSessao.Id,
            UsuarioWindows = usuario.UsuarioDominio,
            ValidoAte = dadosSessao.Termino,
            Telas = _mapper.Map<List<TelaResponse>>(usuario.Telas),
            Regras = _mapper.Map<List<RegraResponse>>(usuario.Regras)
            
        }, new []{ "Usuário Logado com sucesso"});
    }
}