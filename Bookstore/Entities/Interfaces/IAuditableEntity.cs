using System;
namespace Bookstore.Entities.Interfaces
{
    public interface IAuditableEntity : ISoftDeletable
    {
        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }

    }
}
