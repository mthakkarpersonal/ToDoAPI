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
    }
}
