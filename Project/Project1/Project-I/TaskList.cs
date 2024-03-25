using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Project1.Project_I
{
    internal class TaskList
    {
        private List<Task> tasks;

        // Constructor
        public TaskList()
        {
            tasks = new List<Task>();
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void ShowTasks(string sortOrder)
        {
            List<Task> displayTasks = tasks;
            switch (sortOrder.ToLower())
            {
                case "date":
                    displayTasks = tasks.OrderBy(t => t.DueDate).ToList();
                    break;
                case "project":
                    displayTasks = tasks.OrderBy(t => t.Project).ToList();
                    break;
                default:
                    // if neither 'date' or 'project' is chossen, the original format is displaying.
                    break;
            }

            foreach (Task task in displayTasks)
            {
                Console.WriteLine(task);
            }
        }

        public void SaveTasks(string filePath)
        {
            List<string> lines = tasks.Select(t => $"{t.Titel},{t.DueDate},{t.Status},{t.Project}").ToList();
            File.WriteAllLines(filePath, lines);
        }

        public void LoadTasks(string filePath)
        {
            if (!File.Exists(filePath)) {
                Console.WriteLine("No existing task file found");
                return;
            }

            tasks.Clear(); // For clearing exsiting tasks before loading new tasks

            // Read lines into a string array then convert to a List<string>
            List<string> lines = new List<string>(File.ReadAllLines(filePath));

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length != 4)
                {
                    Console.WriteLine("Skipping invalid task line.");
                    continue;
                }

                try
                {
                    Task task = new Task(parts[0], DateTime.Parse(parts[1]), parts[2], parts[3]);
                    tasks.Add(task);
                }
                catch
                {
                    Console.WriteLine("Error parsing task line. Check date format.");
                }
            }
        }

        public void EditTasks(int taskId) 
        {
            if (taskId < 0 || taskId >= tasks.Count) 
            {
                Console.WriteLine("Invalid task ID");
                return;
            }

            Task taskToEdit = tasks[taskId];
            Console.WriteLine("Editing Task: " + taskToEdit);

            Console.WriteLine("Please Input new title (Leave blank to keep current): ");
            string newTitle = Console.ReadLine();
            if(!string.IsNullOrEmpty(newTitle))
            {
                taskToEdit.Titel = newTitle;
            }

            Console.WriteLine("Please Input new due date (yyyy-MM-dd, Leave blank to keep current): ");
            string newDueDate = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDueDate))
            {
                if (DateTime.TryParseExact(newDueDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime newDDate))
                {
                    taskToEdit.DueDate = newDDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
        }
    }
}
