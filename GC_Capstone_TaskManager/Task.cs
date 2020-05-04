using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GC_Capstone_TaskManager
{
    class Task
    {
        #region fields
        private int key;
        private string title;
        private string description;
        private string owner;
        private DateTime dueDate;
        private bool completion;
        #endregion
        #region properties
        public int Key
        {
            get //referenced when we call this property from an object
            {
                return key;
            }
            set //used when we give the property its value
            {
                key = value;
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }
        public DateTime DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                dueDate = value;
            }
        }
        public bool Completion
        {
            get
            {
                return completion;
            }
            set
            {
                completion = value;
            }
        }
        #endregion
        #region constructors
        public Task(int _key, string _title, string _description, string _owner, DateTime _dueDate, bool _completion)
        {
            key = _key;
            title = _title;
            description = _description;
            owner = _owner;
            dueDate = _dueDate;
            completion = false;
        }
        public Task() { }
        #endregion
        #region methods
        public static List<Task> InstantiateDefaultTaskList()
        {
            List<Task> tasks = new List<Task>()
            {
                new Task(1, "Approve Script", "Collect script from screenwriter and submit to executive producer for final approval.", "Parthenon Huxley", DateTime.Parse("5/8/2020"), false),
                new Task(2, "Schedule Table Read", "Call talent agents to find a mutually convenient date, email script to talent, book a conference room, and print scripts.", "Parthenon Huxley", DateTime.Parse("7/8/2020"), false),
                new Task(3, "Breakdown Script", "Highlight script by stunts, locations, wardrobe changes, talent and time of day.", "Takuto Sato", DateTime.Parse("5/15/2020"), false),
                new Task(4, "Scout Locations", "Scout a bombed-out McMansion, high desert and opulent casino for availability, ambient noise and electrical access.", "Risqué d/Explosion", DateTime.Parse("5/1/2021"), false),
                new Task(5, "Finalize Casting", "Collect cast list approval from director and send final contracts to talent agents.", "Parthenon Huxley", DateTime.Parse("5/1/2020"), false),
            };
            return tasks;
        }
        public static bool PrintMenu(List<Task> tasks)
        {
            //WELCOME
            Console.WriteLine("Select an action:\n");
            Thread.Sleep(500);
            //PRINT MENU
            string[] actions = new string[] { "List Tasks", "Add Task", "Remove Task", "Mark Task as Complete", "Quit" };
            for (int i = 0; i < actions.Length; i++)
            {
                Thread.Sleep(250);
                Console.WriteLine($"{i + 1}. {actions[i]}");
                if (i == actions.Length - 1)
                {
                    Console.WriteLine("");
                }
            }
            //VALIDATE SELECTION
            int selection;
            bool loop;
            if (int.TryParse(Console.ReadLine().Trim(), out selection))
            {
                Console.WriteLine("{0}. {1}\n", selection, actions[selection - 1]);
                if (selection == 1)
                {
                    PrintTaskList(tasks, true);
                }
                else if (selection == 2)
                {
                    AddTask(tasks);
                }
                else if (selection == 3)
                {
                    RemoveTask(tasks);
                }
                else if (selection == 4)
                {
                    CompleteTask(tasks);
                }
                else if (selection == 5)
                {
                    return loop = !Quit();
                }
            } else
            {
                throw new FormatException($"Selection is invalid. Selection must be an integer 1-{actions.Length}.");
            }
            //LOOP
            loop = AskYesOrNo("Would you like to take another action?");
            return loop;

        }
        public static bool AskYesOrNo(string question)
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine(question);
                string response = Console.ReadLine().ToLower();
                //Regex yesTrue = new Regex(@"\b((y(es)?)|(t(rue)?))\b/gi");
                Regex yesTrue = new Regex(@"(y(es)?)|(t(rue)?)");
                Regex noFalse = new Regex(@"(n(o)?)|(f(alse)?)");
                try
                {
                    if (yesTrue.IsMatch(response))
                    {
                        loop = false;
                        return true;
                    }
                    if (noFalse.IsMatch(response))
                    {
                        loop = false;
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Enter yes or no.");
                        loop = true;
                    }
                }
                catch
                {
                    new Exception("Enter \"yes\" or \"no\".");
                }
            }
            return true; // WHY DO I NEED THIS RETURN HERE FOR MY METHOD TO WORK?
        }
        public static void PrintTaskList(List<Task> tasks)
        {
            foreach (Task task in tasks)
            {
                Console.WriteLine($"{task.Key}. {task.Title}");
            }
        }
        public static void PrintTaskList(List<Task> tasks, bool  withProperties)
        {
            foreach (Task task in tasks)
                if (withProperties)
                {
                    PrintTaskProperties(task);
                }
                else
                {
                    Console.WriteLine($"{task.Key}. {task.Title}");
                }
        }
        public static void PrintTaskProperties(Task task)
        {
            Console.WriteLine($"Task {task.Key}: {task.Title}");
            //Format subsequent lines with indentation.
            string completion;
            if (!task.Completion)
            {
                completion = "Incomplete";
            }
            else
            {
                completion = "Complete";
            }
            Console.WriteLine("{0, -5}{1}{2}", "", "Status: ", completion);
            Console.WriteLine("{0, -5}{1}{2}", "", "Owner: ", task.Owner);
            Console.WriteLine("{0, -5}{1}{2}", "", "Due Date: ", task.DueDate.ToShortDateString());
            Console.WriteLine("{0, -5}{1}{2}", "", "Description: ", task.Description + "\n");
        }

        public static Task SelectTask(List<Task> tasks, string action)
        {
            Console.WriteLine($"Which task would you like to {action}? (Input task number.)");
            PrintTaskList(tasks, false);
            int selection = int.Parse(Console.ReadLine());
            Console.WriteLine($"You have selected Task {selection}.");
            foreach(Task task in tasks)
            {
                if (selection == task.Key)
                {
                return task;
                }
            }
            throw new FormatException($"Choose a Task number 1-{tasks.Count}");
        }
        public static bool VerifySelection()
        {
            return AskYesOrNo($"Are you sure?");
        }
        public static bool VerifySelection(string action)
        {
            return AskYesOrNo($"Are you sure you want to {action}?");
        }
        public static void AddTask(List<Task> tasks)
        {
            //Get values from user and assign values as the properties of new task.
            int key = tasks.Count + 1;

            //List prompts to assign a value to each property of class Task.
            List<TaskQuery> taskQueries = new List<TaskQuery>() {new TaskQuery("title", "What do you want to call this task?"), new TaskQuery("description", "What does this task entail?"), new TaskQuery("owner", "Who owns this task?"), new TaskQuery("dueDate", "When is this task due?")};
            //Instantiate new object of class Task.
            var newTask = new Task();
            //List the properties of the Task class.
            var type = newTask.GetType();
            var properties = type.GetProperties().ToList();
            //Print the property queries, collect user response and assign the value of each response to a property of the object.
            properties[0].SetValue(newTask, tasks.Count + 1);
            properties[5].SetValue(newTask, false);
            for (int i = 1; i < properties.Count - 1; i++)
            {
                if (i < properties.Count - 1)
                {
                    Console.WriteLine(taskQueries[i - 1].Query);
                    string response = Console.ReadLine();
                    if (i == 3)
                    {
                        if (ValidateName(response))
                        {
                            properties[i].SetValue(newTask, response);
                        }
                        else if (!ValidateName(response))
                        {
                            while (!ValidateName(response))
                            {
                                Console.WriteLine("User input is not a valid name. Please enter a valid name.");
                                response = Console.ReadLine();
                            }
                        }
                        try
                        {
                            properties[i].SetValue(newTask, response);
                        }
                        catch
                        {
                            FormatException e;
                        }
                    }
                    else if (i < 4)
                    {
                        if (ValidateWord(response))
                        {
                            properties[i].SetValue(newTask, response);
                        }
                        else if (!ValidateWord(response))
                        {
                            while (!ValidateWord(response))
                            {
                                Console.WriteLine("User input is not a valid word. Please enter a valid word.");
                                response = Console.ReadLine();
                            }
                        }
                        try
                        {
                            properties[i].SetValue(newTask, response);
                        }
                        catch
                        {
                            FormatException e;
                        }
                    }
                    else if (i == 4)
                    {
                        if (ValidateDate(response))
                        {
                            properties[i].SetValue(newTask, response);
                        }
                        else if (!ValidateDate(response))
                        {
                            while (!ValidateDate(response))
                            {
                                Console.WriteLine("User input is not a valid date. Please enter a valid date.");
                                response = Console.ReadLine();
                            }
                        }
                        try
                        {
                            properties[i].SetValue(newTask, response);
                        }
                        catch
                        {
                            FormatException e;
                        }
                    }
                    //else if (i == 5)
                    //{
                    //    string lowerCaseResponse = response.ToLower();
                    //    bool responseBool;
                    //    if (bool.TryParse(lowerCaseResponse, out bool result))
                    //    {
                    //        responseBool = result;
                    //        properties[i].SetValue(newTask, responseBool);
                    //    }
                    //    else if (lowerCaseResponse == "yes")
                    //    {
                    //        responseBool = true;
                    //        properties[i].SetValue(newTask, responseBool);
                    //    }
                    //    else if (lowerCaseResponse == "no")
                    //    {
                    //        responseBool = false;
                    //        properties[i].SetValue(newTask, responseBool);
                    //    }
                    //}
                }
            }
            tasks.Add(newTask);
            Console.WriteLine($"\nThe task \"{newTask.title}\" has been added to the list.");
            PrintTaskProperties(newTask);
        }

        public static bool ValidateWRegEx(string valueType, string regExString, string input)
        {
            Regex regEx = new Regex(regExString);

            if (regEx.IsMatch(input))
            {
                //Console.WriteLine($"{input} is a {valueType}.");
                return true;
            }
            else
            {
                //Console.WriteLine($"{input} is not a {valueType}.");
                return false;
            }
        }

        public static bool ValidateWord(string input)
        {
            bool isValid = ValidateWRegEx("name", @"([A-Z]|[a-z])\w+", input);
            return isValid;
        }

        public static bool ValidateName(string input)
        {
            bool isValid = ValidateWRegEx("name", @"[A-Z]{1}[a-z]{0,29}", input);
            return isValid;
        }

        public static bool ValidateDate(string input)
        {
            bool isValid = ValidateWRegEx("date", @"(([0][1-9])|([1-2][\d])|(3[01]))\/(([0][1-9])|([1][12]))\/(19|20)\d{2}", input);
            return isValid;
        }

        public static void RemoveTask(List<Task> tasks)
        {
            bool remove = AskYesOrNo("Would you like to remove a task from the list?");
            if (remove)
            {
                Task task = SelectTask(tasks, "remove");  
                if (VerifySelection($"remove {task.Title}"))
                {
                    tasks.Remove(task);
                    Console.WriteLine($"\"{task.Title}\" has been removed from the list.");
                }
            }
        }
        public static void CompleteTask(List<Task> tasks)
        {
            string action = "mark as complete";
            Task task = SelectTask(tasks, action);
            if (VerifySelection($"{action} + Task {task.Key}. {task.Title}?"))
            {
                task.completion = true;
                Console.WriteLine($"I will {action} Task {task.Key}.{task.Title}.");
            }
        }
        public static bool Quit()
        {
            if (AskYesOrNo("Are you sure you want to quit Task Manager?"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
