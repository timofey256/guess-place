namespace PlaceGuesser.Exceptions;

public class AppInvalidIdException : Exception
{
    public AppInvalidIdException(string message) : base(message) {}
}