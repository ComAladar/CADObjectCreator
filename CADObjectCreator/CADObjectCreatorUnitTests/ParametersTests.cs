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
            parameters.ShelfBootsPlaceLength = parameters["ShelfLength"];
            parameters.ShelfBootsPlaceWidth = parameters["ShelfWidth"];
            return parameters;
        }

        [Test]
        public void ShelfLegsRadius_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfLegsRadius;

            //Assert
            Assert.AreEqual(parameters.ShelfLegsRadius,actual,"Геттер не возвращает корректное значение.");
        }

        [Test]
        public void ShelfBindingRadius_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfBindingRadius;

            //Assert
            Assert.AreEqual(parameters.ShelfBindingRadius, actual, "Геттер не возвращает корректное значение.");
        }

        [Test]
        public void ShelfSlopeRadius_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfSlopeRadius;

            //Assert
            Assert.AreEqual(parameters.ShelfSlopeRadius, actual, "Геттер не возвращает корректное значение.");

        }

        [Test]
        public void FilletRadius_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.FilletRadius;

            //Assert
            Assert.AreEqual(parameters.FilletRadius, actual, "Геттер не возвращает корректное значение.");

        }

        [Test]
        public void RadiusMargin_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.RadiusMargin;

            //Assert
            Assert.AreEqual(parameters.RadiusMargin, actual, "Геттер не возвращает корректное значение.");

        }

        [Test]
        public void ShelfBootsPlaceHeight_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfBootsPlaceHeight;

            //Assert
            Assert.AreEqual(parameters.ShelfBootsPlaceHeight, actual, "Геттер не возвращает корректное значение.");

        }

        [Test]
        public void ShelfBindingHeight_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfBindingHeight;

            //Assert
            Assert.AreEqual(parameters.ShelfBindingHeight, actual, "Геттер не возвращает корректное значение.");

        }

        [Test]
        public void ShelfBootsPlaceLength_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfBootsPlaceLength;

            //Assert
            Assert.AreEqual(parameters.ShelfBootsPlaceLength,actual,"Геттер не возвращает корректное значение.");
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
            Assert.AreEqual(expected,parameters.ShelfBootsPlaceLength , "Сеттер не присваивает корректное значение.");
        }

        [Test]
        public void ShelfBootsPlaceWidth_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.ShelfBootsPlaceWidth;

            //Assert
            Assert.AreEqual(parameters.ShelfBootsPlaceWidth, actual, "Геттер не возвращает корректное значение.");
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
        public void GetMaxParameter_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.GetMaxParameter("ShelfLength");

            //Assert
            Assert.AreEqual(parameters.GetMaxParameter("ShelfLength"),actual,"Метод возвращает не правильное значение.");

        }

        [Test]
        public void GetMinParameter_ReturnCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            var actual = parameters.GetMinParameter("ShelfLength");

            //Assert
            Assert.AreEqual(parameters.GetMinParameter("ShelfLength"), actual, "Метод возвращает не правильное значение.");

        }

        [Test]
        public void Parameters_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameters = CreateParameters();

            //Act
            double expected = 430.5;
            parameters["ShelfLength"]=expected;

            //Assert
            Assert.AreEqual(expected,parameters["ShelfLength"],"Сеттер не присваивает корректное значение.");
        }

    }
}
