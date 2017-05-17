using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsTrue { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}