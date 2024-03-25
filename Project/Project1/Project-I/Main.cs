using System.Diagnostics;

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
                    default:
                        Console.WriteLine("Invalid option, please try again.");
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
    }
}
