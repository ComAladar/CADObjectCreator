using System;
using NUnit.Framework;
using CADObjectCreatorParameters;
using NUnit.Framework.Constraints;

namespace CADObjectCreatorUnitTests
{
    [TestFixture]
    public class ParameterTests
    {
        private Parameter<double> CreateParameter()
        {
            var Parameter = new Parameter<double>("parameter", 100, 250, 160);
            return Parameter;
        }


        [Test]
        public void Name_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            parameter.Name = "Новое имя";
            var actual = parameter.Name;

            //Assert
            Assert.AreEqual(parameter.Name,actual,"Геттер не возвращает корректное значение.");
        }

        [Test]
        public void Name_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            var expected = "Новое имя";
            parameter.Name = expected;

            //Assert
            Assert.AreEqual(expected,parameter.Name,"Сеттер не присваивает корректное значение.");
        }

        [Test]
        public void Name_SetThrowsArgumentException_WhiteSpaces()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            string testName = " ";

            //Assert
            Assert.Throws<ArgumentException>((() => { parameter.Name = testName; }),
                "У сеттера не происходит срабатывание ArgumentException при строке с белыми пробелами");
        }

        [Test]
        public void Name_SetThrowsArgumentException_NullString()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            string testName = null;

            //Assert
            Assert.Throws<ArgumentException>((() => { parameter.Name = testName; }), "У сеттера не происходит срабатывание ArgumentException при строке null");
        }

        [Test]
        public void Name_SetThrowsArgumentException_EmptyString()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            string testName = "";

            //Assert
            Assert.Throws<ArgumentException>((() => { parameter.Name = testName; }), "У сеттера не происходит срабатывание ArgumentException при пустой строке");
        }

        [Test]
        public void Min_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            parameter.Min = 50;
            var actual = parameter.Min;

            //Assert
            Assert.AreEqual(parameter.Min,actual,"Геттер не возвращает правильное значение.");
        }

        [Test]
        public void Min_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            double expected = 15.33;
            parameter.Min = expected;

            //Assert
            Assert.AreEqual(expected,parameter.Min,"Сеттер не присваивает правильное значение.");
        }

        [Test]
        public void Min_SetThrowsArgumentException_MaxIsLower()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            double testAmount = 999.99;

            //Assert
            Assert.Throws<ArgumentException>((() => { parameter.Min = testAmount; }),
                "У сеттера не происходит срабатывание ArgumentException при задаче значения больше максимального.");
        }

        [Test]
        public void Max_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            parameter.Max = 300;
            var actual = parameter.Max;

            //Assert
            Assert.AreEqual(parameter.Max, actual, "Геттер не возвращает правильное значение.");
        }

        [Test]
        public void Max_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            double expected = 350.99;
            parameter.Max = expected;

            //Assert
            Assert.AreEqual(expected, parameter.Max, "Сеттер не присваивает правильное значение.");
        }

        [Test]
        public void Max_SetThrowsArgumentException_MinIsHigher()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            double testAmount = 10.5;

            //Assert
            Assert.Throws<ArgumentException>((() => { parameter.Max = testAmount; }),
                "У сеттера не происходит срабатывание ArgumentException при задаче значения меньше минимального.");
        }

        [Test]
        public void Value_GetCorrectValue_ReturnCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            parameter.Value = 150;
            var actual = parameter.Value;

            //Assert
            Assert.AreEqual(parameter.Value,actual,"Геттер не возвращает правильное значение.");
        }

        [Test]
        public void Value_SetCorrectValue_SetCorrectValue()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            var expected = 200.99;
            parameter.Value = expected;

            //Assert
            Assert.AreEqual(expected,parameter.Value,"Сеттер не присваивает правильное значение.");
        }

        [Test]
        public void Value_SetThrowsArgumentException_ValueIsLessThanMin()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            double testAmount = 1.1;

            //Assert
            Assert.Throws<ArgumentException>((() => { parameter.Value = testAmount; }),
                "У сеттера не происходит срабатывание ArgumentException при значении меньше минимума.");
        }

        [Test]
        public void Value_SetThrowsArgumentException_ValueIsHigherThanMax()
        {
            //Setup
            var parameter = CreateParameter();

            //Act
            double testAmount = 9999999.99;

            //Assert
            Assert.Throws<ArgumentException>((() => { parameter.Value = testAmount; }),
                "У сеттера не происходит срабатывание ArgumentException при значении больше максимума.");
        }

    }
}
