namespace PlaceGuesser.Models;

public struct Coordinates
{
    public readonly int X;
    public readonly int Y;

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Coordinates ParseCoordinate(string coord)
    {
        string[] coordinates = coord.Split();
        return new Coordinates(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
    }
}