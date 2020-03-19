using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qa_engine_api.Models
{
    public class Question
    {
        [Key] // Enables auto-increment.
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }

        // Parernt
        public virtual User User { get; set; }
        // Child
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
