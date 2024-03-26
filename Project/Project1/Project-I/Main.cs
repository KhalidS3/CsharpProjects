using System.Globalization;

namespace Project.Project1.Project_I
{
    class Program
    {
        static TaskList taskList = new TaskList();
        static void Main()
        {

            Console.WriteLine("Welcome to ToDoLy");
            taskList.LoadTasks("D:\\Lexicon\\Project\\Project\\Project1\\Project-I\\tasks.txt");

            bool running = true;

            while (running)
            {
                DisplayMainMenu();
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ShowTasks();
                        break;
                    case "2":
                        AddNewTask();
                        break;
                    case "3":
                        EditTaskInteractive();
                        break;
                    case "4":
                        SaveAndQuit();
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

        static void DisplayMainMenu()
        {
            Console.WriteLine("\nPick an option:");
            Console.WriteLine("(1) Show Task List (by date or project)");
            Console.WriteLine("(2) Add New Task");
            Console.WriteLine("(3) Edit Task (update, mark as done, remove)");
            Console.WriteLine("(4) Save and Quit");
        }

        static void ShowTasks()
        {
            Console.WriteLine("Show tasks by (date) or (project)?");
            string sortOrder = Console.ReadLine();
            taskList.ShowTasks(sortOrder);
        }

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

            Task newTask = new Task(title, dueDate, "Pending", project);
            taskList.AddTask(newTask);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task added successfully.");
            Console.ResetColor();

        }

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

        static void EditSpecificTask()
        {
            int taskId = GetAndValidateTaskId();
            if (taskId == -1)
            {
                return;
            }

            Console.WriteLine("Enter new title (leave blank to keep current):");
            string newTitle = Console.ReadLine();

            Console.WriteLine("Enter new due date (yyyy-MM-dd, leave blank to keep current):");
            string newDueDateString = Console.ReadLine();
            DateTime? newDueDate = null;

            if (!string.IsNullOrEmpty(newDueDateString))
            {
                if (DateTime.TryParseExact(newDueDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    newDueDate = parsedDate;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Please enter the date in yyyy-MM-dd format.");
                    Console.ResetColor();

                    return;
                }
            }

            taskList.EditTask(taskId, newTitle, newDueDate);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task edited successfully.");
            Console.ResetColor();
        }

        static void MarkTaskAsDone()
        {
            int taskId = GetAndValidateTaskId();
            if (taskId == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();
                return;
            }

            taskList.MarkAsDone(taskId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task updated status to successfully.");
            Console.ResetColor();
        }

        static void RemoveATask()
        {
            int taskId = GetAndValidateTaskId();
            if (taskId == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.ResetColor();
                return;
            }

            taskList.RemoveTask(taskId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task removed successfully.");
            Console.ResetColor();
        }

        static void SaveAndQuit()
        {
            taskList.SaveTasks("D:\\Lexicon\\Project\\Project\\Project1\\Project-I\\tasks.txt");
            Console.WriteLine("Tasks saved. Exiting application...");
        }
    }
}
