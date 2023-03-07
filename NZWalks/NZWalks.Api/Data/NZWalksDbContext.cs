using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Data
{
    public class NZWalksDbContext:DbContext
    {


        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options)
            :base(options) { }

        //create properties a models
        //by creating following properties we are informing Entity FrameWork
        //to create the tables
        public DbSet<Region> Regions { get; set; } 
        
        public DbSet<Walk> walks { get; set; }

        public DbSet<WalkDifficulty> walksDifficulty { get; set; }

        
    }
}
