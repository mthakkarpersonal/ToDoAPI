using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Model.Models
{
    public class ToDo : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
