using Gamification.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gamification
{
    public class Context : DbContext
    {
        public Context(): base()
        {

        }

        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Question> Questions { get; set; }

        public System.Data.Entity.DbSet<Gamification.Models.Answer> Answers { get; set; }
    }
}