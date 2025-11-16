using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NS.Domain.Enums;

namespace NS.Domain.Entities;

[Table("NS_RESPOSTA_FORMULARIO")]
public class FormularioEntity
{
    [Key]
    [Column("ID_RESPOSTA")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo nivel experiencia é obrigatorio!")]
    [Column("NIVEL_EXPERIENCIA", TypeName = "varchar2(30)")]
    public NivelExperiencia NivelExperiencia { get; set; }

    [Required(ErrorMessage = "O campo objetivo carreira é obrigatorio!")]
    [Column("OBJETIVO_CARREIRA", TypeName = "varchar2(30)")]
    public ObjetivoCarreira ObjetivoCarreira { get; set; }

    [Required(ErrorMessage = "O campo area interesse 1 é obrigatorio!")]
    [Column("AREA_INTERESSE_1", TypeName = "varchar2(30)")]
    public AreaTecnologia AreaTecnologia1 { get; set; }

    [Column("AREA_INTERESSE_2", TypeName = "varchar2(30)")]
    public AreaTecnologia? AreaTecnologia2 { get; set; }

    [Column("AREA_INTERESSE_3", TypeName = "varchar2(30)")]
    public AreaTecnologia? AreaTecnologia3 { get; set; }

    [Required(ErrorMessage = "O campo horas de estudo é obrigatorio!")]
    [Column("TEMPO_ESTUDO_SEMANAL", TypeName = "varchar2(30)")]
    public HorasEstudo HorasEstudo { get; set; }

    [MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres!")]
    [Column("HABILIDADES_EXISTENTES", TypeName = "varchar2(400)")]
    public string? Habilidades { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    [Column("ID_USUARIO_FINAL")]
    public long IdUsuario { get; set; }

    [JsonIgnore]
    public UsuarioEntity Usuario { get; set; }
}
