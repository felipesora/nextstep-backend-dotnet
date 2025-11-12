using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NS.Domain.Entities;

[Table("NS_NOTA_TRILHA")]
public class NotaTrilhaEntity
{
    [Key]
    [Column("ID_NOTA")]
    public long Id { get; set; }

    [Required]
    [Range(0, 5, ErrorMessage = "A nota deve estar entre 0 e 5.")]
    [Column("VALOR_NOTA", TypeName = "NUMBER(1)")]
    public int ValorNota { get; set; }

    [Column("OBSERVACAO", TypeName = "varchar2(400)")]
    public string Observacao { get; set; }

    [Required]
    [ForeignKey("Trilha")]
    [Column("ID_TRILHA")]
    public long IdTrilha { get; set; }

    public TrilhaEntity Trilha { get; set; }
}
