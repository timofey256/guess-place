namespace PlaceGuesser.Models;

public static class CoordinatesComparer
{
    public static int CompareCoordinates(Coordinates c1, Coordinates c2)
    {
        return (c1.X - c2.X) + (c1.Y - c2.Y);
    }
}