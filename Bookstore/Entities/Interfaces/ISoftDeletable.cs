using System;
namespace Bookstore.Entities.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
