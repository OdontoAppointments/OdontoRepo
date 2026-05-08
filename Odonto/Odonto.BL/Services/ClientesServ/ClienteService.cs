using FluentValidation;
using Odonto.BL.DTO.ClienteDTO.Request;
using Odonto.BL.DTO.ClienteDTO.Response;
using Odonto.BL.ServicesInterfaces.IntClientesServ;
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
        _repository         = repository;
        _criarValidator     = criarValidator;
        _atualizarValidator = atualizarValidator;
    }

    public async Task<Result<ClienteResponse>> ObterPorIdAsync(int id)
    {
        var cliente = await _repository.ObterPorId(id);

        if (cliente is null)
            return Result<ClienteResponse>.Falha($"Cliente com ID {id} não encontrado.");

        return Result<ClienteResponse>.Ok(MapearParaResponse(cliente));
    }

    public async Task<Result<ClienteResponse>> ObterPorNomeAsync(string nome)
    {
        var cliente = await _repository.ObterPorNome(nome);

        if (cliente is null)
            return Result<ClienteResponse>.Falha($"Cliente '{nome}' não encontrado.");

        return Result<ClienteResponse>.Ok(MapearParaResponse(cliente));
    }

    public async Task<Result<IEnumerable<ClienteResponse>>> ListarAsync()
    {
        var clientes = await _repository.ObterTodos();

        if (clientes is null || !clientes.Any())
            return Result<IEnumerable<ClienteResponse>>.Ok(
                Enumerable.Empty<ClienteResponse>(),
                "Nenhum cliente cadastrado."
            );

        return Result<IEnumerable<ClienteResponse>>.Ok(
            clientes.Select(MapearParaResponse),
            "Clientes listados com sucesso."
        );
    }

    public async Task<Result<ClienteResponse>> CriarAsync(CriarClienteRequest request)
    {
        var validacao = await _criarValidator.ValidateAsync(request);
        if (!validacao.IsValid)
        {
            var erros = string.Join("; ", validacao.Errors.Select(e => e.ErrorMessage));
            return Result<ClienteResponse>.Falha(erros);
        }

        var emailEmUso = await _repository.ObterPorEmail(request.Email);
        if (emailEmUso is not null)
            return Result<ClienteResponse>.Falha($"O e-mail '{request.Email}' já está cadastrado.");

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

        return Result<ClienteResponse>.Ok(MapearParaResponse(cliente), "Cliente criado com sucesso.");
    }

    public async Task<Result<ClienteResponse>> AtualizarAsync(int id, AtualizarClienteRequest request)
    {
        var validacao = await _atualizarValidator.ValidateAsync(request);
        if (!validacao.IsValid)
        {
            var erros = string.Join("; ", validacao.Errors.Select(e => e.ErrorMessage));
            return Result<ClienteResponse>.Falha(erros);
        }

        var cliente = await _repository.ObterPorId(id);
        if (cliente is null)
            return Result<ClienteResponse>.Falha($"Cliente com ID {id} não encontrado.");

        if (request.Email is not null && request.Email != cliente.Email)
        {
            var emailEmUso = await _repository.ObterPorEmail(request.Email);
            if (emailEmUso is not null)
                return Result<ClienteResponse>.Falha($"O e-mail '{request.Email}' já está cadastrado.");
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

        return Result<ClienteResponse>.Ok(MapearParaResponse(cliente), "Cliente atualizado com sucesso.");
    }

    public async Task<Result<bool>> RemoverAsync(int id)
    {
        var cliente = await _repository.ObterPorId(id);

        if (cliente is null)
            return Result<bool>.Falha($"Cliente com ID {id} não encontrado.");

        await _repository.Remover(cliente.Id);

        return Result<bool>.Ok(true, "Cliente removido com sucesso.");
    }

    private static ClienteResponse MapearParaResponse(Cliente cliente) =>
        new(
            Id:             cliente.Id,
            Nome:           cliente.Nome,
            Email:          cliente.Email,
            Telefone:       cliente.Telefone,
            Endereco: new EnderecoResponse(
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