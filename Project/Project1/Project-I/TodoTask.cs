namespace Project.Project1.Project_I
{
    internal class TodoTask : Task
    {
        // Class constructor
        public TodoTask(string title, DateTime dueDate, string status, string project)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }

        public int OriginalId { get; set; }
        // Class methods
        public override string DisplayTaskDetails()
        {
            return $"{Title} due on {DueDate.ToShortDateString()}, Status: {Status}, Project: {Project}";
        }
    }
}
