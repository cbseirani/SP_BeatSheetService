using System.Net;

namespace BeatSheetService.Common;

public class ValidationException(string message) : Exception(message)
{
    public static int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
}

public class NotFoundException(string message) : Exception(message)
{
    public static int StatusCode { get; } = (int)HttpStatusCode.NotFound;
}