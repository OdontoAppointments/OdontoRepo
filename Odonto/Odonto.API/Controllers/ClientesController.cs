using Microsoft.AspNetCore.Mvc;
using Odonto.BL.DTO.ClienteDTO.Request;
using Odonto.BL.ServicesInterfaces.IntClientesServ;

namespace Odonto.API.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var result = await _service.ListarAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var result = await _service.ObterPorIdAsync(id);

        return result.Sucesso
            ? Ok(result)
            : NotFound(result);
    }

    [HttpGet("{nome}")]
    public async Task<IActionResult> ObterPorNome(string nome)
    {
        var result = await _service.ObterPorNomeAsync(nome);

        return result.Sucesso
            ? Ok(result)
            : NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request)
    {
        var result = await _service.CriarAsync(request);

        return result.Sucesso
            ? CreatedAtAction(nameof(ObterPorId), new { id = result.Dados!.Id }, result)
            : BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarClienteRequest request)
    {
        var result = await _service.AtualizarAsync(id, request);

        return result.Sucesso
            ? Ok(result)
            : NotFound(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var result = await _service.RemoverAsync(id);

        return result.Sucesso
            ? Ok(result)
            : NotFound(result);
    }
}