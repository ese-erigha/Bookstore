using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bookstore.Repository.Interfaces;
using Bookstore.Service.Interfaces;
using Bookstore.Core.Interfaces;
using Bookstore.Entities.Implementations;
using System.Collections.Generic;
using System.Linq;
using Bookstore.Service.Implementations;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Bookstore.Helpers;

namespace Bookstore.Tests.Services
{
    [TestClass]
    public class AuthorServiceTest
    {
        private Mock<IAuthorRepository> _mockRepository;
        private IAuthorService _service;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private List<Author> _listAuthor;
        private Author _author;
        private PaginationInfo _paginationInfo;
        private PaginationQuery<Author> _paginationResults;

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

            _author = _listAuthor[0];
            _paginationInfo = new PaginationInfo() {PageNumber = 1, PageSize = 3};
            _paginationResults = new PaginationQuery<Author>()
                {Count = _listAuthor.Count, Items = _listAuthor};
        }


        [TestMethod]
        public void Author_Find_By()
        {
            Expression<Func<Author, bool>> predicate = (y) => y.Id > 1;
            List<Author> mockResults = _listAuthor.Where(x => x.Id > 1).ToList();

            //Arrange
            _mockRepository.Setup(x => x.FindBy(predicate)).Returns(mockResults);

            //Act
            List<Author> results = _service.FindBy(predicate) as List<Author>;


            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public async Task Author_Find_One_By()
        {
            Expression<Func<Author, bool>> predicate = (y) => y.Id == 1;
            Author mockResult = _listAuthor.Where(x => x.Id == 1).SingleOrDefault();

            //Arrange
            _mockRepository.Setup((x) => x.FindOneBy(predicate)).ReturnsAsync(mockResult);

            //Act
            Author result = await _service.FindOneBy(predicate) as Author;


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mockResult, result);
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

        [TestMethod]
        public void Can_Paginate_Author()
        {
            //Arrange
            _mockRepository.Setup(x => x.Paginate(_paginationInfo)).Returns(_paginationResults);

            //Act
            PaginationQuery<Author> results = _service.Paginate(_paginationInfo);


            //Assert
            Assert.IsNotNull(results);
           //Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void Can_Update_Author()
        {
            
            _mockRepository.Setup(x => x.Update(_author)).Verifiable();
        }

        [TestMethod]
        public async Task Author_Get_One_By_Id()
        {
            //Arrange
            _mockRepository.Setup((x) => x.GetByIdAsync(_author.Id)).ReturnsAsync(_author);

            //Act
            Author result = await _service.GetByIdAsync(_author.Id) as Author;


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_author, result);
        }

        [TestMethod]
        public void Can_Add_Author()
        {
            //Arrange
            int Id = 1;
            Author author = new Author {FullName = "Femi Adesina"};
            _mockRepository.Setup(m => m.Add(author)).Returns((Author a) => {
                a.Id = Id;
                return a;
            });


            //Act
            _service.Create(author);

            //Assert
            Assert.AreEqual(Id, author.Id);

            //Verify that Repository was called only once
            _mockRepository.Verify(m => m.Add(author),Times.Once);
            
        }

        [TestMethod]
        public void Can_Delete_Author()
        {
            //Arrange
            Author author = _listAuthor[0];
            Author mockAuthor = _listAuthor[0];
            _mockRepository.Setup(m => m.Delete(author)).Returns((Author a) => a);


            //Act
            _service.Delete(author);

            //Assert
            Assert.AreEqual(author,mockAuthor);

            //Verify that Repository was called only once
            _mockRepository.Verify(m => m.Delete(author), Times.Once);
        }
    }
}
