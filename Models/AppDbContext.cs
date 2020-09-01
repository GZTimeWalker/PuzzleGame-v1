using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GZTimeServer.Models
{
    public class AppDbContext : DbContext
    {
        protected readonly string _connectionstring;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(_connectionstring != null)
                optionsBuilder.UseSqlServer(_connectionstring);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<LevelProcess> LevelProcesses { get; set; }
        public DbSet<AnimeProcess> AnimeProcesses { get; set; }
        public DbSet<CodeKey> CodeKeys { get; set; }
        public DbSet<MazeProcess> MazeProcesses { get; set; }
        public DbSet<LiveLike> LiveLikes { get; set; }
        public DbSet<Rank> Ranks { get; set; }
    }
}
