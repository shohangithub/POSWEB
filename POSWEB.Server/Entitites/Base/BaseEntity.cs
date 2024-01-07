using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace POSWEB.Server.Entitites;

public interface IBaseEntity
{
    User CreatedBy { get; set; }
    DateTime CreatedTime { get; set; }
    User? LastUpdatedBy { get; set; }
    DateTime? LastUpdatedTime { get; set; }
}

public class BaseEntity
{
    public required int CreatedById { get; set; }
    public User CreatedBy { get; set; }
    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column(TypeName = "datetime")]
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public int? LastUpdatedById { get; set; }
    public User? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedTime { get; set; }
}
