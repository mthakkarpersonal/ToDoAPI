using Microsoft.AspNetCore.Mvc;
using ToDoAPI.DAL.UnitOfWork;
using ToDoAPI.Model.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public ToDosController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/<ToDosController>
        [HttpGet]
        public async Task<IEnumerable<ToDo>> Get()
        {
            return await _uow.ToDoRepository.GetAllAsync();
        }

        // GET api/<ToDosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ToDosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ToDosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ToDosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
