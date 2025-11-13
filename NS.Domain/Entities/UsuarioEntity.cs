using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NS.Domain.Entities;

[Table("NS_USUARIO_FINAL")]
public class UsuarioEntity
{
    [Key]
    [Column("ID_USUARIO_FINAL")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatorio!")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres!")]
    [MaxLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres!")]
    [Column("NOME", TypeName = "varchar2(150)")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatorio!")]
    [Column("EMAIL", TypeName = "varchar2(150)")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatorio!")]
    [Column("SENHA", TypeName = "varchar2(150)")]
    public string Senha { get; set; }

    [Column("DATA_CADASTRO", TypeName = "TIMESTAMP(6)")]
    public DateTime DataCadastro { get; set; }

    public ICollection<NotaTrilhaEntity> Notas { get; set; } = new List<NotaTrilhaEntity>();
}
