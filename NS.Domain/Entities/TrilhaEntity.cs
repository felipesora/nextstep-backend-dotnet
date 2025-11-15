using NS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NS.Domain.Entities;

[Table("NS_TRILHA")]
public class TrilhaEntity
{
    [Key]
    [Column("ID_TRILHA")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatorio!")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres!")]
    [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres!")]
    [Column("NOME", TypeName = "varchar2(150)")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo descrição é obrigatorio!")]
    [MinLength(10, ErrorMessage = "A descrição deve ter no mínimo 10 caracteres!")]
    [MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres!")]
    [Column("DESCRICAO", TypeName = "varchar2(400)")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo Area é obrigatorio!")]
    [Column("AREA", TypeName = "varchar2(30)")]
    public AreaTrilha Area { get; set; }

    [Required(ErrorMessage = "O campo Nivel é obrigatorio!")]
    [Column("NIVEL", TypeName = "varchar2(30)")]
    public NivelTrilha Nivel { get; set; }

    [Required(ErrorMessage = "O campo Status é obrigatorio!")]
    [Column("STATUS", TypeName = "varchar2(30)")]
    public StatusTrilha Status { get; set; }

    [Column("DATA_CRIACAO", TypeName = "TIMESTAMP(6)")]
    public DateTime DataCriacao { get; set; }

    public ICollection<NotaTrilhaEntity> Notas { get; set; } = new List<NotaTrilhaEntity>();

    public ICollection<ConteudoEntity> Conteudos { get; set; } = new List<ConteudoEntity>();
}
