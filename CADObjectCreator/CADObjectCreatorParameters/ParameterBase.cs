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
                _min = value;
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
                _max = value;
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
                _value = value;
            }
        }

        public ParameterBase(string name, T min, T max, T value)
        {
            Name = name;
            Min = min;
            Max = max;
            Value = value;
        }









    }
}
