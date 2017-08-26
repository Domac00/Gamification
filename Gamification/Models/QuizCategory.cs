using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class QuizCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}