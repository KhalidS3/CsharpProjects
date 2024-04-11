using System.Globalization;

namespace Project.Project1.Project_I
{
    class Program
    {
        static TaskListManager taskList = new TaskListManager(); // Create a new TaskList object
        static void Main()
        {
            Console.WriteLine("Welcome to ToDoLy");
            taskList.LoadTasks(@"D:\Lexicon\Project\Project\bin\Debug\net8.0\tasks.txt"); // Load tasks from file
            taskList.NotifyTaskDueDate(); // Notify about tasks due date

            bool running = true;

            while (running) // Main loop
            {
                DisplayMainMenu(); // Display main menu
                string option = Console.ReadLine(); // Read user's option
                switch (option) // Process user's option
                {
                    case "1":
                        ShowTasks(); // Show tasks
                        break;
                    case "2":
                        AddNewTask(); // Add new task
                        break;
                    case "3":
                        EditTaskInteractive(); // Edit task
                        break;
                    case "4":
                        SaveAndQuit(); // Save tasks and quit
                        running = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        // Displays the main menu
        static void DisplayMainMenu()
        {
            Console.WriteLine("\nPick an option:");
            Console.WriteLine("(1) Show Task List (by date or project)");
            Console.WriteLine("(2) Add New Task");
            Console.WriteLine("(3) Edit Task (update, mark as done, remove)");
            Console.WriteLine("(4) Save and Quit");
        }

        // Shows tasks
        static void ShowTasks()
        {
            string sortOrder = string.Empty;
            do
            {
                Console.WriteLine("Show tasks by (date) or (project)?");
                sortOrder = Console.ReadLine().ToLower();
                if (!new[] { "date", "project" }.Any(order => order == sortOrder)) // Check if the input is in the array
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input, please enter either 'date' or 'project'.");
                    Console.ResetColor();
                }
            } while (!new[] { "date", "project" }.Any(order => order == sortOrder));

            taskList.ShowTasks(sortOrder); // Show tasks sorted by sortOrder
        }

        // Adds a new task
        static void AddNewTask()
        {
            Console.WriteLine("Enter task title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter due date (format: yyyy-MM-dd):");
            DateTime dueDate;
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date, please enter in format yyyy-MM-dd:");
                Console.ResetColor();
            }

            Console.WriteLine("Enter project name:");
            string project = Console.ReadLine();

            TodoTask newTask = new TodoTask(title, dueDate, "Pending", project); // Create new task
            taskList.AddTask(newTask); // Add new task to the list
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task added successfully.");
            Console.ResetColor();
        }

        // Gets and validates task ID
        static int GetAndValidateTaskId()
        {
            int taskId;
            Console.WriteLine("Enter the ID of the task:");

            if (!int.TryParse(Console.ReadLine(), out taskId) || taskId < 0 || taskId >= taskList.TaskCount())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid task ID or the ID does not exist. Please enter a valid task ID.");
                Console.ResetColor();
                return -1; // Indicates invalid or non-existent ID.
            }
            return taskId;
        }


        // Edits a task
        static void EditTaskInteractive()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Choose an option for the task:");
                Console.WriteLine("(1) Edit Task");
                Console.WriteLine("(2) Mark Task as Done");
                Console.WriteLine("(3) Remove Task");
                Console.WriteLine("(4) Retun to Main Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        // Call a method to handle task editing
                        EditSpecificTask();
                        break;
                    case "2":
                        // Mark the task as done
                        MarkTaskAsDone();
                        break;
                    case "3":
                        // Remove the task
                        RemoveATask();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option, please try again.");
                        Console.ResetColor();
                        break;
                }
            }

        }

        // This function is used to edit a specific task
        static void EditSpecificTask()
        {
            // Get and validate the task ID
            int taskId = GetAndValidateTaskId();
            // If the task ID is -1 (invalid), return immediately
            if (taskId == -1)
            {
                return;
            }

            // Prompt the user to enter a new title for the task
            Console.WriteLine("Enter new title (leave blank to keep current):");
            string newTitle = Console.ReadLine();

            // Prompt the user to enter a new due date for the task
            Console.WriteLine("Enter new due date (yyyy-MM-dd, leave blank to keep current):");
            string newDueDateString = Console.ReadLine();
            DateTime? newDueDate = null;

            // If the user entered a new due date, try to parse it
            if (!string.IsNullOrEmpty(newDueDateString))
            {
                if (DateTime.TryParseExact(newDueDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    newDueDate = parsedDate;
                }
                else
                {
                    // If the date format is invalid, display an error message and return immediately
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                    Console.ResetColor();

                    return;
                }
            }

            // Edit the task with the new title and/or due date
            taskList.EditTask(taskId, newTitle, newDueDate);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task edited successfully.");
            Console.ResetColor();
        }

        // This function is used to mark a task as done
        static void MarkTaskAsDone()
        {
            // Get and validate the task ID
            int taskId = GetAndValidateTaskId();
            // If the task ID is -1 (invalid), return immediately
            if (taskId == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();
                return;
            }

            // Mark the task as done
            taskList.MarkAsDone(taskId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task updated status to successfully.");
            Console.ResetColor();
        }

        // This function is used to remove a task
        static void RemoveATask()
        {
            // Get and validate the task ID
            int taskId = GetAndValidateTaskId();
            // If the task ID is -1 (invalid), return immediately
            if (taskId == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();
                return;
            }

            // Remove the task
            taskList.RemoveTask(taskId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task removed successfully.");
            Console.ResetColor();
        }

        // This function is used to save the tasks and quit the application
        static void SaveAndQuit()
        {
            // Save the tasks to a file
            taskList.SaveTasks(@"D:\Lexicon\Project\Project\bin\Debug\net8.0\tasks.txt");
            // Notify the user that the tasks have been saved and the application is exiting
            Console.WriteLine("Tasks saved. Exiting application...");
        }
    }
}
