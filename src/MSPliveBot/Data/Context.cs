using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MSPliveBot.Data
{
    public class Context:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Bot.db");
        }
    }

    public class Admin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
