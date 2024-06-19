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
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoRepository _emprestimoRepository;

        private readonly ILivroRepository _livroRepository;

        private readonly IMapper _mapper;

        public EmprestimoController(IEmprestimoRepository emprestimoRepository, IMapper mapper,ILivroRepository livroRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _mapper = mapper;
            _livroRepository = livroRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimoes()
        {
            var emprestimoes = await _emprestimoRepository.SelecionarTodos();
            var emprestimoesDTO = _mapper.Map<IEnumerable<EmprestimoResponseDTO>>(emprestimoes);

            return Ok(emprestimoesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarEmprestimo(EmprestimoDTO emprestimo)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

            Emprestimo newEmprestimo = new Emprestimo();

            var livro = await _livroRepository.SelecionarPorId(emprestimo.IdLivro);

            if (livro == null)
            {
                return NotFound("Livro não encontrado");
            }

            newEmprestimo.IdLivro = emprestimo.IdLivro;
            newEmprestimo.IdCliente = emprestimo.IdCliente;
            newEmprestimo.DataPrevistaDevolucao = emprestimo.DataPrevistaDevolucao;
            newEmprestimo.DataEmprestimo = dataAtual;
            newEmprestimo.Status = "Nao devolvido";

            livro.Quantidade--;

            _livroRepository.Alterar(livro);


            _emprestimoRepository.Incluir(newEmprestimo);
            if (await _emprestimoRepository.SaveAllAsync())
            {
                return Ok(emprestimo);
            }

            return BadRequest("Erro ao salvar emprestimo");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarEmprestimo(int id, EmprestimoDTO emprestimo)
        {
            var emprestimoUpdate = await _emprestimoRepository.SelecionarPorId(id);

            emprestimoUpdate.IdCliente = emprestimo.IdCliente;
            emprestimoUpdate.IdLivro = emprestimo.IdLivro;
            emprestimoUpdate.DataPrevistaDevolucao = emprestimo.DataPrevistaDevolucao;

            _emprestimoRepository.Alterar(emprestimoUpdate);
            if (await _emprestimoRepository.SaveAllAsync())
            {
                return Ok(emprestimoUpdate);
            }

            return BadRequest("Erro ao alterar emprestimo");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirEmprestimo(int id)
        {

            var emprestimo = await _emprestimoRepository.SelecionarPorId(id);

            if (emprestimo.Status.Equals("Nao Devolvido"))
            {

                var livro = await _livroRepository.SelecionarPorId(emprestimo.IdLivro);

                livro.Quantidade++;

                _livroRepository.Alterar(livro);

            }

            _emprestimoRepository.Excluir(emprestimo);
            if (await _emprestimoRepository.SaveAllAsync())
            {
                return Ok(id);
            }

            return BadRequest("Erro ao excluir emprestimo");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var emprestimo = await _emprestimoRepository.SelecionarPorId(id);
            if (emprestimo == null)
            {
                return NotFound("Emprestimo não encontrado");
            }
            return Ok(_mapper.Map<EmprestimoResponseDTO>(emprestimo));
        }

        [HttpPatch("devolucao/{id}")]
        public async Task<ActionResult> DevoluçaoEmprestimo(int id)
        {

            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

            var emprestimo = await _emprestimoRepository.SelecionarPorId(id);
            if (emprestimo == null)
            {
                return NotFound("Emprestimo não encontrado");
            }

            emprestimo.Status = "Devolvido";
            emprestimo.DataDevolucao = dataAtual;

            var livro = await _livroRepository.SelecionarPorId(emprestimo.IdLivro);

            livro.Quantidade++;

            _livroRepository.Alterar(livro);

            _emprestimoRepository.Alterar(emprestimo);

            if (await _emprestimoRepository.SaveAllAsync())
            {
                return Ok(_mapper.Map<EmprestimoResponseDTO>(emprestimo));
            }

            return BadRequest();
        }
    }
}
