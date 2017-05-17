﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}