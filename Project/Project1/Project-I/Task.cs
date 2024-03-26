namespace Project.Project1.Project_I
{
    internal class Task
    {
        // Class Properties
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Project { get; set; }

        // Class constructor
        public Task(string title, DateTime dueDate, string status, string project)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }

        // Class methods
        public override string ToString()
        {
            return $"{Title} due on {DueDate.ToShortDateString()}, Status: {Status}, Project: {Project}";
        }
    }
}
