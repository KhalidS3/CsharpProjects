public abstract class Task
{
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public string Project { get; set; }

    public abstract string DisplayTaskDetails();
}

public interface ITaskListManager
{
    void AddTask(Task task);
    void EditTask(int taskId, string newTitle, DateTime? newDueDate);
    void MarkAsDone(int taskId);
    void RemoveTask(int taskId);
}
