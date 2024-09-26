using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    
    [Required(ErrorMessage ="O Nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O Nome deve ter no máximo {1} caracteres")]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    
    [Required]
    [StringLength(300,ErrorMessage = "O Nome deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }
    
    [Required]
    [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}
