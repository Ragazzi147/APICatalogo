using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]// /produtos

    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        // /produtos/primeiro
        /*[HttpGet("primeiro")]
          [HttpGet("teste")]
          [HttpGet("/primeiro")]*/
        [HttpGet("{valor:alpha:length(5)}")]
        public ActionResult<Produto> Get(string valor)
        {

            var teste = valor;
            return _context.Produtos.FirstOrDefault();
            /* if (produto is null)
             {
                 return NotFound("Produtos não encontrados...");
             }
             return produto;*/
        }

        [HttpGet]
        public ActionResult<Produto> Get1()
        {
            var produto = _context.Produtos.FirstOrDefault();
            if (produto == null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produto;
        }

        // /produtos
        [HttpGet("get2")]
        public async Task<ActionResult<IEnumerable<Produto>>> Get2()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        // /produtos/id
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get([FromQuery] int id)
        {
            try
            {
                
                var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);
                if (produto == null)
                {
                    return NotFound("Produto não encontrado..."); 
                }
                return Ok(produto);
            }
            catch (Exception ) 
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao processar a solicitação. Por favor, tente novamente mais tarde.");
            }

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
