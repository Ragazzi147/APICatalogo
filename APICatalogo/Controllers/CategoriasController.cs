using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
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
        private readonly AppDbContext _context;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(AppDbContext context,
                                    ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;   
        }

         [HttpGet("UsandoFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoFromServices
            ([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

         [HttpGet("SemUsarFromServices/{nome}")]
         public ActionResult<string> GetSaudacaoSemFromServices
            ([FromServices] IMeuServico meuServico, string nome)
         {
             return meuServico.Saudacao(nome);
         }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(p =>  p.Produtos).ToList();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
         
                return await _context.Categorias.AsNoTracking().ToListAsync();
      
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

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
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning($"Dados inválidos");
                return BadRequest("Dados inválidos");
            }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com id = {id} não encontrada...");
                return NotFound($"Categoria com id = {id} não encontrada...");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }
}
