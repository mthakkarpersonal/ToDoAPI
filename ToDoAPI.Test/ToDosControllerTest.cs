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
            _uow.Setup(_ => _.ToDoRepository.GetAllAsync()).ReturnsAsync(new List<ToDo>());

            // Act
            var result = (OkObjectResult)await _toDoController.Get();

            //Assert
            _uow.Verify(_ => _.ToDoRepository.GetAllAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task GetById_CallRequest_GetByIdInvoked()
        {
            //arrange
            int toDoId = 1;
            _uow.Setup(_ => _.ToDoRepository.GetAync(toDoId)).ReturnsAsync(new ToDo() { });

            // Act
            var result = (OkObjectResult)await _toDoController.GetToDo(toDoId);

            //Assert
            _uow.Verify(_ => _.ToDoRepository.GetAync(toDoId), Times.Exactly(1));
        }
    }
}
