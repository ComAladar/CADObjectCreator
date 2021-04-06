using System;
using NUnit.Framework;
using CADObjectCreatorParameters;

namespace CADObjectCreatorUnitTests
{
    [TestFixture]
    public class ParametersTests
    {
        private Parameters CreateParameters()
        {
            var parameters = new Parameters();
            parameters.ShelfBootsPlaceLength = parameters[ParametersName.ShelfLength].Value;
            parameters.ShelfBootsPlaceWidth = parameters[ParametersName.ShelfWidth].Value;
            return parameters;
        }

        [Test]
        public void ShelfBootsPlaceLength_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfBootsPlaceLength;

            //Assert
            Assert.AreEqual(parameters.ShelfBootsPlaceLength,actual,
                "Геттер не возвращает корректное значение.");
        }

        [Test]
        public void ShelfBootsPlaceLength_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            double expected = 100;
            parameters.ShelfBootsPlaceLength = expected;
            expected = expected * 0.7;

            //Assert
            Assert.AreEqual(expected,parameters.ShelfBootsPlaceLength , 
                "Сеттер не присваивает корректное значение.");
        }

        [Test]
        public void ShelfBootsPlaceWidth_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfBootsPlaceWidth;

            //Assert
            Assert.AreEqual(parameters.ShelfBootsPlaceWidth, actual, 
                "Геттер не возвращает корректное значение.");
        }

        [Test]
        public void ShelfBootsPlaceWidth_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            double expected = 100;
            parameters.ShelfBootsPlaceWidth = expected;
            expected = expected * 0.85;

            //Assert
            Assert.AreEqual(expected, parameters.ShelfBootsPlaceWidth, "Сеттер не присваивает корректное значение.");
        }


        [Test]
        public void Parameters_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            double expected = 430.5;
            parameters[ParametersName.ShelfLength].Value=expected;

            //Assert
            Assert.AreEqual(expected, parameters[ParametersName.ShelfLength].Value, "Сеттер не присваивает корректное значение.");
        }

    }
}
