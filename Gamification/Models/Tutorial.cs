using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class Tutorial
    {
        public int id { get; set; }

        public string Name { get; set; }

        public bool isCompleted { get; set; }

        public string TutorialLink { get; set; }

        public virtual ICollection<UserQuizData> UserQuizData { get; set; }
    }

}