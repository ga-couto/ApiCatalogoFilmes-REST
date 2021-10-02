using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiCatalogoFilmes.ViewModel;
using ApiCatalogoFilmes.InputModel;
using ApiCatalogoFilmes.Services;
using ApiCatalogoFilmes.Exceptions;

namespace ApiCatalogoFilmes.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        
        private readonly IFilmeServices _filmeServices;

        public FilmesController(IFilmeServices filmeServices)
        {
            _filmeServices = filmeServices;
        }


       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var filmes = await _filmeServices.Obter(pagina, quantidade);

            if (filmes.Count() == 0)
                return NoContent();

            return Ok(filmes);
        }

        [HttpGet("{idFilme:guid}")]
        public async Task<ActionResult<FilmeViewModel>> Obter([FromRoute] Guid idFilme)
        {
            var filme = await _filmeServices.Obter(idFilme);

            if (filme == null)
                return NoContent();

            return Ok(filme);
        }

        [HttpPost]
        public async Task<ActionResult<List<FilmeViewModel>>> InserirFilme([FromBody] FilmeInputModel filmeInputModel)        
        {
            try
            {
                var filme = await _filmeServices.Inserir(filmeInputModel);

                return Ok(filme);
            }
            catch (FilmeJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe esse filme numa prdutora.");
            }
        }

        [HttpPut("{idFilme:guid}")]
        public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idFilme, [FromBody] FilmeInputModel filmeInputModel)        
        {
            try
            {
                await _filmeServices.Atualizar(idFilme, filmeInputModel);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Esse filme não existe em nosso catalogo.");
            }
        }//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        [HttpPatch("{idFilme:guid}/sinopse/{sinopse:string}")]
        public async Task<ActionResult<List<object>>> AtualizarFilme(Guid idFilme, string sinopse)
        {
            try
            {
                await _filmeServices.Atualizar(idFilme, sinopse);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Esse filme não existe em nosso catalogo.");
            }
        }

        [HttpPatch("{idFilme:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idFilme, [FromRoute] double preco)        
        {
            try
            {
                await _filmeServices.Atualizar(idFilme, preco);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Esse filme não existe em nosso catalogo.");
            }
        }


        [HttpDelete("{idFilme:guid}")]
        public async Task<ActionResult> DeletarFilme([FromRoute] Guid idFilme)        
        {
            try
            {
                await _filmeServices.Deletar(idFilme);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Esse filme não existe em nosso catalogo.");
            }
        }
    }
}
