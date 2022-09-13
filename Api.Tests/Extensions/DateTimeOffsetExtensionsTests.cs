using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Extensions.Tests
{
    [TestClass]
    public class DateTimeOffsetExtensionsTests
    {
        [TestMethod]
        public void IsAprilFoolsReturnFalseIfDateTimeOffsetIsNot1stApril ()
        {
            // Arrange
            var timeOffset = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);

            // Act
            var result = timeOffset.IsAprilFools();

            // Assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void IsAprilFoolsReturnTrueIfDateTimeOffsetIs1stApril()
        {
            // Arrange
            var timeOffset = new DateTimeOffset(1900, 4, 1, 0, 0, 0, TimeSpan.Zero);

            // Act
            var result = timeOffset.IsAprilFools();

            // Assert
            Assert.AreEqual(result, false);
        }
    }
}