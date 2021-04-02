using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    /// <summary>
    /// Класс параметров. Хранит все параметры.
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Поле длины отделения для обуви.
        /// </summary>
        private double _shelfBootsPlaceLength;

        /// <summary>
        /// Поле ширины отделения для обуви.
        /// </summary>
        private double _shelfBootsPlaceWidth;

        /// <summary>
        /// Список содержащий универсальные параметры этажерки.
        /// </summary>
        private List<Parameter<double>> _parameters = new List<Parameter<double>>()
        {
            new Parameter<double>("ShelfLength",420,480,420),
            new Parameter<double>("ShelfWidth",190,220,190),
            new Parameter<double>("ShelfHeight",20,40,20),
            new Parameter<double>("ShelfLegsHeight",40,70,40),
            new Parameter<double>("ShelfBindingHeight",160,180,160)
        };

        /// <summary>
        /// Возвращает радиус ножек этажерки.
        /// </summary>
        public int ShelfLegsRadius { get; private set; } = 20;

        /// <summary>
        /// Возвращает радиус креплений этажерки.
        /// </summary>
        public int ShelfBindingRadius { get; private set; } = 10;

        /// <summary>
        /// Возвращает радиус уклона отделения для обуви этажерки.
        /// </summary>
        public int ShelfSlopeRadius { get; private set; } = 5;

        /// <summary>
        /// Возвращает радиус скруглений сторон этажерки.
        /// </summary>
        public double FilletRadius { get; private set; } = 0.5;

        /// <summary>
        /// Возвращает отступ для креплений и ножек этажерки.
        /// </summary>
        public double RadiusMargin { get; private set; } = 21.5;

        /// <summary>
        /// Возвращает высоту отделения для обуви этажерки.
        /// </summary>
        public int ShelfBootsPlaceHeight { get; private set; } = 10;

        /// <summary>
        /// Возвращает высоту креплений этажерки.
        /// </summary>
        public int ShelfBindingHeight { get; private set; } = 10;

        /// <summary>
        /// Возвращает и задает зависимое значение длины отделения для обуви этажерки.
        /// </summary>
        public double ShelfBootsPlaceLength
        {
            get
            {
                return _shelfBootsPlaceLength;
            }
            set
            {
                _shelfBootsPlaceLength = 0.7 * value;
            }
        }

        /// <summary>
        /// Возвращает и задает зависимое значение ширины отделения для обуви этажерки.
        /// </summary>
        public double ShelfBootsPlaceWidth
        {
            get
            {
                return _shelfBootsPlaceWidth;
            }
            set
            {
                _shelfBootsPlaceWidth = 0.85 * value;
            }
        }

        /// <summary>
        /// Возвращает и задает значение универсального параметра.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[string index]
        {
            get
            {
                return _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Value;
            }
            set
            {
                _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Value = value;
            }
        }

        /// <summary>
        /// Возвращает максимальное значение универсального параметра.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetMaxParameter(string index)
        {
            return _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Max;
        }

        /// <summary>
        /// Возвращает минимальное значение универсального параметра.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetMinParameter(string index)
        {
            return _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Min;
        }

    }
}
