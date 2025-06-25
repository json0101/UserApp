namespace api.Dtos
{
    public record ResponseDto
    (
        string message,
        int statusCode,
        bool ok,
        object? entity
    );
}
