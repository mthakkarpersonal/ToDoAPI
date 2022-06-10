using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoAPI.API.Controllers;
using ToDoAPI.DAL.UnitOfWork;
using ToDoAPI.Model.Models;
using Xunit;

namespace ToDoAPI.Test
{
    public class ToDosControllerTest
    {
        private readonly Mock<IUnitOfWork> _uow;
        private readonly ToDosController _toDoController;
        public ToDosControllerTest()
        {
            _uow = new Mock<IUnitOfWork>();
            _toDoController = new ToDosController(_uow.Object);
        }

        [Fact]
        public async Task Get_CallRequest_GetInvoked()
        {
            //arrange
            List<ToDo> toDosList = new List<ToDo>();
            _uow.Setup(_ => _.ToDoRepository.GetAllAsync()).ReturnsAsync(toDosList);

            // Act
            var result = await _toDoController.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            _uow.Verify(_ => _.ToDoRepository.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task GetToDoById_WhenCalled_OkResultReturned()
        {
            //arrange
            int toDoId = 1;
            _uow.Setup(_ => _.ToDoRepository.GetAync(toDoId)).ReturnsAsync(new ToDo() { });

            // Act
            var result = await _toDoController.GetToDo(toDoId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            _uow.Verify(_ => _.ToDoRepository.GetAync(toDoId), Times.Exactly(1));
        }
        [Fact]
        public async Task GetToDoById_WhenCalled_NotFoundResultReturned()
        {
            //arrange
            int toDoId = 5;
            ToDo? todo = null;
            _uow.Setup(_ => _.ToDoRepository.GetAync(toDoId)).ReturnsAsync(todo);// .ReturnsAsync(null);

            // Act
            var result = await _toDoController.GetToDo(toDoId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            _uow.Verify(_ => _.ToDoRepository.GetAync(toDoId), Times.Exactly(1));
        }

        [Fact]
        public async Task Post_WhenCalled_OkResultReturned()
        {
            //arrange
            ToDo? todo = new ToDo
            {
                Id = 1,
                Name = "Test",
                IsCompleted = true
            };
            _uow.Setup(_ => _.ToDoRepository.AddAsync(todo)).ReturnsAsync(todo);

            // Act
            var result = await _toDoController.Post(todo);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
            _uow.Verify(_ => _.ToDoRepository.AddAsync(todo), Times.Exactly(1));
        }

        [Fact]
        public async Task Post_WhenCalled_BadRequestReturned()
        {
            //arrange
            ToDo? todo = new ToDo
            {
                Id = 1,
                //Name = "Test",
                IsCompleted = true
            };
            _toDoController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _toDoController.Post(todo);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /*[Fact]
        public async Task Update_WhenCalled_OkResultReturned()
        {
            //arrange
            int toDoId = 1;
            ToDo todo = new ToDo
            {
                Name = "Test",
                IsCompleted = true
            };
            _uow.Setup(_ => _.ToDoRepository.UpdateAsync(todo));
            _uow.Setup(_ => _.ToDoRepository.GetAync(toDoId)).ReturnsAsync(new ToDo() { });
           

            // Act
            var result = await _toDoController.Put(toDoId, todo);

            //Assert
            //Assert.IsType<OkResult>(result);
            _uow.Verify(_ => _.ToDoRepository.UpdateAsync(todo), Times.Exactly(1));
        } */

        [Fact]
        public async Task Delete_WhenCalled_OkResultReturned()
        {
            //arrange
            ToDo? todo = new ToDo
            {
                Id = 1,
                Name = "Test",
                IsCompleted = true
            };
            //_uow.Setup(_ => _.ToDoRepository.GetAync(todo.Id)).ReturnsAsync(new ToDo() { });
            _uow.Setup(_ => _.ToDoRepository.DeleteAsync(todo.Id)).ReturnsAsync(true);

            // Act
            var result = await _toDoController.Delete(todo.Id);

            //Assert
            Assert.IsType<OkResult>(result);
            _uow.Verify(_ => _.ToDoRepository.DeleteAsync(todo.Id), Times.Exactly(1));
        }
    }
}
