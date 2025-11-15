using NS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NS.Domain.Entities;

[Table("NS_CONTEUDO_TRILHA")]
public class ConteudoEntity
{
    [Key]
    [Column("ID_CONTEUDO")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo título é obrigatorio!")]
    [MinLength(3, ErrorMessage = "O título deve ter no mínimo 3 caracteres!")]
    [MaxLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres!")]
    [Column("TITULO", TypeName = "varchar2(150)")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O campo descrição é obrigatorio!")]
    [MinLength(10, ErrorMessage = "A descrição deve ter no mínimo 10 caracteres!")]
    [MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres!")]
    [Column("DESCRICAO", TypeName = "varchar2(400)")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo tipo é obrigatorio!")]
    [Column("TIPO", TypeName = "varchar2(30)")]
    public TipoConteudo Tipo { get; set; }

    [Column("LINK", TypeName = "varchar2(150)")]
    public string Link { get; set; }

    [Column("DATA_CRIACAO", TypeName = "TIMESTAMP(6)")]
    public DateTime DataCriacao { get; set; }

    [Required]
    [ForeignKey("Trilha")]
    [Column("ID_TRILHA")]
    public long IdTrilha { get; set; }

    [JsonIgnore]
    public TrilhaEntity Trilha { get; set; }
}
