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
        public ToDoRepositoryTest()
        {
            DbContextOptionsBuilder<ApplicationDBContext> dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString() // Use GUID so every test will use a different db
                );

            _context = new ApplicationDBContext(dbOptions.Options);
            _logger = new Mock<ILogger>();
        }

        [Fact]
        public async void Todo_AddNewTodo_NewItemAdded()
        {
            // Arrange
            var repo = new ToDoRepository(_context, _logger.Object);
            ToDo todo = new()
            {
                Id = 1,
                Name = "Test",
                IsCompleted = true
            };

            // Act
            await repo.AddAsync(todo);
            _context.SaveChanges();

            // Assert
            List<ToDo> users = _context.ToDos.ToList();
            Assert.Equal(1, todo.Id);
            Assert.Single(users);
        }

        [Fact]
        public async void Todo_DeleteNewTodo_ItemDeleted()
        {
            // Arrange
            var repo = new ToDoRepository(_context, _logger.Object);
            ToDo todo = new()
            {
                Id = 1,
                Name = "Test",
                IsCompleted = true
            };
            await repo.AddAsync(todo);
            _context.SaveChanges();

            // Act
            _context.ToDos.Remove(todo);
            _context.SaveChanges();

            // Assert

            List<ToDo> users = _context.ToDos.ToList();
            Assert.Empty(users);
        }

        [Fact]
        public async void Todo_GetNewTodo_ItemRetrived()
        {
            // Arrange
            var repo = new ToDoRepository(_context, _logger.Object);
            ToDo todo = new()
            {
                Id = 1,
                Name = "Test",
                IsCompleted = true
            };
            await repo.AddAsync(todo);
            _context.SaveChanges();

            // Act
            var todoRetrived = _context.ToDos.Find(todo.Id);

            // Assert
            Assert.Equal(1, todoRetrived.Id);
        }
    }
}
