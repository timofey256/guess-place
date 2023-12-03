using System.ComponentModel.DataAnnotations;

namespace PlaceGuesser.Models;

public class Video
{
    [Key]
    public int VideoId { get; set; }
    public string Filename { get; set; }
    public string Continent { get; set; }
    public string Country { get; set; }
    public string CoordinateN { get; set; }
    public string CoordinateE { get; set; }
    public string? SourceUrl { get; set; }
}