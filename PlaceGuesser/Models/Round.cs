namespace PlaceGuesser.Models;

public struct Round
{
    public readonly Coordinates Coordinates;
    public readonly string fileNameInStorage;
    
    public Round(Coordinates coordinates, string fileName)
    {
        Coordinates = coordinates;
        fileNameInStorage = fileName;
    }
}