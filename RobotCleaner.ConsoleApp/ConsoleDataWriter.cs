using System;
using RobotCleaner.Domain;

namespace RobotCleaner.ConsoleApp
{
    public class ConsoleDataWriter : IDataWriter
    {
        public void Write(string data)
        {
            Console.WriteLine(data);
        }
    }
}