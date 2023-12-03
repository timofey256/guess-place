using Microsoft.EntityFrameworkCore;
using PlaceGuesser.Models;

namespace PlaceGuesser.Data
{
    public class VideoDBContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
        public VideoDBContext(DbContextOptions<VideoDBContext> options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().HasData(
                new Video
                {
                    VideoId = 1,
                    Filename = "481230-1622228-vienna.mp4",
                    Continent = "Europe",
                    Country = "Austria",
                    CoordinateN = "48.12.30",
                    CoordinateE = "16.22.228",
                    SourceUrl = "https://www.youtube.com/watch?v=sAgr4O0pQ2s&t=115s"
                }
            );
        }
    
    }
}