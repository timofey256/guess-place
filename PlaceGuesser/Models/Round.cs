namespace PlaceGuesser.Models;

public struct Round
{
    public readonly string VideoId;
    public readonly Coordinates Coordinates;
    
    public Round(string id, Coordinates coordinates)
    {
        Coordinates = coordinates;
        VideoId = id;
    }
}