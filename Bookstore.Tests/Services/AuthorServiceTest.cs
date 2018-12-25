using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bookstore.Repository.Interfaces;
using Bookstore.Service.Interfaces;
using Bookstore.Core.Interfaces;
using Bookstore.Entities.Implementations;
using System.Collections.Generic;
using Bookstore.Service.Implementations;

namespace Bookstore.Tests.Services
{
    [TestClass]
    public class AuthorServiceTest
    {
        private Mock<IAuthorRepository> _mockRepository;
        private IAuthorService _service;
        Mock<IUnitOfWork> _mockUnitOfWork;
        List<Author> _listAuthor;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IAuthorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new AuthorService(_mockUnitOfWork.Object,_mockRepository.Object);
            _listAuthor = new List<Author>()
            {
                new Author(){Id= 1, FullName = "Ese Erigha"},
                new Author(){Id= 2, FullName = "Abraham Ogbumah"},
                new Author(){Id= 3, FullName = "Ehimah Obuse"}
            };
        }

        [TestMethod]
        public void Author_Get_All()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(_listAuthor);

            //Act
            List<Author> results = _service.GetAll() as List<Author>;


            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3,results.Count);
        }
    }
}
