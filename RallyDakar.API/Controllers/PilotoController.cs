using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;

namespace RallyDakar.API.Controllers
{
    [ApiController]
    [Route("api/pilotos")]
    public class PilotoController : ControllerBase
    {
        IPilotoRepositorio _pilotoRepositorio;

        public PilotoController(IPilotoRepositorio pilotoRepositorio)
        {
            _pilotoRepositorio = pilotoRepositorio;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            try
            {
                var pilotos = _pilotoRepositorio.ObterTodos();

                if (!pilotos.Any())
                    return NotFound();

                return Ok(pilotos);
            }
            catch (Exception ex)
            {
                //Ideia é se a API estiver sendo consumida por um parceiro de equipe para ter mais detalhes sobre o erro
                //return BadRequest(ex.Message);

                //_logger.Info("ex.Message");

                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpGet("{id}", Name = "Obter")]
        public IActionResult Obter(int id)
        {
            try
            {
                var piloto = _pilotoRepositorio.Obter(id);

                if (piloto == null)
                    return NotFound();

                return Ok(piloto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody] Piloto piloto)
        {
            try
            {
                if (_pilotoRepositorio.Existe(piloto.Id))
                    return StatusCode(409, "Ja existe um piloto com a mesma identificacao.");

                _pilotoRepositorio.Adicionar(piloto);


                return CreatedAtRoute("Obter", new { id = piloto.Id }, piloto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }

        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcialmente(int id, [FromBody] JsonPatchDocument<Piloto> patchPiloto)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(id))
                    return NotFound();

                var piloto = _pilotoRepositorio.Obter(id);

                patchPiloto.ApplyTo(piloto);

                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno no sistema. Por favor entre em contato com suporte.");
            }
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Piloto piloto)
        {
            try
            {
                if (!_pilotoRepositorio.Existe(piloto.Id))
                    return NotFound();

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

    }
}
