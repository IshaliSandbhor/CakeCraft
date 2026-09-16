namespace dotnetapp.Models;

public class ErrorLog { public int Id { get; set; } public DateTime CreatedUtc { get; set; } = DateTime.UtcNow; public string Path { get; set; } = ""; public string Message { get; set; } = ""; public string? StackTrace { get; set; } }
