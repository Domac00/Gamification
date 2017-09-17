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

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int xp { get; set; }

        public int NumberOfSolvedQuizes { get; set; }

        public int NumberOfCompletedTutorials { get; set; }

        public int NumberOfAchievements { get; set; }

        public float Accuracy { get; set; }

        public bool isAdmin { get; set; }

        public virtual ICollection<Achievement> Achievement { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }

        public virtual ICollection<Tutorial> Tutorial { get; set; }

        public int checkUserLevel(int xp)
        {
            if (xp < 10){return 1;}
            else if(xp >=10 && xp<30 ){ return 2; }
            else if (xp >=30 && xp< 50){ return 3; }
            else if(xp>=50 && xp< 90) { return 4; }
            else if(xp >= 90 && xp<130) { return 5; }
            else if (xp >= 130 && xp < 180) { return 6; }
            else if (xp >= 180 && xp < 250) { return 7; }
            else if (xp >= 250 && xp < 320) { return 8; }
            else if (xp >= 300 && xp < 400) { return 9; }
            else if (xp >= 400 && xp < 500) { return 10; }
            else { return 0; }
        }


    }
}