using Application.Autenticacao.Interfaces;
using AutoMapper;
using Base;
using Dto.Autenticacao.Request;
using Dto.Autenticacao.Response;
using Entities.Autenticacao;
using Entities.Autenticacao.Interfaces;

namespace Application.Autenticacao;

public class BaseService : IBaseService
{
    private readonly IRegraRepository _regraRepository;
    private readonly ITelaRepository _telaRepository;
    private readonly IMapper _mapper;

    public BaseService(IRegraRepository regraRepository, ITelaRepository telaRepository, IMapper mapper)
    {
        _regraRepository = regraRepository;
        _telaRepository = telaRepository;
        _mapper = mapper;
    }

    #region Methodos
    /// <summary>
    /// Validar Regras se existem e estão Ativas
    /// </summary>
    /// <param name="regras"></param>
    /// <returns></returns>
    public async Task<List<Regra>> ValidarRegras(string[] regras)
    {
        var regrasValidadas = new List<Regra>();
        foreach (var item in regras)
        {
            var regra = await _regraRepository.BuscarRegraPorNome(item);
            if (regra != null)
                regrasValidadas.Add(regra);
        }
        return regrasValidadas;
    }

    

    /// <summary>
    /// Validar Telas se existem e estão Ativas
    /// </summary>
    /// <param name="telas"></param>
    /// <returns></returns>
    public async Task<List<Tela>> ValidarTelas(string[] telas)
    {
        var telasValidadads = new List<Tela>();
        foreach (var item in telas)
        {
            var tela = await _telaRepository.BuscarTelaPorNome(item);
            if (tela != null)
                telasValidadads.Add(tela);
        }
        return telasValidadads;
    }
    #endregion
    
    #region Regras (CRUD)
    public async Task<ReturnOk<RegraResponse>> DetalhesRegra(string id)
    {
        if (string.IsNullOrEmpty(id))
            return new ReturnOk<RegraResponse>(null, new[] { "Por favor, informe a Regra" }, false, 400);
        var regra = await _regraRepository.LoadRecordByIdAsync(id);
        if(regra == null)
            return new ReturnOk<RegraResponse>(null, new[] { $"A Regra => {id} não foi encontrada" }, false, 400);
        return new ReturnOk<RegraResponse>(_mapper.Map<RegraResponse>(regra), new[] { "Detalhes Regra" });
    }
    public async Task<ReturnOk<ListarRegrasResponse>> ListarRegras(ListarRegrasRequest request)
    {
        return new ReturnOk<ListarRegrasResponse>(new ListarRegrasResponse()
        {
            Data = _mapper.Map<List<RegraResponse>>(await _regraRepository.ListarRegras(request.Limit, request.Page)),
            Limit = request.Limit,
            Page = request.Page
        }, new []{ "Registros" });
    }

    public async Task<ReturnOk<RegraResponse>> CadastrarRegra(CadastrarOuEditarRegraRequest request, string userId)
    {
        if (string.IsNullOrEmpty(request.Nome))
            return new ReturnOk<RegraResponse>(null, new[] { "Por favor, informe o nome da Regra" }, false, 400);
        var regraJaExiste = await _regraRepository.BuscarRegraPorNome(request.Nome);
        if(regraJaExiste != null)
            return new ReturnOk<RegraResponse>(null, new[] { $"A Regra => {request.Nome} já existe" }, false, 400);

        var novaRegra = new Regra(userId)
        {
            Nome = request.Nome,
            Descricao = request.Descricao ?? "NAO_FOI_INFORMADA",
            Chave = request.Nome.ToUpper()
                .Replace(" ", "")
                .Replace(",", "")
                .Replace(".", "")
                .Replace("_", "")
                .Replace("-", "")
                .Replace("/", "")
        };
        await _regraRepository.InsertRecordAsync(novaRegra);

        return new ReturnOk<RegraResponse>(_mapper.Map<RegraResponse>(novaRegra), new[] { "Cadastro Realizado" });
    }

    public async Task<ReturnOk<RegraResponse>> EditarRegra(CadastrarOuEditarRegraRequest request, string userId)
    {
        if (string.IsNullOrEmpty(request.Id))
            return new ReturnOk<RegraResponse>(null, new[] { "Por favor, informe a Regra ser alterada" }, false, 400);
        var regra = await _regraRepository.LoadRecordByIdAsync(request.Id);
        if(regra == null)
            return new ReturnOk<RegraResponse>(null, new[] { $"A Regra => {request.Id} não foi encontrada" }, false, 400);
        
        regra.UsuarioAtualizacaoId = userId;
        var regrAtualizada = _mapper.Map(request, regra);
        await _regraRepository.UpdateRecordAsync(regrAtualizada.Id, regrAtualizada);
        
        return new ReturnOk<RegraResponse>(_mapper.Map<RegraResponse>(regrAtualizada), new[] { "Cadastro Atualizado" });
    }

