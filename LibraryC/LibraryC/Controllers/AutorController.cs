using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using LibraryC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {

        private readonly IAutorRepository _autorRepository;

        private readonly IMapper _mapper;

        public AutorController(IAutorRepository autorRepository, IMapper mapper)
        {
            _autorRepository = autorRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            var autores = await _autorRepository.SelecionarTodos();
            var autoresDTO = _mapper.Map<IEnumerable<AutorResponseDTO>>(autores);

            return Ok(autoresDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarAutor(AutorDTO autor)
        {
            _autorRepository.Incluir(_mapper.Map<Autor>(autor));
            if(await _autorRepository.SaveAllAsync())
            {
                return Ok(autor);
            }

            return BadRequest("Erro ao salvar autor");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarAutor(int id,AutorDTO autor)
        {
            var autorUpdate = await _autorRepository.SelecionarPorId(id);

            autorUpdate.Nome = autor.Nome;

            _autorRepository.Alterar(autorUpdate);
            if (await _autorRepository.SaveAllAsync())
            {
                return Ok(autorUpdate);
            }

            return BadRequest("Erro ao alterar autor");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirAutor(int id)
        {

            var autor = await _autorRepository.SelecionarPorId(id);

            _autorRepository.Excluir(autor);
            if (await _autorRepository.SaveAllAsync())
            {
                return Ok("Autor excluido com sucesso");
            }

            return BadRequest("Erro ao excluir autor");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var autor = await _autorRepository.SelecionarPorId(id);
            if(autor == null)
            {
                return NotFound("Autor não encontrado");
            }
            return Ok(_mapper.Map<AutorResponseDTO>(autor));
        }

    }
}
