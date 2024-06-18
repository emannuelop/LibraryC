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
    public class BibliotecaController : ControllerBase
    {
        private readonly IBibliotecaRepository _bibliotecaRepository;

        private readonly IMapper _mapper;

        public BibliotecaController(IBibliotecaRepository bibliotecaRepository, IMapper mapper)
        {
            _bibliotecaRepository = bibliotecaRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biblioteca>>> GetBibliotecaes()
        {
            var bibliotecaes = await _bibliotecaRepository.SelecionarTodos();
            var bibliotecaesDTO = _mapper.Map<IEnumerable<BibliotecaResponseDTO>>(bibliotecaes);

            return Ok(bibliotecaesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarBiblioteca(BibliotecaDTO biblioteca)
        {
            _bibliotecaRepository.Incluir(_mapper.Map<Biblioteca>(biblioteca));
            if (await _bibliotecaRepository.SaveAllAsync())
            {
                return Ok(biblioteca);
            }

            return BadRequest("Erro ao salvar biblioteca");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarBiblioteca(int id, BibliotecaDTO biblioteca)
        {
            var bibliotecaUpdate = await _bibliotecaRepository.SelecionarPorId(id);

            bibliotecaUpdate.Nome = biblioteca.Nome;
            bibliotecaUpdate.Endereco = biblioteca.Endereco;

            _bibliotecaRepository.Alterar(bibliotecaUpdate);
            if (await _bibliotecaRepository.SaveAllAsync())
            {
                return Ok(bibliotecaUpdate);
            }

            return BadRequest("Erro ao alterar biblioteca");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirBiblioteca(int id)
        {

            var biblioteca = await _bibliotecaRepository.SelecionarPorId(id);

            _bibliotecaRepository.Excluir(biblioteca);
            if (await _bibliotecaRepository.SaveAllAsync())
            {
                return Ok("Biblioteca excluido com sucesso");
            }

            return BadRequest("Erro ao excluir biblioteca");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var biblioteca = await _bibliotecaRepository.SelecionarPorId(id);
            if (biblioteca == null)
            {
                return NotFound("Biblioteca não encontrado");
            }
            return Ok(_mapper.Map<BibliotecaResponseDTO>(biblioteca));
        }
    }
}
