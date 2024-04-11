# To-Do List Application

This application is a simple command-line to-do list manager built with C#. It allows users to manage their tasks effectively, providing features such as adding, viewing, editing, and removing tasks.

## Components

### Main.cs

The `Program` class serves as the entry point of the application. It initializes a `TaskListManager` object to manage tasks and provides a CLI for user interactions. 
Users can view tasks, add new ones, edit existing tasks, and save changes before exiting the application.

### Task.cs

- **Task Class**: An abstract base class for tasks, defining common properties like `Title`, `DueDate`, `Status`, and `Project`. It also declares an abstract method 
`DisplayTaskDetails()` for displaying task information.

- **ITaskListManager Interface**: Specifies the contract for task management, including methods for adding, editing, marking tasks as completed, and removing tasks.

### TodoTask.cs

- **TodoTask Class**: Inherits from the `Task` class to represent individual to-do tasks. It implements the `DisplayTaskDetails()` method to provide a detailed string representation of the task.

### TaskListManager.cs

- **TaskListManager Class**: Implements the `ITaskListManager` interface, managing a list of `TodoTask` objects. It supports adding tasks, displaying them with sorting options, 
saving to and loading from a file, editing task details, marking tasks as done, and removing tasks.

### tasks.txt

This text file stores task information, with each line representing a task's title, due date, status, and associated project. The `TaskListManager` class uses this file to persist tasks between application 
runs.

## Features

- **View Tasks**: Users can view all tasks, sorted by date or project.
- **Add New Tasks**: New tasks can be added with details such as title, due date, and project.
- **Edit Tasks**: Existing tasks can be edited to update their title, due date, or status.
- **Mark as Done**: Tasks can be marked as completed.
- **Remove Tasks**: Unwanted tasks can be removed from the list.
- **Data Persistence**: Tasks are saved to and loaded from the `tasks.txt` file, ensuring data persistence between sessions.

## Usage

When you run the application, you will be presented with a menu of options:

1. Show Task List (by date or project)
2. Add New Task
3. Edit Task (update, mark as done, remove)
4. Save and Quit

Select an option by entering the corresponding number.

This application showcases object-oriented programming principles such as [abstraction](https://www.geeksforgeeks.org/c-sharp-abstract-classes/), [inheritance](https://www.geeksforgeeks.org/c-sharp-multiple-inheritance-using-interfaces/), 
[encapsulation](https://www.tutorialspoint.com/csharp/csharp_encapsulation.htm), and [polymorphism](https://www.tutorialspoint.com/csharp/csharp_polymorphism.htm), making it a great educational tool for understanding these concepts in practice.