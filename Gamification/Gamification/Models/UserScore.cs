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

        public int NumberOfQuestions { get; set; }

        public int score {  get; set; }

       public UserScore() { score = 0; }
    }
}