using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLikeMath.Common;
using TagLikeMath.Data.Entities;

namespace TagLikeMath.Data
{
    internal class DataContext : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public DbSet<Post_standard_Tag> Post_standard_Tag { get; set; }
        public DbSet<Post_stat_Tag> Post_stat_Tag { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<User_standard_Tag> User_standard_Tag { get; set; }
        public DbSet<User_stat_Tag> User_stat_Tag { get; set; }
        //public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DataContext()
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionRules.PostgreConncection);
        }

    }
}