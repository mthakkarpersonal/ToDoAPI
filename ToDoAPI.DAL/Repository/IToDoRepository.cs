using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.DAL.BaseRepository;
using ToDoAPI.Model.Models;

namespace ToDoAPI.DAL.Repository
{
    public interface IToDoRepository : IBaseRepository<ToDo>
    {

    }
}
