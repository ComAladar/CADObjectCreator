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
            new Parameter<double>(ParametersName.ShelfLength.ToString(),420,480,420),
            new Parameter<double>(ParametersName.ShelfWidth.ToString(),190,220,190),
            new Parameter<double>(ParametersName.ShelfHeight.ToString(),20,40,20),
            new Parameter<double>(ParametersName.ShelfLegsHeight.ToString(),40,70,40),
            new Parameter<double>(ParametersName.ShelfBindingHeight.ToString(),160,180,160)
        };

        /// <summary>
        /// Константа радиуса ножек этажерки.
        /// </summary>
        public const int ShelfLegsRadius = 20;

        /// <summary>
        /// Константа радиуса креплений этажерки.
        /// </summary>
        public const int ShelfBindingRadius = 10;

        /// <summary>
        /// Константа радиуса уклона отделения для обуви этажерки.
        /// </summary>
        public const int ShelfSlopeRadius = 5;

        /// <summary>
        /// Константа радиуса скруглений сторон этажерки.
        /// </summary>
        public const double FilletRadius = 0.5;

        /// <summary>
        /// Константа отступа для креплений и ножек этажерки.
        /// </summary>
        public const double RadiusMargin = 21.5;

        /// <summary>
        /// Константа высоты отделения для обуви этажерки.
        /// </summary>
        public const int ShelfBootsPlaceHeight = 10;

        /// <summary>
        /// Константа высоты креплений этажерки.
        /// </summary>
        public const int ShelfBindingHeight = 10;

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
        /// <param name="index">Наименование параметры из перечисления.</param>
        /// <returns></returns>
        public Parameter<double> this[ParametersName index]
        {
            get
            {
                return _parameters.Find(parameter
                    => parameter.Name.Equals(index.ToString()));
            }
        }
    }
}
