using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RallyDakar.API.Modelos;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/pilotos")]
    public class PilotoController : ControllerBase
    {
        private readonly IPilotoRepositorio _pilotoRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<PilotoController> _logger;

        public PilotoController(IPilotoRepositorio pilotoRepositorio, IMapper mapper, ILogger<PilotoController> logger)
        {
            _pilotoRepositorio = pilotoRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpGet]
        //public IActionResult ObterTodos()
        //{
        //    try
        //    {
        //        var pilotos = _pilotoRepositorio.ObterTodos();

        //        if (!pilotos.Any())
        //            return NotFound();

        //        return Ok(pilotos);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Ideia é se a API estiver sendo consumida por um parceiro de equipe para ter mais detalhes sobre o erro
        //        //return BadRequest(ex.Message);

        //        //_logger.Info("ex.Message");

        //        return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
        //    }
        //}

        [HttpGet("{id}", Name = "Obter")]
        public IActionResult Obter(int id)
        {
            try
            {
                var piloto = _pilotoRepositorio.Obter(id);

                if (piloto == null)
                    return NotFound();

                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);

                return Ok(pilotoModelo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody] PilotoModelo pilotoModelo)
        {
            try
            {
                _logger.LogInformation("Mapeando piloto modelo.");
                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _logger.LogInformation($"Verificando se existe piloto com id informado {piloto.Id}");
                if (_pilotoRepositorio.Existe(piloto.Id))
                {
                    _logger.LogWarning($"Ja existe piloto com a mesma identificacao {piloto.Id}");
                    return StatusCode(409, "Ja existe um piloto com a mesma identificacao.");
                }

                _logger.LogInformation($"Adicionando piloto Nome {piloto.Nome} Sobrenome {piloto.SobreNome}");
                _pilotoRepositorio.Adicionar(piloto);
                _logger.LogInformation("Operacao adicionar piloto ocorreu sem erros");

                _logger.LogInformation("Mapeando o retorno");
                var pilotoModeloRetorno = _mapper.Map<PilotoModelo>(piloto);

                _logger.LogInformation("Chamando o metodo Obter");
                return CreatedAtRoute("Obter", new { id = piloto.Id }, pilotoModeloRetorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }

        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcialmente(int id, [FromBody] JsonPatchDocument<PilotoModelo> patchPilotoModelo)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(id))
                    return NotFound();

                var piloto = _pilotoRepositorio.Obter(id);

                var pilotoModelo = _mapper.Map<PilotoModelo>(piloto);


                patchPilotoModelo.ApplyTo(pilotoModelo);

                piloto = _mapper.Map(pilotoModelo, piloto);
                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] PilotoModelo pilotoModelo)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(pilotoModelo.Id))
                    return NotFound();

                var piloto = _mapper.Map<Piloto>(pilotoModelo);

                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var piloto = _pilotoRepositorio.Obter(id);

                if (piloto == null)
                    return NotFound();

                _pilotoRepositorio.Deletar(piloto);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpOptions]
        public IActionResult ListarOperacoesPermitidas()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, OPTIONS");
            return Ok();
        }

    }
}
