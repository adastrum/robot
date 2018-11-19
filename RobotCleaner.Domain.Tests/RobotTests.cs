using System;
using System.Collections.Generic;
using Xunit;

namespace RobotCleaner.Domain.Tests
{
    public class RobotTests
    {
        [Fact]
        public void Create_SetsCoordinates()
        {
            var robot = new Robot(10, 22);
            Assert.Equal(10, robot.X);
            Assert.Equal(22, robot.Y);
        }

        [Fact]
        public void Move_ChangesCoordinates()
        {
            var robot = new Robot(10, 22);
            robot.Move(Direction.E, 2);
            Assert.Equal(12, robot.X);
            Assert.Equal(22, robot.Y);
        }

        [Fact]
        public void GetCleanedPlacesCount_ReturnsNumberOfUniquePlacesCleaned()
        {
            var robot = new Robot(10, 22);
            robot.Move(Direction.E, 2);
            robot.Move(Direction.N, 1);
            Assert.Equal(4, robot.GetCleanedPlacesCount());
        }
    }

    public class Robot
    {
        private Point _point;
        private readonly Dictionary<Point, bool> _visited = new Dictionary<Point, bool>();

        public Robot(int x, int y)
        {
            _point = new Point(x, y);
        }

        public int X => _point.X;
        public int Y => _point.Y;

        public void Move(Direction direction, int numberOfSteps)
        {
            for (var i = 0; i < numberOfSteps; i++)
            {
                Step(direction);
            }
        }

        private void Step(Direction direction)
        {
            var x = X;
            var y = Y;

            switch (direction)
            {
                case Direction.E:
                    x++;
                    break;
                case Direction.W:
                    x--;
                    break;
                case Direction.S:
                    y--;
                    break;
                case Direction.N:
                    y++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            _point = new Point(x, y);
            _visited[_point] = true;
        }

        public int GetCleanedPlacesCount()
        {
            return _visited.Count + 1;
        }
    }

    public enum Direction
    {
        E,
        W,
        S,
        N
    }

    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
