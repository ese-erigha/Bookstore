using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bookstore.Service.Interfaces;
using Bookstore.Controllers;
using System.Collections.Generic;
using Bookstore.Entities.Implementations;

namespace Bookstore.Tests.Controllers
{
    [TestClass]
    public class AuthorControllerTest
    {
        private Mock<IAuthorService> _mockAuthorService;
        private AuthorController _authorController;
        private List<Author> Authors;

        [TestInitialize]
        public void Initialize()
        {
            _mockAuthorService = new Mock<IAuthorService>();
            
        }



        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
