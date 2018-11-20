using System;
using System.Text;
using RobotCleaner.Domain;

namespace RobotCleaner.ConsoleApp
{
    public class ConsoleDataReader : IDataReader
    {
        public string Read()
        {
            var sb = new StringBuilder();
            var s = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(s))
            {
                sb.AppendLine(s);
                s = Console.ReadLine();
            }

            return sb.ToString();
        }
    }
}