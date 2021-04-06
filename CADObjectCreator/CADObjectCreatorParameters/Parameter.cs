using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    /// <summary>
    /// Универсальный класс для задачи параметров.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Parameter<T> where  T: struct
    {
        /// <summary>
        /// Поле минимального значения параметра.
        /// </summary>
        private T _min;

        /// <summary>
        /// Поле максимального значения параметра.
        /// </summary>
        private T _max;

        /// <summary>
        /// Поле значения параметра.
        /// </summary>
        private T _value;

        /// <summary>
        /// Поле наименования параметра.
        /// </summary>
        private string _name;

        /// <summary>
        /// Возвращает и задает название параметра.
        /// </summary>
        /// <exception cref="ArgumentException">Не может быть null или пустым.</exception>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Имя не может быть null или пустым!");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Возвращает и задает минимальное значение параметра.
        /// </summary>
        /// <exception cref="ArgumentException">Не может быть больше чем максимум параметра.</exception>
        public T Min
        {
            get
            {
                return _min;

            }
            set
            {
                //Минимум не равен максимум && Максимум не меньше минимума
                var comparerResult = Comparer<T>.Default.Compare(_max,value );
                if (comparerResult < 0)
                {
                    throw new ArgumentException();
                }
                _min = value;
            }
        }

        /// <summary>
        /// Возвращает и задает максимальное значение параметра.
        /// </summary>
        ///  <exception cref="ArgumentException">Не может быть меньше чем минимум параметра.</exception>
        public T Max
        {
            get
            {
                return _max;
            }
            set
            {
                //Максимум не равне минимуму && Минимум не больше максимума
                var comparerResult = Comparer<T>.Default.Compare(_min, value);
                if (comparerResult > 0)
                {
                    throw new ArgumentException();
                }
                _max = value;
            }
        }

        /// <summary>
        /// Возвращает и задает значение параметра.
        /// </summary>
        /// <exception cref="ArgumentException">Не может быть больше чем максимум и не может быть меньше чем минимум.</exception>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                 //TODO: RSDN
                var comparerResultMin = Comparer<T>.Default.Compare(_min, value);
                if (comparerResultMin > 0)
                {
                    throw new ArgumentException("Введенное значение должно быть больше чем "+ _min);
                }
                 //TODO: RSDN
                var comparerResultMax = Comparer<T>.Default.Compare(_max, value);
                if (comparerResultMax < 0)
                {
                    throw new ArgumentException("Введенное значение должно быть меньше чем " +_max);
                }
                _value = value;
            }
        }

        /// <summary>
        /// Конструктор класса Parameter с заданием названия, минимума, максимума и значения параметра.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        public Parameter(string name, T min, T max, T value)
        {
            //TODO: проверку сразу вместо присваивания
            _min = min;
            _max = max;
            var comparerResult = Comparer<T>.Default.Compare(_max, _min);
            if (comparerResult < 0)
            {
                throw new ArgumentException("Заданный максимум меньше минимума.");
            }
            Name = name;
            Value = value;
        }
    }
}
