using System.ComponentModel.DataAnnotations.Schema;
namespace Bookstore.Entities.Implementations
{
    public abstract class BaseEntity : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }
    }
}
