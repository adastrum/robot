using System;
using System.Text;
using RobotCleaner.Domain;

namespace RobotCleaner.ConsoleApp
{
    public class ConsoleDataReader : IDataReader
    {
        public string Read()
        {
            var s = Console.ReadLine();
            var numberOfLines = int.Parse(s) + 1;

            var sb = new StringBuilder();
            sb.AppendLine(s);

            for (var i = 0; i < numberOfLines; i++)
            {
                sb.AppendLine(Console.ReadLine());
            }

            return sb.ToString();
        }
    }
}