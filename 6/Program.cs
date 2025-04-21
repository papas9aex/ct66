using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TaskManagerWithTracing
{
    class Program
    {
        static List<string> tasks = new List<string>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Выберите операцию: Add / Remove / List / Exit");
                string command = Console.ReadLine();
                switch (command?.ToLower())
                {
                    case "add":
                        TraceOperation("Add", AddTask);
                        break;
                    case "remove":
                        TraceOperation("Remove", RemoveTask);
                        break;
                    case "list":
                        TraceOperation("List", ListTasks);
                        break;
                    case "exit":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }

        static void TraceOperation(string operationName, Action operation)
        {
            var stopwatch = Stopwatch.StartNew();
            Trace.WriteLine($">>> Начало операции: {operationName} — {DateTime.Now}");
            try
            {
                operation.Invoke();
                Trace.WriteLine($"<<< Операция {operationName} выполнена успешно. Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"!!! Ошибка в операции {operationName}: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
            }
        }

        static void AddTask()
        {
            Console.Write("Введите название задачи: ");
            string task = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(task))
                throw new ArgumentException("Задача не может быть пустой.");

            tasks.Add(task);
            Console.WriteLine("Задача добавлена.");
        }

        static void RemoveTask()
        {
            Console.Write("Введите название задачи для удаления: ");
            string task = Console.ReadLine();
            if (tasks.Remove(task))
                Console.WriteLine("Задача удалена.");
            else
                Console.WriteLine("Задача не найдена.");
        }

        static void ListTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
                return;
            }

            Console.WriteLine("Список задач:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"- {task}");
            }
        }
    }
}
