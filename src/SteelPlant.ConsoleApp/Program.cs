using SteelPlant.ConsoleApp;
using SteelPlant.ConsoleApp.Models;

namespace SteelPlant.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Steel Plant MES System Prototype ===");

        var dbService = new DatabaseService();

        while (true)
        {
            Console.WriteLine("\nMenu:" +
                "\n1. View Steel Grades" + 
                "\n2. Start New Batch" +
                "\n3. Finish Batch" +
                "\n4. Exit");

            Console.Write("\nSelect an option: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var grades = dbService.GetSteelGrades();
                    Console.WriteLine("\nAvailable Steel Grades:");
                    foreach (var g in grades)
                    {
                        Console.WriteLine($"ID: {g.GradeId} | {g.GradeName} | Target Temperature: {g.TargetTemperature}°C");
                    }
                    break;
                case "2":
                    Console.Write("Enter Steel Grade ID to start batch: ");
                    if (int.TryParse(Console.ReadLine(), out int gradeId))
                    {
                        dbService.StartNewBatch(gradeId);
                        Console.WriteLine("Batch started successfully.");
                    }
                    break;
                case "3":
                    Console.Write("Enter ID of batch to finish: ");
                    int bId = int.Parse(Console.ReadLine());
                    Console.Write("Enter final weight of the batch: ");
                    decimal weight = decimal.Parse(Console.ReadLine());

                    dbService.FinishBatch(bId, weight);
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
