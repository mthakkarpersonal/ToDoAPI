using Microsoft.Extensions.Logging;
using ToDoAPI.DAL.Repository;

namespace ToDoAPI.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger _logger;
        public UnitOfWork(ApplicationDBContext dbContext, ILoggerFactory logger)
        {
            _dbContext = dbContext;
            _logger = logger.CreateLogger("logs");
            this.ToDoRepository = new ToDoRepository(_dbContext, _logger);
            _logger.LogInformation("UnitOfWork Constructor Called");
        }
        public IToDoRepository ToDoRepository { get; private set; }


        public int Save()
        {
            _logger.LogInformation("Save Method Called");
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            _logger.LogInformation("SaveAsync Method Called");
            return await _dbContext.SaveChangesAsync();
        }
    }
}
