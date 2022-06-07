using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.DAL.Repository;

namespace ToDoAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IToDoRepository ToDoRepository { get; }
        void Save();
    }
}
