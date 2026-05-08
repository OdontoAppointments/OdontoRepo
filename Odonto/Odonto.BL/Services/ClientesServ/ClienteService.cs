using FluentValidation;
using Odonto.BL.DTO.ClienteDTO.Request;
using Odonto.BL.DTO.ClienteDTO.Response;
using Odonto.BL.ServicesInterfaces.IntClientesServ;
using Odonto.Domain.Exceptions.Clientes;
using Odonto.Domain.Models;
using Odonto.Infra.RepositoryInterfaces.IClienteRepo;

namespace Odonto.BL.Services.ClientesServ;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly IValidator<CriarClienteRequest> _criarValidator;
    private readonly IValidator<AtualizarClienteRequest> _atualizarValidator;
 
    public ClienteService(
        IClienteRepository repository,
        IValidator<CriarClienteRequest> criarValidator,
        IValidator<AtualizarClienteRequest> atualizarValidator)
    {
        _repository        = repository;
        _criarValidator    = criarValidator;
        _atualizarValidator = atualizarValidator;
    }
 
    public async Task<ClienteResponse> ObterPorIdAsync(int id)
    {
        var cliente = await _repository.ObterPorId(id)
            ?? throw new ClienteNaoEncontradoException(id);
 
        return MapearParaResponse(cliente);
    }

    public async Task<ClienteResponse> ObterPorNomeAsync(string nome)
    {
        var cliente = await _repository.ObterPorNome(nome)
            ?? throw new ClienteNaoEncontradoException(nome);

        return MapearParaResponse(cliente);
    }

    public async Task<IEnumerable<ClienteResponse>> ListarAsync()
    {
        var clientes = await _repository.ObterTodos();

        if (clientes == null || !clientes.Any())
            return Enumerable.Empty<ClienteResponse>();

        return clientes.Select(MapearParaResponse);
    }

    public async Task<ClienteResponse> CriarAsync(CriarClienteRequest request)
    {
        var validacao = await _criarValidator.ValidateAsync(request);
        if (!validacao.IsValid)
            throw new DadosInvalidosException(string.Join("; ", validacao.Errors.Select(e => e.ErrorMessage)));
 
        var emailEmUso = await _repository.ObterPorEmail(request.Email);
        if (emailEmUso != null)
            throw new EmailJaCadastradoException(request.Email);
 
        var endereco = new Endereco(
            request.Endereco.Rua,
            request.Endereco.Numero,
            request.Endereco.Bairro,
            request.Endereco.CEP,
            request.Endereco.Estado,
            request.Endereco.Cidade
        );
 
        var cliente = new Cliente(
            request.Nome,
            request.Email,
            request.Telefone,
            endereco,
            request.DataNascimento
        );
 
        await _repository.Adicionar(cliente);
 
        return MapearParaResponse(cliente);
    }
 
    public async Task<ClienteResponse> AtualizarAsync(int id, AtualizarClienteRequest request)
    {
        var validacao = await _atualizarValidator.ValidateAsync(request);
        if (!validacao.IsValid)
            throw new DadosInvalidosException(string.Join("; ", validacao.Errors.Select(e => e.ErrorMessage)));
 
        var cliente = await _repository.ObterPorId(id)
            ?? throw new ClienteNaoEncontradoException(id);
 
        if (request.Email is not null && request.Email != cliente.Email)
        {
            var emailEmUso = await _repository.ObterPorEmail(request.Email);
            if (emailEmUso != null)
                throw new EmailJaCadastradoException(request.Email);
        }
 
        cliente.Atualizar(request.Nome, request.Email, request.Telefone, request.DataNascimento);
 
        if (request.Endereco is not null)
        {
            cliente.Endereco.Atualizar(
                request.Endereco.Rua,
                request.Endereco.Numero,
                request.Endereco.Bairro,
                request.Endereco.CEP,
                request.Endereco.Estado,
                request.Endereco.Cidade
            );
        }
 
        await _repository.Atualizar(cliente);
 
        return MapearParaResponse(cliente);
    }
    
 
    public async Task RemoverAsync(int id)
    {
        var cliente = await _repository.ObterPorId(id)
            ?? throw new ClienteNaoEncontradoException(id);
 
        await _repository.Remover(cliente.Id);
    }
    
 
    private static ClienteResponse MapearParaResponse(Cliente cliente) =>
        new(
            Id:             cliente.Id,
            Nome:           cliente.Nome,
            Email:          cliente.Email,
            Telefone:       cliente.Telefone,
            Endereco:       new EnderecoResponse(
                Id:     cliente.Endereco.Id,
                Rua:    cliente.Endereco.Rua,
                Numero: cliente.Endereco.Numero,
                Bairro: cliente.Endereco.Bairro,
                CEP:    cliente.Endereco.CEP,
                Estado: cliente.Endereco.Estado,
                Cidade: cliente.Endereco.Cidade
            ),
            DataNascimento: cliente.DataNascimento,
            DataCriacao:    cliente.CriadoEm
        );
}