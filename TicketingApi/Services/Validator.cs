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

    public void ValidateProjectExists(ProjectModel project)
    {
        if (project == null)
        {
            throw new KeyNotFoundException("Project not found.");
        }
    }

    public void ValidateTaskExists(TaskModel task)
    {
        if (task == null)
        {
            throw new KeyNotFoundException("Task not found.");
        }
    }

    public void ValidateUserExists(UserModel user)
    {
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
    }
}
