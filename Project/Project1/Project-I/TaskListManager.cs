namespace Project.Project1.Project_I
{
    internal class TaskListManager: ITaskListManager
    {
        private List<TodoTask> tasks; // List to store tasks

        // Constructor
        public TaskListManager()
        {
            tasks = new List<TodoTask>(); // Initialize the tasks list
        }

        // Returns the count of tasks
        public int TaskCount()
        {
            return tasks.Count;
        }

        // Adds a new task to the list
        public void AddTask(Task task)
        {
            if (task is TodoTask todoTask)
            {
                tasks.Add(todoTask);
            }
        }

        // Displays tasks sorted by the provided sortOrder
        public void ShowTasks(string sortOrder)
        {
            List<TodoTask> displayTasks = tasks.ToList(); // Create a copy of the tasks list
            switch (sortOrder.ToLower())
            {
                case "date":
                    // Sort tasks by due date
                    displayTasks = tasks.OrderBy(t => t.DueDate).ToList();
                    break;
                case "project":
                    // Sort tasks by project
                    displayTasks = tasks.OrderBy(t => t.Project).ToList();
                    break;
                default:
                    // Invalid sort order
                    Console.WriteLine("Invalid sort order. Please choose either 'date' or 'project'.");
                    return;
            }

            // Display tasks with line numbers as IDs
            foreach (var task in displayTasks)
            {
                Console.WriteLine($"ID: {task.OriginalId}, {task.DisplayTaskDetails()}");
            }
            //for (int i = 0; i < displayTasks.Count; i++)
            //{
            //    Console.WriteLine($"ID: {i + 1}, {displayTasks[i].DisplayTaskDetails()}");
            //}
        }

        // Saves tasks to a file
        public void SaveTasks(string filePath)
        {
            // Convert tasks to strings and write to file
            List<string> lines = tasks.Select(t => $"{t.Title},{t.DueDate.ToString("yyyy-MM-dd")},{t.Status},{t.Project}").ToList();
            File.WriteAllLines(filePath, lines);
        }

        // Loads tasks from a file
        public void LoadTasks(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // File does not exist
                Console.WriteLine("No existing task file found");
                return;
            }

            // Clear existing tasks before loading new ones
            tasks.Clear();

            // Read lines from file
            List<string> lines = new List<string>(File.ReadAllLines(filePath));

            // Parse tasks from lines
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(',');
                if (parts.Length != 4)
                {
                    Console.WriteLine("Skipping invalid task line.");
                    continue;
                }

                try
                {
                    TodoTask task = new TodoTask(parts[0], DateTime.Parse(parts[1]), parts[2], parts[3])
                    {
                        OriginalId = i + 1 // Set OriginalId to line number
                    };
                    tasks.Add(task);
                }
                catch
                {
                    Console.WriteLine("Error parsing task line. Check date format.");
                }
            }
            /*
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length != 4)
                {
                    // Invalid line format
                    Console.WriteLine("Skipping invalid task line.");
                    continue;
                }

                try
                {
                    // Create task and add to list
                    TodoTask task = new TodoTask(parts[0], DateTime.Parse(parts[1]), parts[2], parts[3]);
                    tasks.Add(task);
                }
                catch
                {
                    // Error parsing line
                    Console.WriteLine("Error parsing task line. Check date format.");
                }
            }
            */
        }

        // Edits a task
        public void EditTask(int taskId, string newTitle, DateTime? newDueDate)
        {
            TodoTask taskToEdit;
            try
            {
                // Get task by ID
                taskToEdit = GetTaskById(taskId);
            }
            catch (ArgumentException)
            {
                // Invalid task ID
                Console.WriteLine("Invalid task ID.");
                return;
            }

            // Update task title if provided
            if (!string.IsNullOrEmpty(newTitle))
            {
                taskToEdit.Title = newTitle;
            }

            // Update task due date if provided
            if (newDueDate.HasValue)
            {
                taskToEdit.DueDate = newDueDate.Value;
            }

            Console.WriteLine("Task updated successfully.");
        }

        // Marks a task as done
        public void MarkAsDone(int taskId)
        {
            TodoTask taskToMark;
            try
            {
                // Get task by ID
                taskToMark = GetTaskById(taskId);
            }
            catch (ArgumentException)
            {
                // Invalid task ID
                Console.WriteLine("Invalid task ID or the ID does not exist. Please enter a valid task ID.");
                return;
            }

            // Mark task as completed
            taskToMark.Status = "Completed";
            Console.WriteLine($"Task '{taskToMark.Title}' marked as completed.");
        }

        // Removes a task
        public void RemoveTask(int taskId)
        {
            TodoTask taskToRemove;
            try
            {
                // Get task by ID
                taskToRemove = GetTaskById(taskId);
            }
            catch (ArgumentException)
            {
                // Invalid task ID
                Console.WriteLine("Invalid task ID.");
                return;
            }

            // Remove task from list
            tasks.Remove(taskToRemove);
            Console.WriteLine("Task removed successfully.");
        }

        // Gets a task by its ID
        public TodoTask GetTaskById(int id)
        {
            if (id >= 1 && id <= tasks.Count)
            {
                // Return task by ID
                return tasks[id - 1];
            }
            else
            {
                // Invalid task ID
                throw new ArgumentException("Invalid task ID or the ID does not exist. Please enter a valid task ID.");
            }
        }

        // Notifies about tasks due date
        public void NotifyTaskDueDate()
        {
            DateTime now = DateTime.Now;
            // Get tasks that are due within 2 days and are not completed
            var tasksToNotify = tasks.Where(task => task.Status != "Completed" && task.DueDate.Date <= now.AddDays(2));

            // Notify about each task
            foreach (var task in tasksToNotify)
            {
                if (task.DueDate.Date == now.Date)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Task '{task.Title}' is due today.");
                }
                else if (task.DueDate < now)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Task '{task.Title}' is overdue.");
                }
                else if (task.DueDate.Date == now.AddDays(1).Date)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Task '{task.Title}' is due tomorrow.");

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Task '{task.Title}' is due in {task.DueDate.Subtract(now).Days} days.");
                }
                Console.ResetColor();
            }
        }
    }
}
