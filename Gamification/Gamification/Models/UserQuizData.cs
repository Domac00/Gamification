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

        public string UserName { get; set; }

        public int xp { get; set; }

        public int NumberOfSolvedQuizes { get; set; }

        public virtual ICollection<Achievement> Achievement { get; set; }


    }
}