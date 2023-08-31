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
            Regras = _mapper.Map<List<RegraResponse>>(usuario.Regras),
            Menus = new List<MenuResponse>()
            {
                new()
                {
                    Nome = "Home",
                    Items = new List<MenuResponse>()
                    {
                        new()
                        {
                            Nome = "Dashboard",
                            Regra = "",
                            Icone = "house",
                            Url = "/dasboard"
                        }
                    }
                },
                new()
                {
                    Nome = "Company",
                    Regra = "SALES",
                    Icone = "gear",
                    Items = new List<MenuResponse>()
                    {
                        new()
                        {
                            Nome = "Create",
                            Regra = "SALES",
                            Icone = "builder",
                            Url = "/company/create"
                        },
                        new()
                        {
                            Nome = "List",
                            Regra = "SALES",
                            Icone = "builder",
                            Url = "/company/list"
                        }
                    }
                },
                new()
                {
                    Nome = "Settings",
                    Regra = "ROOT_ADMIN",
                    Icone = "gear",
                    Items = new List<MenuResponse>()
                    {
                        new()
                        {
                            Nome = "Roles",
                            Regra = "ROOT_ADMIN",
                            Icone = "law",
                            Url = "/settings/roles",
                            Items = new List<MenuResponse>()
                            {
                                new()
                                {
                                    Nome = "Create",
                                    Regra = "ROOT_ADMIN",
                                    Icone = "plus-add",
                                    Url = "/settings/roles/create"
                                },
                                new()
                                {
                                    Nome = "List",
                                    Regra = "ROOT_ADMIN",
                                    Icone = "list",
                                    Url = "/settings/roles/list"
                                }
                            }
                        }
                    }
                },
            }
        }, new []{ "Usuário Logado com sucesso"});
    }
}