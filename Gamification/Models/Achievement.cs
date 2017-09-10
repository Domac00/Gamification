using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
   
    public class Achievement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CheckAchievement(UserQuizData uqd, UserScore us, Quiz quiz)
        {
          
            //Prvi kviz odigran
            if (uqd.NumberOfSolvedQuizes == 1 && !uqd.Achievement.Any(a => a.Id == 6)) { return 6; }
            //5 rjesenih kvizova
            else if (uqd.NumberOfSolvedQuizes == 5 && !uqd.Achievement.Any(a=>a.Id==7)) { return 7; }
            //10 kvizova
            else if (uqd.NumberOfSolvedQuizes == 10 && !uqd.Achievement.Any(a => a.Id == 8)) { return 8; }
            //30 Kvizova
            else if (uqd.NumberOfSolvedQuizes == 30 && !uqd.Achievement.Any(a => a.Id == 9)) { return 9; }
            //level 2 100%
            else if ( us.QuizLevel==2 && us.Percentage==100 && !uqd.Achievement.Any(a => a.Id == 10)) { return 10; }
            //Rješeni svi level 1 kvizovi
          
            else return 0;
        }

        public virtual ICollection<UserQuizData> UserQuizData { get; set; }
    }
}