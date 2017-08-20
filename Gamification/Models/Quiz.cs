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

        public float SumOfGrades { get; set; }

        public int NumberOfRatings { get; set; }

        public float Rating { get; set; }

        public virtual ICollection<UserQuizData> UserQuizData { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}