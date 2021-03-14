using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    public class ParameterBase<T> where  T: struct
    {
        private T _min;
        private T _max;
        private T _value;
        private string _name;

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
                    throw new ArgumentException("Имя не может быть null или пустым!");
                }
                _name = value;
            }
        }

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
                else _min = value;
            }
        }

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
                else _max = value;
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                var ComparerResultMin = Comparer<T>.Default.Compare(_min, value);
                if (ComparerResultMin > 0)
                {
                    throw new ArgumentException("Введенное значение должно быть больше чем "+ _min);
                }

                var ComparerResultMax = Comparer<T>.Default.Compare(_max, value);
                if (ComparerResultMax < 0)
                {
                    throw new ArgumentException("Введенное значение должно быть меньше чем " +_max);
                }
                _value = value;
            }
        }

        public ParameterBase(string name, T min, T max, T value)
        {
            _min = min;
            _max = max;
            Name = name;
            Min = min;
            Max = max;
            Value = value;
        }









    }
}
