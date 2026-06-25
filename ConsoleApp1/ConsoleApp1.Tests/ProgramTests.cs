using Xunit;
using System.IO;
using System;

namespace ConsoleApp1.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_ShouldExecuteWithoutErrors()
        {
            // Arrange
            var originalOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Program.Main(Array.Empty<string>());
                var output = stringWriter.ToString();

                // Assert
                Assert.NotNull(output);
                Assert.NotEmpty(output);
            }
            finally
            {
                // Cleanup
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Main_ShouldOutputHelloWorld()
        {
            // Arrange
            var originalOut = Console.Out;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Program.Main(Array.Empty<string>());
                var output = stringWriter.ToString().Trim();

                // Assert
                Assert.Equal("Hello, World!", output);
            }
            finally
            {
                // Cleanup
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void Main_ShouldNotThrowException()
        {
            // Arrange & Act & Assert
            var exception = Record.Exception(() => Program.Main(Array.Empty<string>()));
            Assert.Null(exception);
        }
    }
}
