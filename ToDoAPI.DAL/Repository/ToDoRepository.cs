using Microsoft.Extensions.Logging;
using ToDoAPI.DAL.BaseRepository;
using ToDoAPI.Model.Models;

namespace ToDoAPI.DAL.Repository
{
    public class ToDoRepository : BaseRepository<ToDo>, IToDoRepository
    {
        public ToDoRepository(ApplicationDBContext dbContext,ILogger logger) : base(dbContext, logger)
        {
        }
    }
}
