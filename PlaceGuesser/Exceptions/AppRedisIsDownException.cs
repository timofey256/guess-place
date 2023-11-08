namespace PlaceGuesser.Exceptions;

public class AppRedisIsDownException : Exception
{
    private const string _message = "Redis server is not started. Whole message : {0}";
    public AppRedisIsDownException(string wholeMessage) : base(string.Format(_message, wholeMessage)) {}
}