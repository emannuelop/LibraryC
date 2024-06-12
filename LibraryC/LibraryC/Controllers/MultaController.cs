﻿using AutoMapper;
using LibraryC.DTOs;
using LibraryC.Interfaces;
using LibraryC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultaController : ControllerBase
    {
        private readonly IMultaRepository _multaRepository;

        private readonly IMapper _mapper;

        public MultaController(IMultaRepository multaRepository, IMapper mapper)
        {
            _multaRepository = multaRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Multa>>> GetMultaes()
        {
            var multaes = await _multaRepository.SelecionarTodos();
            var multaesDTO = _mapper.Map<IEnumerable<MultaResponseDTO>>(multaes);

            return Ok(multaesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarMulta(MultaDTO multa)
        {
            _multaRepository.Incluir(_mapper.Map<Multa>(multa));
            if (await _multaRepository.SaveAllAsync())
            {
                return Ok("Multa cadastrado com sucesso");
            }

            return BadRequest("Erro ao salvar multa");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterarMulta(int id, MultaDTO multa)
        {
            var multaUpdate = await _multaRepository.SelecionarPorId(id);

            multaUpdate.IdCliente = multa.IdCliente;
            multaUpdate.Valor = multa.Valor;

            _multaRepository.Alterar(multaUpdate);
            if (await _multaRepository.SaveAllAsync())
            {
                return Ok("Multa alterado com sucesso");
            }

            return BadRequest("Erro ao alterar multa");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirMulta(int id)
        {

            var multa = await _multaRepository.SelecionarPorId(id);

            _multaRepository.Excluir(multa);
            if (await _multaRepository.SaveAllAsync())
            {
                return Ok("Multa excluido com sucesso");
            }

            return BadRequest("Erro ao excluir multa");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> SelecionarPorId(int id)
        {
            var multa = await _multaRepository.SelecionarPorId(id);
            if (multa == null)
            {
                return NotFound("Multa não encontrado");
            }
            return Ok(_mapper.Map<MultaResponseDTO>(multa));
        }
    }
}