    public async Task<ReturnOk<RegraResponse>> RemoverRegra(string regraId, string userId)
    {
        if (string.IsNullOrEmpty(regraId))
            return new ReturnOk<RegraResponse>(null, new[] { "Por favor, informe a Regra a ser removida" }, false, 400);
        var regra = await _regraRepository.LoadRecordByIdAsync(regraId);
        if(regra == null)
            return new ReturnOk<RegraResponse>(null, new[] { $"A Regra => {regraId} não foi encontrada" }, false, 400);
        
        regra.RegistroRemovido = true;
        regra.UsuarioAtualizacaoId = userId;
        await _regraRepository.UpdateRecordAsync(regraId, regra);
        
        return new ReturnOk<RegraResponse>(_mapper.Map<RegraResponse>(regra), new[] { "Cadastro Removido" });
    }

    public async Task<ReturnOk<TelaResponse>> DetalhesTela(string id)
    {
        if (string.IsNullOrEmpty(id))
            return new ReturnOk<TelaResponse>(null, new[] { "Por favor, informe a Tela ser alterada" }, false, 400);
        var tela = await _telaRepository.LoadRecordByIdAsync(id);
        if(tela == null)
            return new ReturnOk<TelaResponse>(null, new[] { $"A Tela => {id} não foi encontrada" }, false, 400);
        return new ReturnOk<TelaResponse>(_mapper.Map<TelaResponse>(tela), new[] { "Detalhes Tela" });
    }

    #endregion
    
    #region Telas (CRUD)
    public async Task<ReturnOk<ListarTelasResponse>> ListarTelas(ListarTelasRequest request)
    {
        return new ReturnOk<ListarTelasResponse>(new ListarTelasResponse()
        {
            Data = _mapper.Map<List<TelaResponse>>(await _telaRepository.ListarTelas(request.Limit, request.Page)),
            Limit = request.Limit,
            Page = request.Page
        }, new []{ "Registros" });
    }
    
    public async Task<ReturnOk<TelaResponse>> CadastrarTela(CadastrarOuEditarTelaRequest request, string userId)
    {
        if (string.IsNullOrEmpty(request.Nome) || string.IsNullOrEmpty(request.Caminho))
            return new ReturnOk<TelaResponse>(null, new[] { "Por favor, informe o Nome e Caminho da Tela" }, false, 400);
        var telaJaExiste = await _telaRepository.BuscarTelaPorNome(request.Nome);
        if(telaJaExiste != null)
            return new ReturnOk<TelaResponse>(null, new[] { $"A Tela => {request.Nome} já existe" }, false, 400);
        telaJaExiste = await _telaRepository.BuscarUsuarioPorCaminhho(request.Caminho);
        if(telaJaExiste != null)
            return new ReturnOk<TelaResponse>(null, new[] { $"O Caminho informado => {request.Caminho} já existe" }, false, 400);

        var novaTela = new Tela(userId)
        {
            Nome = request.Nome,
            Caminho = request.Caminho
        };
        await _telaRepository.InsertRecordAsync(novaTela);

        return new ReturnOk<TelaResponse>(_mapper.Map<TelaResponse>(novaTela), new[] { "Cadastro Realizado" });
    }

    public async Task<ReturnOk<TelaResponse>> EditarTela(CadastrarOuEditarTelaRequest request, string userId)
    {
        if (string.IsNullOrEmpty(request.Id))
            return new ReturnOk<TelaResponse>(null, new[] { "Por favor, informe a Tela a ser alterada" }, false, 400);
        var tela = await _telaRepository.LoadRecordByIdAsync(request.Id);
        if(tela == null)
            return new ReturnOk<TelaResponse>(null, new[] { $"A Tela => {request.Id} não foi encontrada" }, false, 400);
        var telaJaExiste = await _telaRepository.BuscarTelaPorNome(request.Nome);
        if(telaJaExiste != null && telaJaExiste.Id != request.Id)
            return new ReturnOk<TelaResponse>(null, new[] { $"A Tela => {request.Nome} já existe" }, false, 400);
        telaJaExiste = await _telaRepository.BuscarUsuarioPorCaminhho(request.Caminho);
        if(telaJaExiste != null && telaJaExiste.Id != request.Id)
            return new ReturnOk<TelaResponse>(null, new[] { $"O Caminho informado => {request.Caminho} já existe" }, false, 400);
        
        
        tela.UsuarioAtualizacaoId = userId;
        var telaAtualizada = _mapper.Map(request, tela);
        await _telaRepository.UpdateRecordAsync(telaAtualizada.Id, telaAtualizada);
        
        return new ReturnOk<TelaResponse>(_mapper.Map<TelaResponse>(telaAtualizada), new[] { "Cadastro Atualizado" });
    }

    public async Task<ReturnOk<TelaResponse>> RemoverTela(string id, string userId)
    {
        if (string.IsNullOrEmpty(id))
            return new ReturnOk<TelaResponse>(null, new[] { "Por favor, informe a Tela a ser removida" }, false, 400);
        var tela = await _telaRepository.LoadRecordByIdAsync(id);
        if(tela == null)
            return new ReturnOk<TelaResponse>(null, new[] { $"A Tela => {id} não foi encontrada" }, false, 400);
        
        tela.RegistroRemovido = true;
        tela.UsuarioAtualizacaoId = userId;
        await _telaRepository.UpdateRecordAsync(id, tela);
        
        return new ReturnOk<TelaResponse>(_mapper.Map<TelaResponse>(tela), new[] { "Cadastro Removido" });
    }
    #endregion
}