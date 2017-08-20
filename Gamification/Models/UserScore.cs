using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class UserScore
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int QuizId { get; set; }

        public int QuizLevel { get; set; }

        public float NumberOfQuestions { get; set; }

        public float score {  get; set; }

        public List<int> UserAnswers { get; set; }

        public float Percentage { get; set; }

        public float Rate { get; set; }

        public string msg { get; set; }

        public UserScore() { score = 0; UserAnswers = new List<int>(); }
    }
}