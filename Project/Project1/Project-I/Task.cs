using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Project1.Project_I
{
    internal class Task
    {
        // Class Properties
        public string Titel {  get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Project {  get; set; }

        // Class constructor
        public Task(string title, DateTime dueDate, string status, string project) 
        {
            Titel = title;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }

        // Class methods
        public override string ToString() 
        {
            return $"{Titel} due on {DueDate.ToShortDateString()}, Status: {Status}, Project: {Project}";
        }
    }
}
