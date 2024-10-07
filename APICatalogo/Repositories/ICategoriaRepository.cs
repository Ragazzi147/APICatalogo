using APICatalogo.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace APICatalogo.Repositories
{
    public interface ICategoriaRepository
    {
        object Categorias { get; set; }

        IEnumerable<Categoria> GetCategorias();
        IEnumerable<Categoria> GetCategoriasProdutos();
        Categoria GetCategoria(int id);
        Categoria Create(Categoria categoria);
        Categoria Update(Categoria categoria);  
        Categoria Delete(int id);   

    }
}
