using System;
using Microsoft.Extensions.DependencyInjection;
using RobotCleaner.Domain;

namespace RobotCleaner.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IDataReader, ConsoleDataReader>()
                .AddSingleton<IDataWriter, ConsoleDataWriter>()
                .AddSingleton(typeof(Terminal))
                .BuildServiceProvider();

            var terminal = serviceProvider.GetService<Terminal>();

            var instructions = terminal.ReadInstructions();

            var robot = new Robot(instructions.X, instructions.Y);

            foreach (var command in instructions.Commands)
            {
                robot.Move(command.Direction, command.NumberOfSteps);
            }

            terminal.WriteReport(robot.GetCleanedPlacesCount());

            Console.WriteLine("Press ENTER to terminate");
            Console.ReadLine();
        }
    }
}
