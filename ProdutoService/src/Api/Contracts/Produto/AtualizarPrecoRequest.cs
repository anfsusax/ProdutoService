using System.ComponentModel.DataAnnotations;

public sealed class AtualizarPrecoRequest
{
    [Required]
    public decimal Preco { get; set; }
}
