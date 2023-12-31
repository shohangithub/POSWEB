using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
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
    public required User CreatedBy { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedTime { get; set; }
    public int? LastUpdatedById { get; set; }
    public User? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedTime { get; set; }
}
