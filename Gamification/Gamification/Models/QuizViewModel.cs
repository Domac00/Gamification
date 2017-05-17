using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class QuizViewModel
    {
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }

        public string Name { get; set; }

        public string Question { get; set; }

       

        public ICollection<Answer> Answers { get; set; }
    }
}