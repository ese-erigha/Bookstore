using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using Bookstore.Entities.Implementations;
using Bookstore.Core.Implementations;
using System.Collections.Generic;
using System.Linq;
using Bookstore.Repository.Interfaces;
using Bookstore.Repository.Implementations;
using Bookstore.Tests.Helpers;

namespace Bookstore.Tests.Repositories
{
    [TestClass]
    public class AuthorRepositoryTest
    {
        private IAuthorRepository _repository;
        private Mock<DbSet<Author>> _mockAuthorSet;
        private Mock<DatabaseContext> _mockDatabaseContext;
        private IEnumerable<Author> _enumerableAuthor;
        private IQueryable<Author> authorData;
        private Author _author;

        [TestInitialize]
        public void Initialize()
        {
            _enumerableAuthor = new List<Author>()
            {
                new Author(){Id= 1, FullName = "Ese Erigha"},
                new Author(){Id= 2, FullName = "Abraham Ogbumah"},
                new Author(){Id= 3, FullName = "Ehimah Obuse"}
            };

            authorData = _enumerableAuthor.AsQueryable();

            _mockAuthorSet = new Mock<DbSet<Author>>();
            _author = _enumerableAuthor.ToList()[0];
        }

        /*[TestMethod]
        public void Author_Get_All()
        {
            
            _mockDatabaseContext = new Mock<DatabaseContext>();
            _mockDatabaseContext.Setup(x => x.Set<Author>()).Returns(_mockAuthorSet.Object) ;
            _repository = new AuthorRepository(_mockDatabaseContext.Object);

            
            var results = _repository.GetAll();


         
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.ToList().Count);
        }*/
    }
}
