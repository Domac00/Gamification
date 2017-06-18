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

        public string UserId { get; set; }

        public int CheckAchievement(UserQuizData uqd)
        {
            //Prvi kviz odigran
            if (uqd.NumberOfSolvedQuizes == 1) { return 1; }
            //5 rjesenih kvizova
            else if (uqd.NumberOfSolvedQuizes == 5) { return 2; }
            else return 0;
        }

        public virtual ICollection<UserQuizData> UserQuizData { get; set; }
    }
}