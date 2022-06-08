﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _uow.ToDoRepository.GetAllAsync());
        }

        // GET api/<ToDosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todo = await _uow.ToDoRepository.GetAync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        // POST api/<ToDosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDo data)
        {
            await _uow.ToDoRepository.AddAsync(data);
            await _uow.SaveAsync();
            return Ok(data);
        }

        // PUT api/<ToDosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ToDo data)
        {
            var todo = await _uow.ToDoRepository.GetAync(id);
            if (todo == null)
            {
                return NotFound();
            }
            if (todo != null)
            {
                todo.Name = data.Name;
                todo.IsCompleted = data.IsCompleted;
            }
            _uow.ToDoRepository.Update(todo);
            await _uow.SaveAsync();
            return Ok();
        }

        // DELETE api/<ToDosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _uow.ToDoRepository.GetAync(id);
            if (todo == null)
            {
                return NotFound();
            }
            _uow.ToDoRepository.Delete(todo);
            await _uow.SaveAsync();
            return Ok();
        }
    }
}