using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public object Categorias { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            return _context.Categorias.ToList();
            
        }
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }
        public Categoria GetCategoria(int id)
        {
            return _context.Categorias.FirstOrDefault(c=> c.CategoriaId == id);
        }
        public Categoria Create(Categoria categoria)
        {
            if (categoria == null) 
            { 
                throw new ArgumentNullException(nameof(categoria));
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }
        public Categoria Update(Categoria categoria)
        {
            if (categoria == null) 
            {
                throw new ArgumentNullException(nameof(categoria));
            }
            _context.Entry(categoria).State= EntityState.Modified;
            _context.SaveChanges();
            
            return categoria;
        }
        public Categoria Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria;

        }

    }
}
