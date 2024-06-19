using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        private readonly IMapper _mapper;

        public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientees()
        {
            var clientees = await _clienteRepository.SelecionarTodos();
            var clienteesDTO = _mapper.Map<IEnumerable<ClienteResponseDTO>>(clientees);

            return Ok(clienteesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarCliente(ClienteDTO cliente)
        {
            _clienteRepository.Incluir(_mapper.Map<Cliente>(cliente));
            if (await _clienteRepository.SaveAllAsync())
            {
                return Ok(cliente);
            }

            return BadRequest("Erro ao salvar cliente");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarCliente(int id, ClienteDTO cliente)
        {
            var clienteUpdate = await _clienteRepository.SelecionarPorId(id);

            clienteUpdate.Nome = cliente.Nome;
            clienteUpdate.Cpf = cliente.Cpf;
            clienteUpdate.Telefone = cliente.Telefone;
            clienteUpdate.Email = cliente.Email;

            _clienteRepository.Alterar(clienteUpdate);
            if (await _clienteRepository.SaveAllAsync())
            {
                return Ok(clienteUpdate);
            }

            return BadRequest("Erro ao alterar cliente");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirCliente(int id)
        {

            var cliente = await _clienteRepository.SelecionarPorId(id);

            _clienteRepository.Excluir(cliente);
            if (await _clienteRepository.SaveAllAsync())
            {
                return Ok(id);
            }

            return BadRequest("Erro ao excluir cliente");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var cliente = await _clienteRepository.SelecionarPorId(id);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado");
            }
            return Ok(_mapper.Map<ClienteResponseDTO>(cliente));
        }
    }
}
