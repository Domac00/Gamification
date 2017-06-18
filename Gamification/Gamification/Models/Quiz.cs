using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public int QuizLevel { get; set; }


        public virtual ICollection<Question> Questions { get; set; }
    }
}