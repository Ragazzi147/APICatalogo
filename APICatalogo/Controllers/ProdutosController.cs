using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]// /produtos
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        // /produtos/primeiro
        [HttpGet("primeiro")]
        public ActionResult<Produto> GetPrimeiro()
        {
            var produto = _context.Produtos.FirstOrDefault();
            if (produto is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produto;
        }

        // /produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }

        // /produtos/id
        [HttpGet("{id:int}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null) 
            {
                return NotFound("Produto não encontrado...");
            }
            return produto;
        }
        // /produtos
        [HttpPost]
        public ActionResult Post(Produto produto)
        {

            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId}, produto);
        }

        // /produtos/id
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto) 
        { 
        
            if (id != produto.ProdutoId )
            {
                return BadRequest();

            }

            _context.Entry(produto).State = EntityState .Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        // /produtos/id
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            
            if(produto is null)
            {
                return NotFound("Produto não localizado...");

            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
