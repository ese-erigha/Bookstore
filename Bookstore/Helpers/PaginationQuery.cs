using System;
using System.Collections.Generic;
using Bookstore.Entities.Interfaces;

namespace Bookstore.Helpers
{
    public class PaginationQuery<TDest> where TDest : IAuditableEntity
    {
        public IList<TDest> Items { get; set; } = new List<TDest>();

        public int Count { get; set; }
    }
}
