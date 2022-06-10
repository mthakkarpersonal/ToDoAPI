using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.DAL;
using ToDoAPI.DAL.BaseRepository;
using ToDoAPI.DAL.Repository;
using ToDoAPI.Model.Models;
using Xunit;

namespace ToDoAPI.Test
{
    public class ToDoRepositoryTest
    {
        private readonly ApplicationDBContext _context;
        private readonly Mock<ILogger> _logger;
        private readonly ToDoRepository _repo;
        private ToDo? _todo1 = null;
        private ToDo? _todo2 = null;
        public ToDoRepositoryTest()
        {
            DbContextOptionsBuilder<ApplicationDBContext> dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString() // Use GUID so every test will use a different db
                );

            _context = new ApplicationDBContext(dbOptions.Options);
            _logger = new Mock<ILogger>();
            _repo = new ToDoRepository(_context, _logger.Object);
            _todo1 = new()
            {
                Id = 1,
                Name = "Test",
                IsCompleted = true
            };
            _todo2 = new()
            {
                Id = 2,
                Name = "Test 2",
                IsCompleted = false
            };
        }

        [Fact]
        public async void Todo_GetAllTodo_AllItemRetrived()
        {
            // Arrange
            await _repo.AddAsync(_todo1);
            await _repo.AddAsync(_todo2);
            _context.SaveChanges();

            // Act
            var todoRetrived = await _repo.GetAllAsync();

            // Assert
            Assert.Equal(2, todoRetrived.Count);
        }

        [Fact]
        public async void Todo_AddNewTodo_ItemAdded()
        {
            // Arrange

            // Act
            await _repo.AddAsync(_todo1);
            _context.SaveChanges();

            // Assert
            IReadOnlyList<ToDo> users = await _repo.GetAllAsync();
            Assert.Equal(1, _todo1.Id);
            Assert.Single(users);
        }

        [Fact]
        public async void Todo_UpdateExistingTodo_ItemUpdated()
        {
            // Arrange
            await _repo.AddAsync(_todo1);
            _context.SaveChanges();
            var todoRetrived = await _repo.GetAync(_todo1.Id);

            // Act
            todoRetrived.IsCompleted = false;
            _repo.Update(todoRetrived);

            // Assert
            IReadOnlyList<ToDo> users = await _repo.GetAllAsync();
            Assert.Equal(false, _todo1.IsCompleted);
        }

        [Fact]
        public async void Todo_DeleteNewTodo_ItemDeleted()
        {
            // Arrange
            await _repo.AddAsync(_todo1);
            _context.SaveChanges();

            // Act
            _repo.Delete(_todo1.Id);
            _context.SaveChanges();

            // Assert
            IReadOnlyList<ToDo> users = await _repo.GetAllAsync();
            Assert.Empty(users);
        }

        [Fact]
        public async void Todo_GetNewTodo_ItemRetrived()
        {
            // Arrange
            await _repo.AddAsync(_todo1);
            _context.SaveChanges();

            // Act
            var todoRetrived = await _repo.GetAync(_todo1.Id);

            // Assert
            Assert.Equal(1, todoRetrived.Id);
        }
    }
}
