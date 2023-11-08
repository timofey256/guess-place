namespace PlaceGuesser.Exceptions;

public class AppInvalidIdException : Exception
{
    public readonly int Id;
    private const string _messageTemplate = "Unexpected ID of a game. There's no such ID={0} in the database.";

    public AppInvalidIdException(int id) : base(string.Format(_messageTemplate, id))
    {
        Id = id;
    }
}