using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qa_engine_api.Models
{
    public class Answer
    {
        [Key] // Enables auto-increment.
        public int Id { get; set; }
        public string Description { get; set; }
        public int Vote { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }
        public int QuestionId { get; set; }

        // Parernt
        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
    }
}
