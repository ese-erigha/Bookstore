using System;
namespace Bookstore.Model.ResponseDto
{
    public class Base
    {
        public long Id { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}
