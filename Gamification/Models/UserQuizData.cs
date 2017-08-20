using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class UserQuizData
    {
        [Key]
        public string UserId { get; set; }

        public int UserLevel { get; set; }

        public string UserName { get; set; }

        public int xp { get; set; }

        public int NumberOfSolvedQuizes { get; set; }

        public virtual ICollection<Achievement> Achievement { get; set; }

        public float Accuracy { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }

        public virtual ICollection<Tutorial> Tutorial { get; set; }

        public int checkUserLevel(int xp)
        {
            if (xp < 30){return 1;}
            else if(xp >=30 && xp<50 ){ return 2; }
            else if (xp >=50 && xp< 100){ return 3; }
            else if(xp>=100 && xp< 200) { return 4; }
            else if(xp >= 200 && xp<350) { return 5; }
            else { return 0; }
        }


    }
}