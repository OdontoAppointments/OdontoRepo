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

    // GET: api/clientes
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var clientes = await _service.ListarAsync();

        return Ok(clientes);
    }

    // GET: api/clientes/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var cliente = await _service.ObterPorIdAsync(id);

        return Ok(cliente);
    }

    // POST: api/clientes
    [HttpPost]
    public async Task<IActionResult> Criar(
        [FromBody] CriarClienteRequest request)
    {
        var clienteCriado = await _service.CriarAsync(request);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = clienteCriado.Id },
            clienteCriado
        );
    }

    // PUT: api/clientes/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id,
        [FromBody] AtualizarClienteRequest request)
    {
        var clienteAtualizado =
            await _service.AtualizarAsync(id, request);

        return Ok(clienteAtualizado);
    }

    // DELETE: api/clientes/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _service.RemoverAsync(id);

        return NoContent();
    }
}