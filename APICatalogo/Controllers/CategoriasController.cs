﻿using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoriaRepository repository,
                                    ILogger<CategoriasController> logger)
        {
            _repository = repository;
            _logger = logger;   
        }


        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categoria = _repository.GetCategoriasProdutos();
            return Ok(categoria);
        }

        [HttpGet]
        
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = _repository.GetCategorias();
            return Ok(categorias);

        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            
            var categoria = _repository.GetCategoria(id);

            if (categoria == null)
                {
                _logger.LogWarning($"Categoria com id= {id} Não encontrada");
                    return NotFound($"Categoria com id ={id} não encontrada...");
                }
            return Ok(categoria);

        }
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null) 
            { 
                _logger.LogWarning($"Dados inválidos");
                return BadRequest("Dados inválidos");
            }
            var categoriaCriada = _repository.Create(categoria);

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning($"Dados inválidos");
                return BadRequest("Dados inválidos");
            }
            _repository.Update(categoria);  
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _repository.GetCategoria(id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com id = {id} não encontrada...");
                return NotFound($"Categoria com id = {id} não encontrada...");
            }
            var categoriaExcluida = _repository.Delete(id);
            return Ok(categoriaExcluida);
        }


    }
}
