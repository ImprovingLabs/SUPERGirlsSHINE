using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Domain;

namespace Calculator.Tests
{
    [TestClass]
    public class AdditionTests
    {
        [TestMethod]
        public void CanAddTwoIntegers()
        {
            // Arrange
            var calculator = new Calculator.Domain.Calculator();

            // Act
            int result = calculator.Add(7,8);

            // Assert
            Assert.AreEqual(15, result);
        }
    }
}
