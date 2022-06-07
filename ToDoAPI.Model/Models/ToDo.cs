using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAPI.Model.Models
{
    public class ToDo //: BaseEntity
    {
        public int Id { get; set; }
        public int? EntBy { get; set; }
        public DateTime? EntDt { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
