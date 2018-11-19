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
}
