using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Model.MailDto
{
    public class UserAccountCreatedToken
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
    }
}