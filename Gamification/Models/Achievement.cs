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

        public int AchievementCategoryId { get; set; }

        public virtual ICollection<UserQuizData> UserQuizData { get; set; }

        public virtual AchievementCategory AchievementCategory { get; set; }
    }
}