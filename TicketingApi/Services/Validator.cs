using TicketingApi.Models;

public class Validator
{

    public void ValidateId(int id, string entityName)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"Invalid {entityName} ID.");
        }
    }

    public void ValidateObjectNotNull(object obj, string objectName)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(objectName, $"{objectName} cannot be null.");
        }
    }
}
