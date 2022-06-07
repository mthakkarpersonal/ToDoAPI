using ToDoAPI.DAL.Repository;

namespace ToDoAPI.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            this.ToDoRepository = new ToDoRepository(_dbContext);
        }
        public IToDoRepository ToDoRepository { get; private set; }


        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
