using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAPI.Model.Models;

namespace ToDoAPI.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ToDo>().HasData(
               new ToDo
               {
                   Id = 1,
                   Name = "Test1",
                   IsCompleted = false,
                   EntDt = DateTime.Now,
                   ModDt = DateTime.Now
               },
               new ToDo
               {
                   Id = 2,
                   Name = "Test2",
                   IsCompleted = false,
                   EntDt = DateTime.Now,
                   ModDt = DateTime.Now
               }
            );
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }
        public void OnBeforSaveChanges()
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                var currentDate = DateTime.Now;
                entry.Entity.ModDt = currentDate;
                //entry.Entity.ModBy = 1;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.EntDt = currentDate;
                    //entry.Entity.EntBy = 1;
                }
            }
        }
    }
}
