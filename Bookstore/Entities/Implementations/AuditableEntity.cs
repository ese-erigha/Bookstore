using System;
using System.ComponentModel.DataAnnotations;
using Bookstore.Entities.Interfaces;

namespace Bookstore.Entities.Implementations
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        [ScaffoldColumn(false)] //ASP.NET MVC will not generate controls in views
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedDate { get; set; }

        [ScaffoldColumn(false)] //ASP.NET MVC will not generate controls in views
        public bool IsDeleted { get; set; }
    }
}
