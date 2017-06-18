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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Gamification.Migrations.Configuration>("DefaultConnection"));
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }

        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserQuizData> UserQuizData { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Achievement> Achievement { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserQuizData>().HasMany(u => u.Achievement).WithMany(a => a.UserQuizData);
            base.OnModelCreating(modelBuilder);
        }

    }
}