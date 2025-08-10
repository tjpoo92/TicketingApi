namespace TicketingApi.Models;

public class ConnectionStringOptions
{
    public const string ConnectionStrings = "ConnectionStrings";
    public string DefaultConnection { get; set; } = string.Empty;
}
